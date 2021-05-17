using Dapper;
using FindSelf.Application.Configuration.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Application.Queries.Users
{
    public class UserTranscationQueryService : IUserTranscationQueryService
    {
        private readonly IDbFactory db;

        public UserTranscationQueryService(IDbFactory db)
        {
            this.db = db;
        }

        public async Task<bool> IsExistTranscationAsync(Guid requestId)
        {
            using var conn = db.Connection;
            var count = await conn.ExecuteScalarAsync<int>("SELECT 1 FROM UserTranscation WHERE RequestId = @requestId LIMIT 1", new { requestId });
            return count != 0;
        }
    }
}
