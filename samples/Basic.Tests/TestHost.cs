using System;
using Microsoft.AspNet.Http;
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
			services
				.AddIdentity<AppUser, AppRole>()
				.AddUserStore<InMemoryUserStore<AppUser, AppRole>>()
				.AddRoleStore<InMemoryRoleStore<AppRole>>();
			var inMemoryRepository = new InMemoryRepository();
			services.AddInstance<IRepository>(inMemoryRepository);
			services.AddInstance<IIdentityUserRepository<AppUser, AppRole>>(inMemoryRepository);
			services.AddInstance<IIdentityRoleRepository<AppRole>>(inMemoryRepository);
			services.AddInstance<IHttpContextAccessor>(new FakeHttpContextAccessor());
			return services.BuildServiceProvider();
		}

		public IServiceProvider Provider { get; private set; }

		private class FakeHttpContextAccessor : IHttpContextAccessor
		{
			public HttpContext HttpContext { get; set; }
		}
	}
}
