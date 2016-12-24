using MR.AspNet.Identity.EntityFramework6;

namespace Key.Int.Models
{
	public class AppUser : IdentityUserInt
	{
		public string Name { get; set; }
	}
}
