using MediatR;

namespace FindSelf.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest<UserDTO>
    {
        public string UserNickname { get; set; }
    }
}