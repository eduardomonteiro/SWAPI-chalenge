using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;
using System;

namespace swapi_based_resupply_distance.Services
{
	public class MapperService : IMapper
	{
		private readonly IParser parser;

		public MapperService(IParser parser)
		{
			this.parser = parser;
		}

		public Starship GetStarship(StarWarsAPI.Model.Starship starship)
		{
			return starship == null ? null : new Starship
			{
				Id = Guid.NewGuid(),
				Name = starship.name,
				Model = starship.model,
				Manufacturer = starship.manufacturer,
				MGLT = parser.ParseInt(starship.MGLT),
				Consumables = parser.ParseDuration(starship.consumables),
				Url = starship.url
			};
		}
	}
}
