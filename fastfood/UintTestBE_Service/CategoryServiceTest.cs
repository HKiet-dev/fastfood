using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace UintTestBE_Service
{
    public class CategoryServiceTest
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
            _dbContext.SaveChanges();
        }
        [Fact]
        public void GetAll_ShouldReturnCategories_WhenCategoriesExist()
        {
            var result = _service.GetAll();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Single((IEnumerable<CategoryDto>)result.Result);
        }
        [Fact]
        public void Create_ShouldReturnSuccess_WhenCategoryIsAdded()
        {
            var category = new Category { Id = 2, Name = "New Category" };
            var categoryDto = new CategoryDto { Id = 2, Name = "New Category" };

            var result = _service.Create(category);

            Assert.True(result.IsSuccess);
            Assert.Equal("Tạo danh mục thành công", result.Message);
            Assert.NotNull(result.Result);
            Assert.Equal(categoryDto.Name, ((CategoryDto)result.Result).Name);
        }

        [Fact]
        public void GetByName_ShouldReturnFailure_WhenNoCategoriesFound()
        {
            var result = _service.GetByName("NonExisting");

            Assert.False(result.IsSuccess);
            Assert.Equal("Không tồn tại danh mục nào tương ứng với từ khoá 'NonExisting'", result.Message);
        }

        [Fact]
        public void Delete_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var result = _service.Delete(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Danh mục này không tồn tại", result.Message);
        }



        [Fact]
        public void GetById_ShouldReturnCategory_WhenCategoryExists()
        {
            var result = _service.GetById(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Equal("Test Category", ((CategoryDto)result.Result).Name);
        }

        [Fact]
        public void GetById_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var result = _service.GetById(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Danh mục này không tồn tại", result.Message);
        }

        [Fact]
        public void GetByName_ShouldReturnCategories_WhenCategoriesExist()
        {
            var result = _service.GetByName("Test");

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Single((IEnumerable<CategoryDto>)result.Result);
        }

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

        [Fact]
        public void Update_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var category = new Category { Id = 999, Name = "NonExisting Category" };

            var result = _service.Update(category);

            Assert.False(result.IsSuccess);
            Assert.Equal("Danh mục này không tồn tại", result.Message);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
