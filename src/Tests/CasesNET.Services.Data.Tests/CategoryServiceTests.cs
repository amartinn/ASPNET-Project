namespace CasesNET.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data.Tests.FakeModels;
    using CasesNET.Services.Mapping;
    using Moq;
    using Xunit;

    public class CategoryServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Category>> categoryRepository;
        private readonly string categoryId = "categoryId";
        private readonly string categoryName = "name";

        public CategoryServiceTests()
        {
            this.categoryRepository = new Mock<IDeletableEntityRepository<Category>>();
            AutoMapperConfig.RegisterMappings(typeof(FakeCategoryModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetAllMethodShouldReturnAllCategories()
        {
            // Arrange
            const int expected = 3;
            var fakeCategories = new List<Category>
            {
                new Category { Id = this.categoryId, Name = this.categoryName },
                new Category { Id = this.categoryId, Name = this.categoryName },
                new Category { Id = this.categoryId, Name = this.categoryName },
            };
            this.categoryRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCategories.AsQueryable());

            var service = new CategoryService(this.categoryRepository.Object, null);

            // Act
            var items = service.GetAll<FakeCategoryModel>();

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetNameByIdshouldReturntheCorrectName()
        {
            // Arrange
            const string expected = "name";
            var fakeCategories = new List<Category>
            {
                new Category { Id = this.categoryId, Name = this.categoryName },
                new Category { Id = this.categoryId + "2", Name = this.categoryName + "2" },
                new Category { Id = this.categoryId + "2", Name = this.categoryName + "2" },
            };
            this.categoryRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCategories.AsQueryable());

            var service = new CategoryService(this.categoryRepository.Object, null);

            // Act
            var item = service.GetNameById(this.categoryId);

            // Assert
            var actual = item;
            Assert.Equal(expected, actual);
        }
    }
}
