using Newtonsoft.Json;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace TankWars
{
	/// <summary>
	/// Model for Projectiles
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class Projectile
	{

		public const double speed = 25;
		[JsonProperty()]
		public int proj { get; private set; }
		[JsonProperty()]
		public Vector2D loc { get; internal set; }
		[JsonProperty()]
		public Vector2D dir { get; internal set; }
		[JsonProperty()]
		public bool died { get; internal set; }
		[JsonProperty()]
		public int owner { get; private set; }
		public Vector2D velocity { get; internal set; }

		/// <summary>
		/// Default projectile constructor for deserializing JSON
		/// </summary>
		public Projectile()
		{

		}
		/// <summary>
		/// Constructor used primarily for moving/re-drawing a projectile
		/// </summary>
		/// <param name="projectile">The projectile's id, as an int</param>
		/// <param name="location">The projectiles Location, as a vector2D</param>
		/// <param name="direction">The projectiles Direction, as a vector2D</param>
		/// <param name="dead">Boolean indicating whether the projectile is "dead"</param>
		/// <param name="own">The owner of the projectile as an int Matches the player's ID</param>
		public Projectile(int projectile, Vector2D location, Vector2D direction, bool dead, int own)
		{
			proj = projectile;
			loc = location;
			dir = direction;
			died = dead;
			owner = own;
			velocity = new Vector2D(0, 0);
		}
		/// <summary>
		/// Overrides tostring to make it serialize into valid JSON
		/// </summary>
		/// <returns>The Object in valid JSON format</returns>
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this) + "\n";
		}
	}
}
