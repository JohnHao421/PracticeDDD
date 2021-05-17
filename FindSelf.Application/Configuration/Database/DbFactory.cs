using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace FindSelf.Application.Configuration.Database
{
    public class DbFactory : IDbFactory
    {
        private readonly string dbConnectionStr;

        public IDbConnection Connection => new MySqlConnection(dbConnectionStr);

        public DbFactory(string dbConnectionStr)
        {
            this.dbConnectionStr = dbConnectionStr;
        }
    }
}
