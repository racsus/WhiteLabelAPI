using System;
using System.Collections.Generic;
using AutoMapper;
using WhiteLabelAPI.Authentication;
using WhiteLabelAPI.Extensions;
using Infrastructure.DependencyBuilder;
using WhiteLabelAPI.WebAPI.AutoMapper;
using WhiteLabelAPI.WebAPI.Midlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WhiteLabelAPI
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"connectionstrings{(env.IsDevelopment() ? "" : "")}.json", false, true)
                .AddJsonFile("apisecuritysettings.json", false, true)

                //Services

                .AddEnvironmentVariables();

            if (env.IsEnvironment("Development"))
            {
                //Development environment settings
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Authentication
            services.AddAuthenticationService(Configuration.BindSettings<ApiSecuritySettings>(Core.Constants.Settings.ApiSecuritySettingsSectionName));

            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // DI
            DependencyBuilder.AddDependencies(services, Configuration);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WhiteLabel API",
                    Description = "WhiteLabel ASP.NET Core Web API",
                    TermsOfService = new Uri("https://www.yourwebsite.com/termsandservice")
                });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.DocumentFilter<SecurityRequirementsDocumentFilter>();
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            #region New AutoMapper Configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ObjectProfile());
                mc.AddProfile(new ModelProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyHeader()
                                          .AllowAnyMethod()
                                          .AllowAnyOrigin());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture API V1");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }

        public IConfiguration Configuration { get; }
    }
}
