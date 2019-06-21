using swapi_based_resupply_distance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swapi_based_resupply_distance.Interfaces
{
    public interface IRedis
	{
		Task<List<Starship>> GetAll();
		void SaveStarshipsFromApi(List<Starship> starships);
		void ClearData();
	}
}
