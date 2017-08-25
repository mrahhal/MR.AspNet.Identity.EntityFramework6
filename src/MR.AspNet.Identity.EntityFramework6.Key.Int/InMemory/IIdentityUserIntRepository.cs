namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public interface IIdentityUserIntRepository : IIdentityUserIntRepository<IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt, IdentityUserTokenInt>>
	{
	}

	public interface IIdentityUserIntRepository<TUser> : IIdentityUserIntRepository<TUser, IdentityRoleInt>
		where TUser : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt, IdentityUserTokenInt>, new()
	{
	}

	public interface IIdentityUserIntRepository<TUser, TRole> : IIdentityUserRepository<TUser, TRole, IdentityUserRoleInt, IdentityUserClaimInt, IdentityUserLoginInt, IdentityRoleClaimInt, IdentityUserTokenInt, int>
		where TUser : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt, IdentityUserTokenInt>, new()
		where TRole : IdentityRole<int, IdentityUserRoleInt, IdentityRoleClaimInt>, new()
	{
	}
}
