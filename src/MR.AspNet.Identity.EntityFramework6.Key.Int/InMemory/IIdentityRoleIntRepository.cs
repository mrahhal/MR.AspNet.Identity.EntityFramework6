namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public interface IIdentityRoleIntRepository : IIdentityRoleIntRepository<IdentityRoleInt>
	{
	}

	public interface IIdentityRoleIntRepository<TRole> : IIdentityRoleRepository<TRole, IdentityUserRoleInt, IdentityRoleClaimInt, int>
		where TRole : IdentityRoleInt, new()
	{
	}
}
