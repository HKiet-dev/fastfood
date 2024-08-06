using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Services;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace UintTestBE_Service
{
    public class CartServiceTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CartService _service;

        public CartServiceTest()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("FatFoodDB")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CartDetail, CartDetailDto>().ReverseMap()));

            _service = new CartService(_dbContext, _mapper);

            // Seed data
            _dbContext.CartDetail.Add(new CartDetail { UserId = "abc1234", ProductId = 2, Quantity = 3, Total = 1000000 });
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        [Fact]
        public void GetAll_ShouldReturnCart_WhenCartExist()
        {
            var result = _service.GetAll();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Single((IEnumerable<CartDetailDto>)result.Result);
        }
        [Fact]
        public void CreateCart_ShouldReturnSuccess_WhenCartDetailIsAdded()
        {
            // Arrange
            var cartDetail = new CartDetail { UserId = "abc7890", ProductId = 3, Quantity = 3 };

            // Mock the Product in the database
            var product = new Product { Id = 3, Name = "Test Product", Price = 1000000, IsActive = true, ImageUrl = "http://example.com/image1.jpg" };
            _dbContext.Product.Add(product);
            _dbContext.SaveChanges();

            // Act
            var result = _service.CreateCart(cartDetail);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Thêm vào giỏ hàng thành công", result.Message);
            Assert.NotNull(result.Result);
            var resultCartDetail = (CartDetailDto)result.Result;
            Assert.Equal(cartDetail.UserId, resultCartDetail.UserId);
            Assert.Equal(cartDetail.ProductId, resultCartDetail.ProductId);
            Assert.Equal(cartDetail.Quantity, resultCartDetail.Quantity);
        }
        [Fact]
        public void DeleteCart_ShouldReturnSuccess_WhenItemExists()
        {
            // Arrange
            var product = new Product { Id = 3, Name = "Test Product", Price = 1000000, IsActive = true, ImageUrl = "http://example.com/image1.jpg" };
            _dbContext.Product.Add(product);

            var cartDetail = new CartDetail { UserId = "user1", ProductId = 1, Quantity = 2, Total = 200 };
            _dbContext.CartDetail.Add(cartDetail);
            _dbContext.SaveChanges();

            // Act
            var response = _service.DeleteCart(1, "user1");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Xóa mục giỏ hàng thành công", response.Message);

            // Verify the cart detail was removed from the database
            var dbCartDetail = _dbContext.CartDetail.SingleOrDefault(cd => cd.UserId == "user1" && cd.ProductId == 1);
            Assert.Null(dbCartDetail);
        }
        [Fact]
        public void DeleteCart_ShouldReturnFailure_WhenItemDoesNotExist()
        {
            // Act
            var response = _service.DeleteCart(999, "user1");

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Mục giỏ hàng không tồn tại", response.Message);

            // Verify no change in the database
            var dbCartDetail = _dbContext.CartDetail.SingleOrDefault(cd => cd.UserId == "user1" && cd.ProductId == 999);
            Assert.Null(dbCartDetail);
        }
        [Fact]
        public void DeleteAllById_ShouldReturnSuccess_WhenItemsExist()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", Price = 1000000, IsActive = true, ImageUrl = "http://example.com/image1.jpg" };
            _dbContext.Product.Add(product);

            var cartDetail = new CartDetail { UserId = "user1", ProductId = 1, Quantity = 2, Total = 200 };
            _dbContext.CartDetail.AddRange(cartDetail);
            _dbContext.SaveChanges();

            // Act
            var response = _service.DeleteAllById("user1");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Xóa tất cả sản phẩm trong giỏ hàng của người dùng thành công", response.Message);

            // Verify that all items were removed from the database
            var dbCartItems = _dbContext.CartDetail.Where(cd => cd.UserId == "user1").ToList();
            Assert.Empty(dbCartItems);
        }
        [Fact]
        public void DeleteAllById_ShouldReturnFailure_WhenNoItemsExist()
        {
            // Act
            var response = _service.DeleteAllById("user1");

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Không có sản phẩm nào trong giỏ hàng của người dùng", response.Message);

            // Verify no changes in the database
            var dbCartItems = _dbContext.CartDetail.Where(cd => cd.UserId == "user1").ToList();
            Assert.Empty(dbCartItems);
        }
        [Fact]
        public void GetCart_ShouldReturnCartDetails_WhenItemsExist()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", Price = 1000000, IsActive = true, ImageUrl = "http://example.com/image1.jpg" };
            _dbContext.Product.Add(product);

            var cartDetail = new CartDetail { UserId = "user1", ProductId = 1, Quantity = 2, Total = 200 };
            _dbContext.CartDetail.Add(cartDetail);
            _dbContext.SaveChanges();

            // Act
            var response = _service.getCart("user1");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Result);
            var resultList = (IEnumerable<ListCartDetail>)response.Result;

        }

        [Fact]
        public void GetCart_ShouldReturnFailure_WhenNoItemsExist()
        {
            // Arrange
            // No items in the cart for user1

            // Act
            var response = _service.getCart("user1");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Result);
            var resultList = (IEnumerable<ListCartDetail>)response.Result;
            Assert.Empty(resultList);
        }
    }
}
