using AutoMapper;
using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository repository;
        private readonly ICheckUserNicknameUnique checkUserNicknameService;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUserRepository repository, ICheckUserNicknameUnique checkUserNicknameService, IMapper mapper)
        {
            this.repository = repository;
            this.checkUserNicknameService = checkUserNicknameService;
            this.mapper = mapper;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create();
            user.ChangeNickname(request.UserNickname, checkUserNicknameService);
            user = repository.Add(user);

            await repository.UnitOfWork.CommitAsync();
            return mapper.Map<UserDTO>(user);
        }
    }
}