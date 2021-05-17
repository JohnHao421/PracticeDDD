using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Entities.UserAggregate.Rules
{
    public class UserNotFindRule : IBusinessRule
    {
        private readonly User user;
        private readonly Guid uid;

        public UserNotFindRule(User user, Guid uid)
        {
            this.user = user;
            this.uid = uid;
        }

        public string Message => $"{uid},用户未找到";

        public bool IsBroken()
        {
            return user == null;
        }
    }
}
