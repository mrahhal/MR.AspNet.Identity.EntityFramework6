namespace MR.AspNet.Identity.EntityFramework6
{
	/// <summary>
	/// An implementation of <see cref="IdentityRole{TKey}"/> which uses an int as the primary key.
	/// </summary>
	public class IdentityRoleInt : IdentityRole<int, IdentityUserRoleInt, IdentityRoleClaimInt>
	{
		public IdentityRoleInt()
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="IdentityRoleInt"/>.
		/// </summary>
		/// <param name="roleName">The role name.</param>
		public IdentityRoleInt(string roleName)
		{
			Name = roleName;
		}
	}
}
