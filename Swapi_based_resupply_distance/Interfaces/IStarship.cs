using swapi_based_resupply_distance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swapi_based_resupply_distance.Interfaces
{
	//named like this to not conflict with StarWarsAPI
	public interface IStarship
	{
		void SetAllOnRedis();
		List<Starship> GetAllFromApi();
	}
}
