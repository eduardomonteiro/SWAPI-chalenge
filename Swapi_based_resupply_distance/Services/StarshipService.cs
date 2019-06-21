using StarWarsAPI;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;
using System.Collections.Generic;
using System.Linq;

namespace swapi_based_resupply_distance.Services
{
	public class StarshipService : IStarship
	{
		private readonly IMapper mapper;
		private readonly IRedis redis;


		public StarshipService(IMapper mapper, IRedis redis)
		{
			this.mapper = mapper;
			this.redis = redis;
		}

		public void SetAllOnRedis()
		{
			var starships = GetAllFromApi();
			redis.SaveStarshipsFromApi(starships);
		}

		public List<Starship> GetAllFromApi()
		{
			var api = new StarWarsAPIClient();

			var starships = new List<Starship>();

			int pageNo = 0;

			while (true)
			{
				var page = api.GetAllStarship((++pageNo).ToString());

				starships.AddRange(page.Result.results.Select(x => mapper.GetStarship(x)));

				if (!page.Result.isNext)
				{
					break;
				}
			}

			return starships;
		}
	}
}
