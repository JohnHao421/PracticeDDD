using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Entities.UserAggregate.Rules
{
    public class UserNicknameUniqueRule : IBusinessRule
    {
        private readonly string nickname;
        private readonly ICheckUserNicknameUnique checkService;

        public UserNicknameUniqueRule(string nickname, ICheckUserNicknameUnique checkService)
        {
            this.nickname = nickname;
            this.checkService = checkService;
        }

        public string Message => "昵称已经被占用";

        public bool IsBroken()
        {
            return !checkService.IsUnique(nickname);
        }
    }
}
