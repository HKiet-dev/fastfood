using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using System.Net.WebSockets;
#pragma warning disable 1591
namespace BackEnd.Repository.Services
{
    public class CartService(ApplicationDbContext db, IMapper mapper) : ICartService
    {
        readonly ApplicationDbContext _db = db;
        readonly IMapper _mapper = mapper;
        readonly ResponseDto response = new ResponseDto();
        public ResponseDto GetAll()
        {
            var cart = _db.CartDetail.ToList();
            if (cart.Any()) 
            {
                response.Result = _mapper.Map<List<CartDetailDto>>(cart);
                return response;
            }
            response.IsSuccess = false;
            response.Message = "Không có giỏ hàng nào tồn tại";
            return response;
        }
        public ResponseDto CreateCart(CartDetail cartDetail)
        {
            try 
            {
                var eProduct = _db.Product.FirstOrDefault(p => p.Id == cartDetail.ProductId);
                if (eProduct is null)
                {
                    response.IsSuccess = false;
                    response.Message = "Sản phẩm không tồn tại";
                    return response;
                }

                var eCartDetal = _db.CartDetail.SingleOrDefault( c => c.UserId == cartDetail.UserId && c.ProductId == cartDetail.ProductId);
                if (eCartDetal is null)
                {
                    cartDetail.Total = cartDetail.Quantity * eProduct.Price;
                    _db.CartDetail.Add(cartDetail);
                }
                else
                {
                    eCartDetal.Quantity += cartDetail.Quantity;
                    eCartDetal.Total = eProduct.Price * eCartDetal.Quantity;
                    _db.CartDetail.Update(eCartDetal);
                }
                _db.SaveChanges();
                response.Result = _mapper.Map<CartDetailDto>(cartDetail);
                response.IsSuccess = true;
                response.Message = "Thêm vào giỏ hàng thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Đã xảy ra lỗi khi lưu: {ex.Message}";
            }
            return response;
        }

        public ResponseDto DeleteCart(int prodcutId, string userId )
        {
            try
            {
                var eCartDetails = _db.CartDetail.FirstOrDefault(c => c.ProductId == prodcutId  && c.UserId == userId);
                if (eCartDetails is null) 
                {
                    response.IsSuccess = false;
                    response.Message = "Mục giỏ hàng không tồn tại";
                    return response;
                }
                _db.CartDetail.Remove(eCartDetails);
                _db.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Xóa mục giỏ hàng thành công";

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Đã xảy ra lỗi khi xóa mục giỏ hàng: {ex.Message}";
            }
            return response; 
        }

       

        public ResponseDto GetById(string userId)
        {
            try
            {
                var cartItems = _db.CartDetail.Where(c => c.UserId == userId).ToList();
                if (!cartItems.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "Người dùng chưa có giỏ hàng";
                    return response;
                }
                response.Result = _mapper.Map<List<CartDetailDto>>(cartItems);
                response.IsSuccess = true;
                response.Message = "Lấy danh sách giỏ hàng thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Đã xảy ra lỗi khi lấy danh sách giỏ hàng: {ex.Message}";
            }
            return response;
        }

        public ResponseDto UpdateCart(CartDetail cartDetail)
        {
            throw new NotImplementedException();
        }

        public ResponseDto DeleteAllById(string userId)
        {
            try
            {
                // Tìm tất cả sản phẩm trong giỏ hàng của người dùng
                var cartItems = _db.CartDetail.Where(c => c.UserId == userId).ToList();

                // Kiểm tra nếu không có sản phẩm nào trong giỏ hàng của người dùng
                if (!cartItems.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "Không có sản phẩm nào trong giỏ hàng của người dùng";
                    return response;
                }

                // Xóa tất cả sản phẩm trong giỏ hàng của người dùng
                _db.CartDetail.RemoveRange(cartItems);
                _db.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Xóa tất cả sản phẩm trong giỏ hàng của người dùng thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Đã xảy ra lỗi khi xóa sản phẩm trong giỏ hàng của người dùng: {ex.Message}";
            }
            return response;
        }
    }
}
