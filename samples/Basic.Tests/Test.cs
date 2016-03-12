using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Basic.Tests
{
	public class Test : TestHost
	{
		[Fact]
		public async Task Basic_Passes()
		{
			var repository = Provider.GetService<IRepository>();
			var userManager = Provider.GetService<UserManager<AppUser>>();

			await userManager.CreateAsync(new AppUser()
			{
				UserName = "foo"
			});

			Assert.True(repository.Users.Any());
		}
	}
}
