namespace CasesNET.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data;
    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Data.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class SettingsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Setting>>();
            repository.Setup(r => r.All()).Returns(new List<Setting>
                                                        {
                                                            new Setting(),
                                                            new Setting(),
                                                            new Setting(),
                                                        }.AsQueryable());
            var service = new SettingsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            throw new NotImplementedException();
        }
    }
}
