namespace Core.Interfaces.Authentication
{
    public interface IApiSecuritySettings
    {
        string ApiId { get; set; }
        string ApiKey { get; set; }
    }
}
