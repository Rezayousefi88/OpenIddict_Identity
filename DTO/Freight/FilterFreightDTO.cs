using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Freight
{
    public class FilterFreightDTO
    {
        public int SourceCityId { get; set; }
        public int DestinationCityId { get; set; }
        public string LoadDate { get; set; }
        public string ExpireDate { get; set; }
        public int Weight { get; set; }
    }
}
