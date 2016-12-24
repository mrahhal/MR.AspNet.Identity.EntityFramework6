using System.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace MR.AspNet.Identity.EntityFramework6
{
	/// <summary>
	/// Creates a new instance of a persistence store for roles.
	/// </summary>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	public class RoleStoreInt<TContext> : RoleStoreInt<IdentityRoleInt, TContext>
		where TContext : DbContext
	{
		public RoleStoreInt(TContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}

	/// <summary>
	/// Creates a new instance of a persistence store for roles.
	/// </summary>
	/// <typeparam name="TRole">The type of the class representing a role.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	public class RoleStoreInt<TRole, TContext> : RoleStore<TRole, IdentityUserRoleInt, IdentityRoleClaimInt, TContext, int>
		where TRole : IdentityRoleInt, new()
		where TContext : DbContext
	{
		public RoleStoreInt(TContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}
}
