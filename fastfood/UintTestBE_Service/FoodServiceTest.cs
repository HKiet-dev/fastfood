using AutoMapper;
using BackEnd.Data;
using BackEnd.Models.Dtos;
using BackEnd.Models;
using BackEnd.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UintTestBE_Service
{
    public class FoodServiceTest : IDisposable
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly FoodService _service;
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        public FoodServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FatFoodDB")
               .Options;

            _dbContext = new ApplicationDbContext(options);
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProductDto, Product>().ReverseMap()));

            _service = new FoodService(_dbContext, _mapper);

            _dbContext.Product.AddRange(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test Food 1",
                    CategoryId = 1,
                    IsActive = true,
                    Price = 10000,
                    View = 5,
                    ImageUrl = "http://example.com/image1.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Test Food 2",
                    CategoryId = 1,
                    IsActive = true,
                    Price = 20000,
                    View = 10,
                    ImageUrl = "http://example.com/image2.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Inactive Food",
                    CategoryId = 2,
                    IsActive = false,
                    Price = 30000,
                    View = 15,
                    ImageUrl = "http://example.com/image3.jpg"
                }
            });
            _dbContext.SaveChanges();
        }
        [Fact]
        public void CreateProduct_ShouldReturnSuccessResponse()
        {
            // Arrange
            var newProduct = new Product
            {
                Id = 2,
                Name = "New Food",
                CategoryId = 1,
                IsActive = true,
                Price = 20000,
                View = 10,
                ImageUrl = "http://example.com/image2.jpg"
            };

            // Act
            var response = _service.Create(newProduct);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Thêm thức ăn thành công", response.Message);
            Assert.NotNull(response.Result);
            Assert.Equal(newProduct.Name, ((ProductDto)response.Result).Name);
        }
        [Fact]
        public void CreateProduct_ShouldReturnErrorResponse_WhenProductIsNull()
        {
            // Arrange
            Product newProduct = null;

            // Act
            var response = _service.Create(newProduct);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Thức ăn truyền vào không hợp lệ", response.Message);
        }
        [Fact]
        public void CreateProduct_ShouldReturnErrorResponse_WhenProductNameExists()
        {
            // Arrange
            var existingProduct = new Product
            {
                Id = 2,
                Name = "Test Food", // Same name as the existing product
                CategoryId = 1,
                IsActive = true,
                Price = 20000,
                View = 10,
                ImageUrl = "http://example.com/image2.jpg"
            };

            // Act
            var response = _service.Create(existingProduct);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Thức ăn tên : Test Food đã tồn tại", response.Message);
        }

        [Fact]
        public void DeleteProduct_ShouldReturnSuccessResponse()
        {
            // Arrange
            var productId = 1;

            // Act
            var response = _service.Delete(productId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Xoá thức ăn thành công", response.Message);

            var deletedProduct = _dbContext.Product.Find(productId);
            Assert.Null(deletedProduct);
        }
        [Fact]
        public void DeleteProduct_ShouldReturnErrorResponse_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 999; // Non-existent product ID

            // Act
            var response = _service.Delete(productId);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Thức ăn này không tồn tại", response.Message);
        }

        [Fact]
        public void GetAll_ShouldReturnPagedProducts()
        {
            // Arrange
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetAll(page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Lấy danh sách sản phẩm thành công.", response.Message);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(2, result["TotalCount"].Value<int>());
            Assert.Equal(1, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Contains(products, p => p.Name == "Test Food 1");
            Assert.Contains(products, p => p.Name == "Test Food 2");
        }
        [Fact]
        public void GetByCategoryId_ShouldReturnPagedProducts()
        {
            // Arrange
            var categoryId = 1;
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetByCategoryId(categoryId, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(2, result["TotalCount"].Value<int>());
            Assert.Equal(1, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Contains(products, p => p.Name == "Test Food 1");
            Assert.Contains(products, p => p.Name == "Test Food 2");
        }

        [Fact]
        public void GetByCategoryId_ShouldReturnEmpty_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = 999; // Non-existent category
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetByCategoryId(categoryId, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(0, result["TotalCount"].Value<int>());
            Assert.Equal(0, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Empty(products);
        }

        [Fact]
        public void GetByFilter_ShouldReturnEmpty_WhenNoProductsInRange()
        {
            // Arrange
            decimal? minrange = 30000;
            decimal? maxrange = 40000;
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetByFilter(minrange, maxrange, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(0, result["TotalCount"].Value<int>());
            Assert.Equal(0, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Empty(products);
        }

        [Fact]
        public void GetByFilter_ShouldReturnPagedProductsInRange()
        {
            // Arrange
            decimal? minrange = 10000;
            decimal? maxrange = 20000;
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetByFilter(minrange, maxrange, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(2, result["TotalCount"].Value<int>());
            Assert.Equal(1, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Contains(products, p => p.Name == "Test Food 1");
            Assert.Contains(products, p => p.Name == "Test Food 2");
        }

        [Fact]
        public void GetById_ShouldReturnProduct_WhenIdIsValid()
        {
            // Arrange
            var productId = 1;

            // Act
            var response = _service.GetById(productId);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);
            var product = result.ToObject<ProductDto>();
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal("Test Food 1", product.Name);
        }

        [Fact]
        public void GetById_ShouldReturnError_WhenProductNotFound()
        {
            // Arrange
            var productId = 999; // Non-existent product ID

            // Act
            var response = _service.GetById(productId);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Thức ăn này không tồn tại", response.Message);
        }

        [Fact]
        public void GetById_ShouldIncrementViewCount_WhenProductFound()
        {
            // Arrange
            var productId = 1;

            // Act
            var response = _service.GetById(productId);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);
            var product = result.ToObject<ProductDto>();

            // Check view count increment
            var updatedProduct = _dbContext.Product.Find(productId);
            Assert.Equal(6, updatedProduct.View); // Initial view count was 5, so it should be 6 now
        }
        [Fact]
        public void GetBySearch_ShouldReturnPagedProducts_WhenQueryMatches()
        {
            // Arrange
            var query = "Test Food";
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetBySearch(query, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(2, result["TotalCount"].Value<int>());
            Assert.Equal(1, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Contains(products, p => p.Name == "Test Food 1");
            Assert.Contains(products, p => p.Name == "Test Food 2");
        }

        [Fact]
        public void GetBySearch_ShouldReturnEmpty_WhenNoProductMatchesQuery()
        {
            // Arrange
            var query = "Nonexistent Food";
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.GetBySearch(query, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);

            Assert.Equal(0, result["TotalCount"].Value<int>());
            Assert.Equal(0, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Empty(products);
        }

        [Fact]
        public void Sorting_ShouldReturnProductsSortedByPriceAsc()
        {
            // Arrange
            var sort = "asc";
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.Sorting(sort, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);
            Assert.Equal(2, result["TotalCount"].Value<int>());
            Assert.Equal(1, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Equal(10000, products[0].Price); // Test Food 1
            Assert.Equal(20000, products[1].Price); // Test Food 2
        }

        [Fact]
        public void Sorting_ShouldReturnProductsSortedByPriceDesc()
        {
            // Arrange
            var sort = "desc";
            var page = 1;
            var pageSize = 2;

            // Act
            var response = _service.Sorting(sort, page, pageSize);

            // Assert
            Assert.True(response.IsSuccess);

            var result = JObject.FromObject(response.Result);
            Assert.Equal(2, result["TotalCount"].Value<int>());
            Assert.Equal(1, result["TotalPages"].Value<int>());

            var products = result["Products"].ToObject<List<ProductDto>>();
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Equal(20000, products[0].Price); // Test Food 3
            Assert.Equal(10000, products[1].Price); // Test Food 2
        }

        [Fact]
        public void Update_ShouldUpdateExistingProduct()
        {
            // Arrange
            var updatedProduct = new Product
            {
                Id = 1,
                Name = "Updated Test Food 1",
                Description = "Updated Description",
                Price = 15000,
                View = 6,
                IsActive = true,
                IsCombo = false,
                ImageUrl = "http://example.com/updated_image1.jpg",
                CategoryId = 2
            };

            // Act
            var response = _service.Update(updatedProduct);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Thức ăn đã cập nhật", response.Message);

            var result = JObject.FromObject(response.Result);
            Assert.Equal("Updated Test Food 1", result["Name"].Value<string>());
            Assert.Equal("Updated Description", result["Description"].Value<string>());
            Assert.Equal(15000, result["Price"].Value<decimal>());
            Assert.Equal(6, result["View"].Value<int>());
            Assert.Equal(true, result["IsActive"].Value<bool>());
            Assert.Equal(false, result["IsCombo"].Value<bool>());
            Assert.Equal("http://example.com/updated_image1.jpg", result["ImageUrl"].Value<string>());
            Assert.Equal(2, result["CategoryId"].Value<int>());
        }

        [Fact]
        public void Update_ShouldReturnError_WhenProductNotFound()
        {
            // Arrange
            var updatedProduct = new Product
            {
                Id = 999, // Non-existent ID
                Name = "Non-existent Product",
                Description = "Description",
                Price = 15000,
                View = 6,
                IsActive = true,
                IsCombo = false,
                ImageUrl = "http://example.com/non_existent_image.jpg",
                CategoryId = 2
            };

            // Act
            var response = _service.Update(updatedProduct);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Thức ăn này không tồn tại", response.Message);
        }



    }
}
