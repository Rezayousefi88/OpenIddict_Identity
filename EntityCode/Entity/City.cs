using System;
using System.Collections.Generic;
using System.Text;

namespace EntityCode.Entity
{
    public class City : BaseEntity
    {
        public long Code { get; set; }
        public string CityName { get; set; }
        public string Ename { get; set; }
        public long StateCode { get; set; }
        public int Pelak { get; set; }
        public int Enable { get; set; }
        public int Distance { get; set; }
        public int TravelTime { get; set; }
        public int Bonus { get; set; }
    }
}
