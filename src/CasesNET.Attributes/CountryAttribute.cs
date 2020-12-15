namespace CasesNET.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json;

    [AttributeUsage(AttributeTargets.Property)]
    public class CountryAttribute : ValidationAttribute
    {
        private new const string ErrorMessage = "Please enter a valid Country!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\..\..\Web\CasesNET.Web\wwwroot\country-list.json"));
            var json = File.ReadAllText(path);
            var countryNames = this.GetCountryData(json);
            var exists = countryNames.Any(x => x.Country == (string)value);
            return exists == true ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }

        private List<(string Country, string[] Cities)> GetCountryData(string jsonString)
             => JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString)
             .Select(x => (x.Key, x.Value))
             .ToList();
    }
}
