using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Models;
using SuperCRM.Response;
using System.Runtime.Serialization;

namespace SuperCRM.Request.Self
{
	[DataContract]
	[Route("/self/change-email", "POST")]
	[SkipActivityAuthorization]
	[VerificationNotRequired]
	public class ChangeEmail : IReturn<BaseRecordResponse<AppUserDetails>>
	{
		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string Password { get; set; }
	}
}
