using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using FrontEnd.Services.IService;
using QRCoder;
using FrontEnd.Helper;
using Microsoft.AspNetCore.Components.Forms;

namespace FrontEnd.Components.PagesAdmin
{
	public partial class ProductsManager : ComponentBase
	{
		[Parameter]
		public EventCallback<InputFileChangeEventArgs> OnChangeInputFile { get; set; }
		private IBrowserFile file;
		[Inject]
		protected CloudinaryServices CloudinaryService { get; set; }

		[Inject]
		protected IFoodService _foodService { get; set; }

		private List<Product>? Products { get; set; }

		private PageNavigation page { get; set; }

		[SupplyParameterFromForm]
		private Product createProduct { get; set; }
		[SupplyParameterFromForm]
		private Product editProduct { get; set; }

		private string notification = "";
		private string search { get; set; }

		protected override async Task OnInitializedAsync()
		{
			createProduct ??= new();
			editProduct ??= new();
			page ??= new();
			//CloudinaryService ??= new();
			await LoadFoods();

		}

		private async Task LoadFoods()
		{
			var foodsResponse = await _foodService.GetAll(page.CurrentPage, 10);
			if (foodsResponse != null && foodsResponse.IsSuccess)
			{
				var result = foodsResponse.Result as dynamic;
				Products = JsonConvert.DeserializeObject<List<Product>>(result.products.ToString());
				page.TotalCount = result.totalCount;
			}
			else
			{
				Products = null;
			}
		}

		private async Task CreateFood()
		{
			try
			{
				await UploadImage(createProduct);
				createProduct.QR = GenerateQRCode(createProduct);

				var response = await _foodService.Create(createProduct);
				if (response.IsSuccess && response != null)
				{
					notification = "Thêm người dùng thành công !!!";
					await LoadFoods();
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadFoods();
			}
		}

		private async Task EditFood()
		{
			try
			{
				await UploadImage(editProduct);
				var response = await _foodService.Update(editProduct);
				if (response.IsSuccess && response != null)
				{
					notification = "Sửa người dùng thành công !!!";
					await LoadFoods();
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadFoods();
			}
		}

		private async Task Delete()
		{
			try
			{
				var response = await _foodService.Delete(editProduct.Id);
				if (response.IsSuccess && response != null)
				{
					notification = "Xóa người dùng thành công !!!";
					await LoadFoods();
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadFoods();
			}
		}

		private string GenerateQRCode(Product product)
		{
			string infomationProduct = 
				$"Tên món ăn: {product.Name}," +
				$"Mô tả: {product.Description}," +
				$"Giá: {product.Price}" +
				$"Hình ảnh: {product.ImageUrl}" +
				$"Loại: {product.CategoryId}";

			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(infomationProduct, QRCodeGenerator.ECCLevel.Q);
			PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

			byte[] qrCodeImage = qrCode.GetGraphic(20);

			// Convert the byte array to a Base64 string
			string base64Image = Convert.ToBase64String(qrCodeImage);

			return $"data:image/png;base64,{base64Image}";
		}

		private async Task Navigate(int currentPage)
		{
			page.CurrentPage = currentPage;
			if (search == null)
			{
				await LoadFoods();
			}
			else
			{
				await GetBySearch();
			}
		}

		private async Task GetFood(int id)
		{
			try
			{
				var response = await _foodService.GetById(id);
				if (response.IsSuccess && response != null)
				{
					editProduct = JsonConvert.DeserializeObject<Product>(response.Result.ToString());
				}
				else
				{
					throw new Exception(response.Message);
				}
			}
			catch (Exception ex)
			{
				notification = ex.Message;
				await LoadFoods();
			}
		}


		private void HandleFileSelected(InputFileChangeEventArgs e)
		{
			file = e.File;
		}
		private async Task UploadImage(Product product)
		{
			if (file != null)
			{
				using var fileStream = file.OpenReadStream();
				var fileName = file.Name;

				var cloudinaryUrl = await CloudinaryService.UploadImageAsync(fileStream, fileName);

				if (!string.IsNullOrEmpty(cloudinaryUrl))
				{
					product.ImageUrl = cloudinaryUrl;
				}
			}
		}

		private async Task GetBySearch()
		{
			if (search != "")
			{
				var searchNameResponse = await _foodService.GetBySearch(search, page.CurrentPage, 10);
				var result = searchNameResponse.Result as dynamic;
				if (result.totalCount != 0)
				{
					if (searchNameResponse != null && searchNameResponse.IsSuccess)
					{
						Products = JsonConvert.DeserializeObject<List<Product>>(result.products.ToString());
						//foreach (var item in Products)
						//{
						//	if (item.Avatar == "string" || item.Avatar == null)
						//	{
						//		item.Avatar = "/Img/default_avatar.png";
						//	}
						//}
						page.TotalCount = result.totalCount;
					}
					else
					{
						Products = null;
					}
				}
				else
				{
					var searchIdResponse = await _foodService.GetById(int.Parse(search));
					if (searchIdResponse.Result != null && searchIdResponse.IsSuccess)
					{
						Product product = JsonConvert.DeserializeObject<Product>(searchIdResponse.Result.ToString());
						//if (user.Avatar == "string" || user.Avatar == null)
						//{
						//	user.Avatar = "/Img/default_avatar.png";
						//}
						Products.Clear();
						Products.Add(product);
					}
					else
					{
						Products = null;
					}
				}
			}
			else
			{
				await LoadFoods();
			}
		}
	}
}