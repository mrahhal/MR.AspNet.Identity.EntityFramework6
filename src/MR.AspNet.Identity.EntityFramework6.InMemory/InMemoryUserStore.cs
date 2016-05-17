using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MR.AspNet.Identity.EntityFramework6.InMemory
{
	public class InMemoryUserStore : InMemoryUserStore<IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>>
	{
		public InMemoryUserStore(IIdentityUserRepository repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStore<TUser> : InMemoryUserStore<TUser, IdentityRole>
	   where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
	{
		public InMemoryUserStore(IIdentityUserRepository<TUser> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStore<TUser, TRole> : InMemoryUserStore<TUser, TRole, IdentityUserRole, IdentityUserClaim, IdentityUserLogin, IdentityRoleClaim, string>
		where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
		where TRole : IdentityRole<string, IdentityUserRole, IdentityRoleClaim>, new()
	{
		public InMemoryUserStore(IIdentityUserRepository<TUser, TRole> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStore<TUser, TRole, TKey> : InMemoryUserStore<TUser, TRole, IdentityUserRole<TKey>, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, TKey>
		where TUser : IdentityUser<TKey, IdentityUserLogin<TKey>, IdentityUserRole<TKey>, IdentityUserClaim<TKey>>, new()
		where TRole : IdentityRole<TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>, new()
		where TKey : IEquatable<TKey>
	{
		public InMemoryUserStore(IIdentityRepository<TUser, TRole, TKey> repository)
			: base(repository)
		{
		}
	}

	public class InMemoryUserStore<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TKey> :
		IUserLoginStore<TUser>,
		IUserRoleStore<TUser>,
		IUserClaimStore<TUser>,
		IUserPasswordStore<TUser>,
		IUserSecurityStampStore<TUser>,
		IUserEmailStore<TUser>,
		IUserLockoutStore<TUser>,
		IUserPhoneNumberStore<TUser>,
		IQueryableUserStore<TUser>,
		IUserTwoFactorStore<TUser>
		where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
		where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>, new()
		where TUserRole : IdentityUserRole<TKey>, new()
		where TUserClaim : IdentityUserClaim<TKey>, new()
		where TUserLogin : IdentityUserLogin<TKey>, new()
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TKey : IEquatable<TKey>
	{
		private IIdentityUserRepository<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TKey> _repository;

		public InMemoryUserStore(IIdentityUserRepository<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TKey> repository)
		{
			_repository = repository;
		}

		public IQueryable<TUser> Users => _repository.Users;

		public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			foreach (var claim in claims)
			{
				user.Claims.Add(new TUserClaim
				{
					ClaimType = claim.Type,
					ClaimValue = claim.Value,
					UserId = user.Id
				});
			}
			return Task.FromResult(0);
		}

		public Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			user.Roles.Add(new TUserRole
			{
				RoleId = role.Id,
				UserId = user.Id
			});
			return Task.FromResult(0);
		}

		public Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
		{
			_repository.Add(user);
			return Task.FromResult(IdentityResult.Success);
		}

		public Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
		{
			_repository.Remove(user);
			return Task.FromResult(IdentityResult.Success);
		}

		public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
			=> Task.FromResult(_repository.Users.Where(u => u.NormalizedEmail == normalizedEmail).FirstOrDefault());

		public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
			=> Task.FromResult(_repository.Users.Where(u => u.Id.Equals(userId)).FirstOrDefault());

		public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
			=> Task.FromResult(_repository.Users.Where(u => u.NormalizedUserName == normalizedUserName).FirstOrDefault());

		public Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.AccessFailedCount);

		public Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
		{
			var list = user.Claims.Select(u => new Claim(u.ClaimType, u.ClaimValue)).ToList() as IList<Claim>;
			return Task.FromResult(list);
		}

		public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.Email);

		public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.EmailConfirmed);

		public Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.LockoutEnabled);

		public Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.LockoutEnd);

		public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.NormalizedEmail);

		public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.NormalizedUserName);

		public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.PasswordHash);

		public Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.PhoneNumber);

		public Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.PhoneNumberConfirmed);

		public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
		{
			var roles = user.Roles.Select(r => r.RoleId);
			return Task.FromResult(_repository.Roles.Where(r => roles.Contains(r.Id)).Select(r => r.Name).ToList() as IList<string>);
		}

		public Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.SecurityStamp);

		public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(ConvertIdToString(user.Id));

		public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.UserName);

		public Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
		{
			var list = _repository.Users.Where(u => u.Claims.Any(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value)) as IList<TUser>;
			return Task.FromResult(list);
		}

		public Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			var list = new List<TUser>();
			foreach (var user in _repository.Users.ToList())
			{
				if (user.Roles.Any(ur => ur.RoleId.Equals(role.Id))) list.Add(user);
			}
			return Task.FromResult(list as IList<TUser>);
		}

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

		public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));

		public Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
		{
			user.AccessFailedCount++;
			return Task.FromResult(user.AccessFailedCount);
		}

		public Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			return Task.FromResult(user.Roles.Any(ur => ur.RoleId.Equals(role.Id) && ur.UserId.Equals(user.Id)));
		}

		public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			foreach (var c in claims)
			{
				var claim = user.Claims.FirstOrDefault(uc => uc.ClaimType == c.Type && uc.ClaimValue == c.Value);
				if (claim != null)
				{
					user.Claims.Remove(claim);
				}
			}
			return Task.FromResult(0);
		}

		public Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
		{
			var role = _repository.Roles.Where(r => r.Name == roleName).First();
			var userRole = user.Roles.FirstOrDefault(ur => ur.RoleId.Equals(role.Id));
			if (userRole != null) user.Roles.Remove(userRole);
			return Task.FromResult(0);
		}

		public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
		{
			foreach (var c in user.Claims)
			{
				if (c.ClaimType == claim.Type && c.ClaimValue == claim.Value)
				{
					c.ClaimType = newClaim.Type;
					c.ClaimValue = newClaim.Value;
					break;
				}
			}
			return Task.FromResult(0);
		}

		public Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
		{
			user.AccessFailedCount = 0;
			return Task.FromResult(0);
		}

		public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
		{
			user.Email = email;
			return Task.FromResult(0);
		}

		public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
		{
			user.EmailConfirmed = confirmed;
			return Task.FromResult(0);
		}

		public Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
		{
			user.LockoutEnabled = enabled;
			return Task.FromResult(0);
		}

		public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
		{
			user.LockoutEnd = lockoutEnd;
			return Task.FromResult(0);
		}

		public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
		{
			user.NormalizedEmail = normalizedEmail;
			return Task.FromResult(0);
		}

		public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
		{
			user.NormalizedUserName = normalizedName;
			return Task.FromResult(0);
		}

		public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
		{
			user.PasswordHash = passwordHash;
			return Task.FromResult(0);
		}

		public Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
		{
			user.PhoneNumber = phoneNumber;
			return Task.FromResult(0);
		}

		public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
		{
			user.PhoneNumberConfirmed = confirmed;
			return Task.FromResult(0);
		}

		public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
		{
			user.SecurityStamp = stamp;
			return Task.FromResult(0);
		}

		public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
		{
			user.UserName = userName;
			return Task.FromResult(0);
		}

		public Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(IdentityResult.Success);

		public void Dispose()
		{
		}

		public Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
		{
			var l = new TUserLogin
			{
				UserId = user.Id,
				ProviderKey = login.ProviderKey,
				LoginProvider = login.LoginProvider,
				ProviderDisplayName = login.ProviderDisplayName
			};
			user.Logins.Add(l);
			return Task.FromResult(0);
		}

		public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			var login = user.Logins.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
			if (login != null)
			{
				user.Logins.Remove(login);
			}
			return Task.FromResult(0);
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
		{
			var userId = user.Id;
			var list = user.Logins.Where(l => l.UserId.Equals(userId)).ToList();
			return Task.FromResult(
				list.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList() as IList<UserLoginInfo>);
		}

		public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			var loginRepository = _repository as IIdentityLoginRepository<IdentityUserLogin>;
			if (loginRepository == null)
			{
				throw new NotSupportedException();
			}
			var logins = loginRepository.UserLogins;
			var userLogin = logins.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
			if (userLogin != null)
			{
				return Task.FromResult(Users.FirstOrDefault(u => u.Id.Equals(userLogin.UserId)));
			}
			return Task.FromResult(default(TUser));
		}

		public Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
		{
			user.TwoFactorEnabled = enabled;
			return Task.FromResult(0);
		}

		public Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.TwoFactorEnabled);
	}
}
