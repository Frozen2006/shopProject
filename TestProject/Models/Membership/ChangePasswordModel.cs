using System.ComponentModel.DataAnnotations;

namespace iTechArt.Shop.Web.Models
{
	public class ChangePasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Old Password")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Value \"{0}\" must have more then {2} chars.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("Password", ErrorMessage = "Password and confirm password values is not equal!")]
		public string ConfirmPassword { get; set; }

		public bool IsSuccess { get; set; }
	}
}