using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Services;
using Microsoft.EntityFrameworkCore;
namespace UintTestBE_Service
{
    public class OrderServicTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly OrderService _service;
        public OrderServicTest()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("OrderServiceTestDB")
               .Options;

            _dbContext = new ApplicationDbContext(options);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>().ReverseMap();
                // Add other mappings if needed
            }));


            _service = new OrderService(_dbContext, _mapper);

            // Seed data
            var product = new Product
            {
                Id = 1,
                Name = "Test Food",
                CategoryId = 1,
                IsActive = true,
                Price = 10000,
                View = 5,
                ImageUrl = "http://example.com/image1.jpg"
            };

            var order = new Order
            {
                OrderId = 1,
                UserId = "abc123",
                FullName = "John Doe",
                Address = "123 Elm St",
                PhoneNumber = "555-1234",
                PaymentType = "Credit Card",
                OrderDate = DateTime.Now,
                PaymentStatus = "Paid",
                OrderStatus = "Shipped"
            };

            var orderDetail = new OrderDetail
            {
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = 100.00m,
                Quantity = 2,
                Total = 200.00m
            };

            _dbContext.Product.Add(product);
            _dbContext.Orders.Add(order);
            _dbContext.OrderDetail.Add(orderDetail);
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        [Fact]
        public void Orders_ShouldReturnOrders_WhenOrdersExist()
        {
            // Act
            var result = _service.Orders();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Single((List<OrderDto>)result.Result);
        }
        [Fact]
        public void AddOrder_ShouldAddOrderSuccessfully()
        {
            // Arrange
            var newOrder = new Order
            {
                OrderId = 2,
                FullName = "Jane Smith",
                Address = "456 Oak St",
                PhoneNumber = "555-5678",
                PaymentType = "PayPal",
                OrderDate = DateTime.Now,
                PaymentStatus = "Pending",
                OrderStatus = "Processing",
                UserId = "xyz789" // Include UserId if it's required
            };

            // Act
            var addedOrder = _service.AddOrder(newOrder);

            // Assert
            Assert.NotNull(addedOrder);
            Assert.Equal(newOrder.OrderId, addedOrder.OrderId);
            Assert.Equal(newOrder.FullName, addedOrder.FullName);
            Assert.Equal(newOrder.Address, addedOrder.Address);

            var orderInDb = _dbContext.Orders.Find(newOrder.OrderId);
            Assert.NotNull(orderInDb);
            Assert.Equal(newOrder.FullName, orderInDb.FullName);
            Assert.Equal(newOrder.Address, orderInDb.Address);
        }

        [Fact]
        public void AddOrderDetail_ShouldAddOrderDetailsSuccessfully()
        {
            // Chuẩn bị
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    OrderId =1,
                    ProductId = 1,
                    Quantity = 2,
                    UnitPrice = 10000,
                    Total = 20000 // Tính tổng giá trị
                },
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 1,
                    UnitPrice = 20000,
                    Total = 20000 // Tính tổng giá trị
                }
            };

            // Thực hiện
            _service.AddOrderDetail(orderDetails);

            // Kiểm tra
            var orderDetailsInDb = _dbContext.OrderDetail.ToList();
            Assert.Equal(3, orderDetailsInDb.Count); // Tổng số chi tiết đơn hàng nên là 3 (2 mới + 1 đã có)
            Assert.Contains(orderDetailsInDb, od => od.ProductId == 1 && od.Quantity == 2 && od.UnitPrice == 10000 && od.Total == 20000);
            Assert.Contains(orderDetailsInDb, od => od.ProductId == 2 && od.Quantity == 1 && od.UnitPrice == 20000 && od.Total == 20000);
        }

        [Fact]
        public void GetOrderDetails_ShouldReturnOrderDetails_WhenOrderIdExists()
        {
            // Arrange
            int orderId = 1;

            // Act
            var response = _service.GetOrderDetails(orderId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Result);
            var details = (List<OrderDetailDto>)response.Result;
            Assert.Single(details);
            var detail = details.First();
            Assert.Equal("Test Food", detail.Name);
            Assert.Equal(100.00m, detail.UnitPrice);
            Assert.Equal(2, detail.Quantity);
            Assert.Equal(200.00m, detail.Total);
        }
        [Fact]
        public void GetOrderId_ShouldReturnOrders_WhenUserHasOrders()
        {
            // Arrange
            string userId = "abc123";

            // Act
            var response = _service.getOrderId(userId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Result);
            var orders = (List<OrderDto>)response.Result;
            Assert.Equal(1, orders.Count);
        }

        [Fact]
        public void GetOrderId_WithNonExistingUserId_ReturnsError()
        {
            // Arrange
            string userId = "nonexistent";

            // Act
            var response = _service.getOrderId(userId);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Hoá đơn này không tồn tại", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public void Order_WithExistingOrderId_ReturnsOrder()
        {
            // Arrange
            int orderId = 1;

            // Act
            var response = _service.Order(orderId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Result);
            var orderDto = (OrderDto)response.Result;
            Assert.Equal(orderId, orderDto.OrderId);
            Assert.Equal("John Doe", orderDto.FullName);
            Assert.Equal("123 Elm St", orderDto.Address);
            Assert.Equal("555-1234", orderDto.PhoneNumber);
            Assert.Equal("Credit Card", orderDto.PaymentType);
            Assert.Equal("Paid", orderDto.PaymentStatus);
            Assert.Equal("Shipped", orderDto.OrderStatus);
        }

        [Fact]
        public void Order_WithNonExistingOrderId_ReturnsError()
        {
            // Arrange
            int orderId = 999; // An ID that does not exist in the seeded data

            // Act
            var response = _service.Order(orderId);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Hoá đơn này không tồn tại", response.Message);
            Assert.Null(response.Result);
        }


        [Fact]
        public void UpdateOrderStatus_ShouldUpdateStatusAndPaymentStatusSuccessfully()
        {
            // Arrange
            var orderId = 1;
            var newStatus = "Đã giao";

            // Act
            _service.UpdateOrderStatus(orderId, newStatus);

            // Assert
            var order = _dbContext.Orders.Find(orderId);
            Assert.NotNull(order);
            Assert.Equal(newStatus, order.OrderStatus);
            Assert.Equal("Đã thanh toán", order.PaymentStatus);
        }

        [Fact]
        public void UpdateOrderStatus_ShouldUpdateStatusOnly_WhenStatusIsNotDelivered()
        {
            // Arrange
            var orderId = 1;
            var newStatus = "Processing"; // This status should not change PaymentStatus

            // Act
            _service.UpdateOrderStatus(orderId, newStatus);

            // Assert
            var order = _dbContext.Orders.Find(orderId);
            Assert.NotNull(order);
            Assert.Equal(newStatus, order.OrderStatus);
            Assert.Equal("Paid", order.PaymentStatus); // PaymentStatus should remain unchanged
        }


    }
}
