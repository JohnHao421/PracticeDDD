using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Configuration.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
