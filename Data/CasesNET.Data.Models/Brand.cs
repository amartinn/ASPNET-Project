﻿namespace CasesNET.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CasesNET.Data.Common.Models;

    public class Brand : BaseDeletableModel<string>
    {
        public Brand()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cases = new HashSet<Case>();
        }

        public string Name { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
