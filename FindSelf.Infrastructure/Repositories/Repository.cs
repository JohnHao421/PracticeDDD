using FindSelf.Domain.Repositories;
using FindSelf.Domain.SeedWork;
using FindSelf.Infrastructure.Database;

namespace FindSelf.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        protected readonly FindSelfDbContext context;

        public IUnitOfWork UnitOfWork => context;

        public Repository(FindSelfDbContext context)
        {
            this.context = context;
        }
    }
}