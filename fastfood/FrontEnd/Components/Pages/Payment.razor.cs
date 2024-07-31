using FrontEnd.Models;
using FrontEnd.Models.MOMO;
using FrontEnd.Services;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEnd.Components.Pages
{
    public partial class Payment : ComponentBase
    {
        [Inject]
        protected OrderService orderService { get; set; }

        [Inject]
        protected ICartService cartService { get; set; }

        [Inject]
        protected ITokenProvider tokenProvider { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }

        [Parameter]
        public string userId { get; set; }

        private IEnumerable<ListCartDetail> cartDetails { get; set; } = Enumerable.Empty<ListCartDetail>();

        protected Order model= new();

        protected override async Task OnInitializedAsync()
        {
            await GetListCart();
        }

        private async Task GetListCart()
        {
            var token = tokenProvider.GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var cartResponse = await cartService.getCart(userId);

            if (cartResponse != null && cartResponse.IsSuccess)
            {
                cartDetails = JsonConvert.DeserializeObject<IEnumerable<ListCartDetail>>(cartResponse.Result.ToString());
            }
        }

        private async Task PaymentMethod()
        {
            if (model.PaymentType == "COD")
            {
                await COD(model);
            }
            else if (model.PaymentType == "MOMO")
            {
                await Momo();
            }
        }

        protected async Task COD(Order order)
        {
            var response = await orderService.Payment(order);
            if (response.Result != null && response.IsSuccess)
            {
                var responseCartDelete = await cartService.DeleteAllById(userId);
                if (responseCartDelete.IsSuccess)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Đơn hàng đã được đặt.");
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alert", "Phát sinh lỗi trong quá trình đặt đơn COD.");
            }
        }

        private async Task Momo()
        {
            await GetListCart();
            var momoResponse = await orderService.MomoPayment(((int)Math.Round(cartDetails.Sum(x => x.Total))).ToString());
            if (momoResponse.IsSuccess)
            {
                var serializedModel = JsonConvert.SerializeObject(model);
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "paymentModel", serializedModel);
                Navigation.NavigateTo(momoResponse.Message);
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alert", "Phát sinh lỗi trong quá trình đặt đơn MOMO.");
            }
        }
    }
}
