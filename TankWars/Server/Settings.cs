using System.Collections.Generic;
using System.Xml;

namespace TankWars
{
	internal class Settings
	{
		public string LivesMode { get; private set; }
		public int universeSize { get; private set; }
		public int MSPerFrame { get; private set; }
		public int FramesPerShot { get; private set; }
		public int RespawnRate { get; private set; }
		public int PowerupSpawnTime { get; private set; }
		public int MaxPowerups { get; private set; }
		public HashSet<Wall> walls { get; } = new HashSet<Wall>();
		/// <summary>
		/// Constructor the the settings. Takes an XML file and sets the game state 
		/// Based upon file elements
		/// </summary>
		/// <param name="filePath">The filepath to be read from</param>
		public Settings(string filePath)
		{
			double x = 0;
			Vector2D p1 = null;
			Vector2D p2 = null;
			using (XmlReader reader = XmlReader.Create(filePath))
			{
				while (reader.Read())
				{
					if (reader.IsStartElement())
					{
						if (reader.Name == "UniverseSize")
						{
							reader.Read();
							int.TryParse(reader.Value, out int universeSize);
							this.universeSize = universeSize;
						}
						else if (reader.Name == "MSPerFrame")
						{
							reader.Read();
							int.TryParse(reader.Value, out int MSPerFrame);
							this.MSPerFrame = MSPerFrame;
						}
						else if (reader.Name == "FramesPerShot")
						{
							reader.Read();
							int.TryParse(reader.Value, out int FramesPerShot);
							this.FramesPerShot = FramesPerShot;
						}
						else if (reader.Name == "RespawnRate")
						{
							reader.Read();
							int.TryParse(reader.Value, out int RespawnRate);
							this.RespawnRate = RespawnRate;
						}
						else if (reader.Name == "MaxPowerups")
						{
							reader.Read();
							int.TryParse(reader.Value, out int maxPowerups);
							this.MaxPowerups = maxPowerups;
						}
						else if (reader.Name == "PowerupSpawnTime")
						{
							reader.Read();
							int.TryParse(reader.Value, out int PowerupSpawnTime);
							this.PowerupSpawnTime = PowerupSpawnTime;
						}
						else if (reader.Name == "LivesMode")
						{
							reader.Read();
							string Livesmode = reader.Value.ToUpper();
							this.LivesMode = Livesmode;
						}
						else if (reader.Name == "x")
						{
							reader.Read();
							double.TryParse(reader.Value, out x);
						}
						else if (reader.Name == "y")
						{
							if (p1 == null)
							{
								reader.Read();
								double.TryParse(reader.Value, out double y);
								p1 = new Vector2D(x, y);
							}
							else
							{
								reader.Read();
								double.TryParse(reader.Value, out double y);
								p2 = new Vector2D(x, y);
								walls.Add(new Wall(p1, p2));
								p1 = null;
								p2 = null;
							}
						}
					}

				}
			}

		}
	}
}