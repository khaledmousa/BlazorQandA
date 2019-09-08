using QA.Domain.Entities;
using QA.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.DemoServices
{
    public class DemoAuthenticationService : IAuthenticationService
    {
        public User Login(string username, string password)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = $"{username}@domain.com"
            };
        }

        public User Register(string email, string password, string username)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email
            };
        }
    }
}
