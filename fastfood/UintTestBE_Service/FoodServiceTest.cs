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

            _dbContext.Product.Add(new Product
            {
                Id = 1,
                Name = "Test Food",
                CategoryId = 1,
                IsActive = true,
                Price = 10000,
                View = 5,
                ImageUrl = "http://example.com/image1.jpg"
            });
            _dbContext.SaveChanges();
        }
        [Fact]
        public void ThemThucAn_ThanhCong_KhiThucAnDuocThem()
        {
            var product = new Product
            {
                Id = 2,
                Name = "New Food",
                CategoryId = 1,
                IsActive = true,
                Price = 20000,
                View = 10,
                ImageUrl = "http://example.com/image2.jpg"
            };
            var productDto = new ProductDto
            {
                Id = 2,
                Name = "New Food",
                CategoryId = 1,
                IsActive = true,
                Price = 20000,
                View = 10,
                ImageUrl = "http://example.com/image2.jpg"
            };

            var result = _service.Create(product);

            Assert.True(result.IsSuccess);
            Assert.Equal("Thêm thức ăn thành công", result.Message);
            Assert.NotNull(result.Result);
            Assert.Equal(productDto.Name, ((ProductDto)result.Result).Name);
            Assert.Equal(productDto.Price, ((ProductDto)result.Result).Price);
            Assert.Equal(productDto.View, ((ProductDto)result.Result).View);
            Assert.Equal(productDto.ImageUrl, ((ProductDto)result.Result).ImageUrl);
        }
        [Fact]
        public void XoaThucAn_ThatBai_KhiThucAnKhongTonTai()
        {
            var result = _service.Delete(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Thức ăn này không tồn tại", result.Message);
        }

        [Fact]
        public void LayTatCaThucAn_ThanhCong_KhiThucAnTonTai()
        {
            var result = _service.GetAll();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public void LayThucAnTheoId_ThanhCong_KhiThucAnTonTai()
        {
            var result = _service.GetById(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Equal("Test Food", ((ProductDto)result.Result).Name);
            Assert.Equal(10000, ((ProductDto)result.Result).Price);
            Assert.Equal("http://example.com/image1.jpg", ((ProductDto)result.Result).ImageUrl);
        }

        [Fact]
        public void LayThucAnTheoId_ThatBai_KhiThucAnKhongTonTai()
        {
            var result = _service.GetById(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Thức ăn này không tồn tại", result.Message);
        }

        [Fact]
        public void CapNhatThucAn_ThanhCong_KhiThucAnDuocCapNhat()
        {
            // Arrange
            var productId = 1;
            var updatedProduct = new Product
            {
                Id = productId,
                Name = "Updated Food",
                Description = "Updated Description",
                Price = 15000,
                View = 10,
                IsActive = true,
                IsCombo = false,
                ImageUrl = "http://example.com/updated-image.jpg",
                CategoryId = 1
            };

            // Act
            _service.Update(updatedProduct);
            var result = _dbContext.Product.Find(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Food", result.Name);
            Assert.Equal("Updated Description", result.Description);
            Assert.Equal(15000, result.Price);
            Assert.Equal(10, result.View);
            Assert.True(result.IsActive);
            Assert.False(result.IsCombo);
            Assert.Equal("http://example.com/updated-image.jpg", result.ImageUrl);
            Assert.Equal(1, result.CategoryId);
        }

        [Fact]
        public void CapNhatThucAn_ThatBai_KhiThucAnKhongTonTai()
        {
            var product = new Product
            {
                Id = 999,
                Name = "NonExisting Food",
                CategoryId = 1,
                IsActive = true,
                Price = 15000,
                View = 10,
                ImageUrl = "http://example.com/image4.jpg"
            };

            var result = _service.Update(product);

            Assert.False(result.IsSuccess);
            Assert.Equal("Thức ăn này không tồn tại", result.Message);
        }
    }
}
