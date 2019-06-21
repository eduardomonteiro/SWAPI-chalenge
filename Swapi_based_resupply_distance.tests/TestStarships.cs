using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Services;
using System.Threading.Tasks;
using Tests;

namespace Swapi_based_resupply_distance.tests
{
	class TestStarships : TestContainers
	{
		private ICalculator calculatorService;
		private IStarship starshipService;
		private IRedis redisService;
		private IMapper mapperService;
		private IParser parserService;
		private ILogger logger;

		private bool result = false;

		[SetUp]
		public void Setup()
		{
			calculatorService = container.GetInstance<ICalculator>();
			parserService = container.GetInstance<IParser>();
			redisService = container.GetInstance<IRedis>();
			starshipService = container.GetInstance<IStarship>();
			mapperService = container.GetInstance<IMapper>();
			logger = Substitute.For<ILogger>();
		}

		[Test]
		public async Task Test_get_api()
		{
			var starShipsTest = new StarshipService(mapperService, redisService);

			var starshipsReturned =  starShipsTest.GetAllFromApi();

			//logger.Received().Info("Total of " + starshipsReturned.Count + " Starships returned from API.");

			result = starshipsReturned.Count > 0 ? true : false;

			Assert.IsTrue(result);
		}

		[Test]
		public async Task Test_set_data_on_redis()
		{
			starshipService.SetAllOnRedis();

			var starships = await this.calculatorService.ResupplyCalculationFromCache(1000000);

			bool result = starships.Count > 0 ? true : false;

			Assert.IsTrue(result);

			redisService.ClearData();
		}
	}
}
