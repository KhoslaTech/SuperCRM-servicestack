using ServiceStack.FluentValidation;
using SuperCRM.Request.Self;

namespace SuperCRM.Validators.Self
{
	public class SignInValidator : AbstractValidator<SignIn>
	{
		public SignInValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.Password)
				.NotEmpty();
		}
	}
}