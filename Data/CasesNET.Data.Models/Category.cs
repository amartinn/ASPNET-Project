namespace CasesNET.Data.Models
{
    using System.Collections.Generic;

    using CasesNET.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Cases = new HashSet<Case>();
        }

        public string Name { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
