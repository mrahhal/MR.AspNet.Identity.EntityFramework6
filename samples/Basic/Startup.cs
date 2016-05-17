using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MR.AspNet.Identity.EntityFramework6;

namespace Basic
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();
			services.AddTransient((_) => new AppDbContext("Server=(localdb)\\mssqllocaldb;Database=MR.AspNet.Identity.EntityFramework6-Basic;Trusted_Connection=True;MultipleActiveResultSets=true"));
			services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
			services.AddTransient<IRepository, DefaultRepository>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
