namespace CasesNET.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CasesNET.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string Url { get; set; }

        [Required]
        public string CaseId { get; set; }

        public Case Case { get; set; }
    }
}
