using Newtonsoft.Json;
using System;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace TankWars
{
	/// <summary>
	/// Model For the tanks
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class Tank
	{
		public int lives { get; set; }
		public const int maxLives = 3;
		private const int maxHP = 3;
		public Vector2D velocity { get; internal set; }
		public const double EnginePower = 3;
		public int beamCharges { get; internal set; }

		public int shotCounter = 0;
		public int RespawnTimer = 0;
		public const int Size = 60;
		[JsonProperty()]
		public bool join { get; set; }
		[JsonProperty()]
		public int tank { get; private set; }
		[JsonProperty()]
		public string name { get; private set; }
		[JsonProperty()]
		public Vector2D loc { get; internal set; }
		[JsonProperty()]
		public Vector2D bdir { get; internal set; }
		[JsonProperty()]
		public Vector2D tdir { get; internal set; }
		[JsonProperty()]
		public int score { get; internal set; }
		[JsonProperty()]
		public int hp { get; internal set; }
		[JsonProperty()]
		public bool died { get; set; }
		[JsonProperty()]
		public bool dc { get; set; }

		public Tank()
		{

		}
		/// <summary>
		/// Tank Constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		/// <param name="location"></param>
		public Tank(int iD, string name, Vector2D location)
		{
			tank = iD;
			loc = location;
			bdir = new Vector2D(0, -1);
			tdir = new Vector2D(0, -1);
			this.name = name;
			hp = maxHP;
			score = 0;
			died = false;
			dc = false;
			join = true;
			velocity = new Vector2D(0, 0);
			beamCharges = 0;
			lives = 0;
		}
		/// <summary>
		/// Overrides tostring to make it serialize into valid JSON
		/// </summary>
		/// <returns>The Object in valid JSON format</returns>
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this) + "\n";
		}

		/// <summary>
		/// Checks whether the tank collides with other players projectiles
		/// </summary>
		/// <param name="projectileLoc">The location of the projectile as a vector2D</param>
		/// <returns></returns>
		public bool CollidesProjectile(Vector2D projectileLoc)
		{
			double expansion = Size / 2 + 10 / 2;
			double leftofTank = Math.Min(loc.GetX(), loc.GetX()) - expansion;
			double rightofTank = Math.Max(loc.GetX(), loc.GetX()) + expansion;
			double bottomofTank = Math.Max(loc.GetY(), loc.GetY()) + expansion;
			double topofTank = Math.Min(loc.GetY(), loc.GetY()) - expansion;
			return leftofTank < projectileLoc.GetX() && projectileLoc.GetX() < rightofTank && topofTank < projectileLoc.GetY() && projectileLoc.GetY() < bottomofTank;
		}
		/// <summary>
		/// Checks whether the tank collides with powerups
		/// </summary>
		/// <param name="powerupLoc">The location of the powerup, as a vector2D</param>
		/// <returns></returns>
		public bool CollidesPowerUp(Vector2D powerupLoc)
		{
			double expansion = Size / 2 + 10 / 2;
			double leftofTank = Math.Min(loc.GetX(), loc.GetX()) - expansion;
			double rightofTank = Math.Max(loc.GetX(), loc.GetX()) + expansion;
			double bottomofTank = Math.Max(loc.GetY(), loc.GetY()) + expansion;
			double topofTank = Math.Min(loc.GetY(), loc.GetY()) - expansion;
			return leftofTank < powerupLoc.GetX() && powerupLoc.GetX() < rightofTank && topofTank < powerupLoc.GetY() && powerupLoc.GetY() < bottomofTank;
		}
	}
}
