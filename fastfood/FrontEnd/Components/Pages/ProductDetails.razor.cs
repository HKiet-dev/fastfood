using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using FrontEnd.Services;


namespace FrontEnd.Components.Pages
{
    public partial class ProductDetails : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;

        [Inject]
        protected IFoodService _foodService { get; set; }
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected ITokenProvider tokenProvider { get; set; }
        [Inject]
        protected ICartService _cartService { get; set; }

        public CartDetail CartDetails { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        private bool IsLoggedIn { get; set; }

        protected override async Task OnInitializedAsync()
        {

            var productResponse = await _foodService.GetById(Id);

            if (productResponse != null && productResponse.IsSuccess)
            {
                Product = JsonConvert.DeserializeObject<Product>(productResponse.Result.ToString());
            }
            else
            {
                Product = null;
            }
            var relatedProductsResponse = await _foodService.GetAll(1, 8);
            if (relatedProductsResponse != null && relatedProductsResponse.IsSuccess)
            {

                var result = relatedProductsResponse.Result as dynamic;
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
            }
            else
            {
                Products = Enumerable.Empty<Product>();
            }


        }
        private async Task AddToCart()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            IsLoggedIn = authState.User.Identity.IsAuthenticated;
            if (!IsLoggedIn)
            {
                // Redirect to login page if not logged in
                NavigationManager.NavigateTo("/login");
                return;
            }

            if (Product.Id != null)
            {
                try
                {

                    // Retrieve user ID from token
                    var token = tokenProvider.GetToken();
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(token);
                    var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;

                    // Create CartDetail object
                    if (userId == null)
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "Không có user UserId.");
                        NavigationManager.NavigateTo("/login");
                    }
                    var cartDetail = new CartDetail
                    {
                        UserId = userId,
                        ProductId = Product.Id,
                        Quantity = Quantity
                    };

                    // Call the service to add to cart
                    var response = await _cartService.CreateCart(cartDetail);

                    if (response != null && response.IsSuccess)
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "Sản phẩm đã được thêm vào giỏ hàng.");
                        NavigationManager.NavigateTo("/cartcustomer");
                    }
                    else
                    {
                        // Show error message to user
                        await JSRuntime.InvokeVoidAsync("alert", "Có lỗi xảy ra khi thêm sản phẩm vào giỏ hàng.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    await JSRuntime.InvokeVoidAsync("alert", $"Lỗi: {ex.Message}");
                }
            }
        }

    }
}
