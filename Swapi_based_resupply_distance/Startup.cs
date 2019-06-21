using System.Linq;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using swapi_based_resupply_distance.Interfaces;
using swapi_based_resupply_distance.Services;

namespace swapi_based_resupply_distance
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins,
				builder =>
				{
					builder.WithOrigins("http://10.5.0.5",
										"https://10.5.0.5")
								.AllowAnyHeader()
								.AllowAnyMethod(); ;
				});
			});

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			//my services registration
			services.AddTransient<IStarship, StarshipService>();
			services.AddTransient<ICalculator, CalculatorService>();
			services.AddTransient<IMapper, MapperService>();
			services.AddTransient<IRedis, RedisService>();
			services.AddTransient<IParser, ParserService>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//enable OData
			services.AddOData();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseCors(MyAllowSpecificOrigins);

			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
				routes.EnableDependencyInjection();
				routes.Expand().Select().Count().OrderBy().Filter();
			});
		}
	}
}
