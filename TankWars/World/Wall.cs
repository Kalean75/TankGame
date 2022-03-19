using Newtonsoft.Json;
using System;

namespace TankWars
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Wall
	{

		[JsonProperty()]
		public int wall { get; private set; }

		// p1 & p2 should have the same x or y value
		// Length always be multiple of the wal width
		// Endpoints can be anywhere, as long as the distance is a multiple of 50
		// Order of p1 & p2 is irrelevant
		// Walls can be overlap and intersect each other
		[JsonProperty()]
		public Vector2D p1 { get; private set; }
		[JsonProperty()]
		public Vector2D p2 { get; private set; }
		private static int nextId;
		//The thickness of the walls
		public double thickness { get; private set; }

		/// <summary>
		/// The default constructor for deserializing JSON
		/// </summary>
		public Wall()
		{

		}
		/// <summary>
		/// Constuctor for a wall. Takes a P1 and P2 and draws between them
		/// </summary>
		/// <param name="p1">The first endpoint of the wall</param>
		/// <param name="p2">The second endpoiint of the wall</param>
		public Wall(Vector2D p1, Vector2D p2)
		{
			this.p1 = p1;
			this.p2 = p2;
			wall = nextId++;
			thickness = 50;

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
		/// Checks whether the tank collides with a wall
		/// </summary>
		/// <param name="tankLoc">The location of the tank, as a vector2D</param>
		/// <returns></returns>
		public bool CollidesTank(Vector2D tankLoc)
		{
			double expansion = thickness / 2 + Tank.Size / 2;
			double leftofWall = Math.Min(p1.GetX(), p2.GetX()) - expansion;
			double rightofwall = Math.Max(p1.GetX(), p2.GetX()) + expansion;
			double bottomofWall = Math.Max(p1.GetY(), p2.GetY()) + expansion;
			double topofWall = Math.Min(p1.GetY(), p2.GetY()) - expansion;
			return leftofWall < tankLoc.GetX() && tankLoc.GetX() < rightofwall && topofWall < tankLoc.GetY() && tankLoc.GetY() < bottomofWall;
		}
		/// <summary>
		/// Checks whether the tank collides with projectiles or powerups
		/// </summary>
		/// <param name="ObjectLocation">The location of the object, as a vector2D</param>
		/// <returns></returns>
		public bool CollidesOtherObject(Vector2D ObjectLocation)
		{
			double expansion = thickness / 2 + 10 / 2;
			double leftofWall = Math.Min(p1.GetX(), p2.GetX()) - expansion;
			double rightofwall = Math.Max(p1.GetX(), p2.GetX()) + expansion;
			double bottomofWall = Math.Max(p1.GetY(), p2.GetY()) + expansion;
			double topofWall = Math.Min(p1.GetY(), p2.GetY()) - expansion;
			return leftofWall < ObjectLocation.GetX() && ObjectLocation.GetX() < rightofwall && topofWall < ObjectLocation.GetY() && ObjectLocation.GetY() < bottomofWall;
		}
	}
}
