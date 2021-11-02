namespace SuperCRM.Models
{
	public class AppUserDetails : AppUser
	{
		public string SessionId { get; set; }
		public string Secret { get; set; }
		public bool VerificationRequired { get; set; }
	}
}