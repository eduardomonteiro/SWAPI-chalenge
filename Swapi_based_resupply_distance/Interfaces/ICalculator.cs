using swapi_based_resupply_distance.Models;
using swapi_based_resupply_distance.Models.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swapi_based_resupply_distance.Interfaces
{
    public interface ICalculator
    {
        long ToStops( long distance, long mglt, long hours );

        long ToHours( Duration duration );

		Task<List<Starship>> ResupplyCalculation(long distance);

	}
}
