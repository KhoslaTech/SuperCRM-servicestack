using ASPSecurityKit;
using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Models;
using SuperCRM.Response;
using System.Runtime.Serialization;

namespace SuperCRM.Request.Self
{
	[DataContract]
	[Route("/sign-in", "POST")]
	[AllowAnonymous]
	public class SignIn : IReturn<BaseRecordResponse<AppUserDetails>>
	{
		[DataMember]
		public string Email { get; set; }
		[DataMember]
		public string Password { get; set; }
		[DataMember]
		public bool RememberMe { get; set; }
	}

	[DataContract]
	[Route("/sign-out", "POST")]
	[Feature(ApplyTo.Post, RequestFeature.AuthorizationNotRequired, RequestFeature.MFANotRequired)]
	public class SignOut : IReturn<BaseResponse>
	{
	}
}