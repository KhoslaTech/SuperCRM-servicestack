using ServiceStack.FluentValidation;
using SuperCRM.Models;
using SuperCRM.Request.Interactions;

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

	public class CreateInteractionValidator : AbstractValidator<CreateInteraction>
	{
		public CreateInteractionValidator(IValidator<Interaction> interactionValidator)
		{
			RuleFor(x => x).SetValidator(interactionValidator);
		}
	}

	public class EditInteractionValidator : AbstractValidator<EditInteraction>
	{
		public EditInteractionValidator(IValidator<Interaction> interactionValidator)
		{
			RuleFor(x => x.Id).NotEmpty();
			RuleFor(x => x).SetValidator(interactionValidator);
		}
	}
}
