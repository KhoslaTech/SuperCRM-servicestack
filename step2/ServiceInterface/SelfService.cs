using ASPSecurityKit;
using ASPSecurityKit.ServiceStack;
using SuperCRM.DataModels;
using SuperCRM.Request.Self;
using SuperCRM.Response;
using System;
using System.Threading.Tasks;

namespace SuperCRM.ServiceInterface
{
    public class SelfService : ServiceBase
    {
        private readonly IAuthSessionProvider authSessionProvider;

        public SelfService(IUserService<Guid, Guid, DbUser> userService,
            IAuthSessionProvider authSessionProvider, IConfig config,
            IServiceStackSecuritySettings securitySettings)
            : base(userService, securitySettings, config)
        {
            this.authSessionProvider = authSessionProvider;
        }

        public async Task<BaseResponse> Post(SignUp request)
        {
            var dbUser = await this.UserService.NewUserAsync(request.Email, request.Password, request.Name);

            dbUser.Id = Guid.NewGuid();
            if (request.Type == AccountType.Team)
                dbUser.BusinessDetails = new DbBusinessDetails { Name = request.BusinessName };

            if (await this.UserService.CreateAccountAsync(dbUser))
            {
                var result = await this.authSessionProvider.LoginAsync(request.Email, request.Password, false, this.SecuritySettings.LetSuspendedAuthenticate);

                return Ok(PopulateCurrentUserDetails(result));
            }

            return Error(AppOpResult.UsernameAlreadyExists, "An account with this email is already registered.");
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
    }
}
