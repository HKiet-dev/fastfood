using BackEnd.Models.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontEnd.Components.Pages
{
    public partial class Login
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private void GoogleLogin()
        {
            NavigationManager.NavigateTo("https://localhost:7039/api/Auth/signin-google");
        }
        private async Task GoogleLoginCallback(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                // Decode the base64 data
                var jsonResponse = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(data));
                var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(jsonResponse);

                if (loginResponseDto != null)
                {
                    await SignInUser(loginResponseDto); // Hàm SignInUser dùng để đăng nhập người dùng
                                                        // Đặt token cho ứng dụng (nếu cần)
                                                        //_tokenProvider.SetToken(loginResponseDto.Token);

                    // Sử dụng TempData hoặc tương tự để lưu trữ thông báo thành công

                    NavigationManager.NavigateTo("/"); // Điều hướng đến trang chủ hoặc trang khác tùy bạn
                }
            }
            NavigationManager.NavigateTo("/Login"); // Điều hướng đến trang đăng nhập nếu có lỗi
        }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
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

            var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync();
            var userc = authState.User;

            // Điều hướng đến trang chủ sau khi đăng nhập thành công
            NavigationManager.NavigateTo("/");
        }
    }
}
