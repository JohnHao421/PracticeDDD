using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Configuration.Commands
{
    public interface ICommandHandler<in TCommand> : 
        IRequestHandler<TCommand> where TCommand : ICommand
    {

    }

    public interface ICommandHandler<in TCommand, TResponse> : 
        IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
    }
}
