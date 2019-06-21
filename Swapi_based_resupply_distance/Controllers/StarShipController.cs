using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;

namespace swapi_based_resupply_distance.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StarShipController : ControllerBase
	{
		private readonly IRedis redis;
		private ICalculator calculatorService { get; }
		private IStarship starshipService { get; }

		public StarShipController(IRedis redis, ICalculator calculatorService, IStarship starshipService)
		{
			this.redis = redis;
			this.calculatorService = calculatorService;
			this.starshipService = starshipService;
			this.starshipService.SetAllOnRedis();
		}

		// GET api/Starship
		[AcceptVerbs("GET")]
		[EnableQuery()]
		[EnableCors]
		public async Task<List<Starship>> Get(long distance)
		{
			var starships = await this.calculatorService.ResupplyCalculationFromCache(distance);
			redis.ClearData();
			return starships;
		}

		//// GET api/Starship/5
		//[HttpGet("{id}")]
		//public ActionResult<string> Get(int id)
		//{
		//	return "value";
		//}

		//// POST api/Starship
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT api/Starship/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE api/Starship/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
