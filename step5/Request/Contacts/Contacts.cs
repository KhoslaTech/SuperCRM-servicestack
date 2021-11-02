using System;
using System.Runtime.Serialization;
using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Models;
using SuperCRM.Response;

namespace SuperCRM.Request.Contacts
{

	[DataContract]
	[Route("/contacts", "POST")]
	[PossessesPermissionCode]
	public class CreateContact : Contact, IReturn<BaseRecordResponse<Contact>>
	{
	}

	[DataContract]
	[Route("/contacts/{ContactId}", "DELETE")]
	public class DeleteContact : IReturn<BaseResponse>
	{
		[DataMember]
		public Guid ContactId { get; set; }
	}

	[DataContract]
	[Route("/contacts", "PUT")]
	public class EditContact : Contact, IReturn<BaseRecordResponse<Contact>>
	{
	}

	[DataContract]
	[Route("/contacts", "GET")]
	[PossessesPermissionCode]
	public class GetContacts : PageRequestBase, IReturn<BaseListResponse<Contact>>
	{
	}
}
