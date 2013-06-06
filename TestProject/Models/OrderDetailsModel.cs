using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace TestProject.Models
{
    public class OrderDetailsModel
    {
        [Required]
        [Display(Name = "Delivery Time")]
        public List<DeliverySpot> TimeSlot { get; set; }

        [Display(Name = "Comments for curier")]
        public string Comments { get; set; }
    }
}