using System;
using System.Collections.Generic;
using System.Text;

namespace EntityCode.Entity
{
    public class TruckType : BaseEntity
    {
        public TruckType()
        {
            Freight = new HashSet<Freight>();
        }

        public string Name { get; set; }

        public ICollection<Freight> Freight { get; set; }
    }
}
