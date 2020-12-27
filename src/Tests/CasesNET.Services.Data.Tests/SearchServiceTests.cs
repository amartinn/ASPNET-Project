using CasesNET.Data.Common.Repositories;
using CasesNET.Data.Models;
using CasesNET.Services.Data.Tests.FakeModels;
using CasesNET.Services.Mapping;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace CasesNET.Services.Data.Tests
{
    public class SearchServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Case>> caseRepository;
        private string searchTerm = "searchterm";
        private string fakePropertyValue = "test";
        public SearchServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeSearchModel).GetTypeInfo().Assembly);
            this.caseRepository = new Mock<IDeletableEntityRepository<Case>>();
        }

        [Fact]
        public void GetAllCasesBySearchTermMethodShouldReturnTheCorrectItems()
        {
            // Arrange
            const int expectedCount = 5;
            const int totalSeededCases = 100 + expectedCount;

            var fakeCases = this.GetDefaultCases().ToList();

            for (int i = expectedCount; i < totalSeededCases; i++)
            {
                fakeCases.Add(
                     new Case
                     {
                         Name = this.fakePropertyValue,
                         Description = this.fakePropertyValue,
                         Category = new Category
                         {
                             Name = this.fakePropertyValue,
                         },
                         Device = new Device
                         {
                             Name = this.fakePropertyValue,
                             Manufactorer = new Manufacturer
                             {
                                 Name = this.fakePropertyValue,
                             },
                         },
                     });
            }

            this.caseRepository.Setup(s => s.All())
                .Returns(fakeCases.AsQueryable());
            var service = new SearchService(this.caseRepository.Object);

            // Act
            var casesModels = service.GetAllCasesBySearchTerm<FakeSearchModel>(this.searchTerm);

            // Assert
            var actualCount = casesModels.Count();
            Assert.Equal(expectedCount, actualCount);
            Assert.True(casesModels.All(this.SearchPredicate(this.searchTerm)));
        }

        [Fact]
        public void GetCountBySearchTermMethodShouldReturnTheCorrectCount()
        {
            // Arrange
            const int expectedCount = 5;
            const int totalSeededCases = 100 + expectedCount;

            var fakeCases = this.GetDefaultCases().ToList();

            for (int i = expectedCount; i < totalSeededCases; i++)
            {
                fakeCases.Add(
                     new Case
                     {
                         Name = this.fakePropertyValue,
                         Description = this.fakePropertyValue,
                         Category = new Category
                         {
                             Name = this.fakePropertyValue,
                         },
                         Device = new Device
                         {
                             Name = this.fakePropertyValue,
                             Manufactorer = new Manufacturer
                             {
                                 Name = this.fakePropertyValue,
                             },
                         },
                     });
            }

            this.caseRepository.Setup(s => s.All())
                .Returns(fakeCases.AsQueryable());
            var service = new SearchService(this.caseRepository.Object);

            // Act
            var count = service.GetCountBySearchTerm(this.searchTerm);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        private Func<FakeSearchModel, bool> SearchPredicate(string term)
        => x =>
        x.Name.ToLower().Contains(term) ||
        x.CategoryName.ToLower().Contains(term) ||
        x.DeviceName.ToLower().Contains(term) ||
        x.ManufacturerName.ToLower().Contains(term) ||
        x.Description.ToLower().Contains(term);

        private IEnumerable<Case> GetDefaultCases()
        {
            var fakeCases = new List<Case>()
            {
                new Case
                {
                    Name = this.searchTerm,
                    Description = this.fakePropertyValue,
                    Device = new Device
                    {
                        Name = this.fakePropertyValue,
                        Manufactorer = new Manufacturer
                        {
                            Name = this.fakePropertyValue,
                        },
                    },
                    Category = new Category
                    {
                        Name = this.fakePropertyValue,
                    },
                },
                new Case
                {
                    Name = this.fakePropertyValue,
                    Description = this.searchTerm,
                    Device = new Device
                    {
                        Name = this.fakePropertyValue,
                        Manufactorer = new Manufacturer
                        {
                            Name = this.fakePropertyValue,
                        },
                    },
                    Category = new Category
                    {
                        Name = this.fakePropertyValue,
                    },
                },
                new Case
                {
                    Name = this.fakePropertyValue,
                    Description = this.fakePropertyValue,
                    Device = new Device
                    {
                        Name = this.searchTerm,
                        Manufactorer = new Manufacturer
                        {
                            Name = this.fakePropertyValue,
                        },
                    },
                    Category = new Category
                    {
                        Name = this.fakePropertyValue,
                    },
                },
                new Case
                {
                    Name = this.fakePropertyValue,
                    Description = this.fakePropertyValue,
                    Device = new Device
                    {
                        Name = this.fakePropertyValue,
                        Manufactorer = new Manufacturer
                        {
                            Name = this.searchTerm,
                        },
                    },
                    Category = new Category
                    {
                        Name = this.fakePropertyValue,
                    },
                },
                new Case
                {
                    Name = this.fakePropertyValue,
                    Description = this.fakePropertyValue,
                    Device = new Device
                    {
                        Name = this.fakePropertyValue,
                        Manufactorer = new Manufacturer
                        {
                            Name = this.fakePropertyValue,
                        },
                    },
                    Category = new Category
                    {
                        Name = this.searchTerm,
                    },
                },
            };
            return fakeCases;
        }
    }
}
