namespace CasesNET.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CasesNET.Data.Common.Models;

    public class Device : BaseDeletableModel<string>
    {
        public Device()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cases = new HashSet<Case>();
        }

        public string Name { get; set; }

        [Required]
        public string ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
