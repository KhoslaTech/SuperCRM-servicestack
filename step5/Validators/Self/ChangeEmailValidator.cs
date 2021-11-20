using ServiceStack.FluentValidation;
using SuperCRM.Request.Self;

namespace SuperCRM.Validators.Self
{
	public class ChangeEmailValidator : AbstractValidator<ChangeEmail>
	{
		public ChangeEmailValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.MaximumLength(100)
				.EmailAddress();

			RuleFor(x => x.Password)
				.NotEmpty();
		}
	}
}