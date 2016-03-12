using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MR.AspNet.Identity2.EntityFramework6.InMemory
{
	public class InMemoryUserStore<TUser> : InMemoryUserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
		where TUser : IdentityUser, new()
	{
		public InMemoryUserStore(IIdentityUserRepository<TUser> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> :
		IUserLoginStore<TUser, TKey>,
		IUserClaimStore<TUser, TKey>,
		IUserRoleStore<TUser, TKey>,
		IUserPasswordStore<TUser, TKey>,
		IUserSecurityStampStore<TUser, TKey>,
		IQueryableUserStore<TUser, TKey>,
		IUserEmailStore<TUser, TKey>,
		IUserPhoneNumberStore<TUser, TKey>,
		IUserTwoFactorStore<TUser, TKey>,
		IUserLockoutStore<TUser, TKey>
		where TKey : IEquatable<TKey>
		where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
		where TRole : IdentityRole<TKey, TUserRole>, new()
		where TUserLogin : IdentityUserLogin<TKey>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TUserClaim : IdentityUserClaim<TKey>, new()
	{
		private IIdentityUserRepository<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TKey> _repository;

		public InMemoryUserStore(IIdentityUserRepository<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TKey> repository)
		{
			_repository = repository;
		}

		public IQueryable<TUser> Users => _repository.Users;

		public Task AddClaimAsync(TUser user, Claim claim)
		{
			user.Claims.Add(new TUserClaim
			{
				ClaimType = claim.Type,
				ClaimValue = claim.Value,
				UserId = user.Id
			});
			return Task.FromResult(0);
		}

		public Task AddLoginAsync(TUser user, UserLoginInfo login)
		{
			user.Logins.Add(new TUserLogin
			{
				LoginProvider = login.LoginProvider,
				ProviderKey = login.ProviderKey,
				UserId = user.Id
			});
			return Task.FromResult(0);
		}

		public Task AddToRoleAsync(TUser user, string roleName)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			user.Roles.Add(new TUserRole
			{
				RoleId = role.Id,
				UserId = user.Id
			});
			return Task.FromResult(0);
		}

		public Task CreateAsync(TUser user)
		{
			_repository.Add(user);
			return Task.FromResult(0);
		}

		public Task DeleteAsync(TUser user)
		{
			_repository.Remove(user);
			return Task.FromResult(0);
		}

		public void Dispose()
		{
		}

		public Task<TUser> FindAsync(UserLoginInfo login)
		{
			foreach (var user in Users)
			{
				if (user.Logins.Any(u => u.LoginProvider == login.LoginProvider && u.ProviderKey == login.ProviderKey))
				{
					return Task.FromResult(user);
				}
			}
			return Task.FromResult(default(TUser));
		}

		public Task<TUser> FindByEmailAsync(string email)
			=> Task.FromResult(_repository.Users.Where(u => u.Email == email).FirstOrDefault());

		public Task<TUser> FindByIdAsync(TKey userId)
			=> Task.FromResult(_repository.Users.Where(u => u.Id.Equals(userId)).FirstOrDefault());

		public Task<TUser> FindByNameAsync(string userName)
			=> Task.FromResult(_repository.Users.Where(u => u.UserName == userName).FirstOrDefault());

		public Task<int> GetAccessFailedCountAsync(TUser user)
			=> Task.FromResult(user.AccessFailedCount);

		public Task<IList<Claim>> GetClaimsAsync(TUser user)
		{
			var list = user.Claims.Select(u => new Claim(u.ClaimType, u.ClaimValue)) as IList<Claim>;
			return Task.FromResult(list);
		}

		public Task<string> GetEmailAsync(TUser user)
			=> Task.FromResult(user.Email);

		public Task<bool> GetEmailConfirmedAsync(TUser user)
			=> Task.FromResult(user.EmailConfirmed);

		public Task<bool> GetLockoutEnabledAsync(TUser user)
			=> Task.FromResult(user.LockoutEnabled);

		public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
		{
			return
				Task.FromResult(user.LockoutEndDateUtc.HasValue
					? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
					: new DateTimeOffset());
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
		{
			var userId = user.Id;
			var list = user.Logins.Where(l => l.UserId.Equals(userId)).ToList();
			return Task.FromResult(
				list.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList() as IList<UserLoginInfo>);
		}

		public Task<string> GetPasswordHashAsync(TUser user)
			=> Task.FromResult(user.PasswordHash);

		public Task<string> GetPhoneNumberAsync(TUser user)
			=> Task.FromResult(user.PhoneNumber);

		public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
			=> Task.FromResult(user.PhoneNumberConfirmed);

		public Task<IList<string>> GetRolesAsync(TUser user)
		{
			var roles = user.Roles.Select(r => r.RoleId);
			return Task.FromResult(_repository.Roles.Where(r => roles.Contains(r.Id)).Select(r => r.Name).ToList() as IList<string>);
		}

		public Task<string> GetSecurityStampAsync(TUser user)
			=> Task.FromResult(user.SecurityStamp);

		public Task<bool> GetTwoFactorEnabledAsync(TUser user)
			=> Task.FromResult(user.TwoFactorEnabled);

		public Task<bool> HasPasswordAsync(TUser user)
			=> Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));

		public Task<int> IncrementAccessFailedCountAsync(TUser user)
		{
			user.AccessFailedCount++;
			return Task.FromResult(user.AccessFailedCount);
		}

		public Task<bool> IsInRoleAsync(TUser user, string roleName)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			return Task.FromResult(user.Roles.Any(ur => ur.RoleId.Equals(role.Id) && ur.UserId.Equals(user.Id)));
		}

		public Task RemoveClaimAsync(TUser user, Claim claim)
		{
			var c2 = user.Claims.FirstOrDefault(uc => uc.ClaimType == claim.Type && uc.ClaimValue == claim.Value);
			if (c2 != null)
			{
				user.Claims.Remove(c2);
			}
			return Task.FromResult(0);
		}

		public Task RemoveFromRoleAsync(TUser user, string roleName)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			var userRole = user.Roles.FirstOrDefault(ur => ur.RoleId.Equals(role.Id));
			if (userRole != null) user.Roles.Remove(userRole);
			return Task.FromResult(0);
		}

		public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
		{
			var l2 = user.Logins.FirstOrDefault(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey);
			if (l2 != null)
			{
				user.Logins.Remove(l2);
			}
			return Task.FromResult(0);
		}

		public Task ResetAccessFailedCountAsync(TUser user)
		{
			user.AccessFailedCount = 0;
			return Task.FromResult(0);
		}

		public Task SetEmailAsync(TUser user, string email)
		{
			user.Email = email;
			return Task.FromResult(0);
		}

		public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
		{
			user.EmailConfirmed = confirmed;
			return Task.FromResult(0);
		}

		public Task SetLockoutEnabledAsync(TUser user, bool enabled)
		{
			user.LockoutEnabled = enabled;
			return Task.FromResult(0);
		}

		public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
		{
			user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
			return Task.FromResult(0);
		}

		public Task SetPasswordHashAsync(TUser user, string passwordHash)
		{
			user.PasswordHash = passwordHash;
			return Task.FromResult(0);
		}

		public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
		{
			user.PhoneNumber = phoneNumber;
			return Task.FromResult(0);
		}

		public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
		{
			user.PhoneNumberConfirmed = confirmed;
			return Task.FromResult(0);
		}

		public Task SetSecurityStampAsync(TUser user, string stamp)
		{
			user.SecurityStamp = stamp;
			return Task.FromResult(0);
		}

		public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
		{
			user.TwoFactorEnabled = enabled;
			return Task.FromResult(0);
		}

		public Task UpdateAsync(TUser user)
			=> Task.FromResult(IdentityResult.Success);
	}
}
