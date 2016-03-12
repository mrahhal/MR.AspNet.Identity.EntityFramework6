using System.Linq;
using MR.AspNet.Identity.EntityFramework6.InMemory;
using MR.Patterns.Repository;

namespace Basic
{
	public interface IRepository : IRepositoryCore, IIdentityUserRepository<AppUser, AppRole>, IIdentityRoleRepository<AppRole>
	{
		IQueryable<Blog> Blogs { get; }
	}
}
