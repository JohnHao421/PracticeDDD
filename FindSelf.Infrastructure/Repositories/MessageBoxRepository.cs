using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.Repositories;
using FindSelf.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Infrastructure.Repositories
{
    public class MessageBoxRepository : Repository, IMessageBoxRepository
    {
        public MessageBoxRepository(FindSelfDbContext context) : base(context)
        {
        }

        public async Task<MessageBox> GetOrCreateAsync(Guid userId)
        {
            var box = await context.MessageBoxes.FirstOrDefaultAsync(x => x.Uid == userId);
            if (box == null)
            {
                box = MessageBox.Create(userId);
                context.MessageBoxes.Add(box);
            }
            return box;
        }
    }
}
