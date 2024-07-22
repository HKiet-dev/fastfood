﻿using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;

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
            var eUser = _db.Users.FirstOrDefault(u => u.Id == cartDetail.UserId);
            if(eUser is null)
            {
                response.IsSuccess = false;
                response.Message = "Người dùng không tồn tại";
                return response;
            }

            var eProduct = _db.Product.FirstOrDefault(p => p.Id == cartDetail.ProductId);
            if( eProduct is null)
            {
                response.IsSuccess = false;
                response.Message = "Sản phẩm không tồn tại"; 
                return response;
            }

            var eCartDetal = _db.CartDetail.FirstOrDefault(c => c.Product.Id == cartDetail.ProductId && c.User.Id == cartDetail.UserId); 
            if(eCartDetal is null)
            {
                var newCartDetail = new CartDetail
                {
                    UserId = cartDetail.UserId,
                    ProductId = cartDetail.ProductId,
                    Quantity = cartDetail.Quantity
                };

                _db.CartDetail.Add(newCartDetail);
            }
            else
            {
                eCartDetal.Quantity += cartDetail.Quantity;
            }
            try
            {
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
                var eUser = _db.CartDetail.FirstOrDefault(u => u.UserId == userId);
                if(eUser is null)
                {
                    response.IsSuccess = false;
                    response.Message = "Người dùng chưa có giỏ hàng"; 
                    return response;
                }
                response.Result = _mapper.Map<CartDetailDto>(eUser);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Đã xảy ra lỗi khi xóa mục giỏ hàng: {ex.Message}";
            }
            return response;
        }

        public ResponseDto UpdateCart(CartDetail cartDetail)
        {
            throw new NotImplementedException();
        }
    }
}
