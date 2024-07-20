﻿using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace FrontEnd.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected ICategoryService _categoryService { get; set; }
        [Inject]
        protected IFoodService _foodService { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var categoriesTask = _categoryService.GetAll();
            var productsTask = _foodService.GetAll(1, 8);

            await Task.WhenAll(categoriesTask, productsTask);

            var categoriesResponse = await categoriesTask;
            var productsResponse = await productsTask;

            if (categoriesResponse != null && categoriesResponse.IsSuccess)
            {
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoriesResponse.Result.ToString());
            }

            if (productsResponse != null && productsResponse.IsSuccess)
            {
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsResponse.Result.ToString());
            }
        }
    }
}
