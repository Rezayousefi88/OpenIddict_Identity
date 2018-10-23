using System;
using System.Collections.Generic;
using System.Text;

namespace EntityCode.Entity
{
    public class Freight : BaseEntity
    {
        public long? BaseTruckRent { get; set; }
        public int? SourceCityId { get; set; }
        public int? DestinationCityId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LoadDate { get; set; }
        public string LoadTime { get; set; }
        public string ExpireDate { get; set; }
        public string ExpireTime { get; set; }
        public string GoodName { get; set; }
        public int? PackageType { get; set; }
        public int? TruckType { get; set; }
        public int? LoaderType { get; set; }
        public int? Weight { get; set; }
        public string Description { get; set; }
        public string Tell { get; set; }

        public Loader LoaderTypeNavigation { get; set; }
        public PackageType PackageTypeNavigation { get; set; }
        public TruckType TruckTypeNavigation { get; set; }
    }
}
