using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace FrontEnd.Components.Pages
{
    public partial class PurchaseHistory
    {
        [Inject]
        private OrderService _orderService { get; set; }
        public IEnumerable<Order> orders { get; set; } = Enumerable.Empty<Order>();

        protected override async Task OnInitializedAsync()
        {
            await GetOrderByUser();
        }
        private async Task GetOrderByUser()
        {
            var response = await _orderService.OrderByUserId();
            if (response.Result != null)
            {
                orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result.ToString());
            }
        }
    }
}
