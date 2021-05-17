using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.Entities.UserAggregate.Rules;
using FindSelf.Domain.Repositories;
using FindSelf.Domain.SeedWork;
using FindSelf.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Infrastructure.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(FindSelfDbContext context) : base(context)
        {
        }

        public User Add(User user)
        {
            return context.Add(user).Entity;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await context.Users.FindAsync(id) ?? 
                throw new BusinessRuleValidationException(new UserNotFindRule(null, id));
        }
    }
}
