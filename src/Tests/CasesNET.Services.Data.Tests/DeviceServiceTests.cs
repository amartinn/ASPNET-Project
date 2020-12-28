namespace CasesNET.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data.Tests.FakeModels;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Devices;
    using DeepEqual.Syntax;
    using Moq;
    using Xunit;

    public class DeviceServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Device>> deviceRepository;
        private readonly string deviceId = "deviceId";
        private readonly string deviceName = "deviceName";

        public DeviceServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeCaseModel).GetTypeInfo().Assembly);
            this.deviceRepository = new Mock<IDeletableEntityRepository<Device>>();
        }

        [Fact]
        public void GetAllMethodShouldReturnTheCorrectDevices()
        {
            // Arrange
            const int expected = 3;
            var fakeDevices = new List<Device>()
            {
                 new Device
                 {
                     Id = this.deviceId,
                 },
                 new Device
                 {
                     Id = this.deviceId,
                 },
                 new Device
                 {
                     Id = this.deviceId,
                 },
            };
            this.deviceRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeDevices.AsQueryable());

            var service = new DeviceService(this.deviceRepository.Object);

            // Act
            var items = service.GetAll<FakeDeviceModel>();

            // Assert
            var actual = items.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByIdMethodShouldReturnTheCorrectDevice()
        {
            // Arrange
            var expected = this.deviceId;
            var fakeDevices = new List<Device>
            {
                new Device
                {
                    Id = this.deviceId,
                },
                new Device
                {
                    Id = this.deviceId + "1",
                },
            };

            this.deviceRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeDevices.AsQueryable());
            var service = new DeviceService(this.deviceRepository.Object);

            // Act
            var device = service.GetById<FakeDeviceModel>(this.deviceId);

            // Assert
            Assert.Equal(expected, device.Id);
        }

        [Fact]
        public async Task CreateAsyncMethodShouldCreateDevice()
        {
            // Arrange
            var devices = new List<Device>();
            this.deviceRepository.Setup(s => s.AddAsync(It.IsAny<Device>()))
                .Callback((Device item) =>
                {
                    devices.Add(item);
                });
            var service = new DeviceService(this.deviceRepository.Object);
            var createModel = new DeviceCreateInputModel
            {
                Name = this.deviceName,
            };

            // Act
            await service.CreateAsync(createModel);

            // Assert
            var expectedCase = devices[0];
            Assert.NotNull(expectedCase);
            Assert.Equal(expectedCase.Name, createModel.Name);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateTheDevice()
        {
            // Arrange
            var expectedItem = new Device
            {
                Id = this.deviceId,
                Name = this.deviceName,
            };
            var fakeDevices = new List<Device>
            {
                new Device { Id = this.deviceId },
                new Device { Id = this.deviceId + "1" },
            };
            this.deviceRepository.Setup(s => s.All())
                .Returns(fakeDevices.AsQueryable());
            this.deviceRepository.Setup(s => s.Update(It.IsAny<Device>()))
                .Callback((Device item) =>
                {
                    var searchedItem = fakeDevices.FirstOrDefault(x => x.Id == item.Id);
                    searchedItem.Name = item.Name;
                });
            var service = new DeviceService(this.deviceRepository.Object);

            var model = new DeviceEditInputModel
            {
                Id = this.deviceId,
                Name = this.deviceName,
            };

            // Act
            await service.UpdateAsync(model);

            // Assert
            var actualItem = fakeDevices.FirstOrDefault(x => x.Id == this.deviceId);

            // Assert
            var exception = await Record.ExceptionAsync(() => Task.Run(() => actualItem.ShouldDeepEqual(expectedItem)));
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteByIdAsyncShouldDeleteTheCorrectDevice()
        {
            // Arrange
            const int expected = 2;
            var fakeDevices = new List<Device>
            {
                new Device { Id = this.deviceId },
                new Device { Id = this.deviceId + "1" },
                new Device { Id = this.deviceId + "1" },
            };

            this.deviceRepository.Setup(s => s.All())
                .Returns(fakeDevices.AsQueryable());
            this.deviceRepository.Setup(s => s.Delete(It.IsAny<Device>()))
                .Callback((Device item) =>
                {
                    fakeDevices.Remove(item);
                });
            var service = new DeviceService(this.deviceRepository.Object);

            // Act
            await service.DeleteByIdAsync(this.deviceId);

            // Assert
            var actual = fakeDevices.Count();
            Assert.Equal(expected, actual);
            Assert.True(fakeDevices.All(x => x.Id != this.deviceId));
        }
    }
}
