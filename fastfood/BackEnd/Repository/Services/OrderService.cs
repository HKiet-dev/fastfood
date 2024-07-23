using AutoMapper;
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

        public ResponseDto AddOrder(Order order)
        {
            try
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                response.Result = _mapper.Map<OrderDto>(order);
                response.Message = "Tạo hoá đơn thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public void AddOrderDetail(IEnumerable<OrderDetail> orderdetails)
        {
            try
            {
                _db.OrderDetail.AddRange(orderdetails);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
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
            try
            {
                var orders = _db.Orders.Where(o => o.UserId == UserId);
                if (orders == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Hoá đơn này không tồn tại";
                    return response;
                }
                response.Result = _mapper.Map<OrderDto>(orders);
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

        public void UpdateOrderStatus(int OrderId, string message)
        {
            var order = _db.Orders.SingleOrDefault(Order => Order.Id == OrderId);
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
