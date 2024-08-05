using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace FrontEnd.Components.Pages
{
    public partial class OrderDetails
    {
        [Parameter]
        public int OrderId { get; set; }
        [Inject]
        private OrderService OrderService { get; set; }
        private IEnumerable<OrderDetail> orderDetails = Enumerable.Empty<OrderDetail>();
        protected override async Task OnInitializedAsync()
        {
            await GetOrderDetail();
        }

        private async Task GetOrderDetail()
        {
            var response = await OrderService.GetOderDetail(OrderId);
            if (response.Result != null)
            {
                orderDetails = JsonConvert.DeserializeObject<IEnumerable<OrderDetail>>(response.Result.ToString());
            }
        }
    }
}
