using FindSelf.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Domain.Repositories
{
    public interface IUserRepository : IRepository
    {
        User Add(User user);
        Task<User> GetAsync(Guid id);
    }
}
