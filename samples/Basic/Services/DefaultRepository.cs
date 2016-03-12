using System.Linq;
using MR.Patterns.Repository;

namespace Basic
{
	public class DefaultRepository : RepositoryCore<AppDbContext>, IRepository
	{
		public DefaultRepository(AppDbContext context)
			: base(context)
		{
		}

		public IQueryable<AppUser> Users => Context.Users;
		public IQueryable<AppRole> Roles => Context.Roles;
		public IQueryable<Blog> Blogs => Context.Blogs;
	}
}
