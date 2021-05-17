using FindSelf.Domain.Repositories;
using FindSelf.Infrastructure.Database;
using FindSelf.Infrastructure.DomainEventBus;
using FindSelf.Infrastructure.Idempotent;
using FindSelf.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Infrastructure
{
    public static class LayerDIExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, Action<InfrastructureConfiguration> configure)
        {
            var config = new InfrastructureConfiguration();
            configure(config);

            var efLoggerFactory = LoggerFactory.Create(builder => builder.AddDebug());
            services.AddDbContext<FindSelfDbContext>(options => options.UseMySql(config.DbConnectionString)
                .UseLoggerFactory(efLoggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMessageBoxRepository, MessageBoxRepository>();

            services.AddScoped<IRequestManager, RequestManager>();

            AddDomainEventBus(services);
            return services;
        }

        private static void AddDomainEventBus(IServiceCollection services)
        {
            services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();
        }
    }
}
