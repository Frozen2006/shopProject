using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class PaymentModel
    {
        [Required(ErrorMessage = "Card Number is required")]
        [RegularExpression("^4[0-9]{12}(?:[0-9]{3})?$", ErrorMessage = "Invalid card number")]
        [DisplayName("Card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration month is required")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        [DisplayName("Card expiration month")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Expiration day is required")]
        [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
        [DisplayName("Expiration day")]
        public int ExpirationDay { get; set; }

        [Required(ErrorMessage = "Security code is required")]
        [Range(100, 999, ErrorMessage = "Security code must be between 100 and 999")]
        [DisplayName("Security code")]
        public int SecurityCode { get; set; }

        public double Price { get; set; }
        public int OrderId { get; set; }
    }
}