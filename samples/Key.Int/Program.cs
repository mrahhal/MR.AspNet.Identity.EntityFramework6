using System;
using System.Linq;
using Key.Int.Models;
using MR.AspNet.Identity.EntityFramework6;

namespace Key.Int
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var context = CreateContext())
			{
				if (!context.Users.Any())
				{
					for (int i = 0; i < 10; i++)
					{
						context.Users.Add(new AppUser
						{
							UserName = $"foo{i}",
							Email = $"foo{i}@e.c",
							Name = $"Foo Bar{i}"
						});
					}

					context.SaveChanges();
				}
			}

			using (var context = CreateContext())
			{
				if (!context.Roles.Any())
				{
					// You can instead create your custom AppRole that extends IdentityRoleInt.
					context.Roles.Add(new IdentityRoleInt("somerole"));
					context.SaveChanges();
				}
			}

			using (var context = CreateContext())
			{
				Console.WriteLine("Users:");
				var users = context.Users.ToList();
				foreach (var user in users)
				{
					Console.WriteLine($"Id: {user.Id}, Name: {user.Name}");
				}

				Console.WriteLine();
				Console.WriteLine("Roles:");
				var roles = context.Roles.ToList();
				foreach (var role in roles)
				{
					Console.WriteLine($"Id: {role.Id}, Name: {role.Name}");
				}
			}
		}

		private static AppDbContext CreateContext()
		{
			return new AppDbContext();
		}
	}
}
