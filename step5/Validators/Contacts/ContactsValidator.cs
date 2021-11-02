﻿using ServiceStack.FluentValidation;
using SuperCRM.Models;

namespace SuperCRM.Validators.Contacts
{
	public class ContactValidator : AbstractValidator<Contact>
	{
		public ContactValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(128);

			RuleFor(x => x.Phone)
				.MaximumLength(15);

			RuleFor(x => x.Email)
				.MaximumLength(75)
				.EmailAddress();

			RuleFor(x => x.Address1)
				.MaximumLength(128);

			RuleFor(x => x.Address2)
				.MaximumLength(128);

			RuleFor(x => x.AcquiredFrom)
				.NotEmpty()
				.MaximumLength(64);
		}
	}

	public class CreateContactValidator : ContactValidator
	{
		public CreateContactValidator()
		{
		}
	}

	public class EditContactValidator : ContactValidator
	{
		public EditContactValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();
		}
	}
}
