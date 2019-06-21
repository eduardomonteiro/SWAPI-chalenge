using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StarWarsAPI;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swapi_based_resupply_distance.Services
{
	public class RedisService : IRedis
	{
		public Task<List<Starship>> GetAll()
		{
			using (RedisClient client = new RedisClient("10.1.0.6", 6379))
			{
				IRedisTypedClient<Starship> starships = client.As<Starship>();
				var result = starships.GetAll();
				return Task.FromResult(result.ToList());
			}
		}

		public void SaveStarshipsFromApi(List<Starship> starships)
		{
			using (RedisClient client = new RedisClient("10.1.0.6", 6379))
			{
				IRedisTypedClient<Starship> starshipRedis = client.As<Starship>();
				starshipRedis.StoreAll(starships);
			}
		}
		public void ClearData()
		{
			using (RedisClient client = new RedisClient("10.1.0.6", 6379))
			{
				client.FlushAll();
			}
		}
	}
}
