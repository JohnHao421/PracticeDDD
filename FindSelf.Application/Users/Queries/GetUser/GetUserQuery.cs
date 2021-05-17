using FindSelf.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Users.GetUser
{
    public class GetUserQuery : IQuery<UserDTO>
    {
        public Guid Uid { get; set; }
        public GetUserQuery(Guid uid)
        {
            Uid = uid;
        }
    }
}
