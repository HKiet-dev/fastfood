
using FrontEnd.Models;

using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;

using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEnd.Components.Pages
{
    public partial class UserInfomation : ComponentBase
    {
        [Inject]
        private ITokenProvider tokenProvider { get; set; }
        [Inject]
        private IAuthService _authService { get; set; }
        private UserDto User { get; set; }
        public string userId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var token = tokenProvider.GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var userResponse = await _authService.GetUserById(userId);
            if (userResponse != null && userResponse.IsSuccess)
            {
                User = JsonConvert.DeserializeObject<UserDto>(userResponse.Result.ToString());
            }
        }
    }
}