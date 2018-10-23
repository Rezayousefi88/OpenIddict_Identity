using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Driver
{
    public class DriverDTO
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد {0} الزامی می باشد")]
        public string FirstName { get; set; }
        [Display(Name = "شهرت")]
        [Required(ErrorMessage = "فیلد {0} الزامی می باشد")]
        public string LastName { get; set; }
        [Display(Name = "سن")]
        [Range(1,90,ErrorMessage ="{0} در بازه 1 تا 90 سال می باشد")]
        public byte Age { get; set; }
    }
}
