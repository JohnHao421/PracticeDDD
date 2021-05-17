using FindSelf.Application.ApplicationServices.Commands;
using FindSelf.Application.Queries.Users;
using FindSelf.Application.Users.Recharge;
using FindSelf.Infrastructure.Idempotent;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Application.Users.Commands.Recharge
{
    public class RechargeCommandDuplicationHandler : IdentifiedCommandHandler<RechargeBalanceCommand, bool>
    {
        private readonly IUserTranscationQueryService transcationQueryService;

        public RechargeCommandDuplicationHandler(IUserTranscationQueryService transcationQueryService , IRequestManager manager, IMediator mediator, ILogger<IdentifiedCommandHandler<RechargeBalanceCommand, bool>> logger) : base(manager, mediator, logger)
        {
            this.transcationQueryService = transcationQueryService;
        }

        protected override async Task<bool> CreateResultForDuplicateRequestAsync(IdentifiedCommand<RechargeBalanceCommand, bool> request)
        {
            var isDone = await transcationQueryService.IsExistTranscationAsync(request.Id);
            if (isDone) return true;

            return await base.CreateResultForDuplicateRequestAsync(request);
        }
    }
}
