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
    using CasesNET.Web.ViewModels.Administration.Manufacturers;

    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class ManufacturerServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Manufacturer>> manufacturerRepository;
        private readonly string manufacturerId = "manufacturerId";
        private readonly string manufacturerName = "manufacturerName";

        public ManufacturerServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeCartItemModel).GetTypeInfo().Assembly);
            this.manufacturerRepository = new Mock<IDeletableEntityRepository<Manufacturer>>();
        }

        [Fact]
        public void GetAllMethodShouldReturnAllManufacturers()
        {
            // Arrange
            const int expected = 2;
            var fakeManufacturers = new List<Manufacturer>
            {
                new Manufacturer { Id = this.manufacturerId, Name = this.manufacturerName },
                new Manufacturer { Id = this.manufacturerId, Name = this.manufacturerName },
            };
            this.manufacturerRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeManufacturers.AsQueryable());

            var service = new ManufacturerService(this.manufacturerRepository.Object, null);

            // Act
            var items = service.GetAll<FakeManufacturerModel>();

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetNameByIdMethodShouldReturnTheCorrectManufacturer()
        {
            // Arrange
            var expected = this.manufacturerName;
            var fakeManufacturers = new List<Manufacturer>
            {
                new Manufacturer { Id = this.manufacturerId + "2", Name = this.manufacturerName + "2" },
                new Manufacturer { Id = this.manufacturerId, Name = this.manufacturerName },
            };
            this.manufacturerRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeManufacturers.AsQueryable());

            var service = new ManufacturerService(this.manufacturerRepository.Object, null);

            // Act
            var name = service.GetNameById(this.manufacturerId);

            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void GetByIdMethodShouldReturnTheCorrectManufacturer()
        {
            // Arrange
            var expected = this.manufacturerId;
            var fakeManufacturers = new List<Manufacturer>
            {
                new Manufacturer
                {
                    Id = this.manufacturerId,
                },
                new Manufacturer
                {
                    Id = this.manufacturerId + "1",
                },
            };

            this.manufacturerRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeManufacturers.AsQueryable());
            var service = new ManufacturerService(this.manufacturerRepository.Object, null);

            // Act
            var manufacturer = service.GetById<FakeManufacturerModel>(this.manufacturerId);

            // Assert
            Assert.Equal(expected, manufacturer.Id);
        }

        [Fact]
        public async Task CreateAsyncMethodShouldCreateManufacturer()
        {
            // Arrange
            var fakeManufacturers = new List<Manufacturer>();
            this.manufacturerRepository.Setup(s => s.AddAsync(It.IsAny<Manufacturer>()))
                .Callback((Manufacturer item) =>
                {
                    fakeManufacturers.Add(item);
                });
            var service = new ManufacturerService(this.manufacturerRepository.Object, null);
            var formfile = new Mock<IFormFile>();
            formfile.Setup(s => s.FileName)
                .Returns("test.jpg");
            var createModel = new ManufacturerCreateInputModel
            {
                Name = this.manufacturerName,
                Image = formfile.Object,
            };

            // Act
            await service.CreateAsync(createModel, string.Empty);

            // Assert
            var expectedCase = fakeManufacturers[0];
            Assert.NotNull(expectedCase);
            Assert.Equal(expectedCase.Name, createModel.Name);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateTheManufacturer()
        {
            // Arrange
            var expectedItem = new Manufacturer
            {
                Id = this.manufacturerId,
                Name = this.manufacturerName,
            };
            var fakeManufacturers = new List<Manufacturer>
            {
                new Manufacturer
                {
                    Id = this.manufacturerId,
                    Name = this.manufacturerName,
                    Image = new Image
                        {
                            Url = "image",
                            Extension = "jpg",
                        },
                },
                new Manufacturer
                {
                    Id = this.manufacturerId + "1",
                    Name = this.manufacturerName + "1",
                    Image = new Image
                    {
                    Url = "image",
                    Extension = "jpg",
                    },
                },
            };
            this.manufacturerRepository.Setup(s => s.All())
                .Returns(fakeManufacturers.AsQueryable());
            this.manufacturerRepository.Setup(s => s.Update(It.IsAny<Manufacturer>()))
                .Callback((Manufacturer item) =>
                {
                    var searchedItem = fakeManufacturers.FirstOrDefault(x => x.Id == item.Id);
                    searchedItem.Name = item.Name;
                });
            var service = new ManufacturerService(this.manufacturerRepository.Object, null);
            var formFile = new Mock<IFormFile>();
            formFile.Setup(s => s.FileName)
                .Returns("image.jpg");
            var model = new ManufacturerEditInputModel
            {
                Id = this.manufacturerId,
                Name = this.manufacturerName,
                Image = formFile.Object,
            };

            // Act
            await service.UpdateAsync(model, string.Empty);

            // Assert
            var actualItem = fakeManufacturers.FirstOrDefault(x => x.Id == this.manufacturerId);

            // Assert
            Assert.Equal(this.manufacturerName, actualItem.Name);
        }

        [Fact]
        public async Task DeleteByIdAsyncShouldDeleteTheCorrectManufacturer()
        {
            // Arrange
            const int expected = 2;
            var fakeManufacturers = new List<Manufacturer>
            {
                new Manufacturer { Id = this.manufacturerId },
                new Manufacturer { Id = this.manufacturerId + "1" },
                new Manufacturer { Id = this.manufacturerId + "1" },
            };
            this.manufacturerRepository.Setup(s => s.All())
                .Returns(fakeManufacturers.AsQueryable());
            this.manufacturerRepository.Setup(s => s.Delete(It.IsAny<Manufacturer>()))
                .Callback((Manufacturer item) =>
                {
                    fakeManufacturers.Remove(item);
                });
            var service = new ManufacturerService(this.manufacturerRepository.Object, null);

            // Act
            await service.DeleteByIdAsync(this.manufacturerId);

            // Assert
            var actual = fakeManufacturers.Count();
            Assert.Equal(expected, actual);
            Assert.True(fakeManufacturers.All(x => x.Id != this.manufacturerId));
        }
    }
}
