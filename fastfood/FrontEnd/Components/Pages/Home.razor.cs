using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontEnd.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected ITokenProvider TokenProvider { get; set; }
        

        private async Task SignInUser(LoginResponseDto model)
        {
            // Xây dựng identity từ JWT token
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var claims = new List<Claim>();

            // Thêm các claim từ JWT token vào danh sách claims
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value));
            claims.Add(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
            claims.Add(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value));

            // Tạo identity từ danh sách claims
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var user = new ClaimsPrincipal(identity);

            var httpContext = new HttpContextAccessor();

            if (httpContext != null)
            {
                httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
            }
        }
    }
}
