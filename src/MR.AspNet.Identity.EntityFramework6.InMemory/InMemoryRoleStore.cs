using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public class InMemoryRoleStore : InMemoryRoleStore<IdentityRole>
	{
		public InMemoryRoleStore(IIdentityRoleRepository repository)
			: base(repository)
		{
		}
	}

	public class InMemoryRoleStore<TRole> : InMemoryRoleStore<TRole, IdentityUserRole, IdentityRoleClaim, string>
		where TRole : IdentityRole, new()
	{
		public InMemoryRoleStore(IIdentityRoleRepository<TRole> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryRoleStore<TRole, TKey> : InMemoryRoleStore<TRole, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>, TKey>
		where TRole : IdentityRole<TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>, new()
		where TKey : IEquatable<TKey>
	{
		public InMemoryRoleStore(IIdentityRoleRepository<TRole, TKey> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryRoleStore<TRole, TUserRole, TRoleClaim, TKey> :
		IQueryableRoleStore<TRole>,
		IRoleClaimStore<TRole>
		where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TRoleClaim : IdentityRoleClaim<TKey>, new()
		where TKey : IEquatable<TKey>
	{
		private IIdentityRoleRepository<TRole, TUserRole, TRoleClaim, TKey> _repository;

		public IQueryable<TRole> Roles => _repository.Roles;

		public InMemoryRoleStore(IIdentityRoleRepository<TRole, TUserRole, TRoleClaim, TKey> repository)
		{
			_repository = repository;
		}

		public Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
		{
			_repository.Add(role);
			return Task.FromResult(IdentityResult.Success);
		}

		public Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
		{
			_repository.Remove(role);
			return Task.FromResult(IdentityResult.Success);
		}

		public Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
			=> Task.FromResult(_repository.Roles.Where(r => r.Id.Equals(roleId)).FirstOrDefault());

		public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
			=> Task.FromResult(_repository.Roles.Where(r => r.NormalizedName == normalizedRoleName).FirstOrDefault());

		public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
			=> Task.FromResult(role.NormalizedName);

		public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
			=> Task.FromResult(ConvertIdToString(role.Id));

		public virtual TKey ConvertIdFromString(string id)
		{
			if (id == null)
			{
				return default(TKey);
			}
			return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
		}

		public virtual string ConvertIdToString(TKey id)
		{
			if (id.Equals(default(TKey)))
			{
				return null;
			}
			return id.ToString();
		}

		public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
			=> Task.FromResult(role.Name);

		public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
		{
			role.NormalizedName = normalizedName;
			return Task.FromResult(0);
		}

		public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
		{
			role.Name = roleName;
			return Task.FromResult(0);
		}

		public Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
			=> Task.FromResult(IdentityResult.Success);

		public Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
		{
			var list = role.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToArray() as IList<Claim>;
			return Task.FromResult(list);
		}

		public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
		{
			role.Claims.Add(new TRoleClaim
			{
				ClaimType = claim.Type,
				ClaimValue = claim.Value,
				RoleId = role.Id
			});
			return Task.FromResult(0);
		}

		public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
		{
			var c = role.Claims.FirstOrDefault(rc => rc.ClaimType == claim.Type && rc.ClaimValue == claim.Value);
			if (c != null)
			{
				role.Claims.Remove(c);
			}
			return Task.FromResult(0);
		}

		public void Dispose()
		{
		}
	}
}
