using System.Linq;

namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public interface IIdentityLoginRepository<TUserLogin>
	{
		IQueryable<TUserLogin> UserLogins { get; }
	}
}
