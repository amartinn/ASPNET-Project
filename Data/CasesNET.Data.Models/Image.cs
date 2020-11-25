namespace CasesNET.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CasesNET.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string Url { get; set; }

        [ForeignKey("Case")]
        public string CaseId { get; set; }

        public virtual Case Case { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
