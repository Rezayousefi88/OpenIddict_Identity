using System;
using System.Collections.Generic;

namespace EntityCode.Entity
{
    public  class Driver : BaseEntity
    {
        public Driver()
        {
            Employ = new HashSet<Employ>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte? Age { get; set; }

        public ICollection<Employ> Employ { get; set; }
    }
}
