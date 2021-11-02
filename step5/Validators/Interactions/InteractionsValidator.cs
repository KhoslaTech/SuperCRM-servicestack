using ServiceStack.FluentValidation;
using SuperCRM.Models;

namespace SuperCRM.Validators.Interactions
{
	public class InteractionValidator : AbstractValidator<Interaction>
	{
		public InteractionValidator()
		{
			RuleFor(x => x.ContactId).NotEmpty();
			RuleFor(x => x.MethodDetails).MaximumLength(256);
		}
	}

	public class CreateInteractionValidator : InteractionValidator
	{
		public CreateInteractionValidator()
		{
		}
	}

	public class EditInteractionValidator : InteractionValidator
	{
		public EditInteractionValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
		}
	}
}
