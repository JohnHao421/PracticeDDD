using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
