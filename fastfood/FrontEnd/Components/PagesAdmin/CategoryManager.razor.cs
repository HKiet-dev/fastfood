using FrontEnd.Models;
using FrontEnd.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace FrontEnd.Components.PagesAdmin
{
	public partial class CategoryManager : ComponentBase
	{
		[Inject]
		protected ICategoryService _categoryService { get; set; }

		private List<Category> Categorys { get; set; }

		[SupplyParameterFromForm]
		private Category createCategory { get; set; }
		[SupplyParameterFromForm]
		private Category editCategory { get; set; }

		private string notification;
		//private string search;

		protected override async Task OnInitializedAsync()
		{
			createCategory ??= new();
			editCategory ??= new();
			//page ??= new();
			//CloudinaryService ??= new();
			await LoadCategorys();
		}

		private async Task LoadCategorys()
		{
			var categoryResponse = await _categoryService.GetAll();
			if (categoryResponse != null && categoryResponse.IsSuccess)
			{
				//var result = categoryResponse.Result as dynamic;
				Categorys = JsonConvert.DeserializeObject<List<Category>>(categoryResponse.Result.ToString());
				//page.TotalCount = result.totalCount;
			}
			else
			{
				Categorys = null;
			}
		}

		private async Task CreateCategory()
		{
			try
			{
				//await UploadImage(createProduct);
				//createProduct.QR = GenerateQRCode(createProduct);

				var response = await _categoryService.Create(createCategory);
				if (response.IsSuccess && response != null)
				{
					notification = "Thành công: " + response.Message;
					await LoadCategorys();
					createCategory = new();
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadCategorys();
			}
		}

		private async Task DeleteCategory()
		{
			try
			{
				var response = await _categoryService.Delete(editCategory.Id);
				if (response.IsSuccess && response != null)
				{
					notification = "Xóa người dùng thành công !!!";
					await LoadCategorys();
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadCategorys();
			}
		}

		private async Task GetCategory(int id)
		{
			try
			{
				var response = await _categoryService.GetById(id);
				if (response.IsSuccess && response != null)
				{
					editCategory = JsonConvert.DeserializeObject<Category>(response.Result.ToString());
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadCategorys();
			}
		}

		private async Task EditCategory()
		{
			try
			{
				//await UploadImage(editProduct);
				var response = await _categoryService.Update(editCategory);
				if (response.IsSuccess && response != null)
				{
					notification = "Sửa người dùng thành công !!!";
					await LoadCategorys();
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadCategorys();
			}
		}

		private async Task GetBySearch(string search)
		{
			if (search != string.Empty)
			{
				var searchNameResponse = await _categoryService.GetByName(search);
				//if (searchNameResponse.Result.totalCount != 0)
				//{
					if (searchNameResponse != null && searchNameResponse.IsSuccess)
					{
						Categorys = JsonConvert.DeserializeObject<List<Category>>(searchNameResponse.Result.ToString());
						//foreach (var item in Products)
						//{
						//	if (item.Avatar == "string" || item.Avatar == null)
						//	{
						//		item.Avatar = "/Img/default_avatar.png";
						//	}
						//}
						//page.TotalCount = result.totalCount;
					}
					else
					{
						Categorys = null;
					}
				//}
				//else
				//{
					//var searchIdResponse = await _categoryService.GetById(int.Parse(search));
					//if (searchIdResponse.Result != null && searchIdResponse.IsSuccess)
					//{
					//	Product product = JsonConvert.DeserializeObject<Product>(searchIdResponse.Result.ToString());
					//	//if (user.Avatar == "string" || user.Avatar == null)
					//	//{
					//	//	user.Avatar = "/Img/default_avatar.png";
					//	//}
					//	Products.Clear();
					//	Products.Add(product);
					//	search = string.Empty;
					//}
					//else
					//{
					//	Products = null;
					//}
				//}
			}
			else
			{
				await LoadCategorys();
			}
		}
	}
}