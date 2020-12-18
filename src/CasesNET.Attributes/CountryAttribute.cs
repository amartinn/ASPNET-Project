namespace CasesNET.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property)]
    public class CountryAttribute : BaseGeologicalValidationAttribute
    {
        internal override string ErrorMessage => "Please enter a valid Country!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var jsonString = this.GetJsonString();
            var countryData = this.DeserializeJsonStringAsCountryData(jsonString);
            var exists = countryData.Any(x => x.Country == (string)value);
            return exists == true ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
        }
    }
}
