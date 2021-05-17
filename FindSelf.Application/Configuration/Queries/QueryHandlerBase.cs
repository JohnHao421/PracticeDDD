using FindSelf.Application.Configuration.Database;
using FindSelf.Application.Configuration.Queries;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace FindSelf.Application.Users.GetUser
{
    public abstract class QueryHandlerBase<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> , IDisposable
        where TQuery : IQuery<TResponse>
    {
        protected IDbConnection connection => db.Connection;

        protected readonly IDbFactory db;
        public QueryHandlerBase(IDbFactory db)
        {
            this.db = db;
        }

        public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}