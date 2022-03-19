using System;
using System.Collections.Generic;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace TankWars
{
	/// <summary>
	/// Model for the world
	/// </summary>
	public class World
	{
		public Dictionary<int, Tank> Players;
		public Dictionary<int, PowerUp> Powerups;
		public Dictionary<int, Wall> walls;
		public Dictionary<int, Projectile> projectiles;
		public Dictionary<int, Beam> beams;
		public Dictionary<int, ControlCommand> commands;
		public string LivesMode { get; set; }
		private Vector2D turretdirection;
		public int framesUntilRedrawProj { get; set; }
		public int framesUntilRespawn { get; set; }

		public int Size { get; private set; }
		private int id = 0;

		public World(int size)
		{
			Players = new Dictionary<int, Tank>();
			Powerups = new Dictionary<int, PowerUp>();
			walls = new Dictionary<int, Wall>();
			projectiles = new Dictionary<int, Projectile>();
			beams = new Dictionary<int, Beam>();
			Size = size;
			commands = new Dictionary<int, ControlCommand>();
			turretdirection = new Vector2D(0, 0);
		}

		/// <summary>
		/// Updates the world on every frame
		/// </summary>
		public void Update()
		{
			//Iterates through commands and then updates world
			foreach (KeyValuePair<int, ControlCommand> cmd in commands)
			{
				// Update from Commands
				lock (Players)
				{
					Tank tank = Players[cmd.Key];
					tank.tdir = cmd.Value.tdir;
					if (tank.hp > 0)
					{
						// Update tank's movement
						switch (cmd.Value.moving)
						{
							case "up":
								tank.velocity = new Vector2D(0, -1);
								tank.bdir = new Vector2D(0, -1);
								break;
							case "down":
								tank.velocity = new Vector2D(0, 1);
								tank.bdir = new Vector2D(0, 1);
								break;
							case "left":
								tank.velocity = new Vector2D(-1, 0);
								tank.bdir = new Vector2D(-1, 0);

								break;
							case "right":
								tank.velocity = new Vector2D(1, 0);
								tank.bdir = new Vector2D(1, 0);
								break;
							default:
								tank.velocity = new Vector2D(0, 0);
								break;
						}
						tank.velocity *= Tank.EnginePower;

						//Update tank's shooting
						// counts down the shot counter until it's time to fire again
						if (Players[cmd.Key].shotCounter > 0)
						{
							Players[cmd.Key].shotCounter--;
						}
						switch (cmd.Value.fire)
						{
							case "main":
								if (Players[cmd.Key].shotCounter == 0)
								{
									Projectile projectile = new Projectile(id++, tank.loc, tank.tdir, false, tank.tank);
									projectile.velocity = (projectile.dir);
									projectile.velocity *= Projectile.speed;
									projectiles.Add(projectile.proj, projectile);
									Players[cmd.Key].shotCounter = framesUntilRedrawProj;
								}
								break;
							case "alt":
								if (Players[cmd.Key].beamCharges > 0)
								{
									Beam beam = new Beam(id++, tank.tank, tank.loc, tank.tdir);
									beams.Add(beam.beam, beam);
									Players[cmd.Key].beamCharges--;
								}
								break;
						}
					}
					else
					{
						tank.velocity = new Vector2D(0, 0);
					}
				}
			}
			commands.Clear();
			///Goes through projectiles and checks collision
			foreach (Projectile projectile in projectiles.Values)
			{

				Vector2D newLocation = projectile.loc + projectile.velocity;
				bool collision = false;
				foreach (Wall wall in walls.Values)
				{
					if (wall.CollidesOtherObject(newLocation) || projectileworldcollision(newLocation))
					{
						collision = true;
						projectile.died = true;
						break;
					}
				}

				foreach (Tank tank in Players.Values)
				{
					if (tank.hp > 0)
					{
						if (tank.CollidesProjectile(newLocation))
						{
							if (tank.tank != projectile.owner)
							{
								collision = true;
								projectile.died = true;
								tank.hp -= 1;
								if (tank.hp == 0)
								{
									Players[projectile.owner].score += 1;
									if (LivesMode == "ON") CalculateLives(projectile, tank);
									else tank.died = true;
								}
								break;
							}
						}
						if (!collision)
						{
							projectile.loc = newLocation;
						}
					}
				}

			}
			//checks beam collision
			foreach (Beam beam in beams.Values)
			{
				foreach (Tank tank in Players.Values)
				{
					if (Beam.Intersects(beam.org, beam.dir, tank.loc, 30))
					{
						if (beam.owner != tank.tank)
						{
							tank.hp -= tank.hp;
							Players[beam.owner].score++;
							if (LivesMode == "ON")
							{
								if (Players[beam.owner].lives < Tank.maxLives)
									Players[beam.owner].lives++;
								if (tank.lives > 0) tank.lives--;
							}
							tank.died = true;
							tank.RespawnTimer = framesUntilRespawn;
						}
					}
				}
			}
			foreach (Tank tank in Players.Values)
			{
				if (tank.hp > 0)
				{
					if (tank.velocity.Length() == 0)
					{
						continue;
					}
					Vector2D newLocation = tank.loc + tank.velocity;
					newLocation = Tankwraparound(newLocation);
					bool collision = false;
					foreach (Wall wall in walls.Values)
					{
						if (wall.CollidesTank(newLocation))
						{
							collision = true;
							tank.velocity = new Vector2D(0, 0);
							break;
						}
					}
					if (!collision)
					{
						tank.loc = newLocation;
					}
				}
			}
			foreach (PowerUp powerUp in Powerups.Values)
			{
				foreach (Tank tank in Players.Values)
				{
					if (tank.CollidesPowerUp(powerUp.loc))
					{
						powerUp.died = true;
						if (tank.beamCharges < 3)
						{
							tank.beamCharges += 1;
						}
					}
				}
			}
			foreach (Tank tank in Players.Values)
			{
				if (tank.RespawnTimer > 0)
				{
					tank.RespawnTimer--;
				}
				if (tank.died == true)
				{
					tank.RespawnTimer = framesUntilRespawn;
				}
				if (tank.join == true)
				{
					tank.join = false;
				}

				if (tank.dc == true)
				{
					tank.died = true;
					tank.hp = 0;
				}
				if (tank.hp <= 0 && tank.RespawnTimer == 0)
				{
					tank.loc = new Vector2D(RandomizePlacement(Size / 2));
					tank.hp = 3;
				}
			}


		}

		private void CalculateLives(Projectile projectile, Tank tank)
		{
			if (Players[projectile.owner].score % 3 == 0 && Players[projectile.owner].lives < Tank.maxLives)
			{
				Players[projectile.owner].lives++;
			}
			if (tank.lives > 0)
			{
				tank.lives--;
				tank.hp = 3;
			}
			else tank.died = true;

		}

		/// <summary>
		/// Selects random spawn/Respawn locations for tanks and powerups
		/// </summary>
		/// <param name="Size">Size of the world</param>
		/// <returns></returns>
		public Vector2D RandomizePlacement(int Size)
		{
			Random random = new Random();
			int gridSize = (int)walls[0].thickness;
			int numOfSquares = Size / gridSize;
			int lower = -numOfSquares / 2;
			int upper = numOfSquares / 2;
			int rndX = random.Next(lower, upper - 1);
			int rndY = random.Next(lower, upper - 1);

			int baseLoc = gridSize / 2;
			Vector2D loc = new Vector2D(baseLoc + rndX * gridSize, baseLoc + rndY * gridSize);
			bool collidesWall = true;

			while (collidesWall)
			{
				foreach (Wall wall in walls.Values)
				{
					if (wall.CollidesTank(loc))
					{
						rndX = random.Next(lower, upper);
						rndY = random.Next(lower, upper);
						loc = new Vector2D(baseLoc + rndX * gridSize, baseLoc + rndY * gridSize);
						collidesWall = true;
						break;
					}
					collidesWall = false;
				}
			}
			return loc;
		}

		/// <summary>
		/// Checks whether the tank hits the edge of the world then sends it to the other side if it does
		/// </summary>
		/// <param name="tankLoc">The tanks location</param>
		/// <returns></returns>
		public Vector2D Tankwraparound(Vector2D tankLoc)
		{
			double x = tankLoc.GetX();
			double y = tankLoc.GetY();
			if (tankLoc.GetX() >= Size / 2)
			{
				x = -tankLoc.GetX();
				return new Vector2D(x, y);
			}
			if (tankLoc.GetY() >= Size / 2)
			{
				y = -tankLoc.GetY();
				return new Vector2D(x, y);
			}
			if (tankLoc.GetX() <= -Size / 2)
			{
				x = -tankLoc.GetX();
				return new Vector2D(x, y);
			}
			if (tankLoc.GetY() <= -Size / 2)
			{
				y = -tankLoc.GetY();
				return new Vector2D(x, y);
			}
			return new Vector2D(x, y);
		}
		/// <summary>
		/// Checks if projectile hits the edge of the world then destroys it if it does
		/// </summary>
		/// <param name="projectileLoc">The location of the projectile</param>
		/// <returns></returns>
		public bool projectileworldcollision(Vector2D projectileLoc)
		{
			if (projectileLoc.GetX() >= Size / 2)
			{
				return true;
			}
			if (projectileLoc.GetY() >= Size / 2)
			{
				return true;
			}

			if (projectileLoc.GetX() <= -Size / 2)
			{
				return true;
			}
			if (projectileLoc.GetY() <= -Size / 2)
			{
				return true;
			}
			return false;
		}
	}
}
