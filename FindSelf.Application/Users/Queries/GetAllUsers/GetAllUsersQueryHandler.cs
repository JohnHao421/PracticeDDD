using FindSelf.Application.Configuration.Database;
using FindSelf.Application.Configuration.Queries.Paging;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Users.Queries.GetUsers
{
    public class GetAllUsersQueryHandler : PaggingQueryHandlerBase<GetAllUsersQuery, UserDTO>
    {
        public GetAllUsersQueryHandler(IDbFactory db) : base(db)
        {
        }

        public override Task<PaggingView<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM user";
            return ExcuteQueryPaggingViewAsync(sql, request);
        }
    }
}
