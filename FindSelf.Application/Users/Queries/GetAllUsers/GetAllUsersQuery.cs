using Dapper;
using FindSelf.Application.Configuration.Queries;
using FindSelf.Application.Configuration.Queries.Paging;
using FindSelf.Application.Users.GetUser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FindSelf.Application.Users.Queries.GetUsers
{
    public class GetAllUsersQuery : PaggingQueryBase<UserDTO>
    {
        public GetAllUsersQuery(int pageSize, int index) : base(pageSize, index)
        {
        }
    }
}
