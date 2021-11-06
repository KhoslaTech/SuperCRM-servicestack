using System;
using System.Runtime.Serialization;

namespace SuperCRM.Models
{
	public class Contact
	{
		[DataMember]
		public Guid? Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Phone { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string Address1 { get; set; }

		[DataMember]
		public string Address2 { get; set; }

		[DataMember]
		public string AcquiredFrom { get; set; }

		[DataMember]
		public string Notes { get; set; }

		[DataMember]
		public DateTime CreatedDate { get; set; }

		[DataMember]
		public string CreatedByName { get; set; }
	}
}