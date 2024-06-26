using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZemingoCMS.Application.Abstractions.Commands
{
    public interface ICommand<TResult> where TResult : class
    {
    }
}
