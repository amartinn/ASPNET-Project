namespace CasesNET.Data.Models
{
    using System;
    using System.Collections.Generic;

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
    }
}
