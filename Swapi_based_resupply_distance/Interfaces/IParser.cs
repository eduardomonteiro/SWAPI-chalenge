using swapi_based_resupply_distance.Enums;
using swapi_based_resupply_distance.Models.Utils;

namespace swapi_based_resupply_distance.Interfaces
{
    public interface IParser
    {
        Duration ParseDuration( string consumables );

        TimeUnit ParseTimeUnit( string unit );

        int ParseInt( string value );
    }
}
