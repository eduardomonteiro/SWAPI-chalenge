using swapi_based_resupply_distance.Enums;

namespace swapi_based_resupply_distance.Models.Utils
{
	public class Duration
	{
		public Duration()
		{
		}

		public Duration(TimeUnit unit, long time)
		{
			Unit = unit;
			Time = time;
		}

		public TimeUnit Unit { get; set; }

		public long Time { get; set; }
	}
}
