using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Commands
{
    public abstract class CommandBase
    {
        public User IssuedBy { get; private set; }
        public CommandBase(User issuedBy) => IssuedBy = issuedBy;
    }
}
