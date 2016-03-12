using MR.AspNet.Identity.EntityFramework6;
using MR.Patterns.Repository;

namespace Basic
{
	public class AppRole : IdentityRole, IEntity<string>
	{
	}
}
