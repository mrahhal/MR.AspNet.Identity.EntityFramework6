using MR.AspNet.Identity.EntityFramework6;
using MR.Patterns.Repository;

namespace Basic
{
	public class AppUser : IdentityUser, IEntity<string>
	{
		public string Name { get; set; }
	}
}
