using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace FrontEnd.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected ICategoryService _categoryService { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ResponseDto? response = await _categoryService.GetAll();
            if(response != null && response.IsSuccess)
            {
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(response.Result.ToString());
            }
        }
    }
}
