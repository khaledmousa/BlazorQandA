using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QA.Web.Client
{
    public interface IAuthenticationManager
    {
        Task LoginAsync(LoginToken token);
        Task LogoutAsync();
    }

    public class LoginToken
    {
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
