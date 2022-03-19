using Newtonsoft.Json;
/// <summary>
/// Authors:
/// Devin White
/// Xuyen Nguyen
/// </summary>
namespace TankWars
{
	/// <summary>
	/// Model For PowerUps
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class PowerUp
	{
		[JsonProperty()]
		public int power { get; private set; }
		[JsonProperty()]
		public Vector2D loc { get; private set; }
		[JsonProperty()]
		public bool died { get; internal set; }

		public PowerUp()
		{

		}
		public PowerUp(int iD, Vector2D location)
		{
			power = iD;
			loc = location;
			died = false;
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