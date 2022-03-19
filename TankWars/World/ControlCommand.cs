using Newtonsoft.Json;

namespace TankWars
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ControlCommand
	{
		[JsonProperty()]
		public string moving { get; set; }
		[JsonProperty()]
		public string fire { get; set; }
		[JsonProperty()]
		public Vector2D tdir { get; set; }

		public ControlCommand()
		{

		}
	}
}
