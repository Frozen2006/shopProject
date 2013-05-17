using Mvc.Mailer;
using TestProject.Mailers.Models;

namespace TestProject.Mailers
{ 
    public interface IPasswordResetMailer
    {
			MvcMailMessage PasswordReset(MailerModel model);
	}
}