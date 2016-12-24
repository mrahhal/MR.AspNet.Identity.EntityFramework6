namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public class InMemoryRoleStoreInt : InMemoryRoleStoreInt<IdentityRoleInt>
	{
		public InMemoryRoleStoreInt(IIdentityRoleIntRepository repository)
			: base(repository)
		{
		}
	}

	public class InMemoryRoleStoreInt<TRole> : InMemoryRoleStore<TRole, IdentityUserRoleInt, IdentityRoleClaimInt, int>
		where TRole : IdentityRoleInt, new()
	{
		public InMemoryRoleStoreInt(IIdentityRoleIntRepository<TRole> repository)
			: base(repository)
		{
		}
	}
}
