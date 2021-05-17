using System.Data;

namespace FindSelf.Application.Configuration.Database
{
    public interface IDbFactory
    {
        IDbConnection Connection { get; }
    }
}