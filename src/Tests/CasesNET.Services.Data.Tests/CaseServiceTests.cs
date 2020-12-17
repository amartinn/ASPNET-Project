namespace CasesNET.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data.Tests.FakeModels;
    using CasesNET.Services.Mapping;
    using Moq;
    using Xunit;

    public class CaseServiceTests
    {
        private readonly string caseId = "caseId";
        private readonly string categoryId = "categoryId";
        private readonly string manufacturerId = "manufacturerId";

        public CaseServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeCaseModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetByIdMethodShouldReturnTheCorrectCase()
        {
            // Arrange
            var fakeCases = new List<Case>
            {
                new Case { Id = this.caseId },
                new Case { Id = this.caseId + "1" },
                new Case(),
            };
            var mockCaseRepository = new Mock<IRepository<Case>>();
            mockCaseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(mockCaseRepository.Object);

            // Act
            var @case = service.GetById<FakeCaseModel>(this.caseId);

            // Assert
            var expected = this.caseId;
            Assert.Equal(expected, @case.Id);
        }

        [Fact]
        public void GetItemsCountByCategoryIdMethodShouldReturnTheCorrectCount()
        {
            // Arrange
            var fakeCases = new List<Case>
            {
                new Case { Id = this.caseId, CategoryId = this.categoryId },
                new Case { Id = this.caseId + "1", CategoryId = this.categoryId },
                new Case(),
            };
            var mockCaseRepository = new Mock<IRepository<Case>>();

            mockCaseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(mockCaseRepository.Object);

            // Act
            var count = service.GetItemsCountByCategoryId(this.categoryId);

            // Assert
            var expected = 2;
            Assert.Equal(expected, count);
        }

        [Fact]
        public void GetCountByManufacturerMethodShouldReturnTheCorrectCount()
        {
            // Arrange
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
            var mockCaseRepository = new Mock<IRepository<Case>>();

            mockCaseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(mockCaseRepository.Object);

            // Act
            var count = service.GetCountByManufacturer(this.manufacturerId);

            // Assert
            var expected = 2;
            Assert.Equal(expected, count);
        }

        [Fact]
        public void GetAllByCategoryMethodShouldReturnTheCorrectCases()
        {
            // Arrange
            var fakeCases = new List<Case>
            {
                new Case { Id = this.caseId, CategoryId = this.categoryId },
                new Case { Id = this.caseId + "1", CategoryId = this.categoryId },
                new Case(),
            };
            var mockCaseRepository = new Mock<IRepository<Case>>();

            mockCaseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(mockCaseRepository.Object);

            // Act
            var cases = service.GetAllByCategory<FakeCaseModel>(this.categoryId);

            // Assert
            var expected = 2;
            Assert.Equal(expected, cases.Count());
            Assert.True(cases.All(x => x.CategoryId == this.categoryId));
        }

        [Fact]
        public void GetLatestMethodShouldReturnTheCorrectCases()
        {
            // Arrange
            var totalDays = 10;
            var fakeCases = new List<Case>();
            for (int i = 1; i <= totalDays; i++)
            {
                fakeCases.Add(new Case
                {
                    CreatedOn = FakeDateTime.Now(i),
                });
            }

            var mockCaseRepository = new Mock<IRepository<Case>>();

            mockCaseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(mockCaseRepository.Object);

            // Act
            var actualItems = service.GetLatest<FakeCaseModel>().ToList();

            // Assert
            var expectedItems = fakeCases.OrderBy(x => x.CreatedOn).ToList();
            for (int i = 0; i < totalDays; i++)
            {
                var expectedDay = expectedItems[i].CreatedOn.Day;
                var actualDay = actualItems[i].CreatedOn.Day;
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
                        Manufactorer = new Manufacturer
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
                        Manufactorer = new Manufacturer
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
                        Manufactorer = new Manufacturer
                        {
                            Id = this.manufacturerId + "2",
                        },
                    },
                },
            };
            var mockCaseRepository = new Mock<IRepository<Case>>();

            mockCaseRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new CaseService(mockCaseRepository.Object);

            // Act
            var items = service.GetByManufacturerId<FakeCaseModel>(this.manufacturerId, 1, 2);

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
            Assert.True(items.All(x => x.DeviceManufacturerId == this.manufacturerId));
        }
    }
}
