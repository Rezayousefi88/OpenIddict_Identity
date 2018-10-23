using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityCode.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(11), DataType("varchar")]
        public string NationalCode { get; set; }

    }
}
