using FindSelf.Application.Configuration.Commands;
using FindSelf.Application.Configuration.Vaildation;
using FindSelf.Infrastructure.Idempotent;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.ApplicationServices.Commands
{
    public abstract class IdentifiedCommandHandler<T, R> : ICommandHandler<IdentifiedCommand<T, R>, R>
        where T : ICommand<R>
    {
        protected readonly IRequestManager manager;
        protected readonly IMediator mediator;
        protected readonly ILogger<IdentifiedCommandHandler<T, R>> logger;

        public IdentifiedCommandHandler(IRequestManager manager, IMediator mediator, ILogger<IdentifiedCommandHandler<T, R>> logger)
        {
            this.manager = manager;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<R> Handle(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            if (await manager.ExistAsync(request.Id) || !await manager.CreateRequestForCommandAsync<T>(request.Id))
            {
                return await CreateResultForDuplicateRequestAsync(request);
            }

            logger.LogDebug("---Crated Identitified Request Id : {Id}, CommandName: {CommandType}", request.Id, typeof(T));
            return await mediator.Send(request.Command);
        }

        protected virtual Task<R> CreateResultForDuplicateRequestAsync(IdentifiedCommand<T, R> request)
        {
            throw new InvalidCommandException("重复请求", "请求已经收到，正在处理中，请稍后再试.");
        }
    }
}
