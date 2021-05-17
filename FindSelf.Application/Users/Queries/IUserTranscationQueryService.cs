using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Application.Queries.Users
{
    public interface IUserTranscationQueryService 
    {
        Task<bool> IsExistTranscationAsync(Guid requestId);
    }
}
