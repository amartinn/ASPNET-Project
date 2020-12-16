namespace CasesNET.Attributes.Tests
{
    using CasesNET.Attributes.Tests.FakeModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Xunit;

    public class CountyAttributeTests
    {
        [Theory]
        [InlineData("Bulgaria")]
        [InlineData("Australia")]
        [InlineData("Zimbabwe")]
        [InlineData("Turkey")]
        public void CountryAttributeShouldReturnTrueWhenTheCountryExists(string countryName)
        {
            // Arrange
            var target = new ValidationTarget { CountryName = countryName, CityName = "Sofia"};
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(target, context, results, true);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("Bulgari")]
        [InlineData("Turke")]
        [InlineData("Country")]
        public void CountryAttributeShouldReturnFalseWhenTheCountryDoesntExists(string countryName)
        {
            // Arrange
            var target = new ValidationTarget { CountryName = countryName, CityName = "Sofia" };
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(target, context, results, true);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Bulgari")]
        [InlineData("Turke")]
        [InlineData("Country")]
        public void CountryAttributeShouldReturnErrorMessageWhenCountryDoesntExists(string countryName)
        {
            // Arrange
            var expected = "Please enter a valid Country!";
            var target = new ValidationTarget { CountryName = countryName, CityName = "Sofia" };
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
