using System.Linq;
using MR.Patterns.Repository;

namespace Basic
{
	public class InMemoryRepository : InMemoryRepositoryCore, IRepository
	{
		public IQueryable<AppUser> Users => For<AppUser>();
		public IQueryable<AppRole> Roles => For<AppRole>();
		public IQueryable<Blog> Blogs => For<Blog>();
	}
}
