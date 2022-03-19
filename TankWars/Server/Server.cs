using System;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace TankWars
{
	/// <summary>
	/// Main class
	/// </summary>
	class Server
	{
		static void Main(string[] args)
		{
			//Server server = new Server();
			//server.startServer();

			Settings settings = new Settings(@"..\..\..\..\Resources\settings.xml");
			ServerController controller = new ServerController(settings);
			controller.StartServer();

			// Sleep to prevent the program from closing,
			// since all the real work is done in separate threads.
			// StartServer is non-blocking.
			Console.Read();
		}

	}
}
