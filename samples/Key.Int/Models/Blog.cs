using MR.Patterns.Repository;

namespace Key.Int.Models
{
	public class Blog : IEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
