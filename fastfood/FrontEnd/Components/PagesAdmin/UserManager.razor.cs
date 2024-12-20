﻿using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using FrontEnd.Services.IService;
using Newtonsoft.Json;
using Azure;
using Microsoft.AspNetCore.Components.Forms;
using FrontEnd.Helper;

namespace FrontEnd.Components.PagesAdmin
{
    public partial class UserManager : ComponentBase
    {
        [Parameter]
        public EventCallback<InputFileChangeEventArgs> OnChangeInputFile { get; set; }
        private IBrowserFile file;
        [SupplyParameterFromForm]
        private UserDto CreateUser { get; set; }
        [SupplyParameterFromForm]
        private UserDto EditUser { get; set; }
        [Inject]
        protected IUserService _userservice { get; set; }
        [Inject]
        protected CloudinaryServices CloudinaryService { get; set; }
        public List<UserDto>? Users { get; set; }
        public List<UserDto>? SearchUsers { get; set; }
        public string notification = "";
        private PagePagination page { get; set; }
        private string search = null;


        protected override async Task OnInitializedAsync()
        {
            CreateUser ??= new();
            EditUser ??= new();
            page ??= new();
            CloudinaryService ??= new();
            await LoadUsers();

        }

        private async Task LoadUsers()
        {
            var usersResponse = await _userservice.GetAll(page.CurrentPage, 10);
            if (usersResponse != null && usersResponse.IsSuccess)
            {
                var result = usersResponse.Result as dynamic;
                Users = JsonConvert.DeserializeObject<List<UserDto>>(result.users.ToString());
                foreach (var item in Users)
                {
                    if (item.Avatar == "string" || item.Avatar == null)
                    {
                        item.Avatar = "/Img/default_avatar.png";
                    }
                }
                page.TotalCount = result.totalCount;
            }
            else
            {
                Users = null;
            }
        }

        private async Task Navigate(int currentPage)
        {
            page.CurrentPage = currentPage;
            if (search == null)
            {
                await LoadUsers();
            }
            else
            {
                await GetBySearch();
            }
        }




        public async Task Create()
        {
            try
            {
                await UploadAvatar(CreateUser);
                var response = await _userservice.Create(CreateUser);
                if (response.IsSuccess)
                    notification = "Thêm người dùng thành công";
                else
                    notification = "Thêm người dùng thất bại";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task Edit()
        {
            try
            {
                await UploadAvatar(EditUser);
                var response = await _userservice.Update(EditUser);
                if (response != null && response.IsSuccess)
                {
                    notification = "Sửa người dùng thành công";
                }
                else
                {
                    notification = "Sửa người dùng thất bại";
                }
            }
            catch (Exception ex)
            {
                notification = $"Error: {ex.Message}";
                Console.WriteLine(ex.Message);
            }
        }

        private async Task Delete()
        {
            try
            {
                var response = await _userservice.Delete(EditUser.Id);
                if (response != null && response.IsSuccess)
                {
                    notification = "Xóa người dùng thành công";
                }
                else
                {
                    notification = "Xóa người dùng thất bại";
                }
            }
            catch (Exception ex)
            {
                notification = $"Error: {ex.Message}";
                Console.WriteLine(ex.Message);
            }
        }

        private async Task GetUser(string id)
        {
            EditUser = Users.FirstOrDefault(u => u.Id == id);
        }

        private async Task GetBySearch()
        {
            if (search != "")
            {
                var searchNameResponse = await _userservice.GetBySearch(search, page.CurrentPage, 10);
                var result = searchNameResponse.Result as dynamic;
                if (result.totalCount != 0)
                {
                    if (searchNameResponse != null && searchNameResponse.IsSuccess)
                    {
                        Users = JsonConvert.DeserializeObject<List<UserDto>>(result.users.ToString());
                        foreach (var item in Users)
                        {
                            if (item.Avatar == "string" || item.Avatar == null)
                            {
                                item.Avatar = "/Img/default_avatar.png";
                            }
                        }
                        page.TotalCount = result.totalCount;
                    }
                    else
                    {
                        Users = null;
                    }
                }
                else
                {
                    var searchIdResponse = await _userservice.GetById(search);
                    if (searchIdResponse.Result != null && searchIdResponse.IsSuccess)
                    {
                        UserDto user = JsonConvert.DeserializeObject<UserDto>(searchIdResponse.Result.ToString());
                        if (user.Avatar == "string" || user.Avatar == null)
                        {
                            user.Avatar = "/Img/default_avatar.png";
                        }
                        Users.Clear();
                        Users.Add(user);
                    }
                    else
                    {
                        Users = null;
                    }
                }
            }
            else
            {
                await LoadUsers();
            }
        }
        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            file = e.File;
        }

        private async Task UploadAvatar(UserDto user)
        {
            if (file != null)
            {
                using var fileStream = file.OpenReadStream();
                var fileName = file.Name;

                var cloudinaryUrl = await CloudinaryService.UploadImageAsync(fileStream, fileName);

                if (!string.IsNullOrEmpty(cloudinaryUrl))
                {
                    user.Avatar = cloudinaryUrl;
                }
            }
        }
    }
}