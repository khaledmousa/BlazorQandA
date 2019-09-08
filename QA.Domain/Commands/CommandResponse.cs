using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Commands
{
    public class CommandResponse
    {
        public bool IsSuccessful { get; protected set; }
        public string Error { get; protected set; }

        public AuthoredEntity Entity { get; protected set; }

        public static CommandResponse Success() => new CommandResponse { IsSuccessful = true };

        public static CommandResponse Success(AuthoredEntity entity) => new CommandResponse
        {
            IsSuccessful = true,
            Entity = entity
        };
        public static CommandResponse Failure(string error = null) => new CommandResponse { IsSuccessful = false, Error = error };
    }   
}
