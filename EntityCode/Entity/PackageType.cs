using System;
using System.Collections.Generic;
using System.Text;

namespace EntityCode.Entity
{
    public class PackageType : BaseEntity
    {
        public PackageType()
        {
            Freight = new HashSet<Freight>();
        }

        public string Name { get; set; }

        public ICollection<Freight> Freight { get; set; }
    }
}
