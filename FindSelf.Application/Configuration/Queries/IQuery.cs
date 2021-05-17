using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Configuration.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
