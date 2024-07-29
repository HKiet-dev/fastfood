using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using FrontEnd.Services.IService;
using Newtonsoft.Json;

namespace FrontEnd.Components.PagesAdmin
{
    public partial class UserManager : ComponentBase
    {
        private bool isEditUserModalVisible = false;
        [SupplyParameterFromForm]
        private UserDto CreateUser { get; set; }
        [SupplyParameterFromForm]
        private UserDto EditUser { get; set; }
        [Inject]
        protected IUserService _userservice { get; set; }
        public List<UserDto>? Users { get; set; }
        [Inject]
        protected IWebHostEnvironment _env { get; set; }
        public string notification = "";
        //[SupplyParameterFromForm]
        //public PageNavigation page { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CreateUser ??= new();
            EditUser ??= new();
            //page ??= new();
            string defaultAvatar = Path.Combine(_env.WebRootPath, "Img", "default_avatar.png");

            var usersResponse = await _userservice.GetAll();
            if (usersResponse != null && usersResponse.IsSuccess)
            {
                Users = JsonConvert.DeserializeObject<List<UserDto>>(usersResponse.Result.ToString());
                foreach (var item in Users)
                {
                    if (item.Avatar == "string" || item.Avatar == null)
                    {
                        item.Avatar = "/Img/default_avatar.png";
                    }
                }
            }
            else
            {
                Users = null;
            }

        }

        //private async Task GetUser()
        //{
        //    var usersResponse = await _userservice.GetAll();
        //    if (usersResponse != null && usersResponse.IsSuccess)
        //    {
        //        Users = JsonConvert.DeserializeObject<List<UserDto>>(usersResponse.Result.ToString());
        //        foreach (var item in Users)
        //        {
        //            if (item.Avatar == "string" || item.Avatar == null)
        //            {
        //                item.Avatar = "/Img/default_avatar.png";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Users = null;
        //    }
        //}
        //private void NavigateToPage(int pageNumber)
        //{
        //    page.CurrentPage = pageNumber;
        //    GetUser();
        //}

        public async Task Create()
        {
            try
            {
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
    }
}