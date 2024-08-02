using Azure.Core;
using FrontEnd.Helper;
using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;

namespace FrontEnd.Components.Pages
{
	public partial class UserInfomation : ComponentBase
	{
		[CascadingParameter]
		Task<AuthenticationStateProvider> AuthenticationStateProvider { get; set; }
        #region Inject
        [Inject]
        protected CloudinaryServices CloudinaryService { get; set; }
        [Inject]
		ITokenProvider TokenProvider { get; set; }
		[Inject]
		IUserService _userService { get; set; }
        #endregion
        #region Define
        [Parameter]
        public EventCallback<InputFileChangeEventArgs> OnChangeInputFile { get; set; }
        private IBrowserFile file;
        private UserDto User { get; set; }
		private bool IsEdit { get; set; } = false;
		private string userId;
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
        private async Task ChangePassword()
		{
			IsChangePasswordShow = !IsChangePasswordShow;
		}

		protected override async Task OnInitializedAsync()
		{
			User ??= new();
			token = TokenProvider.GetToken();

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
        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            file = e.File;
			IsEdit = true;	
        }

        private async Task UploadAvatar()
        {
            if (file != null)
            {
                using var fileStream = file.OpenReadStream();
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