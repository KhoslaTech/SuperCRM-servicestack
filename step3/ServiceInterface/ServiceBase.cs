using System;
using System.Collections.Generic;
using SuperCRM.Models;
using SuperCRM.Response;
using SuperCRM.DataModels;
using ASPSecurityKit;
using ASPSecurityKit.ServiceStack;
using ServiceStack;

namespace SuperCRM.ServiceInterface
{
	public class ServiceBase : Service
	{
		protected readonly IUserService<Guid, Guid, DbUser> UserService;
		protected readonly IServiceStackSecuritySettings SecuritySettings;
		protected readonly IConfig Config;

		public ServiceBase(IUserService<Guid, Guid, DbUser> userService,
			IServiceStackSecuritySettings securitySettings, IConfig config)
		{
			this.UserService = userService;
			this.SecuritySettings = securitySettings;
			this.Config = config;
		}

		protected virtual AppUserDetails PopulateCurrentUserDetails(LoginResult result = null)
		{
			var userDetails = new AppUserDetails
			{
				Id = this.UserService.CurrentUser.Id,
				Name = this.UserService.CurrentUser.Name,
				Username = this.UserService.CurrentUser.Username,
				SessionId = result != null ? IdentityTokenType.GetToken(result.Auth.AuthUrn) : null,
				Secret = result?.Auth.Secret,
				VerificationRequired = this.SecuritySettings.MustHaveBeenVerified && !this.UserService.CurrentUser.Verified,
				CreatedDate = this.UserService.CurrentUser.CreatedDate
			};

			return userDetails;
		}

		protected BaseResponse Ok() => new BaseResponse();

		protected BaseIdResponse Ok(Guid id) => new BaseIdResponse { Id = id };

		protected BaseRecordResponse<T> Ok<T>(T record) => new BaseRecordResponse<T> { Record = record };

		protected BaseListResponse<T> Ok<T>(IList<T> records) => new BaseListResponse<T> { Records = records };

		protected BaseListResponse<T> Ok<T>(List<T> records) => new BaseListResponse<T> { Records = records };

		protected BaseListResponse<T> Ok<T>(IList<T> records, long totalCount) => new BaseListResponse<T> { Records = records, TotalCount = totalCount };

		protected BaseResponse Error(string code) => BaseResponse.Error(code);

		protected BaseResponse Error(string code, string message) => BaseResponse.Error(code, message);
	}
}