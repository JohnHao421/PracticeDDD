using FindSelf.Application.Configuration.Vaildation;
using FindSelf.Domain.SeedWork;
using FindSelf.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Configuration.Transcation
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly FindSelfDbContext context;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> logger;

        public TransactionBehaviour(FindSelfDbContext context, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType();

            if (context.HasTranscation)
            {
                return await next();
            }

            try
            {
                var strategy = context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using var transaction = await context.BeginTransactionAsync();
                    logger.LogDebug("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);
                    response = await next();
                    logger.LogDebug("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
                    await context.CommitTranscationAsync(transaction);
                });

                return response;
            }
            catch (InvalidCommandException)
            {
                throw;
            }
            catch (BusinessRuleValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
}
