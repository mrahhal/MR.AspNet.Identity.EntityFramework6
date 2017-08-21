using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MR.AspNet.Identity.EntityFramework6.InMemory;
using MR.Patterns.Repository;

namespace Basic.Tests
{
	public class TestHost
	{
		static TestHost()
		{
			UnitTestDetector.SetIsInUnitTest();
		}

		public TestHost()
		{
			Provider = CreateProvider();
		}

		private IServiceProvider CreateProvider()
		{
			var services = new ServiceCollection();
			services.AddLogging();
			services.AddIdentityCore<AppUser>(_ => { })
				.AddRoles<AppRole>()
				.AddUserStore<InMemoryUserStore<AppUser, AppRole>>()
				.AddRoleStore<InMemoryRoleStore<AppRole>>();
			var inMemoryRepository = new InMemoryRepository();
			services.AddSingleton<IRepository>(inMemoryRepository);
			services.AddSingleton<IIdentityUserRepository<AppUser, AppRole>>(inMemoryRepository);
			services.AddSingleton<IIdentityRoleRepository<AppRole>>(inMemoryRepository);
			services.AddSingleton<IHttpContextAccessor>(new FakeHttpContextAccessor());
			return services.BuildServiceProvider();
		}

		public IServiceProvider Provider { get; private set; }

		private class FakeHttpContextAccessor : IHttpContextAccessor
		{
			public HttpContext HttpContext { get; set; }
		}
	}
}
