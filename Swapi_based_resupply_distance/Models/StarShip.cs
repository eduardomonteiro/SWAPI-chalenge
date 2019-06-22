using Newtonsoft.Json;
using swapi_based_resupply_distance.Models.Utils;

namespace swapi_based_resupply_distance.Models
{
	public class Starship : BaseModel
	{
		public string Name { get; set; }

		public string Model { get; set; }

		public string Manufacturer { get; set; }

		public long MGLT { get; set; }
		public Duration Consumables { get; set; }	
		public long ResupplyFrequency { get; set; }

		public string Url { get; set; }
	}
}
