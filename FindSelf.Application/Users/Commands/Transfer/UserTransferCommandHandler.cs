using FindSelf.Application.Configuration.Commands;
using FindSelf.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Users.Transfer
{
    public class UserTransferCommandHandler : ICommandHandler<UserTransferCommand, bool>
    {
        private readonly IUserRepository repository;

        public UserTransferCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(UserTransferCommand command, CancellationToken cancellationToken)
        {
            var payer = await repository.GetAsync(command.PayerId);
            var receiver = await repository.GetAsync(command.ReceiverId);
            payer.TransferTo(receiver, command.Amount, command.RequestId);

            await repository.UnitOfWork.CommitAsync();
            return true;
        }
    }

}