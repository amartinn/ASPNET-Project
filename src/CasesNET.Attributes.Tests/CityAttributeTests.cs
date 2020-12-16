using CasesNET.Attributes.Tests.FakeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace CasesNET.Attributes.Tests
{
    public class CityAttributeTests
    {
        [Theory]
        [InlineData("Sofia")]
        [InlineData("Dimitrovgrad")]
        [InlineData("Haskovo")]
        [InlineData("Burgas")]
        public void CityAttributeShouldReturnTrueWhenTheCityExists(string cityName)
        {
            // Arrange
            var target = new ValidationTarget { CityName = cityName, CountryName = "Bulgaria" };
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(target, context, results, true);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("Sofi")]
        [InlineData("Burga")]
        [InlineData("city")]
        public void CityAttributeShouldReturnFalseWhenTheCityDoesntExists(string cityName)
        {
            // Arrange
            var target = new ValidationTarget { CityName = cityName, CountryName = "Bulgaria" };
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(target, context, results, true);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Sofi")]
        [InlineData("Burga")]
        [InlineData("city")]
        public void CityAttributeShouldReturnErrorMessageWhenCityDoesntExists(string cityName)
        {
            // Arrange
            var expected = "Please enter a valid City!";
            var target = new ValidationTarget { CityName = cityName, CountryName = "Bulgaria" };
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            _ = Validator.TryValidateObject(target, context, results, true);

            // Assert
            var actual = results[0].ErrorMessage;
            Assert.Equal(expected, actual);
        }
    }
}
