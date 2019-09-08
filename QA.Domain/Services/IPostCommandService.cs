using QA.Domain.Commands;
using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IPostCommandService
    {
        CommandResponse Execute(CommandBase command);
    }
}
