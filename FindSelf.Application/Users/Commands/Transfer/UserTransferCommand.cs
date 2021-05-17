using FindSelf.Application.Configuration.Commands;
using FindSelf.Domain.Entities.UserAggregate;
using MediatR;
using System;

namespace FindSelf.Application.Users.Transfer
{
    public class UserTransferCommand : ICommand<bool>
    {
        public UserTransferCommand()
        {

        }

        public UserTransferCommand(Guid payerId, Guid receiverId, decimal amount, Guid requestId)
        {
            PayerId = payerId;
            ReceiverId = receiverId;
            Amount = amount;
            RequestId = requestId;
        }

        public Guid PayerId { get; set; }
        public Guid ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public Guid RequestId { get; set; }
    }

}