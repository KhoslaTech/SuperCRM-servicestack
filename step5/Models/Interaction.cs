using System;
using SuperCRM.DataModels;

namespace SuperCRM.Models
{
	public class Interaction
	{
		public Guid? Id { get; set; }

		public Guid? ContactId { get; set; }

		public InteractionMethod Method { get; set; }

		public string MethodDetails { get; set; }

		public string Notes { get; set; }

		public DateTime InteractionDate { get; set; }

		public DateTime CreatedDate { get; set; }

		public string CreatedByName { get; set; }
	}
}
