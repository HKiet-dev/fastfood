using Azure.Core;
using FrontEnd.Helper;
using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
namespace FrontEnd.Components.Pages
{
    public partial class UserInfomation : ComponentBase
    {

        #region Inject
        [Inject]
        NavigationManager Navigation { get; set; }
        [Inject]
        CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; }
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
        private void ShowChangePassword()
        {
            IsChangePasswordShow = !IsChangePasswordShow;
        }

        protected override async Task OnInitializedAsync()
        {
            User ??= new();
            token = await AuthenticationStateProvider.GetTokenAsync();
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

        public async Task ChangePassword()
        {
            ChangePassDto changePass = new ChangePassDto
            {
                id = userId,
                oldPassword = oldPassword,
                newPassword = newPassword
            };
            var response = await _userService.ChangePassword(changePass);
            if (response.IsSuccess)
                notification = "Đã đổi mật khẩu thành công";
            else
                notification = "Đổi mật khẩu thất bại";
        }
        private async Task TryLogout()
        {
            await AuthenticationStateProvider.LogoutAsync();
            Navigation.NavigateTo("/");
        }
    }
}