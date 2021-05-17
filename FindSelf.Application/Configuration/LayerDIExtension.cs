using FindSelf.Application.Queries;
using FindSelf.Application.Queries.Users;
using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using FindSelf.Application.Configuration.Transcation;
using FindSelf.Application.Configuration.Vaildation;
using FindSelf.Application.Configuration.Database;
using FluentValidation;
using System.Reflection;
using FindSelf.Application.Configuration.Commands;
using System.Linq;
using FindSelf.Application.Commands;
using FindSelf.Application.Users.DomainServices;
using FindSelf.Application.Users.CreateUser;

namespace FindSelf.Application
{
    public static class LayerDIExtension
    {
        private static Assembly ApplicationAssembly = typeof(ApplicationConfiguration).Assembly;

        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, Action<ApplicationConfiguration> configure)
        {
            var config = new ApplicationConfiguration();
            configure(config);

            services.AddAutoMapper(typeof(LayerDIExtension).Assembly);

            services.AddTransient<IUserTranscationQueryService, UserTranscationQueryService>();
            services.AddSingleton<IDbFactory>(new DbFactory(config.DbConnectionString));

            services.AddTransient<ICheckUserNicknameUnique, CheckUserNicknameUnique>();

            AddMediatR(services);
            AddVaildators(services);

            return services;
        }

        private static void AddVaildators(IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandVaildator>();
        }

        private static void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(ApplicationAssembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        }
    }
}
