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
	/// Model for Beams
	/// </summary>
	public class Beam
	{
		[JsonIgnore]
		public int framesUntilFade = 50;
		[JsonProperty()]
		public int beam { get; private set; }
		[JsonProperty()]
		public Vector2D org { get; private set; }
		[JsonProperty()]
		public Vector2D dir { get; private set; }
		[JsonProperty()]
		public int owner { get; private set; }
		/// <summary>
		/// Default constructor for JSON
		/// </summary>
		public Beam()
		{
		}
		/// <summary>
		/// Constructor for a new beam
		/// </summary>
		/// <param name="ID">The ID of the beam</param>
		/// <param name="own">The owner of the beam. Same as the owner's ID</param>
		/// <param name="origin">The beams origin, used for calculating collision and drawing</param>
		/// <param name="direction">The beams direction, used for calculating collision and drawing </param>
		public Beam(int ID, int own, Vector2D origin, Vector2D direction)
		{
			beam = ID;
			owner = own;
			org = origin;
			dir = direction;
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
		/// Determines if a ray interescts a circle
		/// </summary>
		/// <param name="rayOrig">The origin of the ray</param>
		/// <param name="rayDir">The direction of the ray</param>
		/// <param name="center">The center of the circle</param>
		/// <param name="r">The radius of the circle</param>
		/// <returns></returns>
		public static bool Intersects(Vector2D rayOrig, Vector2D rayDir, Vector2D center, double r)
		{
			// ray-circle intersection test
			// P: hit point
			// ray: P = O + tV
			// circle: (P-C)dot(P-C)-r^2 = 0
			// substituting to solve for t gives a quadratic equation:
			// a = VdotV
			// b = 2(O-C)dotV
			// c = (O-C)dot(O-C)-r^2
			// if the discriminant is negative, miss (no solution for P)
			// otherwise, if both roots are positive, hit

			double a = rayDir.Dot(rayDir);
			double b = ((rayOrig - center) * 2.0).Dot(rayDir);
			double c = (rayOrig - center).Dot(rayOrig - center) - r * r;

			// discriminant
			double disc = b * b - 4.0 * a * c;

			if (disc < 0.0)
				return false;

			// find the signs of the roots
			// technically we should also divide by 2a
			// but all we care about is the sign, not the magnitude
			double root1 = -b + Math.Sqrt(disc);
			double root2 = -b - Math.Sqrt(disc);

			return (root1 > 0.0 && root2 > 0.0);
		}
	}
}
