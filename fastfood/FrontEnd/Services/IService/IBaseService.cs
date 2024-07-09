using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDTO, bool withBearer = true);
    }
}
