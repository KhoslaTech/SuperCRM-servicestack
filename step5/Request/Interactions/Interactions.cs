using System;
using System.Runtime.Serialization;
using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Models;
using SuperCRM.Response;

namespace SuperCRM.Request.Interactions
{
	[DataContract]
	[Route("/interactions", "POST")]
	public class CreateInteraction : Interaction, IReturn<BaseRecordResponse<Interaction>>
	{
	}

	[DataContract]
	[Route("/interactions/{InteractionId}", "DELETE")]
	public class DeleteInteraction : IReturn<BaseResponse>
	{
		[DataMember]
		public Guid InteractionId { get; set; }
	}

	[DataContract]
	[Route("/interactions", "PUT")]
	public class EditInteraction : Interaction, IReturn<BaseRecordResponse<Interaction>>
	{
	}

	[DataContract]
	[Route("/interactions", "GET")]
	[Route("/contacts/{ContactId}/interactions", "GET")]
	[PossessesPermissionCode]
	public class GetInteractions : PageRequestBase, IReturn<BaseListResponse<Interaction>>
	{
		[DataMember]
		public Guid? ContactId { get; set; }
	}
}
