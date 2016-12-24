namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public class InMemoryUserStoreInt : InMemoryUserStoreInt<IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>>
	{
		public InMemoryUserStoreInt(IIdentityUserIntRepository repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStoreInt<TUser> : InMemoryUserStoreInt<TUser, IdentityRoleInt>
	   where TUser : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>, new()
	{
		public InMemoryUserStoreInt(IIdentityUserIntRepository<TUser> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStoreInt<TUser, TRole> : InMemoryUserStore<TUser, TRole, IdentityUserRoleInt, IdentityUserClaimInt, IdentityUserLoginInt, IdentityRoleClaimInt, int>
		where TUser : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>, new()
		where TRole : IdentityRole<int, IdentityUserRoleInt, IdentityRoleClaimInt>, new()
	{
		public InMemoryUserStoreInt(IIdentityUserIntRepository<TUser, TRole> repository)
			: base(repository)
		{
		}
	}
}
