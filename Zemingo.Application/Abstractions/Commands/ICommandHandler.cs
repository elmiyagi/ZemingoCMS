using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZemingoCMS.Application.Models;

namespace ZemingoCMS.Application.Abstractions.Commands
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult> where TResult : CommandResult
    {
        Task<TResult> Handle(TCommand command);
    }
}
