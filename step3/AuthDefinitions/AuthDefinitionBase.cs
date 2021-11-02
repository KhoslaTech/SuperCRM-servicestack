using SuperCRM.DataModels;
using ASPSecurityKit.Authorization;
using ASPSecurityKit.ServiceStack;

namespace SuperCRM.AuthDefinitions
{
	public abstract class AuthDefinitionBase : IAuthorizeRequestDefinitionsContainer
	{
		protected readonly AppDbContext dbContext;
		protected readonly IEntityIdAuthorizer entityIdAuthorizer;

		protected AuthDefinitionBase(AppDbContext dbContext, IEntityIdAuthorizer entityIdAuthorizer)
		{
			this.dbContext = dbContext;
			this.entityIdAuthorizer = entityIdAuthorizer;
		}
	}
}