using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Services;
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
        //Test GellCart 
        // Trường hợp lấy danh sách giỏ hàng thành công :GetAll_ShouldReturnCart_WhenCartExist
        [Fact]
        public void GetAll_ShouldReturnCart_WhenCartExist()
        {
            var result = _service.GetAll();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Single((IEnumerable<CartDetailDto>)result.Result);
        }
        //Test GellCart Service
        //Trường hợp không có sản phẩm trong giỏ hàng :GetAll_ShouldReturnFailure_WhenNoCartItemsExist
        [Fact]
        public void GetAll_ShouldReturnFailure_WhenNoCartItemsExist()
        {
            // Act
            _dbContext.CartDetail.RemoveRange(_dbContext.CartDetail);
            _dbContext.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Không có giỏ hàng nào tồn tại", result.Message);
            Assert.Null(result.Result);
        }

        //Test AddCart Service
        //Trường Hợp Thêm được : CreateCart_ShouldReturnSuccess_WhenCartDetailIsAdded
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
        //Test Add Cart Service
        //Trường hợp lỗi không tìm thấy sản phẩm : CreateCart_ShouldReturnFailure_WhenProductDoesNotExist
        [Fact]
        public void CreateCart_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            // Arrange
            var cartDetail = new CartDetail { UserId = "abc7890", ProductId = 999, Quantity = 3 };

            // Act
            var result = _service.CreateCart(cartDetail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Sản phẩm không tồn tại", result.Message);
        }
        // Test Add Cart Service
        // Trường hợp nếu giỏ hàng có tồn tại sản phẩm rồi thì update số lượng và tổng tiền
        [Fact]
        public void CreateCart_ShouldUpdateQuantity_WhenCartDetailAlreadyExists()
        {
            // Arrange
            var product = new Product
            {
                Id = 3,
                Name = "Test Product",
                Price = 1000000,
                IsActive = true,
                ImageUrl = "http://example.com/image1.jpg"
            };
            _dbContext.Product.Add(product);
            _dbContext.SaveChanges();

            var existingCartDetail = new CartDetail
            {
                UserId = "abc7890",
                ProductId = 3,
                Quantity = 3,
                Total = 3000000
            };
            _dbContext.CartDetail.Add(existingCartDetail);
            _dbContext.SaveChanges();
            var newCartDetail = new CartDetail
            {
                UserId = "abc7890",
                ProductId = 3,
                Quantity = 2,
                Total = 200000
            };

            // Act
            var result = _service.CreateCart(newCartDetail);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Thêm vào giỏ hàng thành công", result.Message);
            var resultCartDetail = (CartDetailDto)result.Result;
            Assert.Equal(existingCartDetail.UserId, resultCartDetail.UserId);
            Assert.Equal(existingCartDetail.ProductId, resultCartDetail.ProductId); // Total should be recalculated
        }

        //Test Detele Cart IdUser 
        // Trường hợp nếu có tồn tại trong giỏ hàng của User thì sẽ xoá thành công
        [Fact]
        public void DeleteCart_ShouldReturnSuccess_WhenItemExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", Price = 1000000, IsActive = true, ImageUrl = "http://example.com/image1.jpg" };
            _dbContext.Product.Add(product);
            _dbContext.SaveChanges();

            var cartDetail = new CartDetail { UserId = "user1", ProductId = 1, Quantity = 2, Total = 200 };
            _dbContext.CartDetail.Add(cartDetail);
            _dbContext.SaveChanges();

            // Act
            var response = _service.DeleteCart(1, "user1");
            var dbCartDetail = _dbContext.CartDetail.SingleOrDefault(cd => cd.UserId == "user1" && cd.ProductId == 1);
            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Xóa mục giỏ hàng thành công", response.Message);

            Assert.Null(dbCartDetail);
        }

        //Test Detele Cart IdUser
        //Trường hợp: Nếu User không tồn tại trong giỏ hàng hoặc không có giỏ hàng thì sẽ thông báo không User không tồn tại giỏ hàng
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
        //Test Detele All Cart User
        //Trường hợp  User có tồn tại giỏ hàng ít nhất 1 sản phẩm trở lên
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

        //Test Detele All Cart User
        //Trường hợp : User không tồn tại giỏ hàng nào 
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

        //Test GetCartUser 
        //Trường hợp : Nếu tồn tại UserId trong giỏ hàng thì sẽ lấy danh sách giỏ hàng của UserId đó 
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

        //Test GetCartUser
        //Trường hợp : Nếu UserId không tồn tại thì trả về trường hợp giỏ hàng của UserId không tồn tại 
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
        //Test GetCart 
        //Trường hợp : lấy toàn bộ chi tiết sản phẩm mà UserId đã thêm vào

        [Fact]
        public void GetById_ShouldReturnCartItems_WhenItemsExist()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", Price = 1000000, IsActive = true, ImageUrl = "http://example.com/image1.jpg" };
            _dbContext.Product.Add(product);

            var cartItems = new[]
            {
                new CartDetail { UserId = "user1", ProductId = 1, Quantity = 2, Total = 200 }
            };
            _dbContext.CartDetail.AddRange(cartItems);
            _dbContext.SaveChanges();

            // Act
            var response = _service.GetById("user1");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Result);
            var resultList = (List<CartDetailDto>)response.Result;
            var resultItem = resultList.First();

            Assert.Equal("user1", resultItem.UserId);
            Assert.Equal(1, resultItem.ProductId);
            Assert.Equal(2, resultItem.Quantity);

        }
        [Fact]
        public void GetById_ShouldReturnFailure_WhenCartDoesNotExist()
        {
            // Arrange
            var nonExistentUserId = "nonexistentUserId";

            // Act
            var result = _service.GetById(nonExistentUserId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Người dùng chưa có giỏ hàng", result.Message);
            Assert.Null(result.Result);
        }


    }
}
