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
    public class CityAttribute : ValidationAttribute
    {
        private new const string ErrorMessage = "Please enter a valid City!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\..\..\CasesNET.Web\wwwroot\country-list.json"));
            var json = File.ReadAllText(path);
            var countryData = this.GetCountryData(json);
            var exists = countryData.Any(x => x.Cities.Any(y => y == (string)value));
            return exists == true ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }

        private List<(string Country, string[] Cities)> GetCountryData(string jsonString)
            => JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString)
            .Select(x => (x.Key, x.Value))
            .ToList();
    }
}
