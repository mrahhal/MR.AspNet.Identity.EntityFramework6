using System.Data.Entity;
using MR.AspNet.Identity.EntityFramework6;

namespace Key.Int.Models
{
	public class AppDbContext : IdentityDbContextInt<AppUser>
	{
		public AppDbContext() : base(ConnectionString)
		{
		}

		public AppDbContext(string connectionString) : base(connectionString)
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
		}

		public static string ConnectionString { get; set; } = "Server=(localdb)\\mssqllocaldb;Database=MR.AspNet.Identity.EntityFramework6.Key.Int;Trusted_Connection=True;MultipleActiveResultSets=true";

		public DbSet<Blog> Blogs { get; set; }
	}
}
