using MR.Patterns.Repository;

namespace Basic
{
	public class Blog : IEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
