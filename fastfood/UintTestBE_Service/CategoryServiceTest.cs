using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UintTestBE_Service
{
    public class CategoryServiceTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CategoryService _service;
        public CategoryServiceTest()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("FatFoodDB")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>().ReverseMap()));

            _service = new CategoryService(_dbContext, _mapper);

            // Seed data
            _dbContext.Category.Add(new Category { Id = 1, Name = "Test Category" });
            _dbContext.Category.Add(new Category { Id = 2, Name = "Another Category" });
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        //Test GetAll Category
        //Trường hợp : Nếu có tồn tại danh sách Category 
        [Fact]
        public void GetAll_ShouldReturnCategories_WhenCategoriesExist()
        {
            var result = _service.GetAll();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Single((IEnumerable<CategoryDto>)result.Result);
        }
        //Test GetAll Category
        //Trường hợp : Không có sản phẩm nào tồn  tại trong danh mục Category
        [Fact]
        public void GetAll_ShouldReturnFailure_WhenNoCategoriesExist()
        {
            // Arrange
            // Clear existing categories to simulate no categories in the database
            _dbContext.Category.RemoveRange(_dbContext.Category);
            _dbContext.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Không có danh mục nào tồn tại", result.Message);
            Assert.Null(result.Result);
        }

        //Test  Add Category 
        //Trường hợp: Thêm thành công 
        [Fact]
        public void Create_ShouldReturnSuccess_WhenCategoryIsAdded()
        {
            var category = new Category { Id = 3, Name = "New Category" };
            var categoryDto = new CategoryDto { Id = 3, Name = "New Category" };

            var result = _service.Create(category);

            Assert.True(result.IsSuccess);
            Assert.Equal("Tạo danh mục thành công", result.Message);
            Assert.NotNull(result.Result);
            Assert.Equal(categoryDto.Name, ((CategoryDto)result.Result).Name);
        }
        // Test GetByName 
        //Trường hợp :Tìm thấy Name category trong danh mục
        [Fact]
        public void GetByName_ShouldReturnCategories_WhenCategoriesMatch()
        {
            // Arrange
            var searchName = "Test";

            // Act
            var result = _service.GetByName(searchName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            var categories = (List<CategoryDto>)result.Result;
            Assert.Single(categories);
            Assert.Contains(categories, c => c.Name.Contains(searchName));
        }
        // Test GetByName 
        //Trường hợp :Không tìm thấy Name category trong danh mục
        [Fact]
        public void GetByName_ShouldReturnFailure_WhenNoCategoriesFound()
        {
            var result = _service.GetByName("NonExisting");

            Assert.False(result.IsSuccess);
            Assert.Equal("Không tồn tại danh mục nào tương ứng với từ khoá 'NonExisting'", result.Message);
        }
        //Test Detele 
        // Trường hợp : Tìm thấy category và xoá thành công
        [Fact]
        public void Delete_ShouldReturnSuccess_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;

            // Act
            var result = _service.Delete(categoryId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Xoá danh mục thành công", result.Message);
            Assert.Null(_dbContext.Category.Find(categoryId)); // Ensure the category was deleted
        }
        //Test Detele 
        // Trường hợp :Không tìm thấy category trả về danh mục không tồn tại
        [Fact]
        public void Delete_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var result = _service.Delete(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Danh mục này không tồn tại", result.Message);
        }

        //Test getById
        //Trường hợp : tìm thấy Id category trong danh mục
        [Fact]
        public void GetById_ShouldReturnCategory_WhenCategoryExists()
        {
            var result = _service.GetById(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Equal("Test Category", ((CategoryDto)result.Result).Name);
        }
        //Test getById
        //Trường hợp : Không tìm thấy Id category trong danh mục
        [Fact]
        public void GetById_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var result = _service.GetById(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Danh mục này không tồn tại", result.Message);
        }
        //Test Update 
        //Trường hợp : tìm thấy Id và Update thành công
        [Fact]
        public void Update_ShouldReturnSuccess_WhenCategoryIsUpdated()
        {
            var category = new Category { Id = 1, Name = "Updated Category" };
            var categoryDto = new CategoryDto { Id = 1, Name = "Updated Category" };
            var updatedCategory = _dbContext.Category.Find(1);
            var result = _service.Update(updatedCategory);
            Assert.True(result.IsSuccess);
            Assert.Equal("Cập nhật danh mục thành công", result.Message);
        }
        //Test Update 
        //Trường hợp :Không tìm thấy Id và Update trả về Danh mục không tồn tại
        [Fact]
        public void Update_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var category = new Category { Id = 999, Name = "NonExisting Category" };

            var result = _service.Update(category);

            Assert.False(result.IsSuccess);
            Assert.Equal("Danh mục này không tồn tại", result.Message);
        }


    }
}
