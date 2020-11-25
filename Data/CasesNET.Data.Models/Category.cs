namespace CasesNET.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using CasesNET.Data.Common.Models;

    public class Category : BaseModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cases = new HashSet<Case>();
        }

        public string Name { get; set; }

        public virtual ICollection<Case> Cases { get; set; }

        [ForeignKey("Image")]
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
