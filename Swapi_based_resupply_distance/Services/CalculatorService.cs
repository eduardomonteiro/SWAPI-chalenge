using swapi_based_resupply_distance.Enums;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;
using swapi_based_resupply_distance.Models.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swapi_based_resupply_distance.Services
{
    public class CalculatorService : ICalculator
	{
		private readonly IRedis redis;

		public CalculatorService(IRedis redis)
		{
			this.redis = redis;
		}

		public async Task<List<Starship>> ResupplyCalculation(long distance)
		{
			var starships = await redis.GetAll();

			if (starships == null)
				throw new ArgumentNullException(nameof(starships));

			foreach (var starship in starships)
			{
				if (starship.MGLT == -1)
				{
					//logger.Error($"Unknown speed for: {starship.Name}");
					continue;
				}

				// Calculate total number of hours that the consumables can last.
				var hours = ToHours(starship.Consumables);

				// finaly calculate total number of stops required based on the distance, speed and time
				var stops = ToStops(distance, starship.MGLT, hours);
				starship.ResupplyFrequency = stops;
				//logger.Info($"{starship.Name} needs to stop: ", stops);
			}

			return starships;
		}

		public long ToStops(long distance, long mglt, long hours)
		{
			return (long)((double)distance / (hours * mglt));
		}

		public long ToHours(Duration duration)
		{
			if (duration == null)
				throw new ArgumentNullException(nameof(duration));

			switch (duration.Unit)
			{
				case TimeUnit.Day:
					return 24 * duration.Time;
				case TimeUnit.Week:
					return 168 * duration.Time;
				case TimeUnit.Month:
					return 730 * duration.Time;
				case TimeUnit.Year:
					return 8760 * duration.Time;
				default:
					return 0;
			}
		}
	}
}