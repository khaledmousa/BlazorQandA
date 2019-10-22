using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QA.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }      
        public string Password { get; set; }
    }
}
