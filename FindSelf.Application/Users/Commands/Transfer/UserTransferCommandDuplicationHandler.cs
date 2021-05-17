using FindSelf.Application.ApplicationServices.Commands;
using FindSelf.Application.Queries.Users;
using FindSelf.Application.Users.Transfer;
using FindSelf.Infrastructure.Idempotent;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindSelf.Application.Commands.Users.Transfer
{
    public class UserTransferCommandDuplicationHandler : IdentifiedCommandHandler<UserTransferCommand, bool>
    {
        private readonly IUserTranscationQueryService transcationQueryService;

        public UserTransferCommandDuplicationHandler(IUserTranscationQueryService transcationQueryService,IRequestManager manager, IMediator mediator, ILogger<IdentifiedCommandHandler<UserTransferCommand, bool>> logger) : base(manager, mediator, logger)
        {
            this.transcationQueryService = transcationQueryService;
        }

        protected override async Task<bool> CreateResultForDuplicateRequestAsync(IdentifiedCommand<UserTransferCommand, bool> request)
        {
            var isDone = await transcationQueryService.IsExistTranscationAsync(request.Id);
            if (isDone) return true;

            return await base.CreateResultForDuplicateRequestAsync(request); 
        }
    }
}
