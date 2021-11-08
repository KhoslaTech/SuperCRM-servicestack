using System;
using System.Runtime.Serialization;
using SuperCRM.DataModels;

namespace SuperCRM.Models
{
	public class Interaction
	{
		[DataMember]
		public Guid? Id { get; set; }

		[DataMember]
		public Guid? ContactId { get; set; }

		[DataMember]
		public InteractionMethod Method { get; set; }

		[DataMember]
		public string MethodDetails { get; set; }

		[DataMember]
		public string Notes { get; set; }

		[DataMember]
		public DateTime InteractionDate { get; set; }

		[DataMember]
		public DateTime CreatedDate { get; set; }

		[DataMember]
		public string CreatedByName { get; set; }
	}
}
