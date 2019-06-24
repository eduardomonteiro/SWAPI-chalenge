using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swapi_based_resupply_distance.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StarShipController : ControllerBase
	{
		private readonly IRedis redisService;
		private ICalculator calculatorService { get; }
		private IStarship starshipService { get; }

		public StarShipController(IRedis redisService, ICalculator calculatorService, IStarship starshipService)
		{
			this.redisService = redisService;
			this.calculatorService = calculatorService;
			this.starshipService = starshipService;
		}

		// GET api/Starship
		[HttpGet]
		[EnableQuery()]
		[EnableCors("_myAllowSpecificOrigins")]
		public async Task<List<Starship>> Get()
		{
			var starships = await this.redisService.GetAll();
			return starships;
		}

		// GET api/Starship
		[HttpGet("ResupplyCalc")]
		[EnableQuery()]
		[EnableCors("_myAllowSpecificOrigins")]
		public async Task<List<Starship>> ResupplyCalc(long distance)
		{
			var starships = await this.calculatorService.ResupplyCalculationFromCache(distance);
			return starships;
		}

		[HttpGet("LoadData")]
		[EnableCors("_myAllowSpecificOrigins")]
		public ActionResult<string> LoadData()
		{
			try
			{
				this.starshipService.SetAllOnRedis();
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return "{Redis is ready!}";
		}

		[HttpGet("ClearData")]
		[EnableCors("_myAllowSpecificOrigins")]
		public ActionResult<string> ClearData()
		{
			try
			{
				redisService.ClearData();
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return "`{Redis is empty!}";
		}
	}
}
