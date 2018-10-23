using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.City
{
    public class CityDTO
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "کد شهر")]
        public int Code { get; set; }
        [Display(Name = "نام شهر")]
        public string CityName { get; set; }
        [Display(Name = "نام دیگر")]
        public string Ename { get; set; }
        [Display(Name = "کد استان")]
        public int StateCode { get; set; }
        [Display(Name = "پلاک")]
        public int Pelak { get; set; }
        [Display(Name = "وضعیت")]
        public int Enable { get; set; }
        [Display(Name = "فاصله")]
        public int Distance { get; set; }
        [Display(Name = "زمان")]
        public int TravelTime { get; set; }
        [Display(Name = "امتیاز")]
        public int Bonus { get; set; }
    }
}
