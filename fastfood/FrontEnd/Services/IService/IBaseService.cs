using FrontEnd.Models;
using FrontEnd.Models.MOMO;

namespace FrontEnd.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDTO, bool withBearer = true);
    }
}
