namespace CasesNET.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data.Tests.FakeModels;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Categories;
    using DeepEqual.Syntax;
    using Microsoft.AspNetCore.Http;
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

            var service = new CategoryService(this.categoryRepository.Object, null, null);

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

            var service = new CategoryService(this.categoryRepository.Object, null, null);

            // Act
            var item = service.GetNameById(this.categoryId);

            // Assert
            var actual = item;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByIdMethodShouldReturntheCorrectCategory()
        {
            // Arrange
            var expectedId = this.categoryId;
            var expectedName = this.categoryName;
            var fakeCategories = new List<Category>
            {
                new Category { Id = this.categoryId, Name = this.categoryName },
                new Category { Id = this.categoryId + "1", Name = this.categoryName + "1" },
                new Category { Id = this.categoryId + "2", Name = this.categoryName + "2" },
            };
            this.categoryRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCategories.AsQueryable());

            var service = new CategoryService(this.categoryRepository.Object, null, null);

            // Act
            var item = service.GetById<FakeCategoryModel>(this.categoryId);

            // Assert
            var actualName = item.Name;
            var actualId = item.Id;
            Assert.Equal(expectedId, actualId);
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public async Task CreateAsyncMethodShouldCreateCategory()
        {
            // Arrange
            var categories = new List<Category>();
            this.categoryRepository.Setup(s => s.AddAsync(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    categories.Add(category);
                });

            var service = new CategoryService(this.categoryRepository.Object, null, null);
            var image = new Mock<IFormFile>();
            image.Setup(s => s.FileName)
                .Returns("fileName.jpg");
            var categoryInputModel = new CategoryCreateInputModel
            {
                Image = image.Object,
                Name = "category",
            };

            // Act
            await service.CreateAsync(categoryInputModel, string.Empty);

            // Assert
            var item = categories[0];
            Assert.Equal("category", item.Name);
        }

        [Fact]
        public async Task UpdateAsyncMethodShouldUpdateTheEntity()
        {
            // Arrange
            var expectedItem = new Category
            {
                Id = this.categoryId,
                Name = this.categoryName,
            };
            var fakeCategories = new List<Category>
            {
                new Category
                {
                    Id = this.categoryId,
                    Image = new Image
                    {
                        Url = "fileName",
                        Extension = "jpg",
                    },
                },
                new Category { Id = this.categoryId + "1", Image = new Image { } },
            };
            this.categoryRepository.Setup(s => s.All())
                .Returns(fakeCategories.AsQueryable());
            this.categoryRepository.Setup(s => s.Update(It.IsAny<Category>()))
                .Callback((Category item) =>
                {
                    var searchedItem = fakeCategories.FirstOrDefault(x => x.Id == item.Id);
                    searchedItem.Name = item.Name;
                    searchedItem.Id = item.Id;
                });
            var service = new CategoryService(this.categoryRepository.Object, null, null);
            var image = new Mock<IFormFile>();
            image.Setup(s => s.FileName)
                .Returns("fileName.jpg");
            var model = new CategoryEditInputModel
            {
                Id = this.categoryId,
                Name = this.categoryName,
                Image = image.Object,
            };

            // Act
            await service.UpdateAsync(model, string.Empty);

            // Assert
            var actualItem = fakeCategories.FirstOrDefault(x => x.Id == this.categoryId);

            // Assert
            Assert.Equal(expectedItem.Id, actualItem.Id);
            Assert.Equal(expectedItem.Name, actualItem.Name);
        }

        [Fact]
        public async Task DeleteByIdAsyncMethodShouldDeletetheCategory()
        {
            // Arrange
            const int expected = 0;
            var categories = new List<Category>()
            {
                new Category
                {
                    Id = this.categoryId,
                },
            };
            this.categoryRepository.Setup(s => s.All())
                .Returns(categories.AsQueryable());
            this.categoryRepository.Setup(s => s.Delete(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    categories.Remove(category);
                });

            var service = new CategoryService(this.categoryRepository.Object, null, null);

            // Act
            await service.DeleteByIdAsync(this.categoryId);

            // Assert
            var actual = categories.Count();
            Assert.Equal(expected, actual);
        }
    }
}
