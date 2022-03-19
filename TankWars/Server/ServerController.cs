using NetworkUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace TankWars
{
	/// <summary>
	/// The controller for the server. Provides updates to the world, sends updates to clients
	/// </summary>
	class ServerController
	{
		private Settings settings;
		private World theWorld;
		private Dictionary<int, SocketState> clients = new Dictionary<int, SocketState>();
		private string startupInfo;

		private int powerupID;
		private int powerUpCounter = 0;
		private int powerupSpawnTime;

		private int maxPowerUps;
		private int currentPowerUps = 0;

		/// <summary>
		/// Constructor for the servercontroller Takes a filepath as a string
		/// </summary>
		/// <param name="settings">The settings XML file. Contains world/game properites. </param>
		public ServerController(Settings settings)
		{
			this.settings = settings;
			theWorld = new World(settings.universeSize);
			theWorld.framesUntilRedrawProj = settings.FramesPerShot;
			theWorld.framesUntilRespawn = settings.RespawnRate;
			theWorld.LivesMode = settings.LivesMode;
			maxPowerUps = settings.MaxPowerups;
			powerupSpawnTime = settings.PowerupSpawnTime;

			foreach (Wall wall in settings.walls)
			{
				theWorld.walls[wall.wall] = wall;
			}

			StringBuilder sb = new StringBuilder();
			sb.Append(theWorld.Size);
			sb.Append("\n");
			foreach (Wall wall in theWorld.walls.Values)
			{
				sb.Append(wall.ToString());
			}
			startupInfo = sb.ToString();

		}
		/// <summary>
		/// Starts the server and keeps it running until closed
		/// </summary>
		internal void StartServer()
		{
			Networking.StartServer(NewClient, 11000);
			Thread t = new Thread(Update);
			Console.WriteLine("Server is Running: Accepting Clients");
			t.Start();
		}
		/// <summary>
		/// Updates the world and sends updates to clients on every frame
		/// </summary>
		private void Update()
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			while (true)
			{
				powerUpCounter++;
				while (watch.ElapsedMilliseconds < settings.MSPerFrame)
				{

				}
				watch.Restart();
				StringBuilder sb = new StringBuilder();
				lock (theWorld)
				{
					theWorld.Update();
					foreach (Tank tank in theWorld.Players.Values)
					{
						sb.Append(tank.ToString());
						if (tank.dc)
						{
							theWorld.Players.Remove(tank.tank);
						}
						if (tank.died)
						{
							tank.died = false;
						}

					}
					foreach (Projectile projectile in theWorld.projectiles.Values)
					{
						sb.Append(projectile.ToString());
						if (projectile.died == true)
						{
							theWorld.projectiles.Remove(projectile.proj);
						}
					}

					foreach (Beam beam in theWorld.beams.Values)
					{
						sb.Append(beam.ToString());
						theWorld.beams.Remove(beam.beam);
						currentPowerUps--;

					}

					foreach (PowerUp powerup in theWorld.Powerups.Values)
					{
						sb.Append(powerup.ToString());
						if (powerup.died == true)
						{
							theWorld.Powerups.Remove(powerup.power);
						}
					}

					/// spawns a powerup every 400 frames
					if (powerUpCounter >= powerupSpawnTime && currentPowerUps < maxPowerUps)
					{
						spawnNewPowerUp();
						powerUpCounter = 0;
					}

				}
				string frame = sb.ToString();
				HashSet<int> disconnectedClients = new HashSet<int>();
				lock (clients)
				{
					foreach (SocketState client in clients.Values)
					{
						if (!Networking.Send(client.TheSocket, frame))
						{
							disconnectedClients.Add((int)client.ID);
						}

						foreach (int id in disconnectedClients)
						{
							RemoveClient(id);
						}
					}
				}
			}

		}
		/// <summary>
		/// Reveives a new client and gets data
		/// </summary>
		/// <param name="client">The client</param>
		private void NewClient(SocketState client)
		{
			client.OnNetworkAction = ReceivePlayerName;
			Networking.GetData(client);
		}
		/// <summary>
		/// Spanws a new powerup after a set amount of time determined by settings
		/// </summary>
		private void spawnNewPowerUp()
		{
			///FIX SO PROJECTILES DON'T SPAWN IN WALL
			currentPowerUps++;
			powerupID++;
			theWorld.Powerups.Add(powerupID, new PowerUp(powerupID, theWorld.RandomizePlacement(theWorld.Size)));
		}
		/// <summary>
		/// Receives the player name then begins sending game data
		/// </summary>
		/// <param name="client">The client</param>
		private void ReceivePlayerName(SocketState client)
		{
			string name = client.GetData();
			if (!name.EndsWith("\n"))
			{
				client.GetData();
				return;
			}
			client.RemoveData(0, name.Length);
			name = name.Trim();
			Networking.Send(client.TheSocket, (int)client.ID + "\n");
			Networking.Send(client.TheSocket, startupInfo);

			lock (theWorld)
			{
				theWorld.Players[(int)client.ID] = new Tank((int)client.ID, name, theWorld.RandomizePlacement(theWorld.Size));

			}

			lock (clients)
			{
				clients.Add((int)client.ID, client);
			}

			client.OnNetworkAction = ReceiveControlCommand;

			Networking.GetData(client);

		}
		/// <summary>
		/// A loop that continues to receive and then re-distribute client commands to other clients
		/// </summary>
		/// <param name="client">The client</param>
		private void ReceiveControlCommand(SocketState client)
		{
			string totalData = client.GetData();
			string[] parts = Regex.Split(totalData, @"(?<=[\n])");
			foreach (string p in parts)
			{
				// Ignore empty strings added by the regex splitter
				if (p.Length == 0)
					continue;
				// The regex splitter will include the last string even if it doesn't end with a '\n',
				// So we need to ignore it if this happens. 
				if (p[p.Length - 1] != '\n')
					break;
				ControlCommand command = JsonConvert.DeserializeObject<ControlCommand>(p);
				lock (theWorld)
				{
					if (theWorld.Players[(int)client.ID].hp > 0)
						theWorld.commands[(int)client.ID] = command;
				}
				client.RemoveData(0, p.Length);
			}
			// Remove the client if they aren't still connected
			if (client.ErrorOccurred)
			{

				RemoveClient((int)client.ID);
				return;
			}
			Networking.GetData(client);
		}
		/// <summary>
		/// Removes a client from the clients dictionary
		/// Sets Tanks to died and dc to true
		/// for the other clients to draw properly
		/// </summary>
		/// <param name="id">The ID of the client</param>
		private void RemoveClient(int id)
		{
			Console.WriteLine("Client " + id + " disconnected");
			lock (clients)
			{
				clients.Remove(id);
				if (theWorld.Players.ContainsKey(id))
				{
					theWorld.Players[id].died = true;
					theWorld.Players[id].dc = true;
				}
			}
		}
	}
}
