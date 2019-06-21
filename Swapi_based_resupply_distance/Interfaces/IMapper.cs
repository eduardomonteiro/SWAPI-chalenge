using swapi_based_resupply_distance.Models;

namespace swapi_based_resupply_distance.Interfaces
{
    public interface IMapper
    {
        Starship GetStarship( StarWarsAPI.Model.Starship starship );
    }
}
