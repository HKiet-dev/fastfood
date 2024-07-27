using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using FrontEnd.Services.IService;
using Newtonsoft.Json;

namespace FrontEnd.Components.PagesAdmin
{
	public partial class UserManager : ComponentBase
	{
		[Inject]
		protected IUserService _userservice { get; set; }
		public List<User>? Users { get; set; }
        [Inject]
        protected IWebHostEnvironment _env { get; set; }
        public string notification = "";
        protected override async Task OnInitializedAsync()
		{
            string defaultAvatar = Path.Combine(_env.WebRootPath, "Img", "default_avatar.png");
            var usersResponse = await _userservice.GetAll();
			if (usersResponse != null && usersResponse.IsSuccess)
			{
				Users = JsonConvert.DeserializeObject<List<User>>(usersResponse.Result.ToString());
				foreach(var item in Users)
				{
					if (item.Avatar == "string")
					{
						item.Avatar = "/Img/default_avatar.png";
                    }
				}
            }
			else
			{
				//Users = Enumerable.Empty<User>();
				Users = null;
			}

		}

		

        private async Task Update()
        {
            // Your logic to update the user
        }
    }
}