using System.ComponentModel.DataAnnotations;

namespace iTechArt.Shop.Web.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value \"{0}\" must have more then {2} chars.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password confirm")]
        [Compare("Password", ErrorMessage = "Password and confirm password values is not equal!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

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