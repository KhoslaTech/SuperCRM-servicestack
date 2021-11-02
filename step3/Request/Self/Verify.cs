using System.Runtime.Serialization;
using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Response;

namespace SuperCRM.Request.Self
{
	[DataContract]
	[Route("/verify", "POST")]
	[AllowAnonymous]
	public class Verify : IReturn<BaseResponse>
	{
		[DataMember]
		public string Token { get; set; }
	}

}
