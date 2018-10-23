using System;
using System.Collections.Generic;

namespace EntityCode.Entity
{
    public  class Employ : BaseEntity
    {
        public string EmployCode { get; set; }
        public int PersonId { get; set; }

        public Driver Person { get; set; }
    }
}
