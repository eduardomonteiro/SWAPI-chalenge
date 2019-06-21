using Newtonsoft.Json;
using swapi_based_resupply_distance.Models.Utils;

namespace swapi_based_resupply_distance.Models
{
	public class Starship : BaseModel
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "model")]
		public string Model { get; set; }

		[JsonProperty(PropertyName = "manufacturer")]
		public string Manufacturer { get; set; }

		public long MGLT { get; set; }
		public Duration Consumables { get; set; }	
		public long ResupplyFrequency { get; set; }

		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }
	}
}
