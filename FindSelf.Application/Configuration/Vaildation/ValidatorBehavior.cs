using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Application.Configuration.Vaildation
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var errors = validators.Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                var messageBuilder = new StringBuilder("参数错误:");
                foreach(var error in errors)
                {
                    messageBuilder.AppendLine(error.ErrorMessage);
                }
                throw new InvalidCommandException("客户端参数验证错误", messageBuilder.ToString());
            }
            return next();
        }
    }
}
