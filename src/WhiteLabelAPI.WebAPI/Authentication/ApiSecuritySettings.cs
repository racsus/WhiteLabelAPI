using Core.Interfaces.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace WhiteLabelAPI.Authentication
{
    public class ApiSecuritySettings : AuthenticationSchemeOptions, IApiSecuritySettings
    {
        public string ApiId { get; set; }
        public string ApiKey { get; set; }
    }
}
