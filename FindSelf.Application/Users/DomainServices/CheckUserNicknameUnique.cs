using FindSelf.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using FindSelf.Application.Configuration.Database;

namespace FindSelf.Application.Users.DomainServices
{
    public class CheckUserNicknameUnique : ICheckUserNicknameUnique
    {
        private readonly IDbFactory db;

        public CheckUserNicknameUnique(IDbFactory db)
        {
            this.db = db;
        }

        public bool IsUnique(string nickname)
        {
            using var conn = db.Connection;
            var count = conn.ExecuteScalar<int>("SELECT 1 FROM user WHERE NICKNAME = @Nickname LIMIT 1", new { nickname });
            return count != 1;
        }
    }
}
