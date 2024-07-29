using FrontEnd.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FrontEnd.Helper
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        private ClaimsPrincipal _user = new ClaimsPrincipal();

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_user));
        }

        public void MarkUserAsAuthenticated(LoginResponseDto loginResponse)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginResponse.User.Name),
            new Claim(ClaimTypes.Email, loginResponse.User.Email),
            new Claim("Token", loginResponse.Token)
        };

            if (loginResponse.Role != null)
            {
                claims.AddRange(loginResponse.Role.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var identity = new ClaimsIdentity(claims, "apiauth_type");

            _user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _user = _anonymous;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
