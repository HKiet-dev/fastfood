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

namespace FrontEnd.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected ICategoryService _categoryService { get; set; }
        [Inject]
        protected IFoodService _foodService { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected ITokenProvider TokenProvider { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var categoriesTask = _categoryService.GetAll();
            var productsTask = _foodService.GetAll(1, 8);

            await Task.WhenAll(categoriesTask, productsTask);

            var categoriesResponse = await categoriesTask;
            var productsResponse = await productsTask;

            if (categoriesResponse != null && categoriesResponse.IsSuccess)
            {
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoriesResponse.Result.ToString());
            }

            if (productsResponse != null && productsResponse.IsSuccess)
            {
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsResponse.Result.ToString());
            }

            var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
            if (Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query).TryGetValue("data", out var base64Data))
            {
                var jsonResponse = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
                var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(jsonResponse);

                // Sign in user with the token
                await SignInUser(loginResponseDto);
                TokenProvider.SetToken(loginResponseDto.Token);
                Navigation.NavigateTo("/",forceLoad : true);
            }


        }

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

            var httpContext = new HttpContextAccessor();

            if (httpContext != null)
            {
                httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
            }
        }
    }
}
