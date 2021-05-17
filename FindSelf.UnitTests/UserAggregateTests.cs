using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.Entities.UserAggregate.Rules;
using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FindSelf.UnitTests
{
    public class UserAggregateTests
    {
        [Fact]
        public void UserRegister_VaildateNicknameIsUnique_SuccessRegeister()
        {
            const string Nickname = "jack";

            var user = User.Create();
            user.ChangeNickname(Nickname, new MockCheckUserNicknameUniqueService(true));

            Assert.Equal(user.Nickname, Nickname);
        }

        [Fact]
        public void UserRegister_VaildateNicknameIsUnique_Repetion()
        {
            const string Nickname = "jack";

            var user = User.Create();
            var exception = Assert.Throws<BusinessRuleValidationException>(
                () => user.ChangeNickname(Nickname, new MockCheckUserNicknameUniqueService(false))
            );
            Assert.IsType<UserNicknameUniqueRule>(exception.BrokenRule);
        }
    }

    internal class MockCheckUserNicknameUniqueService : ICheckUserNicknameUnique
    {
        private bool _expection;

        public MockCheckUserNicknameUniqueService(bool expection)
        {
            this._expection = expection;
        }

        public bool IsUnique(string nickname)
        {
            return _expection;
        }
    }
}
