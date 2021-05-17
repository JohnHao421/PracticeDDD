using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Infrastructure.Idempotent
{
    public class IdempotentRequest 
    {
        public Guid Id { get; }
        public string CommandType { get; }

        public IdempotentRequest(Guid id, string commandType)
        {
            Id = id;
            CommandType = commandType ?? throw new ArgumentNullException(nameof(commandType));
        }
    }
}
