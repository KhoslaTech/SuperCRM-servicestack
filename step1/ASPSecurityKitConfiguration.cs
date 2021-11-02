using ASPSecurityKit;
using ASPSecurityKit.ServiceStack;
using Autofac;
using Funq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Web;
using SuperCRM.DataModels;
using SuperCRM.DependencyInjection;
using SuperCRM.Security;

namespace SuperCRM
{
	public class ASPSecurityKitConfiguration
	{
		public static bool IsDevelopmentEnvironment { get; set; }

		public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddHttpContextAccessor();
		}

		public static void Configure(IAppHost appHost, Container funqContainer)
		{
			License.TryRegisterFromExecutionPath();

			appHost.Plugins.Add(new ASPSecurityKitFeature());
			appHost.OnEndRequestCallbacks.Add(DisposeScope);

		}

		public static void ConfigureContainer(ContainerBuilder builder)
		{
			// Register all ASK components and auth definitions
			new ASPSecurityKitRegistry()
				.Register(new ASKContainerBuilder(builder));
			builder.RegisterModule<AppRegistry>();

		}


		private static void DisposeScope(IRequest httpReq)
		{
			var userService = httpReq.TryResolve<IUserService>();
			if (userService.IsAuthenticated)
			{
				var authSessionProvider = httpReq.TryResolve<CachedAuthSessionProvider>();
				authSessionProvider.SaveSession();
			}

			// disposes per-request dbContext
			httpReq.TryResolve<AppDbContext>().Dispose();
		}

	}
}