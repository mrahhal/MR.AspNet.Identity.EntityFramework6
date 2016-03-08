using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MR.AspNet.Identity.EntityFramework6
{
	public static class IdentityEntityFrameworkBuilderExtensions
	{
		/// <summary>
		/// Adds an Entity Framework implementation of identity information stores.
		/// </summary>
		/// <typeparam name="TContext">The Entity Framework database context to use.</typeparam>
		/// <param name="builder">The <see cref="IdentityBuilder"/> instance this method extends.</param>
		/// <returns>The <see cref="IdentityBuilder"/> instance this method extends.</returns>
		public static IdentityBuilder AddEntityFrameworkStores<TContext>(this IdentityBuilder builder)
			where TContext : DbContext
		{
			builder.Services.TryAdd(GetDefaultServices(builder.UserType, builder.RoleType, typeof(TContext)));
			return builder;
		}

		private static IServiceCollection GetDefaultServices(Type userType, Type roleType, Type contextType, Type keyType = null)
		{
			Type userStoreType;
			Type roleStoreType;
			if (keyType != null)
			{
				userStoreType = typeof(UserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
				roleStoreType = typeof(RoleStore<,,>).MakeGenericType(roleType, contextType, keyType);
			}
			else
			{
				userStoreType = typeof(UserStore<,,>).MakeGenericType(userType, roleType, contextType);
				roleStoreType = typeof(RoleStore<,>).MakeGenericType(roleType, contextType);
			}

			var services = new ServiceCollection();
			services.AddScoped(
				typeof(IUserStore<>).MakeGenericType(userType),
				userStoreType);
			services.AddScoped(
				typeof(IRoleStore<>).MakeGenericType(roleType),
				roleStoreType);
			return services;
		}
	}
}
