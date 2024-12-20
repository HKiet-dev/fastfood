﻿using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
#pragma warning disable 1591

namespace BackEnd.Repository.Services
{
    public class OrderService(ApplicationDbContext db, IMapper mapper) : IOrderService
    {
        readonly ApplicationDbContext _db = db;
        readonly IMapper _mapper = mapper;
        readonly ResponseDto response = new ResponseDto();
        public ResponseDto Orders()
        {
            var Orders = _db.Orders.ToList();
            if (Orders.Any())
            {
                response.Result = _mapper.Map<List<OrderDto>>(Orders);
                return response;
            }
            response.IsSuccess = false;
            response.Message = "Không có hoá đơn nào";
            return response;
        }

        public Order AddOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public void AddOrderDetail(IEnumerable<OrderDetail> orderdetails)
        {
                _db.OrderDetail.AddRange(orderdetails);
                _db.SaveChanges();
        }

        public ResponseDto GetOrderDetails(int OrderId)
        {
            var orderDetails = _db.OrderDetail.Where(x => x.OrderId == OrderId).ToList();
            List<OrderDetailDto> ListDetail = new List<OrderDetailDto>();
            foreach (var detail in orderDetails)
            {
                Product? food = _db.Product.Find(detail.ProductId);
                OrderDetailDto detailDTO = new OrderDetailDto();
                detailDTO.Name = food.Name;
                detailDTO.UnitPrice = detail.UnitPrice;
                detailDTO.Quantity = detail.Quantity;
                detailDTO.Total = detail.Total;
                ListDetail.Add(detailDTO);
            }
            response.Result = ListDetail;
            return response;
        }

        public ResponseDto getOrderId(string UserId)
        {
            var response = new ResponseDto();
            try
            {
                var orders = _db.Orders.Where(o => o.UserId == UserId).ToList(); // Convert to List
                if (orders.Count == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Hoá đơn này không tồn tại";
                    return response;
                }
                response.Result = _mapper.Map<List<OrderDto>>(orders); // Map the list
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto Order(int id)
        {
            try
            {
                var order = _db.Orders.Find(id);
                if (order == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Hoá đơn này không tồn tại";
                    return response;
                }
                response.Result = _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public decimal Profit()
        {
            var profit = _db.OrderDetail.Sum(x => x.Total);
            return profit;
        }

        public void CancelOrder(int OrderId)
        {
            var order = _db.Orders.SingleOrDefault(Order => Order.OrderId == OrderId);
            order.PaymentStatus = "Đã huỷ";
            order.OrderStatus = "Đã huỷ";
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void UpdateOrderStatus(int OrderId, string message)
        {
            var order = _db.Orders.SingleOrDefault(Order => Order.OrderId == OrderId);
            order.OrderStatus = message;
            if (message == "Đã giao")
            {
                order.PaymentStatus = "Đã thanh toán";
            }
            _db.Orders.Update(order);
            _db.SaveChanges();
        }
    }
}
