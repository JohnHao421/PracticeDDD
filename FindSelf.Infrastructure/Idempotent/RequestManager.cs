using FindSelf.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Infrastructure.Idempotent
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid id);
        Task<bool> CreateRequestForCommandAsync<T>(Guid id);
    }

    public class RequestManager : IRequestManager
    {
        private readonly FindSelfDbContext context;

        public RequestManager(FindSelfDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateRequestForCommandAsync<T>(Guid id)
        {
            var record = new IdempotentRequest(id, typeof(T).Name);
            context.IdempotentRequests.Add(record);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            var record = await context.IdempotentRequests.FirstOrDefaultAsync(x => x.Id == id);
            return record != null;
        }
    }
}
