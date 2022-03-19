using System;
using System.Windows.Forms;

namespace Client
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			GameController controller = new GameController();
			Application.Run(new TankWars());
		}
	}
}
