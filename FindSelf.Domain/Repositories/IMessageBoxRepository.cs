using FindSelf.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Domain.Repositories
{
    public interface IMessageBoxRepository : IRepository
    {
        Task<MessageBox> GetOrCreateAsync(Guid userId);
    }
}
