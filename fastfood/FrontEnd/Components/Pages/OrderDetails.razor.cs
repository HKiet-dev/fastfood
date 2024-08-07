using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        private Order order { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetOrderDetail();
            await GetOrderByOrderID();
        }

        private async Task GetOrderDetail()
        {
            var response = await OrderService.GetOrderDetail(OrderId);
            if (response.Result != null)
            {
                orderDetails = JsonConvert.DeserializeObject<IEnumerable<OrderDetail>>(response.Result.ToString());
            }
        }

        private async Task GetOrderByOrderID()
        {
            var response = await OrderService.GetOrderByID(OrderId);
            if (response.Result != null)
            {
                order = JsonConvert.DeserializeObject<Order>(response.Result.ToString());
            }
        }

        private async Task Cancel()
        {
            var response = await OrderService.Cancel(OrderId);
            if(response != null && response.IsSuccess)
            {
                HideConfirmModal();
                Navigation.NavigateTo("purchasehistory");
            }
        }

        private bool showModal = false;

        private void ShowConfirmModal()
        {
            showModal = true;
        }

        private void HideConfirmModal()
        {
            showModal = false;
        }
    }
}
