using FindSelf.Application;
using FindSelf.Infrastructure;
using FindSelf.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using FindSelf.Domain.SeedWork;
using FindSelf.API.SeedWork;
using FindSelf.Application.Configuration.Vaildation;
using FluentValidation.AspNetCore;

namespace FindSelf.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();                

            services.AddProblemDetails(configure =>
            {
                configure.Map<InvalidCommandException>(x => new InvalidCommandExceptionProblemDetail(x));
                configure.Map<BusinessRuleValidationException>(x => new BusinessRuleValidationExceptionProblemDetails(x));
            });

            services.AddInfrastructureLayer(opt => opt.DbConnectionString = Configuration.GetConnectionString("MySQL"));
            services.AddApplicationLayer(opt => opt.DbConnectionString = Configuration.GetConnectionString("MySQL"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
