using FindSelf.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Users.Recharge
{
    public class RechargeBalanceCommandHandler : IRequestHandler<RechargeBalanceCommand, bool>
    {
        private readonly IUserRepository repository;

        public RechargeBalanceCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(RechargeBalanceCommand command, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(command.Uid);
            user.Rechrage(command.Amount, command.RequestId);

            return await repository.UnitOfWork.CommitAsync() > 0;
        }
    }
}