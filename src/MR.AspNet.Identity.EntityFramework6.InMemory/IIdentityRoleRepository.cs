using System;
using System.Linq;
using MR.Patterns.Repository;

namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public interface IIdentityRoleRepository : IIdentityRoleRepository<IdentityRole>
	{
	}

	public interface IIdentityRoleRepository<TRole> : IIdentityRoleRepository<TRole, IdentityUserRole, IdentityRoleClaim, string>
		where TRole : IdentityRole, new()
	{
	}

	public interface IIdentityRoleRepository<TRole, TKey> : IIdentityRoleRepository<TRole, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>, TKey>
		where TRole : IdentityRole<TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>, new()
		where TKey : IEquatable<TKey>
	{
	}

	public interface IIdentityRoleRepository<TRole, TUserRole, TRoleClaim, TKey> : IRepositoryCore
		where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TKey : IEquatable<TKey>
	{
		IQueryable<TRole> Roles { get; }
	}
}
