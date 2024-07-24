using BackEnd.Models;
using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Product = BackEnd.Models.Product;

namespace FrontEnd.Components.Pages
{
    public partial class ProductDetails : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        protected IFoodService _foodService { get; set; }

        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        protected override async Task OnInitializedAsync()
        {
            // Lấy thông tin sản phẩm
            var productResponse = await _foodService.GetById(Id);

            if (productResponse != null && productResponse.IsSuccess)
            {
                // Deserialize dữ liệu sản phẩm từ phản hồi
                Product = JsonConvert.DeserializeObject<Product>(productResponse.Result.ToString());
            }
            else
            {
                // Xử lý trường hợp không tìm thấy sản phẩm
                Product = null;
            }

            // Lấy danh sách sản phẩm liên quan
            var relatedProductsResponse = await _foodService.GetAll();

            if (relatedProductsResponse != null && relatedProductsResponse.IsSuccess)
            {
                // Deserialize danh sách sản phẩm liên quan từ phản hồi
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(relatedProductsResponse.Result.ToString());
            }
            else
            {
                // Xử lý trường hợp không có sản phẩm liên quan
                Products = Enumerable.Empty<Product>();
            }
        }

    }
}
