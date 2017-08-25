using System;
using System.Linq;
using MR.Patterns.Repository;

namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public interface IIdentityUserRepository : IIdentityUserRepository<IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim, IdentityUserToken>>
	{
	}

	public interface IIdentityUserRepository<TUser> : IIdentityUserRepository<TUser, IdentityRole>
		where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim, IdentityUserToken>, new()
	{
	}

	public interface IIdentityUserRepository<TUser, TRole> : IIdentityUserRepository<TUser, TRole, IdentityUserRole, IdentityUserClaim, IdentityUserLogin, IdentityRoleClaim, IdentityUserToken, string>
		where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim, IdentityUserToken>, new()
		where TRole : IdentityRole<string, IdentityUserRole, IdentityRoleClaim>, new()
	{
	}

	public interface IIdentityRepository<TUser, TRole, TKey> : IIdentityUserRepository<TUser, TRole, IdentityUserRole<TKey>, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, TKey>
		where TUser : IdentityUser<TKey, IdentityUserLogin<TKey>, IdentityUserRole<TKey>, IdentityUserClaim<TKey>, IdentityUserToken<TKey>>, new()
		where TRole : IdentityRole<TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>, new()
		where TKey : IEquatable<TKey>
	{
	}

	public interface IIdentityUserRepository<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken, TKey> : IRepositoryCore, IIdentityRoleRepository<TRole, TUserRole, TRoleClaim, TKey>
		where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim, TUserToken>, new()
		where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TUserClaim : IdentityUserClaim<TKey>, new()
		where TUserLogin : IdentityUserLogin<TKey>, new()
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TUserToken : IdentityUserToken<TKey>
		where TKey : IEquatable<TKey>
	{
		IQueryable<TUser> Users { get; }
	}
}
