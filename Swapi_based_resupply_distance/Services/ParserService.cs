using swapi_based_resupply_distance.Enums;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models.Utils;
using System;
using System.Linq;

namespace swapi_based_resupply_distance.Services
{
    public class ParserService : IParser
	{
		public Duration ParseDuration(string consumables)
		{
			if (string.IsNullOrEmpty(consumables))
				throw new ArgumentNullException(nameof(consumables));

			var parts = consumables.Split(' ');

			if (parts?.Count() != 2)
				return new Duration(TimeUnit.Unknown, -1);

			return new Duration(ParseTimeUnit(parts[1]), ParseInt(parts[0]));
		}

		public TimeUnit ParseTimeUnit(string unit)
		{
			switch (unit)
			{
				case "day":
				case "days":
					return TimeUnit.Day;
				case "week":
				case "weeks":
					return TimeUnit.Week;
				case "month":
				case "months":
					return TimeUnit.Month;
				case "year":
				case "years":
					return TimeUnit.Year;
				default:
					return TimeUnit.Unknown;
			}
		}

		public int ParseInt(string value)
		{
			if (int.TryParse(value, out var number))
				return number;

			return -1;
		}
	}
}
