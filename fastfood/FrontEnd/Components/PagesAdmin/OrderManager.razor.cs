﻿using FrontEnd.Helper;
using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FrontEnd.Components.PagesAdmin
{
    public partial class OrderManager : ComponentBase
    {
        [Inject]
        protected IOrderService _orderservice { get; set; }

        //Model để update Order Status
        [SupplyParameterFromForm]
        private Order Order { get; set; }

        //In tất cả orders
        private List<Order> Orders { get; set; }

        //In chi tiết order
        private List<OrderDetail> OrderDetails { get; set; }

        public string notification = "";
        //private PagePagination page { get; set; }
        private string search = null;


        protected override async Task OnInitializedAsync()
        {
            Order ??= new();
            OrderDetails ??= new();
            //page ??= new();
            await LoadOrders();

        }


        private async Task LoadOrders()
        {
            var ordersResponse = await _orderservice.GetAll();
            if (ordersResponse != null && ordersResponse.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<List<Order>>(ordersResponse.Result.ToString());
            }
            else
            {
                Orders = null;
            }
        }
        private async Task Cancel(int orderid)
        {
            var response = await _orderservice.Cancel(orderid);
            if (response != null && response.IsSuccess)
            {
                notification="Hủy đơn hàng thành công";
            }
        }
        private async Task LoadOrderDetails(Order order)
        {
            var orderDetailsResponse = await _orderservice.GetOrderDetail(order.OrderId??0);
            if (orderDetailsResponse != null && orderDetailsResponse.IsSuccess)
            {
                Order = order;
                OrderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(orderDetailsResponse.Result.ToString());
            }
            else
            {
                OrderDetails = null;
            }
        }

        private async Task UpdateStatus(int orderId, string message)
        {
            var orderDetailsResponse = await _orderservice.UpdateOrderStatus(orderId, message);
            if (orderDetailsResponse != null && orderDetailsResponse.IsSuccess)
            {
                notification = "Cập nhật thành công";
                await LoadOrders();
            }
            else
            {
                notification = "Cập nhật thất bại";
            }

        }

        private async Task Search()
        {
            if (string.IsNullOrEmpty(search))
            {
                await LoadOrders();
                return;
            }
            var searchId = Convert.ToInt16(search.Trim());
            var orderDetailsResponse = await _orderservice.GetOrderByID(searchId);
            if (orderDetailsResponse != null && orderDetailsResponse.IsSuccess)
            {
                Order = JsonConvert.DeserializeObject<Order>(orderDetailsResponse.Result.ToString());
                Orders.Clear();
                Orders.Add(Order);
            }
            else
            {
                notification = "Không tìm thấy";
            }

        }

    }
}