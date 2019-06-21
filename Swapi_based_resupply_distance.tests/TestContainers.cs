using SimpleInjector;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Services;
using TestToolsToXunitProxy;

namespace Tests
{
	[TestClass]
	public class TestContainers
	{
		protected readonly Container container;

		public TestContainers()
		{
			container = new Container();

			container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

			container.Register<IParser, ParserService>();
			container.Register<ICalculator, CalculatorService>();
			container.Register<IMapper, MapperService>();
			container.Register<IStarship, StarshipService>();
			container.Register<IRedis, RedisService>();

			container.Verify();
		}
	}
}