using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Freight
{
    public class FreightDTO
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "مبلغ")]
        public long? BaseTruckRent { get; set; }
        [Display(Name = "مبدا")]
        public int? SourceCityId { get; set; }
        [Display(Name = "نام مبدا")]
        public string SourceCityName { get; set; }
        [Display(Name = "مقصد")]
        public int? DestinationCityId { get; set; }
        [Display(Name = "نام مقصد")]
        public string DestinationCityName { get; set; }
        [Display(Name = "زمان ایجاد")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "تاریخ بارگیری")]
        public string LoadDate { get; set; }
        [Display(Name = "زمان بارگیری")]
        public string LoadTime { get; set; }
        [Display(Name = "تاریخ باربری")]
        public string ExpireDate { get; set; }
        [Display(Name = "زمان باربری")]
        public string ExpireTime { get; set; }
        [Display(Name = "نام کالا")]
        public string GoodName { get; set; }
        [Display(Name = "نوع بسته")]
        public int? PackageType { get; set; }
        [Display(Name = "نام بسته")]
        public string PackageName { get; set; }
        [Display(Name = "نوع وسیله")]
        public int? TruckType { get; set; }
        [Display(Name = "نام ماشین")]
        public string TruckName { get; set; }
        [Display(Name = "نوع باربر")]
        public int? LoaderType { get; set; }
        [Display(Name = "نام باربر")]
        public string loaderName { get; set; }
        [Display(Name = "وزن")]
        public int? Weight { get; set; }
        [Display(Name = "توضیح")]
        public string Description { get; set; }
        [Display(Name = "تلفن")]
        public string Tell { get; set; }

    }
}
