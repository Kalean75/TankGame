

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TankWars;

/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace View
{
	/// <summary>
	/// Inherits from panel
	/// </summary>
	public partial class Form2 : Panel
	{
		/// <summary>
		/// A panel to handle all drawing methods
		/// </summary>
		public class DrawingPanel : Panel
		{
			public World theWorld;
			public int PlayerID;
			DeathAnimation deathAnimation = new DeathAnimation();
			bool retroModeEngaged;


			private Bitmap explosionAnimation;
			bool explosionCurrentlyAnimating;

			//The World
			private Bitmap World;
			//Walls
			private Bitmap Walls;
			//green tank
			private Bitmap greenTank;
			private Bitmap greenTurret;
			private Bitmap greenProj;
			//blue tank
			private Bitmap blueTank;
			private Bitmap blueTurret;
			private Bitmap blueProj;
			//pink tank
			private Bitmap pinkTank;
			private Bitmap pinkTurret;
			private Bitmap pinkProj;
			//orange tank
			private Bitmap orangeTank;
			private Bitmap orangeTurret;
			private Bitmap orangeProj;
			//purple tank
			private Bitmap purpleTank;
			private Bitmap purpleTurret;
			private Bitmap purpleProj;
			//gold tank
			private Bitmap goldTank;
			private Bitmap goldTurret;
			private Bitmap goldProj;
			//yellow tank
			private Bitmap yellowTank;
			private Bitmap yellowTurret;
			private Bitmap yellowProj;
			//red tank
			private Bitmap redTank;
			private Bitmap redTurret;
			private Bitmap redProj;

			private Bitmap powerUp;
			private Bitmap beamAnimation;
			private bool beamCurrentlyAnimating;

			public DrawingPanel(World w)
			{
				DoubleBuffered = true;
				theWorld = w;
				retroModeEngaged = false;
				explosionAnimation = new Bitmap("..\\..\\..\\Resources\\Images\\explosion.gif");
				explosionCurrentlyAnimating = false;
				beamAnimation = new Bitmap("..\\..\\..\\Resources\\Images\\Beam.gif");
				beamCurrentlyAnimating = false;
				World = new Bitmap("..\\..\\..\\Resources\\Images\\Background.png");
				Walls = new Bitmap("..\\..\\..\\Resources\\Images\\Wall.png");
				greenTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodygreen.png");
				greenTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretgreen.png");
				greenProj = new Bitmap("..\\..\\..\\Resources\\Images\\GreenProjectile.png");
				blueTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodyblue.png");
				blueTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretblue.png");
				blueProj = new Bitmap("..\\..\\..\\Resources\\Images\\BlueProjectile.png");
				pinkTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodypink.png");
				pinkTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretpink.png");
				pinkProj = new Bitmap("..\\..\\..\\Resources\\Images\\PinkProjectile.png");
				orangeTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodyorange.png");
				orangeTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretorange.png");
				orangeProj = new Bitmap("..\\..\\..\\Resources\\Images\\OrangeProjectile.png");
				purpleTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodypurple.png");
				purpleTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretpurple.png");
				purpleProj = new Bitmap("..\\..\\..\\Resources\\Images\\PurpleProjectile.png");
				goldTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodygold.png");
				goldTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretgold.png");
				goldProj = new Bitmap("..\\..\\..\\Resources\\Images\\GoldProjectile.png");
				yellowTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodyyellow.png");
				yellowTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretyellow.png");
				yellowProj = new Bitmap("..\\..\\..\\Resources\\Images\\YellowProjectile.png");
				redTank = new Bitmap("..\\..\\..\\Resources\\Images\\tankbodyred.png");
				redTurret = new Bitmap("..\\..\\..\\Resources\\Images\\tankturretred.png");
				redProj = new Bitmap("..\\..\\..\\Resources\\Images\\RedProjectile.png");
				powerUp = new Bitmap("..\\..\\..\\Resources\\Images\\powerup.png");

			}

			public delegate void ObjectDrawer(object o, PaintEventArgs e);
			/// <summary>
			/// Converts The graphics into something reminiscent of that game buried deep in the Nevada desert....
			/// </summary>
			public void retroMode()
			{
				if (!retroModeEngaged)
				{
					retroModeEngaged = true;
				}
				else
				{
					retroModeEngaged = false;
				}
			}
			/// <summary>
			/// Draws the tanks
			/// The first 8 tanks will be different colors
			///
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The paint Event Args</param>
			private void DrawTank(Object o, PaintEventArgs e)
			{
				int width = 60;
				int height = 60;
				Tank tank = o as Tank;
				RectangleF srcRect = new RectangleF(-(width / 2), -(height / 2), width, height);

				if (retroModeEngaged)
				{
					DrawRetroTank(e, tank);
				}
				else
				{
					// Draw image to screen.
					if (tank.tank % 8 == 0)
					{

						// Draw image to screen.
						e.Graphics.DrawImage(greenTank, srcRect);
					}
					else if (tank.tank % 8 == 1)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(blueTank, srcRect);

					}
					else if (tank.tank % 8 == 2)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(orangeTank, srcRect);

					}
					else if (tank.tank % 8 == 3)
					{

						// Draw image to screen.
						e.Graphics.DrawImage(pinkTank, srcRect);

					}
					else if (tank.tank % 8 == 4)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(purpleTank, srcRect);

					}
					else if (tank.tank % 8 == 5)
					{

						// Draw image to screen.
						e.Graphics.DrawImage(goldTank, srcRect);

					}
					else if (tank.tank % 8 == 6)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(yellowTank, srcRect);
					}
					else if (tank.tank % 8 == 7)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(redTank, srcRect);

					}
				}

			}
			/// <summary>
			/// Draws the tank...Old is gold baby
			/// </summary>
			/// <param name="e">eventargs</param>
			/// <param name="tank">The tank</param>
			private void DrawRetroTank(PaintEventArgs e, Tank tank)
			{
				int width = 50;
				int height = 70;
				if (tank.tank % 8 == 0)
				{
					using (SolidBrush GoldBrush = new SolidBrush(Color.Gold))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();
							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(GoldBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}

				}
				else if (tank.tank % 8 == 1)
				{
					using (SolidBrush LavenderBrush = new SolidBrush(Color.Lavender))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();
							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(LavenderBrush, r);
							e.Graphics.DrawRectangle(pen, r);

						}
					}

				}
				else if (tank.tank % 8 == 2)
				{

					using (SolidBrush BlueBrush = new SolidBrush(Color.Blue))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(BlueBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
				}
				else if (tank.tank % 8 == 3)
				{
					using (SolidBrush RedBrush = new SolidBrush(Color.Red))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(RedBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
				}
				else if (tank.tank % 8 == 4)
				{
					using (SolidBrush PurpleBrush = new SolidBrush(Color.Purple))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(PurpleBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
				}
				else if (tank.tank % 8 == 5)
				{
					using (SolidBrush BrownBrush = new SolidBrush(Color.Brown))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(BrownBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
				}
				else if (tank.tank % 8 == 6)
				{

					using (SolidBrush whiteBrush = new SolidBrush(Color.White))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(whiteBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
				}
				else if (tank.tank % 8 == 7)
				{
					// Draw image to screen.
					using (SolidBrush orangeBrush = new SolidBrush(Color.Orange))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
							e.Graphics.FillRectangle(orangeBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
				}
			}


			/// <summary>
			/// Draws the name and score of the player underneath the tank
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void DrawNameandScore(Object o, PaintEventArgs e)
			{
				Tank tank = o as Tank;
				string DrawString = tank.name + ": " + tank.score;
				int width = (int)tank.loc.GetX() - 30;
				int height = (int)tank.loc.GetY() + 40;
				using (SolidBrush NameandScore = new SolidBrush(Color.White))
				{
					Graphics formGraphics = this.CreateGraphics();
					Font drawFont = new Font("Arial", 12);
					StringFormat drawFormat = new StringFormat();
					Rectangle rect = new Rectangle(0, 0, 200, 20);
					e.Graphics.DrawString(DrawString, drawFont, NameandScore, rect, drawFormat);
				}
			}
			/// <summary>
			/// Draws the HP of the player
			/// Green represents full health(3)
			/// Yellow represents 2 health
			/// Red represents 1 health(near death)
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void DrawHP(Object o, PaintEventArgs e)
			{
				Tank tank = o as Tank;
				//int width = (int)tank.loc.GetX() - 10;
				// int height = (int)tank.loc.GetY() + 40;
				if (tank.hp == 3)
				{
					using (SolidBrush FullHP = new SolidBrush(Color.DarkGreen))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();
							Rectangle rect = new Rectangle(0, 0, 36, 15);
							e.Graphics.FillRectangle(FullHP, rect);
							e.Graphics.DrawRectangle(pen, rect);
						}
					}
				}
				else if (tank.hp == 2)
				{
					// width -= 10;
					using (SolidBrush TwoHP = new SolidBrush(Color.Yellow))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();
							Rectangle rect = new Rectangle(0, 0, 26, 15);
							e.Graphics.FillRectangle(TwoHP, rect);
							e.Graphics.DrawRectangle(pen, rect);
						}
					}
				}
				else if (tank.hp == 1)
				{
					// width -= 10;
					using (SolidBrush OneHP = new SolidBrush(Color.DarkRed))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();
							Rectangle rect = new Rectangle(0, 0, 16, 15);
							e.Graphics.FillRectangle(OneHP, rect);
							e.Graphics.DrawRectangle(pen, rect);
						}
					}
				}
			}
			/// <summary>
			/// Draws the Tank Turret
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void drawTankTurret(Object o, PaintEventArgs e)
			{
				//make 50 x 50
				int width = 50;
				int height = 50;
				Tank tank = o as Tank;
				RectangleF srcRect = new RectangleF(-(width / 2), -(height / 2), width, height);
				if (retroModeEngaged)
				{
					DrawRetroTurret(e, tank);
				}
				else
				{
					// Draw image to screen.
					if (tank.tank % 8 == 0)
					{
						e.Graphics.DrawImage(greenTurret, srcRect);

					}
					else if (tank.tank % 8 == 1)
					{
						e.Graphics.DrawImage(blueTurret, srcRect);

					}
					else if (tank.tank % 8 == 2)
					{

						e.Graphics.DrawImage(orangeTurret, srcRect);

					}
					else if (tank.tank % 8 == 3)
					{
						e.Graphics.DrawImage(pinkTurret, srcRect);

					}
					else if (tank.tank % 8 == 4)
					{
						e.Graphics.DrawImage(purpleTurret, srcRect);
					}
					else if (tank.tank % 8 == 5)
					{
						e.Graphics.DrawImage(goldTurret, srcRect);
					}
					else if (tank.tank % 8 == 6)
					{

						e.Graphics.DrawImage(yellowTurret, srcRect);
					}
					else if (tank.tank % 8 == 7)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(redTurret, srcRect);
					}
				}
			}


			/// <summary>
			/// Draws the tank turret...Old is gold baby
			/// </summary>
			/// <param name="e">eventargs</param>
			/// <param name="tank">The tank</param>
			private void DrawRetroTurret(PaintEventArgs e, Tank tank)
			{
				int width = 15;
				int height = 30;
				if (tank.tank % 8 == 0)
				{
					using (SolidBrush GoldBrush = new SolidBrush(Color.Gold))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(GoldBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush GoldBrush = new SolidBrush(Color.Gold))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(GoldBrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}

				}
				else if (tank.tank % 8 == 1)
				{
					using (SolidBrush LavenderBrush = new SolidBrush(Color.Lavender))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(LavenderBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush LavenderBrush = new SolidBrush(Color.Lavender))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(LavenderBrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}

				}
				else if (tank.tank % 8 == 2)
				{

					using (SolidBrush BlueBrush = new SolidBrush(Color.Blue))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(BlueBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush BlueBrush = new SolidBrush(Color.Blue))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(BlueBrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}

				}
				else if (tank.tank % 8 == 3)
				{
					using (SolidBrush RedBrush = new SolidBrush(Color.Red))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(RedBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush Redbrush = new SolidBrush(Color.Red))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(Redbrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}

				}
				else if (tank.tank % 8 == 4)
				{
					using (SolidBrush PurpleBrush = new SolidBrush(Color.Purple))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(PurpleBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush PurpleBrush = new SolidBrush(Color.Purple))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(PurpleBrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}
				}
				else if (tank.tank % 8 == 5)
				{
					using (SolidBrush BrownBrush = new SolidBrush(Color.Brown))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(BrownBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush Brownbrush = new SolidBrush(Color.Brown))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(Brownbrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}
				}
				else if (tank.tank % 8 == 6)
				{

					using (SolidBrush whiteBrush = new SolidBrush(Color.White))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(whiteBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush whiteBrush = new SolidBrush(Color.White))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(whiteBrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}
				}
				else if (tank.tank % 8 == 7)
				{
					// Draw image to screen.
					using (SolidBrush orangeBrush = new SolidBrush(Color.Orange))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Graphics formGraphics = this.CreateGraphics();

							Rectangle r = new Rectangle(-(width / 2), -(height), width, height);
							e.Graphics.FillRectangle(orangeBrush, r);
							e.Graphics.DrawRectangle(pen, r);
						}
					}
					using (SolidBrush orangeBrush = new SolidBrush(Color.Orange))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(orangeBrush, r);
							e.Graphics.DrawEllipse(pen, r);

						}
					}
				}
			}

			/// <summary>
			/// Draws the death animation
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void DrawDeathAnimation(Object o, PaintEventArgs e)
			{
				int width = 100;
				int height = 100;
				Tank tank = o as Tank;
				RectangleF srcRect = new RectangleF(-(width / 2), -(height / 2), width, height);
				if (retroModeEngaged)
				{
					using (SolidBrush redBrush = new SolidBrush(Color.Red))
					{
						using (Pen pen = new Pen(Color.Black, 2))
						{
							Rectangle r = new Rectangle(-(height / 2), -(height / 2), height, height);
							e.Graphics.FillEllipse(redBrush, r);
							e.Graphics.DrawEllipse(pen, r);
						}
					}
				}
				else
				{
					if (!explosionCurrentlyAnimating)
					{
						//Begin the animation only once.
						ImageAnimator.Animate(explosionAnimation, new EventHandler(this.OnFrameChanged));
						explosionCurrentlyAnimating = true;
					}
					ImageAnimator.UpdateFrames();
					e.Graphics.DrawImage(explosionAnimation, srcRect/*srcRect*/);
				}
			}

			private void OnFrameChanged(object o, EventArgs e)
			{
				this.Invalidate();
			}


			/// <summary>
			/// Draws the World
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			public void drawWorld(Object o, PaintEventArgs e)
			{
				int Width = theWorld.Size;
				int Height = theWorld.Size;
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				if (retroModeEngaged)
				{
					using (SolidBrush greenBrush = new SolidBrush(Color.Green))
					{
						Rectangle r = new Rectangle(-(Width), -(Height), Width, Height);
						e.Graphics.FillRectangle(greenBrush, r);
					}
				}
				else
				{
					RectangleF srcRect = new RectangleF(-(Width), -(Height), Width, Height);
					// Draw image to screen.
					e.Graphics.DrawImage(World, srcRect);
				}
			}
			/// <summary>
			/// Draws the Walls
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void drawWalls(Object o, PaintEventArgs e)
			{
				int Width = 50;
				int Height = 50;
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

				if (retroModeEngaged)
				{
					using (SolidBrush pinkBrush = new SolidBrush(Color.HotPink))
					{
						Rectangle r = new Rectangle(-(Width / 2), -(Height / 2) - 2, Width, Height);
						e.Graphics.FillRectangle(pinkBrush, r);
					}
				}
				else
				{
					RectangleF srcRect = new RectangleF(-(Width / 2), -(Height / 2) - 2, Width, Height);
					// Draw image to screen.
					e.Graphics.DrawImage(Walls, srcRect);
				}

			}

			/// <summary>
			/// Draws the projectiles
			/// The projectiles will match the color of the player
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void drawProjectile(Object o, PaintEventArgs e)
			{
				int width = 10;
				int height = 10;
				Projectile proj = o as Projectile;
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				RectangleF srcRect = new RectangleF(-(width / 2), -(height / 2), width, height);
				if (retroModeEngaged)
				{
					using (SolidBrush YellowBrush = new SolidBrush(Color.Yellow))
					{
						Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);
						e.Graphics.FillEllipse(YellowBrush, r);
					}
				}
				else
				{
					if (proj.owner % 8 == 0)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(greenProj, srcRect);

					}
					else if (proj.owner % 8 == 1)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(blueProj, srcRect);

					}
					else if (proj.owner % 8 == 2)
					{

						// Draw image to screen.
						e.Graphics.DrawImage(orangeProj, srcRect);

					}
					else if (proj.owner % 8 == 3)
					{

						e.Graphics.DrawImage(pinkProj, srcRect);

					}
					else if (proj.owner % 8 == 4)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(purpleProj, srcRect);

					}
					else if (proj.owner % 8 == 5)
					{

						// Draw image to screen.
						e.Graphics.DrawImage(goldProj, srcRect);

					}
					else if (proj.owner % 8 == 6)
					{

						// Draw image to screen.
						e.Graphics.DrawImage(yellowProj, srcRect);

					}
					else if (proj.owner % 8 == 7)
					{
						// Draw image to screen.
						e.Graphics.DrawImage(redProj, srcRect);
					}
				}
			}
			/// <summary>
			/// Draws Powerups
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void drawPowerUp(Object o, PaintEventArgs e)
			{
				int Width = 20;
				int Height = 20;
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				if (retroModeEngaged)
				{
					using (SolidBrush BlackBrush = new SolidBrush(Color.Black))
					{
						Rectangle r = new Rectangle(-(Width / 2), -(Height / 2), Width, Height);
						e.Graphics.FillPie(BlackBrush, r, 270, 270);
					}
				}
				else
				{
					RectangleF srcRect = new RectangleF(-(Width / 2), -(Height / 2) - 2, Width, Height);
					// Draw image to screen.
					e.Graphics.DrawImage(powerUp, srcRect);
				}

			}
			/// <summary>
			/// Draws Beams
			/// </summary>
			/// <param name="o">The Object</param>
			/// <param name="e">The Paint Event Args</param>
			private void drawBeam(Object o, PaintEventArgs e)
			{
				int width = -theWorld.Size;
				int height = 10;
				double beamdist = theWorld.Size * 0.4;
				RectangleF srcRect = new RectangleF(-(width / 2) - (int)beamdist, -(height / 2), width, height);
				//e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				if (retroModeEngaged)
				{
					width = theWorld.Size;
					beamdist = theWorld.Size * 0.51;
					Rectangle srcRect2 = new Rectangle(-(width / 2) - (int)beamdist, -(height / 2), width, height);

					using (SolidBrush BeamBrush = new SolidBrush(Color.DeepSkyBlue))
					{
						e.Graphics.FillRectangle(BeamBrush, srcRect2);
					}
				}
				else
				{
					if (!beamCurrentlyAnimating)
					{
						//Begin the animation only once.
						ImageAnimator.Animate(beamAnimation, new EventHandler(this.OnFrameChanged));
						beamCurrentlyAnimating = true;
					}

					ImageAnimator.UpdateFrames();
					e.Graphics.DrawImage(beamAnimation, srcRect/*srcRect*/);

				}
			}



			/// <summary>
			/// This method performs a translation and rotation to drawn an object in the world.
			/// </summary>
			/// <param name="e">PaintEventArgs to access the graphics (for drawing)</param>
			/// <param name="o">The object to draw</param>
			/// <param name="worldX">The X coordinate of the object in world space</param>
			/// <param name="worldY">The Y coordinate of the object in world space</param>
			/// <param name="angle">The orientation of the objec, measured in degrees clockwise from "up"</param>
			/// <param name="drawer">The drawer delegate. After the transformation is applied, the delegate is invoked to draw whatever it wants</param>
			private void DrawObjectWithTransform(PaintEventArgs e, object o, double worldX, double worldY, double angle, ObjectDrawer drawer)
			{
				// "push" the current transform
				Matrix oldMatrix = e.Graphics.Transform.Clone();

				e.Graphics.TranslateTransform((int)worldX, (int)worldY);
				e.Graphics.RotateTransform((float)angle);
				drawer(o, e);

				// "pop" the transform
				e.Graphics.Transform = oldMatrix;
			}


			/// <summary>
			/// Repaints the Panel on every frame
			/// </summary>
			/// <param name="e">The Paint Event Args</param>
			protected override void OnPaint(PaintEventArgs e)
			{
				if (theWorld != null && theWorld.Players.ContainsKey(PlayerID))
				{

					double playerX = theWorld.Players[PlayerID].loc.GetX();
					double playerY = theWorld.Players[PlayerID].loc.GetY();
					// Center the view on the middle of the world,
					// since the image and world use different coordinate systems
					int viewSize = 900; // view is square, so we can just use width
					e.Graphics.TranslateTransform((float)-playerX + (viewSize / 2), (float)-playerY + (viewSize / 2));
					DrawObjectWithTransform(e, theWorld, theWorld.Size / 2, theWorld.Size / 2, 0, drawWorld);

					if (theWorld.walls.Count > 0)
					{
						foreach (Wall wall in theWorld.walls.Values)
						{
							double startcoordinate;
							double endcoordinate;
							double coordinateLength;
							if (wall.p1.GetX() == wall.p2.GetX())
							{
								startcoordinate = wall.p1.GetY();
								endcoordinate = wall.p2.GetY();
								coordinateLength = startcoordinate - endcoordinate;
								if (coordinateLength < 0)
								{
									for (double i = startcoordinate; i <= endcoordinate; i += 50)
									{

										DrawObjectWithTransform(e, wall, wall.p1.GetX(), i, 0, drawWalls); ;
									}
								}
								else if (coordinateLength > 0)
								{
									for (double i = startcoordinate; i >= endcoordinate; i -= 50)
									{

										DrawObjectWithTransform(e, wall, wall.p1.GetX(), i, 0, drawWalls); ;
									}
								}
							}
							else if (wall.p1.GetY() == wall.p2.GetY())
							{
								startcoordinate = wall.p1.GetX();
								endcoordinate = wall.p2.GetX();
								coordinateLength = startcoordinate - endcoordinate;
								if (coordinateLength < 0)
								{
									for (double i = startcoordinate; i <= endcoordinate; i += 50)
									{

										DrawObjectWithTransform(e, wall, i, wall.p1.GetY(), 0, drawWalls); ;
									}
								}
								else if (coordinateLength > 0)
								{
									for (double i = startcoordinate; i >= endcoordinate; i -= 50)
									{

										DrawObjectWithTransform(e, wall, i, wall.p1.GetY(), 0, drawWalls); ;
									}
								}

							}

						}
					}
					// Draw the powerups
					foreach (PowerUp pow in theWorld.Powerups.Values.ToList())
					{
						if (pow.died == false)
						{
							DrawObjectWithTransform(e, pow, pow.loc.GetX(), pow.loc.GetY(), 0, drawPowerUp);
						}
					}
					//draw the players
					foreach (Tank tank in theWorld.Players.Values.ToList())
					{
						if (!deathAnimation.theDead.ContainsKey(tank.tank) && tank.died)
						{
							deathAnimation.theDead.Add(tank.tank, 30);
						}
						if (tank.hp == 0/*deathanimation.theDead[tank.tank] == 0*/)
						{
							if (deathAnimation.theDead.Keys.Count == 0)
							{
								explosionCurrentlyAnimating = false;
								ImageAnimator.StopAnimate(explosionAnimation, new EventHandler(this.OnFrameChanged));
							}

							if (deathAnimation.theDead.ContainsKey(tank.tank) && deathAnimation.theDead[tank.tank] > 0)
							{
								DrawObjectWithTransform(e, tank, tank.loc.GetX(), tank.loc.GetY(), tank.tdir.ToAngle(), DrawDeathAnimation);
								deathAnimation.theDead[tank.tank]--;
							}
							else /*if (deathAnimation.theDead[tank.tank] <= 0 || tank.dc)*/
							{
								deathAnimation.theDead.Remove(tank.tank);
							}

							//deathAnimation.Remove(tank.tank);
						}
						//}
						else
						{
							//Refactor these for the x and y???
							//draw player name and score
							DrawObjectWithTransform(e, tank, tank.loc.GetX() - 30, tank.loc.GetY() + 40, 0, DrawNameandScore);
							//draw player hp
							DrawObjectWithTransform(e, tank, tank.loc.GetX() - 20, tank.loc.GetY() - 50, 0, DrawHP);
							DrawObjectWithTransform(e, tank, tank.loc.GetX(), tank.loc.GetY(), tank.bdir.ToAngle(), DrawTank);
							DrawObjectWithTransform(e, tank, tank.loc.GetX(), tank.loc.GetY(), tank.tdir.ToAngle(), drawTankTurret);
						}
					}

					//draw projectiles
					foreach (Projectile projectile in theWorld.projectiles.Values.ToList())
					{

						if (projectile.died != true)
						{
							DrawObjectWithTransform(e, projectile, projectile.loc.GetX(), projectile.loc.GetY(), projectile.dir.ToAngle(), drawProjectile);
						}
					}
					///REWRITE THIS(maybe)
					foreach (Beam beam in theWorld.beams.Values.ToList())
					{
						DrawObjectWithTransform(e, beam, beam.org.GetX(), beam.org.GetY(), beam.dir.ToAngle() + 90, drawBeam);
						beam.framesUntilFade--;
						if (beam.framesUntilFade == 0)
						{
							theWorld.beams.Remove(beam.beam);
							ImageAnimator.StopAnimate(beamAnimation, new EventHandler(this.OnFrameChanged));
						}
					}
				}

				// Do anything that Panel (from which we inherit) needs to do*/
				base.OnPaint(e);
			}

		}

	}

	/// <summary>
	/// Internal Class to handle Death Animations
	/// </summary>
	internal class DeathAnimation
	{
		public Dictionary<int, int> theDead;

		public DeathAnimation()
		{
			theDead = new Dictionary<int, int>();
		}
	}

}