using ASPSecurityKit;
using ASPSecurityKit.ServiceStack;
using SuperCRM.DataModels;
using SuperCRM.Request.Self;
using SuperCRM.Response;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SuperCRM.Repositories;

namespace SuperCRM.ServiceInterface
{
    public class SelfService : ServiceBase
    {
        private readonly IAuthSessionProvider authSessionProvider;
        private readonly IUserPermitRepository permitRepository;

		public SelfService(IUserService<Guid, Guid, DbUser> userService,
            IAuthSessionProvider authSessionProvider, IConfig config,
            IServiceStackSecuritySettings securitySettings, IUserPermitRepository permitRepository)
            : base(userService, securitySettings, config)
		{
			this.authSessionProvider = authSessionProvider;
			this.permitRepository = permitRepository;
		}

        public async Task<BaseResponse> Post(SignUp request)
        {
            var dbUser = await this.UserService.NewUserAsync(request.Email, request.Password, request.Name);

            dbUser.Id = Guid.NewGuid();
            if (request.Type == AccountType.Team)
                dbUser.BusinessDetails = new DbBusinessDetails { Name = request.BusinessName };

            if (await this.UserService.CreateAccountAsync(dbUser))
            {
	            await this.permitRepository.AddPermitAsync(dbUser.Id, "Customer", dbUser.Id);
				await SendVerificationMailAsync(dbUser);
                var result = await this.authSessionProvider.LoginAsync(request.Email, request.Password, false, this.SecuritySettings.LetSuspendedAuthenticate);

                return Ok(PopulateCurrentUserDetails(result));
            }

            return Error(AppOpResult.UsernameAlreadyExists, "An account with this email is already registered.");
        }

        private async Task SendVerificationMailAsync(DbUser user)
        {
	        // to use Gmail, you need to enable "Less secure app access" etc. for more information, visit https://support.google.com/a/answer/176600?hl=en#zippy=%2Cuse-the-restricted-gmail-smtp-server%2Cuse-the-gmail-smtp-server
	        var username = "<YourGMailSmtpUsername>";
	        var password = "<YourGMailSmtpPassword>";
	        var host = "smtp.gmail.com";
	        var verificationUrl = $"<verificationUrl>/{user.VerificationToken}";

	        var mail = new MailMessage { From = new MailAddress(username) };
	        mail.To.Add(user.Username);
	        mail.Subject = "Verify your email";
	        mail.Body = $@"<p>Hi {user.Name},<br/>Please click the link below to verify your email.<br/><a href='{verificationUrl}'>{verificationUrl}</a><br/>Thank you!</p>";
	        mail.IsBodyHtml = true;

	        var smtp = new SmtpClient(host, 587)
	        {
		        Credentials = new NetworkCredential(username, password),
		        EnableSsl = true
	        };

	        await smtp.SendMailAsync(mail).ConfigureAwait(false);
        }


        public async Task<BaseResponse> Post(SignIn request)
        {
	        var result = await this.authSessionProvider.LoginAsync(request.Email, request.Password, request.RememberMe, false)
		        .ConfigureAwait(false);
	        switch (result.Result)
	        {
		        case OpResult.Success:
			        return Ok(PopulateCurrentUserDetails(result));
		        case OpResult.Suspended:
			        return Error(result.Result, "This account is suspended.");
		        case OpResult.PasswordBlocked:
			        return Error(result.Result, "Your password is blocked. Please reset the password using the 'forgot password' option.");
		        default:
			        return Error(OpResult.InvalidInput, "The email or password provided is incorrect.");
	        }
        }

        public async Task<BaseResponse> Post(SignOut request)
        {
	        await this.authSessionProvider.LogoutAsync().ConfigureAwait(false);
	        return Ok();
        }

        public async Task<BaseResponse> Post(Verify request)
        {
	        switch (await this.UserService.VerifyAccountAsync(request.Token))
	        {
		        case OpResult.Success:
			        return Ok();
		        case OpResult.AlreadyDone:
			        return Error(OpResult.AlreadyDone, "Account already verified");
		        default:
			        return Error(AppOpResult.InvalidToken, "Verification was not successful; please try again.");
	        }
        }

        public async Task<BaseResponse> Post(ResendVerificationEmail request)
        {
	        if (this.UserService.IsAuthenticated && this.UserService.IsVerified)
	        {
		        return Error(OpResult.AlreadyDone, "Account already verified");
	        }

	        await SendVerificationMailAsync(this.UserService.CurrentUser);
	        return Ok();
        }
    }
}
