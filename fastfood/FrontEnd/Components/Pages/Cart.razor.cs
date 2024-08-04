using FrontEnd.Helper;
using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
namespace FrontEnd.Components.Pages
{
    public partial class Cart : ComponentBase
    {
        [Inject]
        protected CustomAuthenticationStateProvider Authentication { get; set; }
        private bool IsLoggedIn { get; set; }
        [Parameter]
        public string userId { get; set; }
        [Inject]
        protected ICartService cartService { get; set; }
        [Inject]
        protected IFoodService _foodService { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        public CartDetail CartDetail { get; set; }
        public IEnumerable<ListCartDetail> listCartUser { get; set; } = Enumerable.Empty<ListCartDetail>();

        protected override async Task OnInitializedAsync()
        {
            /*var authState = await Authentication.GetAuthenticationStateAsync();
            IsLoggedIn = authState.User.Identity.IsAuthenticated;
            if (!IsLoggedIn)
            {
                Navigation.NavigateTo("/login");
            }*/
            /*var token = tokenProvider.GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;*/
            var token = await Authentication.GetTokenAsync();


            if (token != string.Empty)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            }
            else
            {
                Navigation.NavigateTo("/login");
                return;
            }
            
            var cartResponse = await cartService.getCart(userId);

            if (cartResponse != null && cartResponse.IsSuccess)
            {
                listCartUser = JsonConvert.DeserializeObject<IEnumerable<ListCartDetail>>(cartResponse.Result.ToString());
            }
        }

        /*protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var authState = await Authentication.GetAuthenticationStateAsync();
            IsLoggedIn = authState.User.Identity.IsAuthenticated;
            if (!IsLoggedIn)
            {
                Navigation.NavigateTo("/login");
            }

            var token = await Authentication.GetTokenAsync();

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            }

            var cartResponse = await cartService.getCart(userId);

            if (cartResponse != null && cartResponse.IsSuccess)
            {
                listCartUser = JsonConvert.DeserializeObject<IEnumerable<ListCartDetail>>(cartResponse.Result.ToString());
            }
            *//*return base.OnAfterRenderAsync(firstRender);*//*
        }*/
        private async Task DeleteCartItem(int productId)
        {
            var token = await Authentication.GetTokenAsync();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var cartResponse = await cartService.DeleteCart(productId, userId);
            if (cartResponse != null && cartResponse.IsSuccess)
            {
                listCartUser = listCartUser.Where(item => item.Food.Id != productId).ToList();
            }
        }

        private async Task PaymentAction()
        {
            var token = await Authentication.GetTokenAsync();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            Navigation.NavigateTo($"/payment/{userId}");
        }
    }
}