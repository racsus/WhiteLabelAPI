using WhiteLabelAPI.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WhiteLabelAPI.Extensions
{
    public static class AuthenticationExtensions
    {
        private static AuthenticationBuilder AddCustomAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<ApiSecuritySettings> options)
        {
            return builder.AddScheme<ApiSecuritySettings, BasicAuthenticationHandler>(authenticationScheme, displayName, options);
        }

        public static AuthenticationBuilder AddAuthenticationService(this IServiceCollection services, ApiSecuritySettings options, string defaultScheme = "Basic")
        {
            void Credentials(ApiSecuritySettings o)
            {
                o.ApiId = options.ApiId;
                o.ApiKey = options.ApiKey;
            }

            return services.AddAuthentication(o => { o.DefaultScheme = defaultScheme; }).AddCustomAuthentication(defaultScheme, defaultScheme, Credentials);
        }
    }
}
