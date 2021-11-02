using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack;

namespace SuperCRM
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<IISServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			ASPSecurityKitConfiguration.ConfigureServices(services, Configuration);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			ASPSecurityKitConfiguration.ConfigureContainer(builder);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			ASPSecurityKitConfiguration.IsDevelopmentEnvironment = env.IsDevelopment();
			app.UseServiceStack(new AppHost
			{
				AppSettings = new NetCoreAppSettings(Configuration)
			});
		}
	}
}
