using System.Runtime.Serialization;
using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Response;

namespace SuperCRM.Request.Self
{
	[DataContract]
	[Route("/self/verification-email", "POST")]
	[SkipActivityAuthorization]
	[VerificationNotRequired]
	public class ResendVerificationEmail : IReturn<BaseResponse>
	{
	}
}
