using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Entities.UserAggregate.Rules
{
    public class UserBalanceNonnegativeRule : IBusinessRule
    {
        private readonly decimal amount;

        public UserBalanceNonnegativeRule(decimal amount)
        {
            this.amount = amount;
        }

        public string Message => "金额不能为负";

        public bool IsBroken()
        {
            return amount < 0;
        }
    }
}
