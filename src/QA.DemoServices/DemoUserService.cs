using Bogus;
using QA.Domain.Entities;
using QA.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace QA.DemoServices
{
    public class DemoUserService : IUserService
    {
        private List<User> Users { get; set; }

        public DemoUserService()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "QA.DemoServices.Users.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                Users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(reader.ReadToEnd());
            }
        }       

        public User CreateUser(User user)
        {
            if (user.Id == Guid.Empty && !Users.Any(u => u.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                user.Id = Guid.NewGuid();
                user.Password = HashPassword(user.Password);
                Users.Add(user);
                return user;
            }
            else return null;
        }

        public bool DeleteUser(Guid userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                Users.Remove(user);
                return true;
            }
            else return false;
        }

        public User GetByEmail(string email) => Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        

        public User GetById(Guid userId) => Users.FirstOrDefault(u => u.Id == userId);

        public IEnumerable<User> GetUsers() => Users;        

        private string HashPassword(string password)
        {
            //based on https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129

            var salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);
            var hash = new Rfc2898DeriveBytes(password, salt, 1000).GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }      
    }
}
