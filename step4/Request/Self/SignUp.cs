using ASPSecurityKit.ServiceStack;
using ServiceStack;
using SuperCRM.Models;
using SuperCRM.Response;
using System.Runtime.Serialization;

namespace SuperCRM.Request.Self
{
	public enum AccountType
	{
		Individual,
		Team,
	}

	[DataContract]
	[Route("/sign-up", "POST")]
	[AllowAnonymous]
	public class SignUp : IReturn<BaseRecordResponse<AppUserDetails>>
	{
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string Password { get; set; }

		[DataMember]
		public bool RememberMe { get; set; }

		[DataMember]
		public AccountType Type { get; set; }

		[DataMember]
		public string BusinessName { get; set; }
	}
}