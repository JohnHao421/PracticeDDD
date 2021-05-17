using Dapper;
using FindSelf.Application.Configuration.Database;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Users.GetUser
{
    public class GetUserQueryHandler : QueryHandlerBase<GetUserQuery, UserDTO>
    {
        public GetUserQueryHandler(IDbFactory db) : base(db)
        {
        }

        public override async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await connection.QueryFirstOrDefaultAsync<UserDTO>("SELECT * FROM user WHERE ID = @uid LIMIT 1", new { request.Uid });
            user ??= new UserDTO { Id = request.Uid };
            return user;
        }
    }
}
