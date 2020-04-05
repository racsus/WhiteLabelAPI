using Core.Constants;
using Core.Interfaces.Base;
using Core.Interfaces.Data;
using Core.Interfaces.Security;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Infrastructure.DependencyBuilder
{
    public static class DependencyBuilder
    {
        public static void AddDependencies(IServiceCollection services, IConfiguration configuration)
        {
            //// DbContext
            services.AddSingleton<IDbSettings>(configuration.BindSettings<DbSettings>(Settings.WhiteLabelAPIContext));
            services.AddDbContext<WhiteLabelAPIContext>(options => options.UseSqlServer(configuration.BindSettings<DbSettings>(Settings.WhiteLabelAPIContext).ConnectionString), ServiceLifetime.Transient);
            services.AddTransient<IWhiteLabelAPIContext, WhiteLabelAPIContext>();

            // DI Services
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IReferenceService, ReferenceService>();

            // Reference Generator
            services.AddTransient<IReferenceGenerator, CryptographicReferenceGenerator>();

            // DI Repositories Security
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}