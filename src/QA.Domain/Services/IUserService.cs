using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetById(Guid userId);
        User GetByEmail(string email);
        User CreateUser(User user);        
        bool DeleteUser(Guid userId);
    }
}
