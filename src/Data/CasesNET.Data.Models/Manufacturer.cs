namespace CasesNET.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using CasesNET.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<string>
    {
        public Manufacturer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Devices = new HashSet<Device>();
        }

        public string Name { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
