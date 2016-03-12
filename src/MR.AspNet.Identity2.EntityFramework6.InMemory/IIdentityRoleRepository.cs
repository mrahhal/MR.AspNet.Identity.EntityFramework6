using System;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using MR.Patterns.Repository;

namespace MR.AspNet.Identity2.EntityFramework6.InMemory
{
	public interface IIdentityRoleRepository : IIdentityRoleRepository<IdentityRole>
	{
	}

	public interface IIdentityRoleRepository<TRole> : IIdentityRoleRepository<TRole, IdentityUserRole, string>
		where TRole : IdentityRole, new()
	{
	}

	public interface IIdentityRoleRepository<TRole, TUserRole, TKey> : IRepositoryCore
		where TRole : IdentityRole<TKey, TUserRole>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TKey : IEquatable<TKey>
	{
		IQueryable<TRole> Roles { get; }
	}
}
