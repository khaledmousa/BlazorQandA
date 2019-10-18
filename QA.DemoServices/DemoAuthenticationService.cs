using QA.Domain.Entities;
using QA.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QA.DemoServices
{
    public class DemoAuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        public DemoAuthenticationService(IUserService userService) => _userService = userService;

        public User Login(string email, string password)
        {
            var userByEmail = _userService.GetByEmail(email);
            if (userByEmail != null && ValidatePassowrd(password, userByEmail.Password))
                return userByEmail;
            return null;
        }

        private bool ValidatePassowrd(string password, string savedHash)
        {
            //based on https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129

            var hashBytes = Convert.FromBase64String(savedHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //Compute the hash on the password the user entered             
            var hash = new Rfc2898DeriveBytes(password, salt, 1000).GetBytes(20);

            return hash.SequenceEqual(hashBytes.Skip(16));
        }
    }
}
