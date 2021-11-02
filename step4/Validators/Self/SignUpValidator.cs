using ServiceStack.FluentValidation;
using SuperCRM.Request.Self;

namespace SuperCRM.Validators.Self
{
	public class SignUpValidator : AbstractValidator<SignUp>
	{
		public SignUpValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(60);

			RuleFor(x => x.Email)
				.NotEmpty()
				.MaximumLength(128)
				.EmailAddress();

			RuleFor(x => x.Password)
				.NotEmpty()
				.MaximumLength(512)
				.MinimumLength(6);

			RuleFor(x => x.BusinessName)
				.NotEmpty()
				.When(x => x.Type == AccountType.Team)
				.WithMessage("Business Name is required.")
				.MaximumLength(128);
		}
	}
}