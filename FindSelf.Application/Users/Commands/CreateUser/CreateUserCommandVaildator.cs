using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FindSelf.Application.Users.CreateUser
{
    public class CreateUserCommandVaildator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandVaildator()
        {
            RuleFor(x => x.UserNickname).NotEmpty().WithMessage("昵称不能为空");
            RuleFor(x => x.UserNickname).Must(x => !ContainsIllegalCharacter(x)).WithMessage("昵称中不能包含特殊字符");
        }

        private static bool ContainsIllegalCharacter(string username)
        {
            Regex checkUserName = new Regex("^[A-Za-z0-9]+$");//用户名:英文和数字
            var resultName = checkUserName.Match(username);
            return !resultName.Success;
        }
    }
}
