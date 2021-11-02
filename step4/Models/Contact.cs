using System;
using System.ComponentModel.DataAnnotations;

namespace SuperCRM.Models
{
	public class Contact
	{
		public Guid? Id { get; set; }

		public string Name { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string Address1 { get; set; }

		public string Address2 { get; set; }

		public string AcquiredFrom { get; set; }

		public string Notes { get; set; }

		public DateTime CreatedDate { get; set; }

		public string CreatedByName { get; set; }
	}
}
 