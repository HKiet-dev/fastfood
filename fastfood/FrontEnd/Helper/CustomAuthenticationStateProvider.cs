using BackEnd.Models;
using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontEnd.Helper
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly IAuthService _authService;

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage, IAuthService authService)
        {
            _protectedLocalStorage = localStorage;
            _authService = authService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();

            try
            {
                var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");

                if (storedPrincipal.Success)
                {

                    if (storedPrincipal.Value != null)
                    {
                        var identity = await CreateIdentityFromUser(storedPrincipal.Value);
                        principal = new(identity);
                    }
                    /*var user = JsonConvert.DeserializeObject<UserDto>(storedPrincipal.Value);

                    var userDto = await LookUpUser(user.Id);

                    if (userDto != null)
                    {
                        var identity = await CreateIdentityFromUser(storedPrincipal.Value);
                        principal = new(identity);
                    }*/
                }
            }
            catch
            {

            }

            return new AuthenticationState(principal);
        }

        public async Task<string> GetTokenAsync()
        {
            var token = await _protectedLocalStorage.GetAsync<string>("identity");

            if (token.Success)
            {
                return token.Value;
            }

            return "";
        }

        public async Task<string> LoginAsync(LoginRequestDto loginFormModel, string token)
        {
            /*var (userInDatabase, isSuccess) = LookUpUser(loginFormModel.Email, loginFormModel.Password);*/
            if (loginFormModel != null)
            {
                var user = await _authService.LoginAsync(loginFormModel);

                if (user == null || user.IsSuccess == false)
                {
                    return "Sai tài khoản hoặc mật khẩu";
                }

                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(user.Result.ToString());

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(loginResponse.Token);

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
                var identity = new ClaimsIdentity(claims, "Jwt");
                var principal = new ClaimsPrincipal(identity);
                /*var principal = new ClaimsPrincipal();*/

                if (user.IsSuccess)
                {
                    /*var identity = await CreateIdentityFromUser(loginResponse.User);
                    principal = new ClaimsPrincipal(identity);*/



                    await _protectedLocalStorage.SetAsync("identity", loginResponse.Token/*JsonConvert.SerializeObject(loginResponse.User)*/);
                }

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));

                return "Đăng nhập thành công";
            }
            else
            {
                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(token);

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
                    var identity = new ClaimsIdentity(claims, "Jwt");
                    var principal = new ClaimsPrincipal(identity);
                    /*var principal = new ClaimsPrincipal();*/
                    
                    await _protectedLocalStorage.SetAsync("identity", token);
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
                    return "Đăng nhập thành công";
                }

                return "Tài khoản Google không hợp lệ";
            }
        }

        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("identity");
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        private async Task<ClaimsIdentity> CreateIdentityFromUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

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
            var identity = new ClaimsIdentity(claims, "Jwt");

            foreach (var claim in identity.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            return identity;
            /*var result = new ClaimsIdentity(new Claim[]
            {
                new ("Id",user.Id),
                new (ClaimTypes.Name, user.Name),
                new (ClaimTypes.Email,user.Email),
            }, "BlazorSchool")
            {

            };
            var rolesResponse = await _authService.GetUserRole(user);
            var roles = JsonConvert.DeserializeObject<IList<string>>(rolesResponse.Result.ToString());

            foreach (string role in roles)
            {
                result.AddClaim(new(ClaimTypes.Role, role));
            }

            return result;*/
        }

        /*private (UserDto, bool) LookUpUser(string username, string password)
        {
            var result = _dataProviderService.Users.FirstOrDefault(u => username == u.Username && password == u.Password);

            return (result, result is not null);
        }*/

        private async Task<UserDto> LookUpUser(string userId)
        {
            var userIdResponse = await _authService.GetUserById(userId);
            var result = JsonConvert.DeserializeObject<UserDto>(userIdResponse.Result.ToString());

            return result;
        }

        
    }
}
