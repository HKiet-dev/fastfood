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
using FrontEnd.Helper;

namespace FrontEnd.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; }

        [Inject]
        protected ITokenProvider TokenProvider { get; set; }
        [Inject]
        protected IFoodService _foodService { get; set; }
        [Inject]
        private CustomAuthenticationStateProvider Authentication { get; set; }

        public IEnumerable<Product> Products { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
                if (Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query).TryGetValue("data", out var base64Data))
                {
                    var jsonResponse = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
                    var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(jsonResponse);

                    // Sign in user with the token
                    await Authentication.LoginAsync(null, loginResponseDto.Token);
                    Navigation.NavigateTo("/");
                    return;
                }
            }

            var token = await Authentication.GetTokenAsync();
            if (token != string.Empty)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var role = jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value;
                if (role == "ADMIN")
                {
                    Navigation.NavigateTo("/usermanager");
                    return;
                }
            }

        }

        protected async Task LoadProducts()
        {
            var response = await _foodService.GetAll(1, 4);
            if (response != null && response.IsSuccess)
            {

                var result = response.Result as dynamic;
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
            }
        }
    }
}
