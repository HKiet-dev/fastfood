using Azure;
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
        private int value;
        private int sort;
        public class SearchQuery
        {
            public string ProductName { get; set; }
        }
        private SearchQuery query = new();

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

        private async Task ProductSort()
        {
            ResponseDto response;
            switch (sort)
            {
                case 0:
                    await LoadProducts();
                    break;
                case 1:
                    response = await _foodService.Sorting("desc", currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
                case 2:
                    response = await _foodService.Sorting("asc", currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
            }
        }

        private async Task ProductSearch()
        {
            var response = await _foodService.GetBySearch(query.ProductName,currentPage, pageSize);
            if (response != null && response.IsSuccess)
            {

                var result = response.Result as dynamic;
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                totalPages = result.totalPages;
            }
        }

        private async Task ProductFilter()
        {
            ResponseDto response;
            switch (value)
            {
                case 0:
                    await LoadProducts();
                    break;
                case 1:
                    response = await _foodService.GetByFilter(0, 16000, currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
                case 2:
                    response = await _foodService.GetByFilter(16000, 30000, currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
                case 3:
                    response = await _foodService.GetByFilter(30000, 60000, currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
                case 4:
                    response = await _foodService.GetByFilter(60000, 100000, currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
                case 5:
                    response = await _foodService.GetByFilter(100000, int.MaxValue, currentPage, pageSize);
                    if (response != null && response.IsSuccess)
                    {

                        var result = response.Result as dynamic;
                        Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                        totalPages = result.totalPages;
                    }
                    break;
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

        private async Task GoToPage(int pageSelected)
        {
            currentPage = pageSelected;
            if (value != null)
            {
                await ProductFilter();
            }
            if (sort != null)
            {
                await ProductSort();
            }
            else
            {
                await LoadProducts();
            }

        }

        protected async Task GetByCategoryId(int categoryid)
        {
            var response = await _foodService.GetByCategoryId(categoryid,1, pageSize);
            if (response != null && response.IsSuccess)
            {

                var result = response.Result as dynamic;
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result.products.ToString());
                totalPages = result.totalPages;
            }
        }
    }
}
