using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using swapi_based_resupply_distance.Enums;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Models;
using swapi_based_resupply_distance.Models.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests;

namespace Swapi_based_resupply_distance.tests
{
	public class TestCalculator : TestContainers
	{
		private ICalculator calculatorService;
		private IStarship starshipService;
		private IRedis redisService;
		private IMapper mapperService;
		private IParser parserService;
		private ILogger logger;

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
		public void Test_days()
		{
			var hours = calculatorService.ToHours(new Duration(TimeUnit.Day, 6));
			var exptected = 144;

			NUnit.Framework.Assert.AreEqual(exptected, hours);
		}

		[Test]
		public void Test_years()
		{
			var hours = calculatorService.ToHours(new Duration(TimeUnit.Year, 3));
			var exptected = 26280;

			Assert.AreEqual(exptected, hours);
		}

		[Test]
		public void Test_empty()
		{
			var hours = calculatorService.ToHours(new Duration());
			var exptected = 0;

			Assert.AreEqual(exptected, hours);
		}

		[Test]
		public void Test_null()
		{
			var hours = calculatorService.ToHours(null);
		}

		public Starship PrepareMillenium()
		{
			var starship = new Starship
			{
				Name = "Millennium Falcon",
				Model = "YT-1300 light freighter",
				Manufacturer = "Corellian Engineering Corporation",
				Consumables = parserService.ParseDuration("2 months"),
				Url = "https://swapi.co/api/starships/10/",
				MGLT = 75,
				ResupplyFrequency = 0,
				Id = new System.Guid()
			};
			return starship;
		}

		public void DisposeMillenium()
		{
			using (RedisClient client = new RedisClient("10.1.0.6", 6379))
			{
				IRedisTypedClient<Starship> starshipRedis = client.As<Starship>();
				starshipRedis.FlushAll();
			}
		}

		[Test]
		public async Task Test_runner_info()
		{
			var starships = new List<Starship>();
			starships.Add(PrepareMillenium());

			var resultCalc = (List<Starship>) calculatorService.ResupplyCalculation(1000000, starships).Result;
			
			var result = resultCalc.Count> 0 ? resultCalc.First().ResupplyFrequency == 9? true : false : false;

			Assert.IsTrue(result);

			DisposeMillenium();
		}

		[Test]
		public async Task Test_runner_invalid_mglt()
		{
			var starships = new List<Starship>();
			var starship = PrepareMillenium();
			starship.MGLT = -1;
			starships.Add(starship);

			var resultCalc = (List<Starship>)calculatorService.ResupplyCalculation(1000000, starships).Result;

			var result = resultCalc.Count > 0 ? resultCalc.First().ResupplyFrequency == 0 ? true : false : false;

			Assert.IsTrue(result);

			DisposeMillenium();
		}
		[Test]
		public async Task Test_runner_full_integration()
		{
			long distance = 1000000;
			const long expected = 9;

			starshipService.SetAllOnRedis();
			var starships = await this.calculatorService.ResupplyCalculationFromCache(distance);

			var millenium = starships.FirstOrDefault(x => x.Name == "Millennium Falcon");

			bool result = millenium != null ? millenium.ResupplyFrequency == 9 ? true : false : false;

			Assert.AreEqual(millenium.ResupplyFrequency, expected);

			Assert.IsTrue(result);

			redisService.ClearData();
		}
	}
}
