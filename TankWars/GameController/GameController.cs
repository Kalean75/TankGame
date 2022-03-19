using NetworkUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using TankWars;


namespace Client

{
	public class GameController
	{
		World theWorld;
		int worldSize;
		int playerID;
		string command;

		string moving;
		string fire;


		public delegate void ConnectedHandler();
		public event ConnectedHandler Connected;

		public delegate void errorHandler(string err);
		public event errorHandler Error;

		public delegate void PanelUpdateHandler();
		public event PanelUpdateHandler DrawFrame;

		public delegate void WorldUpdateHandler(World world, int playerID);
		public event WorldUpdateHandler UpdateReady;
		private Vector2D turretdirection;
		/// <summary>
		/// Constructor
		/// </summary>
		public GameController()
		{
			worldSize = -1;
			playerID = -1;
			moving = "none";
			fire = "none";
			turretdirection = new Vector2D(0, 0);
		}

		SocketState theServer = null;

		/// <summary>
		/// Begins the process of connecting to the server
		/// </summary>
		/// <param name="addr">Address to connect to</param>
		public void Connect(string addr)
		{
			Networking.ConnectToServer(OnConnect, addr, 11000);
		}
		/// <summary>
		/// Handles the Left click event.
		/// Sets fire command to main cannon
		/// </summary>
		public void LeftClicked()
		{
			fire = "main";
		}
		/// <summary>
		/// Handles the right click event
		/// Sets fire command to beam
		/// </summary>
		public void RightClicked()
		{
			fire = "alt";
		}
		/// <summary>
		/// Handles turret Movement
		/// </summary>
		/// <param name="turretdir">The Direction of the turret, in the direction of the mouse</param>
		public void AdjustTurret(Vector2D turretdir)
		{
			turretdirection = turretdir;
		}


		/// <summary>
		/// Method to be invoked by the networking library when a connection is made
		/// </summary>
		/// <param name="state">the SocketState</param>
		private void OnConnect(SocketState state)
		{
			if (state.ErrorOccurred)
			{
				// inform the view
				Error("Error connecting to server");
				return;
			}

			theServer = state;

			// Start an event loop to receive messages from the server
			state.OnNetworkAction = WorldDataReceived;
			Connected();
			Networking.GetData(state);
		}
		/// <summary>
		/// Handles KeyUp
		/// Sets moving command to none
		/// </summary>
		public void CancelMoveRequest()
		{
			moving = "none";
		}

		/// <summary>
		/// Method to Update the view
		/// </summary>
		/// <param name="state"></param>
		private void WorldDataReceived(SocketState state)
		{
			if (state.ErrorOccurred)
			{
				// inform the view
				Error(state.ErrorMessage);
				return;
			}
			ProcessData(state);

			// Continue the event loop
			// state.OnNetworkAction has not been changed, 
			// so this same method (ReceiveMessage) 
			// will be invoked when more data arrives
			Networking.GetData(state);


		}
		/// <summary>
		/// Proecesses data received by the Server
		/// </summary>
		/// <param name="state">The Socketstate</param>
		private void ProcessData(SocketState state)
		{
			string totalData = state.GetData();
			string[] parts = Regex.Split(totalData, @"(?<=[\n])");

			// Loop until we have processed all messages.
			// We may have received more than one.

			foreach (string p in parts)
			{
				// Ignore empty strings added by the regex splitter
				if (p.Length == 0)
				{
					continue;
				}
				// The regex splitter will include the last string even if it doesn't end with a '\n',
				// So we need to ignore it if this happens. 
				if (p[p.Length - 1] != '\n')
				{
					break;
				}

				// build a list of messages to send to the view
				if (playerID < 0) playerID = int.Parse(p);
				else if (worldSize < 0)
				{
					worldSize = int.Parse(p);
					theWorld = new World(worldSize);
				}
				else
				{
					ParseWorldElement(p);
				}
				// Then remove it from the SocketState's growable buffer
				state.RemoveData(0, p.Length);
			}
			if (theWorld != null)
			{
				SetCommand();
			}
			if (command != null)
			{
				Networking.Send(state.TheSocket, command);
				fire = "none";
			}

			DrawFrame();
		}
		/// <summary>
		/// Creates the command to send to the server for player's tank
		/// </summary>
		private void SetCommand()
		{

			if (theWorld.Players.ContainsKey(playerID))
			{
				Command c = new Command(moving, fire, turretdirection);
				command = JsonConvert.SerializeObject(c) + "\n";
				fire = "none";
			}
		}
		/// <summary>
		/// Parses JSON objects and deserializes them
		/// </summary>
		/// <param name="asJson">The Json string to deserialize</param>
		private void ParseWorldElement(string asJson)
		{
			JObject jObject = new JObject();
			lock (theWorld)
			{
				jObject = JObject.Parse(asJson);
				//Tanks
				JToken fieldname = jObject["tank"];
				if (fieldname != null)
				{
					Tank tank = JsonConvert.DeserializeObject<Tank>(asJson);
					// Add tank
					if (!theWorld.Players.ContainsKey(tank.tank))
					{
						theWorld.Players.Add(tank.tank, tank);
					}
					if (tank.dc == true)
					{
						theWorld.Players.Remove(tank.tank);
					}
					else
					{
						theWorld.Players[tank.tank] = tank;
					}
					UpdateReady(theWorld, playerID);
					return;
				}
				//Projectiles
				fieldname = jObject["proj"];
				if (fieldname != null)
				{
					Projectile proj = JsonConvert.DeserializeObject<Projectile>(asJson);
					if (!theWorld.projectiles.ContainsKey(proj.proj))
					{
						theWorld.projectiles.Add(proj.proj, proj);
					}
					else
					{
						theWorld.projectiles[proj.proj] = proj;
					}
					if (proj.died)
					{
						theWorld.projectiles.Remove(proj.proj);

					}

					return;
				}
				//Walls
				fieldname = jObject["wall"];
				if (fieldname != null)
				{
					Wall wall = JsonConvert.DeserializeObject<Wall>(asJson);
					if (!theWorld.walls.ContainsKey(wall.wall) && theWorld != null)
					{
						theWorld.walls.Add(wall.wall, wall);
					}
					return;
				}
				//Powerups
				fieldname = jObject["power"];
				if (fieldname != null)
				{
					PowerUp powerUp = JsonConvert.DeserializeObject<PowerUp>(asJson);
					//draw powerup
					if (!theWorld.Powerups.ContainsKey(powerUp.power))
					{
						theWorld.Powerups.Add(powerUp.power, powerUp);
					}
					if (powerUp.died == true)
					{
						theWorld.Powerups.Remove(powerUp.power);
					}
					else
					{
						theWorld.Powerups[powerUp.power] = powerUp;
					}
					return;
				}
				//Beams
				fieldname = jObject["beam"];
				if (fieldname != null)
				{
					Beam beam = JsonConvert.DeserializeObject<Beam>(asJson);
					//draw powerup
					if (!theWorld.Powerups.ContainsKey(beam.beam))
					{
						theWorld.beams.Add(beam.beam, beam);
					}
					else
					{
						theWorld.beams[beam.beam] = beam;
					}
					return;
				}
			}
		}
		/// <summary>
		/// Sends to playerName to the server to begin receiving JSON from the server
		/// </summary>
		/// <param name="playerName"></param>
		public void PlayerNameEntered(string playerName)
		{
			if (theServer != null)
			{
				Networking.Send(theServer.TheSocket, playerName);
			}
		}
		/// <summary>
		/// Handles W key Press event
		/// sets moving to up
		/// </summary>
		public void WPressed()
		{
			moving = "up";
		}
		/// <summary>
		/// Handles S key Press event
		/// sets moving to down
		/// </summary>
		public void SPressed()
		{
			moving = "down";
		}
		/// <summary>
		/// Handles A key Press event
		/// sets moving to Left
		/// </summary>
		public void APressed()
		{
			moving = "left";
		}
		/// <summary>
		/// Handles D key Press event
		/// sets moving to right
		/// </summary>
		public void DPressed()
		{
			moving = "right";
		}
	}

	/// <summary>
	/// Internal Class for issuing command objects
	/// </summary>
	internal class Command
	{
		/// <summary>
		/// Movement command
		/// </summary>
		[JsonProperty()]
		public string moving { get; private set; }
		/// <summary>
		/// Fire Command
		/// </summary>
		[JsonProperty()]
		public string fire { get; private set; }
		/// <summary>
		/// Turret Direction Commandd
		/// </summary>
		[JsonProperty()]
		public Vector2D tdir { get; private set; }
		public Command(string moved, string fired, Vector2D turretdir)
		{
			moving = moved;
			fire = fired;
			tdir = turretdir;
		}
	}
}