using Dapper;
using FindSelf.Application.Configuration.Database;
using FindSelf.Application.Users.GetUser;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Application.Configuration.Queries.Paging
{
    public abstract class PaggingQueryHandlerBase<TPagginQuery, TResponse> : QueryHandlerBase<TPagginQuery, PaggingView<TResponse>>
        where TPagginQuery : PaggingQueryBase<TResponse>
    {
        protected PaggingQueryHandlerBase(IDbFactory db) : base(db)
        {
        }

        protected async Task<PaggingView<TResponse>> ExcuteQueryPaggingViewAsync(string sql, PaggingQueryBase<TResponse> request, object param = null)
        {
            if (sql[sql.Length - 1] != ';') sql += ';';
            var countSql = CreateCountSQL(sql);
            var querySql = CreateQuerySQL(sql, request.Index, request.PageSize);

            var result = await connection.QueryMultipleAsync(countSql + querySql, param);
            var total = result.ReadFirst<int>();
            var models = result.Read<TResponse>();

            return new PaggingView<TResponse>(models, total, request.PageSize, request.Index);
        }

        private string CreateQuerySQL(string sql, int index, int pageSize)
        {
            var skip = (index - 1) * pageSize;
            var take = pageSize;

            var builder = new StringBuilder();
            builder.Append(sql.AsSpan(0, sql.Length - 1));
            builder.Append($" LIMIT {skip},{take};");
            return builder.ToString();
        }

        private static string CreateCountSQL(string sql)
        {
            const string countSatement = "SELECT COUNT(*) ";
            CompareInfo Compare = CultureInfo.InvariantCulture.CompareInfo;
            var Index = Compare.IndexOf(sql, "FROM", CompareOptions.IgnoreCase);
            var countSql = countSatement + sql.AsSpan().Slice(Index).ToString();
            return countSql;
        }
    }
}