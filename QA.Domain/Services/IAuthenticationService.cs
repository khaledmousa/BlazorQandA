using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IAuthenticationService
    {
        User Login(string email, string password);        
    }
}
