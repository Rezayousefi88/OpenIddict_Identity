using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.PackageType
{
    public class PackageTypeDTO
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
    }
}
