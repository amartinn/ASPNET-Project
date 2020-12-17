using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CasesNET.Attributes
{
    public abstract class BaseGeologicalValidationAttribute : ValidationAttribute
    {
        public new virtual string ErrorMessage { get; set; }

        internal List<(string Country, string[] Cities)> DeserializeJsonString(string jsonString)
        => JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString)
        .Select(x => (x.Key, x.Value))
        .ToList();

        internal string GetJsonString()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\..\..\..\Web\CasesNET.Web\wwwroot\country-list.json"));
            return File.ReadAllText(path);
        }
    }
}
