﻿using FrontEnd.Models;
using FrontEnd.Models.MOMO;
using FrontEnd.Services.IService;
using static FrontEnd.Utility.StaticDetails;

namespace FrontEnd.Services
{
    public class OrderService(IBaseService baseService) : IOrderService
    {
        readonly IBaseService _baseService = baseService;
        public async Task<ResponseDto?> Payment(Order order)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = order,
                Url = ApiUrl + "/api/order"
            });
        }

        public async Task<ResponseDto> MomoPayment(string amount)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Url = ApiUrl + $"/api/Payment/momo?amount={amount}"
            });
        }
        public async Task<ResponseDto> VNPayment(int amount)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Url = ApiUrl + $"/api/Payment/vnpay?amount={amount}"
            });
        }

        public async Task<ResponseDto> OrderByUserId()
        {
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = ApiType.GET,
				Url = ApiUrl + $"/api/Order/OrderByUser"
			});
		}

        public async Task<ResponseDto> GetOrderDetail(int OrderId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = ApiUrl + $"/api/Order/Order-details/{OrderId}"
            });
        }
        public async Task<ResponseDto> GetOrderByID(int OrderId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = ApiUrl + $"/api/Order/{OrderId}"
            });
        }
        public async Task<ResponseDto> Cancel(int OrderId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Url = ApiUrl + $"/api/Order/cancel/{OrderId}"
            });
        }

        public async Task<ResponseDto> GetAll()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = ApiUrl + $"/api/Order"
            });
        }


        public async Task<ResponseDto> UpdateOrderStatus(int orderid, string message)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Url = ApiUrl + $"/api/Order/update/{orderid}?message={message}"
            });
        }
    }
}
