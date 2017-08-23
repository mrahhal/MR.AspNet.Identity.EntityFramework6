using System.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace MR.AspNet.Identity.EntityFramework6
{
	/// <summary>
	/// Creates a new instance of a persistence store for users, using the default implementation
	/// of <see cref="IdentityUser{TKey}"/> with an int as a primary key.
	/// </summary>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	public class UserStoreInt<TContext> : UserStoreInt<IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>, TContext>
		where TContext : DbContext
	{
		public UserStoreInt(TContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}

	/// <summary>
	/// Creates a new instance of a persistence store for the specified user type.
	/// </summary>
	/// <typeparam name="TUser">The type representing a user.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	public class UserStoreInt<TUser, TContext> : UserStoreInt<TUser, IdentityRoleInt, TContext>
		where TUser : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>, new()
		where TContext : DbContext
	{
		public UserStoreInt(TContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}

	/// <summary>
	/// Creates a new instance of a persistence store for the specified user type.
	/// </summary>
	/// <typeparam name="TUser">The type representing a user.</typeparam>
	/// <typeparam name="TRole">The type representing a role.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	public class UserStoreInt<TUser, TRole, TContext> : UserStore<TUser, TRole, IdentityUserRoleInt, IdentityUserClaimInt, IdentityUserLoginInt, IdentityRoleClaimInt, TContext, int, IdentityUserTokenInt>
		where TUser : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>, new()
		where TRole : IdentityRole<int, IdentityUserRoleInt, IdentityRoleClaimInt>, new()
		where TContext : DbContext
	{
		public UserStoreInt(TContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}
}
