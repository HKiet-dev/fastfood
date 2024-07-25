using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using BackEnd.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable 1591
namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrderService oService, IMapper mapper, UserManager<User> userManager, ICartService cartService) : ControllerBase
    {
        IOrderService _oService = oService;
        IMapper _mapper = mapper;
        ICartService _cartService = cartService;
        UserManager<User> _usermanager = userManager;

        /// <summary>
        /// Lấy danh sách tất cả các hoá đơn.
        /// </summary>
        /// <returns>Danh sách các hoá đơn (Order).</returns>
        /// <response code="200">Trả về danh sách các hoá đơn.</response>
        /// <response code="404">Không tìm thấy hoá đơn nào.</response>
        [HttpGet]
        public ResponseDto getAllOrder()
        {
            return _oService.Orders();
        }

        /// <summary>
        /// Lấy hoá đơn theo ID.
        /// </summary>
        /// <param name="id">ID của hoá đơn cần lấy.</param>
        /// <returns>Hoá đơn có ID tương ứng.</returns>
        /// <response code="200">Trả về hoá đơn với ID đã chỉ định.</response>
        /// <response code="404">Không tìm thấy hoá đơn với ID đã chỉ định.</response>
        [HttpGet("{id:int}")]
        public ResponseDto GetOrderByID(int id)
        {
            return _oService.Order(id);
        }

        /// <summary>
        /// Tạo hoá đơn mới.
        /// </summary>
        /// <param name="create">Thông tin của hoá đơn mới.</param>
        /// <returns>Hoá đơn mới được tạo.</returns>
        /// <response code="201">Hoá đơn mới được tạo thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        [HttpPost]
        public ResponseDto Create([FromBody] CreateOrderDto create)
        {
            var userId = _usermanager.GetUserId(User);
            var cart = _cartService.getCart(userId);
            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    UserId = userId,
                    FullName = create.FullName,
                    Address = create.Address,
                    PaymentType = create.PaymentType,
                    PhoneNumber = create.PhoneNumber,
                    PaymentStatus = create.PaymentStatus,
                    note = create.note
                };
                var createdOrder = _oService.AddOrder(order);
                var orderDetails = cart.Select(item => new OrderDetail
                {
                    OrderId = createdOrder.OrderId,
                    ProductId = item.Food.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Food.Price,
                    Total = item.Total,
                });
                _oService.AddOrderDetail(orderDetails);
                return new ResponseDto { IsSuccess = true , Message = "Thêm hoá đơn thành công", Result = create};
            }
            return new ResponseDto { IsSuccess = false, Message = "Lỗi xảy ra trong quá trình thanh toán"};
        }
    }
}
