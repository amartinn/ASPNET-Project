using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CasesNET.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CountryAttribute : BaseGeologicalValidationAttribute
    {
        public override string ErrorMessage => "Please enter a valid Country!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var jsonString = this.GetJsonString();
            var countryNames = this.DeserializeJsonString(jsonString);
            var exists = countryNames.Any(x => x.Country == (string)value);
            return exists == true ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
        }
    }
}
