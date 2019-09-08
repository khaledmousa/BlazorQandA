using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IAuthenticationService
    {
        User Login(string username, string password);
        User Register(string email, string password, string username);
    }
}
