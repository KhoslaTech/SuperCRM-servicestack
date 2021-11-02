using ServiceStack.FluentValidation;
using SuperCRM.Request.Self;

namespace SuperCRM.Validators.Self
{
	public class VerifyValidator : AbstractValidator<Verify>
	{
		public VerifyValidator()
		{
			RuleFor(x => x.Token).NotEmpty();
		}
	}
}
