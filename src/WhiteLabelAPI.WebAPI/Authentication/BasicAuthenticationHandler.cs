using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WhiteLabelAPI.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<ApiSecuritySettings>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<ApiSecuritySettings> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }


        internal AuthenticateResult GetTicket()
        {
            string authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("No authorization header.");
            }

            if (!authorizationHeader.StartsWith(Scheme.Name + ' ', StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Scheme");
            }

            var encodedCredentials = authorizationHeader.Substring(Scheme.Name.Length).Trim();

            if (string.IsNullOrEmpty(encodedCredentials))
            {
                const string noCredentialsMessage = "No credentials";
                Logger.LogInformation(noCredentialsMessage);
                return AuthenticateResult.Fail(noCredentialsMessage);
            }

            try
            {
                string decodedCredentials;

                try
                {
                    decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to decode credentials : {encodedCredentials}", ex);
                }

                var delimiterIndex = decodedCredentials.IndexOf(':');
                if (delimiterIndex == -1)
                {
                    const string missingDelimiterMessage = "Invalid credentials, missing delimiter.";
                    Logger.LogInformation(missingDelimiterMessage);
                    return AuthenticateResult.Fail(missingDelimiterMessage);
                }

                var username = decodedCredentials.Substring(0, delimiterIndex);
                var password = decodedCredentials.Substring(delimiterIndex + 1);

                var validateCredentialsContext = new ValidateCredentialsContext(Options)
                {
                    Username = username,
                    Password = password
                };

                var ticket = validateCredentialsContext.ValidateCredentials(Scheme.Name);

                if (ticket != null)
                {
                    Logger.LogInformation($"Credentials validated for {username}");
                    return AuthenticateResult.Success(ticket);
                }

                Logger.LogInformation($"Credential validation failed for {username}");
                return AuthenticateResult.Fail("Invalid credentials.");
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Error Occurred");

                throw;
            }
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var t = GetTicket();

            return Task.FromResult(t);
        }
    }
}
