using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class ChangeDeliveryAddressModel
    {
        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone1 { get; set; }

        [Display(Name = "Phone2")]
        public string Phone2 { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public int Zip { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}