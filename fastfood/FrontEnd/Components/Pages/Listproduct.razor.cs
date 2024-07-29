using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FrontEnd.Components.Pages
{
    public partial class Listproduct
    {
        [Inject]
        protected ICategoryService _categoryService { get; set; }
        [Inject]
        protected IFoodService _foodService { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        private int currentPage = 1;
        private int pageSize = 8;
        private int totalPages;
        private bool hasPreviousPage => currentPage > 1;
        private bool hasNextPage => currentPage < totalPages;

        protected async override Task OnInitializedAsync()
        {
            await LoadCategory();
            await LoadProducts();

        }

        protected async Task LoadCategory()
        {
            var response = await _categoryService.GetAll();
            if (response != null && response.IsSuccess)
            {
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(response.Result.ToString());
            }
        }

        protected async Task LoadProducts()
        {
            var response = await _foodService.GetAll(currentPage, pageSize);
            if (response != null && response.IsSuccess)
            {

                var result = response.Result as dynamic;
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                totalPages = result.totalPages;
            }
        }
        private async Task PreviousPage()
        {
            if (hasPreviousPage)
            {
                currentPage--;
                await LoadProducts();
            }
        }

        private async Task NextPage()
        {
            if (hasNextPage)
            {
                currentPage++;
                await LoadProducts();
            }
        }
        protected async Task GetByCategoryId(int categoryid)
        {
            var productsTask = _foodService.GetByCategoryId(categoryid,1, 8);
            var productsResponse = await productsTask;
            if (productsResponse != null && productsResponse.IsSuccess)
            {
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsResponse.Result.ToString());
            }
        }
    }
}
