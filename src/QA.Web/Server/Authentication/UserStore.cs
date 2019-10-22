using Microsoft.AspNetCore.Identity;
using QA.Domain.Entities;
using QA.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QA.Web.Server.Authentication
{
    public class UserStore : IUserStore<User>
    {
        private readonly IUserService _userService;

        public UserStore(IUserService userService) => _userService = userService;

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var newUser = _userService.CreateUser(user);
            if (newUser != null) return Task.FromResult(IdentityResult.Success);
            else return Task.FromResult(IdentityResult.Failed());
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return _userService.DeleteUser(user.Id) ? Task.FromResult(IdentityResult.Success) : Task.FromResult(IdentityResult.Failed());
        }

        public void Dispose() { }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = Guid.TryParse(userId, out var guid) ? _userService.GetById(guid) : null;
            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) => Task.FromResult(_userService.GetByEmail(normalizedUserName));        

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);        

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) => Task.FromResult(IdentityResult.Success);        
    }
}
