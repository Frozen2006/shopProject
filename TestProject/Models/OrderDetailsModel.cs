using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities;

namespace TestProject.Models
{
    public class OrderDetailsModel
    {
        [Required]
        [Display(Name = "Delivery Time")]
        public List<DeliverySpot> TimeSlot { get; set; }

        [Required]
        [Display(Name = "Comments for curier")]
        public string Comments { get; set; }
    }
}