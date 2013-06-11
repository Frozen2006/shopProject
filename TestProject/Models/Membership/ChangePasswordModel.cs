using System.ComponentModel.DataAnnotations;

namespace iTechArt.Shop.Web.Models
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value \"{0}\" must have more then {2} chars.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password confirm")]
        [Compare("Password", ErrorMessage = "Password and confirm password values is not equal!")]
        public string ConfirmPassword { get; set; }

        public bool IsSuccess { get; set; }
    }
}