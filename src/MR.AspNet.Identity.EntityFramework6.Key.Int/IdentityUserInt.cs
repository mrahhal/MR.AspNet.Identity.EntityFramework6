namespace MR.AspNet.Identity.EntityFramework6
{
	/// <summary>
	/// An implementation of <see cref="IdentityUser{TKey}"/> which uses an int as a primary key.
	/// </summary>
	public class IdentityUserInt : IdentityUser<int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt>
	{
		public IdentityUserInt()
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="IdentityUserInt"/>.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public IdentityUserInt(string userName)
		{
			UserName = userName;
		}
	}
}
