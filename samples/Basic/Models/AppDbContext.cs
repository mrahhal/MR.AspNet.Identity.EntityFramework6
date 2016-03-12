using System.Data.Entity;
using MR.AspNet.Identity.EntityFramework6;

namespace Basic
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim, IdentityRoleClaim>
	{
		public AppDbContext(string connectionString) : base(connectionString)
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
		}

		public DbSet<Blog> Blogs { get; set; }
	}
}
