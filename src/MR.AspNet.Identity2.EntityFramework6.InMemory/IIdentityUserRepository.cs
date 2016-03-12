using System;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using MR.Patterns.Repository;

namespace MR.AspNet.Identity2.EntityFramework6.InMemory
{
	public interface IIdentityUserRepository : IIdentityUserRepository<IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>>
	{
	}

	public interface IIdentityUserRepository<TUser> : IIdentityUserRepository<TUser, IdentityRole>
		where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
	{
	}

	public interface IIdentityUserRepository<TUser, TRole> : IIdentityUserRepository<TUser, TRole, IdentityUserRole, IdentityUserClaim, IdentityUserLogin, string>
		where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
		where TRole : IdentityRole<string, IdentityUserRole>, new()
	{
	}

	public interface IIdentityRepository<TUser, TRole, TKey> : IIdentityUserRepository<TUser, TRole, IdentityUserRole<TKey>, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, TKey>
		where TUser : IdentityUser<TKey, IdentityUserLogin<TKey>, IdentityUserRole<TKey>, IdentityUserClaim<TKey>>, new()
		where TRole : IdentityRole<TKey, IdentityUserRole<TKey>>, new()
		where TKey : IEquatable<TKey>
	{
	}

	public interface IIdentityUserRepository<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TKey> : IRepositoryCore, IIdentityRoleRepository<TRole, TUserRole, TKey>
		where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
		where TRole : IdentityRole<TKey, TUserRole>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TUserClaim : IdentityUserClaim<TKey>, new()
		where TUserLogin : IdentityUserLogin<TKey>, new()
		where TKey : IEquatable<TKey>
	{
		IQueryable<TUser> Users { get; }
	}
}
