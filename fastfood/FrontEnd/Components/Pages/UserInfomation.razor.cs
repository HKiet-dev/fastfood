
using FrontEnd.Helper;
using FrontEnd.Models;

using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEnd.Components.Pages
{
    public partial class UserInfomation : ComponentBase
    {
        #region Inject
        [Inject]
        private ITokenProvider tokenProvider { get; set; }
        [Inject]
        private IAuthService _authService { get; set; }

		[Inject]
        protected CloudinaryServices CloudinaryService { get; set; }
		[Inject]
		CustomAuthenticationStateProvider Authentication { get; set; }

		[Inject]
		IUserService _userService { get; set; }
        #endregion
        #region Define
        [Parameter]
        public EventCallback<InputFileChangeEventArgs> OnChangeInputFile { get; set; }
        private IBrowserFile file;
		private bool IsEdit { get; set; } = false;
        private UserDto User { get; set; }
        public string userId { get; set; }
		private string token;
		private string oldPassword;
		private string newPassword;
		private string confirmPassword;
		private string notification;
		private string formId;
		private bool IsChangePasswordShow = false;
		#endregion

		private void Edit()
		{
			IsEdit = !IsEdit;
		}

		private async Task Save()
		{
			if (IsEdit)
			{
				if (IsChangePasswordShow)
				{
					if (newPassword != confirmPassword)
						notification = "Mật khẩu không trùng khớp";
				}
                await UploadAvatar();
                var response = await _userService.Update(User);
				if (response.IsSuccess)
					notification = "Đã cập nhật thành công";
			}
			IsEdit = false;
		}

        private async Task UploadAvatar()
        {
            throw new NotImplementedException();
        }

        private async Task ChangePassword()
		{
			IsChangePasswordShow = !IsChangePasswordShow;
		}


		protected override async Task OnInitializedAsync()
		{
			User ??= new();
            token = await Authentication.GetTokenAsync();

            if (token != null)
			{
				var handler = new JwtSecurityTokenHandler();
				var jwt = handler.ReadJwtToken(token);

				userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
			}
			if (userId != null)
			{
				var response = await _userService.GetById(userId);
				User = JsonConvert.DeserializeObject<UserDto>(response.Result.ToString());
			}
		}


		private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var token = tokenProvider.GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var userResponse = await _authService.GetUserById(userId);
            if (userResponse != null && userResponse.IsSuccess)
            {
                User = JsonConvert.DeserializeObject<UserDto>(userResponse.Result.ToString());
                using var fileStream = file.OpenReadStream(10 * 1024 * 1024);
                var fileName = file.Name;

                var cloudinaryUrl = await CloudinaryService.UploadImageAsync(fileStream, fileName);

                if (!string.IsNullOrEmpty(cloudinaryUrl))
                {
                    User.Avatar = cloudinaryUrl;
                }
            }
        }
    }
}