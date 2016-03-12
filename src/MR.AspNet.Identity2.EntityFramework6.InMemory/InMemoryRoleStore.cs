using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MR.AspNet.Identity2.EntityFramework6.InMemory
{
	public class InMemoryRoleStore<TRole> : InMemoryRoleStore<TRole, string, IdentityUserRole>, IQueryableRoleStore<TRole>
		where TRole : IdentityRole, new()
	{
		public InMemoryRoleStore(IIdentityRoleRepository<TRole> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryRoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>
		where TRole : IdentityRole<TKey, TUserRole>, new()
		where TKey : IEquatable<TKey>
		where TUserRole : IdentityUserRole<TKey>, new()
	{
		private IIdentityRoleRepository<TRole, TUserRole, TKey> _repository;

		public IQueryable<TRole> Roles => _repository.Roles;

		public InMemoryRoleStore(IIdentityRoleRepository<TRole, TUserRole, TKey> repository)
		{
			_repository = repository;
		}

		public Task CreateAsync(TRole role)
		{
			_repository.Add(role);
			return Task.FromResult(0);
		}

		public Task UpdateAsync(TRole role) => Task.FromResult(0);

		public Task DeleteAsync(TRole role)
		{
			_repository.Remove(role);
			return Task.FromResult(0);
		}

		public Task<TRole> FindByIdAsync(TKey roleId)
			=> Task.FromResult(Roles.FirstOrDefault(r => r.Id.Equals(roleId)));

		public Task<TRole> FindByNameAsync(string roleName)
			=> Task.FromResult(Roles.FirstOrDefault(r => r.Name == roleName));

		public void Dispose()
		{
		}
	}
}
