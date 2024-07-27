namespace FrontEnd.Services.IService
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
        string GetIdentityToken();
    }
}
