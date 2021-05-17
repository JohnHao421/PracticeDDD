using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Entities.UserAggregate.Rules
{
    public class UserIsufficientBalanceRule : IBusinessRule
    {
        private readonly decimal userBalance;
        private readonly decimal amount;

        public UserIsufficientBalanceRule(decimal userBalance, decimal amount)
        {
            this.userBalance = userBalance;
            this.amount = amount;
        }

        public string Message => "用户余额不足";

        public bool IsBroken()
        {
            return userBalance + amount < 0;
        }
    }
}
