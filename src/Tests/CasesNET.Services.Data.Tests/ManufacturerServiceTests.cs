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

            var service = new ManufacturerService(this.manufacturerRepository.Object);

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

            var service = new ManufacturerService(this.manufacturerRepository.Object);

            // Act
            var name = service.GetNameById(this.manufacturerId);

            // Assert
            Assert.Equal(expected, name);
        }
    }
}
