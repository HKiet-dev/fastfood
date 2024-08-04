using FrontEnd.Models;
using FrontEnd.Services.IService;
using FrontEnd.Utility;
using static FrontEnd.Utility.StaticDetails;
namespace FrontEnd.Services
{
    public class CartService(IBaseService baseService): ICartService
    {
        readonly IBaseService _baseService = baseService;
        public async Task<ResponseDto?> CreateCart(CartDetail cartDetail)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = cartDetail,
                Url = ApiUrl + "/api/Cart/addtocart"
            });
        }
        public async Task<ResponseDto?> DeleteAllById()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                //AccessToken = _tokenProvider.GetToken(),
                ApiType = ApiType.DELETE,
                Url = ApiUrl + "/api/Cart/delete-all-by-user-id"
            });
        }
        public async Task<ResponseDto?> DeleteCart(int productId, string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                //AccessToken = _tokenProvider.GetToken(),
                ApiType = ApiType.DELETE,
                Url = ApiUrl + $"/api/Cart/deletecart/{productId} && {userId}"
            });
        }
        public Task<ResponseDto?> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseDto?> GetById(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = ApiUrl + $"/api/Cart/find-id/{userId}"
            });
        }
        public async Task<ResponseDto> getCart(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {   
                ApiType = ApiType.GET,
                Url = ApiUrl + $"/api/Cart/list_cartdetails/{userId}"
            });
        }
        public Task<ResponseDto?> UpdateCart(CartDetail cartDetail)
        {
            throw new NotImplementedException();
        }
    }
}
