﻿namespace CasesNET.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data.Tests.FakeModels;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Cases;
    using DeepEqual.Syntax;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class CaseServiceTests
    {
        private readonly string caseId = "caseId";
        private readonly string categoryId = "categoryId";
        private readonly string manufacturerId = "manufacturerId";
        private readonly Mock<IDeletableEntityRepository<Case>> caseRepository;

        public CaseServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeCaseModel).GetTypeInfo().Assembly);
            this.caseRepository = new Mock<IDeletableEntityRepository<Case>>();
        }

        [Fact]
        public void GetByIdMethodShouldReturnTheCorrectCase()
        {
            // Arrange
            var expected = this.caseId;
            var fakeCases = new List<Case>
            {
                new Case
                {
                    Id = this.caseId,
                    Category = new Category
                    {
                        Id = this.categoryId,
                    },
                    CreatedOn = FakeDateTime.Now(),
                    Device = new Device
                    {
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
                new Case
                {
                    Id = this.caseId + "1",
                    Category = new Category
                    {
                        Id = this.categoryId,
                    },
                    CreatedOn = FakeDateTime.Now(),
                    Device = new Device
                    {
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
            };

            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var @case = service.GetById<FakeCaseModel>(this.caseId);

            // Assert
            Assert.Equal(expected, @case.Id);
        }

        [Fact]
        public void GetItemsCountByCategoryIdMethodShouldReturnTheCorrectCount()
        {
            // Arrange
            const int expected = 2;
            var fakeCases = new List<Case>
            {
                new Case { Id = this.caseId, CategoryId = this.categoryId },
                new Case { Id = this.caseId + "1", CategoryId = this.categoryId },
                new Case(),
            };
            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var count = service.GetItemsCountByCategoryId(this.categoryId);

            // Assert
            Assert.Equal(expected, count);
        }

        [Fact]
        public void GetCountByManufacturerMethodShouldReturnTheCorrectCount()
        {
            // Arrange
            const int expected = 2;
            var fakeCases = new List<Case>
            {
                new Case
                {
                    Id = this.caseId,
                    Device = new Device
                    {
                        ManufacturerId = this.manufacturerId,
                    },
                },
                new Case
                {
                    Id = this.caseId,
                    Device = new Device
                    {
                        ManufacturerId = this.manufacturerId,
                    },
                },
                new Case
                {
                    Id = this.caseId,
                    Device = new Device
                    {
                        ManufacturerId = this.manufacturerId + "2",
                    },
                },
            };
            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var count = service.GetCountByManufacturer(this.manufacturerId);

            // Assert
            Assert.Equal(expected, count);
        }

        [Fact]
        public void GetAllByCategoryMethodShouldReturnTheCorrectCases()
        {
            // Arrange
            const int expected = 2;
            var fakeCases = new List<Case>
            {
                new Case
                {
                    Id = this.caseId,
                    CategoryId = this.categoryId,
                    Category = new Category
                    {
                        Id = this.categoryId,
                    },
                    Device = new Device
                    {
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
                new Case
                {
                    Id = this.caseId,
                    CategoryId = this.categoryId,
                    Category = new Category
                    {
                        Id = this.categoryId,
                    },
                    Device = new Device
                    {
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
                new Case
                {
                    Id = this.caseId,
                    CategoryId = this.categoryId,
                    Category = new Category
                    {
                        Id = this.categoryId + "2",
                    },
                    Device = new Device
                    {
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
            };
            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var items = service.GetAllByCategoryId<FakeCaseModel>(this.categoryId, 1, 2);

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
            Assert.True(items.All(x => x.CategoryId == this.categoryId));
        }

        [Fact]
        public void GetLatestMethodShouldReturnTheCorrectCases()
        {
            // Arrange
            var totalDays = 20;
            var fakeCases = new List<Case>();

            for (int i = 1; i <= totalDays; i++)
            {
                fakeCases.Add(new Case
                {
                    Id = this.caseId,
                    CreatedOn = FakeDateTime.Now(i),
                    Device = new Device
                    {
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                });
            }

            var expectedItems = fakeCases.OrderByDescending(x => x.CreatedOn).ToList();
            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());

            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var actualItems = service.GetLatest<FakeCaseModel>(20).ToList();

            // Assert
            for (int i = 0; i < totalDays; i++)
            {
                var actualDay = actualItems[i].CreatedOn.Day;
                var expectedDay = expectedItems[i].CreatedOn.Day;
                Assert.Equal(expectedDay, actualDay);
            }
        }

        [Fact]
        public void GetByManufacturerIdMethodShouldReturnTheCorrectCases()
        {
            // Arrange
            const int expected = 2;
            var fakeCases = new List<Case>
            {
                new Case
                {
                    Id = this.caseId,
                    Device = new Device
                    {
                        ManufacturerId = this.manufacturerId,
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
                new Case
                {
                    Id = this.caseId,
                    Device = new Device
                    {
                        ManufacturerId = this.manufacturerId,
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId,
                        },
                    },
                },
                new Case
                {
                    Id = this.caseId,
                    Device = new Device
                    {
                        ManufacturerId = this.manufacturerId + "2",
                        Manufacturer = new Manufacturer
                        {
                            Id = this.manufacturerId + "2",
                        },
                    },
                },
            };
            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var items = service.GetAllByManufacturerId<FakeCaseModel>(this.manufacturerId, 1, 2);

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
            Assert.True(items.All(x => x.DeviceManufacturerId == this.manufacturerId));
        }

        [Fact]
        public void GetAllMethodShouldReturnTheCorrectCases()
        {
            // Arrange
            const int expected = 3;
            var fakeCases = new List<Case>()
            {
                 new Case
                 {
                     Id = this.caseId,
                     Category = new Category
                     {
                         Id = this.categoryId,
                     },
                     CreatedOn = FakeDateTime.Now(),
                     Device = new Device
                     {
                         Manufacturer = new Manufacturer
                         {
                             Id = this.manufacturerId,
                         },
                     },
                 },
                 new Case
                 {
                     Id = this.caseId,
                     Category = new Category
                     {
                         Id = this.categoryId,
                     },
                     CreatedOn = FakeDateTime.Now(),
                     Device = new Device
                     {
                         Manufacturer = new Manufacturer
                         {
                             Id = this.manufacturerId,
                         },
                     },
                 },
                 new Case
                 {
                     Id = this.caseId,
                     Category = new Category
                     {
                         Id = this.categoryId,
                     },
                     CreatedOn = FakeDateTime.Now(),
                     Device = new Device
                     {
                         Manufacturer = new Manufacturer
                         {
                             Id = this.manufacturerId,
                         },
                     },
                 },
            };
            this.caseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());

            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            var items = service.GetAll<FakeCaseModel>();

            // Assert
            var actual = items.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteByIdAsyncShouldDeleteTheCorrectCase()
        {
            // Arrange
            const int expected = 2;
            var fakeCases = new List<Case>
            {
                new Case { Id = this.caseId },
                new Case { Id = this.caseId + "1" },
                new Case { Id = this.caseId + "1" },
            };
            this.caseRepository.Setup(s => s.All())
                .Returns(fakeCases.AsQueryable());
            this.caseRepository.Setup(s => s.Delete(It.IsAny<Case>()))
                .Callback((Case item) =>
                {
                    fakeCases.Remove(item);
                });
            var service = new CaseService(this.caseRepository.Object, null);

            // Act
            await service.DeleteByIdAsync(this.caseId);

            // Assert
            var actual = fakeCases.Count();
            Assert.Equal(expected, actual);
            Assert.True(fakeCases.All(x => x.Id != this.caseId));
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateTheCase()
        {
            // Arrange
            var expectedItem = new Case
            {
                Id = this.caseId,
                Name = "Name",
                CategoryId = "CategoryId",
                DeviceId = "DeviceId",
                Description = "Description",
                Price = 100,
            };
            var fakeCases = new List<Case>
            {
                new Case { Id = this.caseId },
                new Case { Id = this.caseId + "1" },
            };
            this.caseRepository.Setup(s => s.All())
                .Returns(fakeCases.AsQueryable());
            this.caseRepository.Setup(s => s.Update(It.IsAny<Case>()))
                .Callback((Case item) =>
                {
                    var searchedItem = fakeCases.FirstOrDefault(x => x.Id == item.Id);
                    searchedItem.Name = item.Name;
                    searchedItem.CategoryId = item.CategoryId;
                    searchedItem.DeviceId = item.DeviceId;
                    searchedItem.Description = item.Description;
                    searchedItem.Price = item.Price;
                });
            var service = new CaseService(this.caseRepository.Object, null);

            var model = new CaseEditInputModel
            {
                Id = this.caseId,
                Name = "Name",
                CategoryId = "CategoryId",
                DeviceId = "DeviceId",
                Description = "Description",
                Price = 100,
            };

            // Act
            await service.UpdateAsync(model);

            // Assert
            var actualItem = fakeCases.FirstOrDefault(x => x.Id == this.caseId);

            // Assert
            var exception = await Record.ExceptionAsync(() => Task.Run(() => actualItem.ShouldDeepEqual(expectedItem)));
            Assert.Null(exception);
        }

        [Fact]
        public async Task CreateAsyncMethodShouldCreateCase()
        {
            // Arrange
            var cases = new List<Case>();
            this.caseRepository.Setup(s => s.AddAsync(It.IsAny<Case>()))
                .Callback((Case item) =>
                {
                    cases.Add(item);
                });
            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.SaveImageToDiskAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .Callback(() => { });
            var service = new CaseService(this.caseRepository.Object, fileService.Object);
            var formfile = new Mock<IFormFile>();
            formfile.Setup(s => s.FileName)
                .Returns("test.jpg");
            var createModel = new CreateCaseInputModel
            {
                CategoryId = this.categoryId,
                DeviceId = "deviceId",
                Price = 3,
                Name = "Name",
                Description = "description",
                Image = formfile.Object,
            };

            // Act
            await service.CreateAsync(createModel, string.Empty);

            // Assert
            var expectedCase = cases[0];
            Assert.NotNull(expectedCase);
            Assert.Equal(expectedCase.CategoryId, createModel.CategoryId);
            Assert.Equal(expectedCase.DeviceId, createModel.DeviceId);
            Assert.Equal(expectedCase.Price, createModel.Price);
            Assert.Equal(expectedCase.Name, createModel.Name);
            Assert.Equal(expectedCase.Description, createModel.Description);
        }
    }
}
