using FindSelf.Application.Configuration.Commands;
using MediatR;
using System;

namespace FindSelf.Application.Users.Recharge
{
    public class RechargeBalanceCommand : ICommand<bool>
    {
        public RechargeBalanceCommand()
        {

        }

        public RechargeBalanceCommand(Guid uid, decimal amount, Guid RequestId)
        {
            Uid = uid;
            Amount = amount;
            this.RequestId = RequestId;
        }

        public Guid Uid { get; set; }
        public decimal Amount { get; set; }
        public Guid RequestId { get; set; }
    }
}