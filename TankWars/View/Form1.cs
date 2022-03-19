using System;
using System.Drawing;
using System.Windows.Forms;
using TankWars;
using View;
using static View.Form2;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace Client
{
	public partial class TankWars : Form
	{
		private GameController controller;
		//private World.World theWorld;
		private bool connected;
		private DrawingPanel drawingPanel;
		private int viewSize;

		/// <summary>
		/// Constructor
		/// </summary>
		public TankWars()
		{
			InitializeComponent();
			ServerNameTextBox.Text = "localhost";
			controller = new GameController();
			controller.Error += ShowError;
			controller.DrawFrame += OnFrame;
			controller.Connected += HandleConnected;
			controller.UpdateReady += HandleUpdate;
			//controller.Keypressed += Moving;

			connected = false;

			viewSize = 900;
			drawingPanel = new DrawingPanel(null);
			drawingPanel.Size = new Size(viewSize, viewSize);
			this.Controls.Add(drawingPanel);

			this.KeyDown += new KeyEventHandler(HandleKeyDown);
			this.KeyUp += new KeyEventHandler(HandleKeyUp);
			drawingPanel.MouseClick += new MouseEventHandler(HandleMouseClick);
			drawingPanel.MouseMove += new MouseEventHandler(HandleMouseMovement);
		}
		/// <summary>
		/// Handles Mouse Movements
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void HandleMouseMovement(object sender, MouseEventArgs e)
		{
			if (connected)
			{
				Vector2D turretdir = new Vector2D(e.X - (viewSize / 2), e.Y - (viewSize / 2));
				turretdir.Normalize();
				controller.AdjustTurret(turretdir);
			}
		}
		/// <summary>
		/// Handles mouse movements
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void HandleMouseClick(object sender, MouseEventArgs e)
		{
			if (connected)
			{
				if (e.Button == MouseButtons.Left)
				{
					controller.LeftClicked();
				}

				if (e.Button == MouseButtons.Right)
				{
					controller.RightClicked();
				}

			}
		}
		/// <summary>
		/// Handles Key Up Events
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void HandleKeyUp(object sender, KeyEventArgs e)
		{
			if (connected)
			{
				if (e.KeyCode == Keys.W)
				{
					controller.CancelMoveRequest();
				}
				if (e.KeyCode == Keys.A)
				{
					controller.CancelMoveRequest();
				}
				if (e.KeyCode == Keys.S)
				{
					controller.CancelMoveRequest();
				}
				if (e.KeyCode == Keys.D)
				{
					controller.CancelMoveRequest();
				}
			}
		}
		/// <summary>
		/// Handles Key Down events
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (connected)
			{
				if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
				{
					controller.WPressed();
					e.Handled = true;
					e.SuppressKeyPress = true;
				}

				if (e.KeyCode == Keys.S || e.KeyCode == Keys.Right)
				{
					controller.SPressed();
					e.Handled = true;
					e.SuppressKeyPress = true;
				}

				if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
				{
					controller.APressed();
					e.Handled = true;
					e.SuppressKeyPress = true;
				}

				if (e.KeyCode == Keys.D || e.KeyCode == Keys.Down)
				{
					controller.DPressed();
					e.Handled = true;
					e.SuppressKeyPress = true;
				}
				if (e.KeyCode == Keys.Space)
				{
					controller.LeftClicked();
					e.Handled = true;
					e.SuppressKeyPress = true;
				}
				if (e.KeyCode == Keys.B)
				{
					controller.RightClicked();
					e.Handled = true;
					e.SuppressKeyPress = true;
				}
				if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Q)
				{
					Application.Exit();
				}
			}
		}
		/// <summary>
		/// Handles updates from the controller
		/// </summary>
		/// <param name="world">The World Data</param>
		/// <param name="playerID">The Player's ID</param>
		private void HandleUpdate(World world, int playerID)
		{
			drawingPanel.theWorld = world;
			drawingPanel.PlayerID = playerID;
		}
		/// <summary>
		/// Handles Connection
		/// </summary>
		private void HandleConnected()
		{
			connected = true;
			controller.PlayerNameEntered(PlayerNameTextBox.Text + "\n");
			this.KeyPreview = true;
		}
		/// <summary>
		/// Shows error message in the event of an error
		/// </summary>
		/// <param name="err">The Error Message</param>
		private void ShowError(string err)
		{
			MessageBox.Show(err);
		}

		/// <summary>
		/// Updates the Form on every frame
		/// </summary>
		private void OnFrame()
		{
			// Invalidate this form and all its children
			// This will cause the form to redraw as soon as it can
			try
			{
				MethodInvoker m = new MethodInvoker(() => { this.Invalidate(true); });
				this.Invoke(m);
			}
			catch (Exception)
			{

			}
		}
		/// <summary>
		/// Conenction button behavior
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void ConnectButton_Click(object sender, EventArgs e)
		{
			if (PlayerNameTextBox.Text == "")
			{
				MessageBox.Show("Please enter a player name");
				return;
			}

			// Disable the controls and try to connect
			ConnectButton.Enabled = false;
			PlayerNameTextBox.Enabled = false;
			ServerNameTextBox.Enabled = false;
			controller.Connect(ServerNameTextBox.Text);
		}
		/// <summary>
		/// Controls Control button behavior
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form3 form3 = new Form3();
			form3.ShowDialog();

		}
		/// <summary>
		/// Controls about button behavior
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form4 form4 = new Form4();
			form4.ShowDialog();
		}
		/// <summary>
		/// Controls retroMode behavior
		/// </summary>
		/// <param name="sender"> The Object</param>
		/// <param name="e">The EventArgs</param>
		private void retroModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			drawingPanel.retroMode();
		}
	}
}