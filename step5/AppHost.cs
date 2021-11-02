using Funq;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Validation;
using SuperCRM.ServiceInterface;

namespace SuperCRM
{
	public class AppHost : AppHostBase
	{
		/// <summary>
		/// Base constructor requires a Name and Assembly where web service implementation is located
		/// </summary>
		public AppHost()
			: base(nameof(SuperCRM), typeof(ServiceBase).Assembly) { }

		/// <summary>
		/// Application specific configuration
		/// This method should initialize any IoC resources utilized by your web service classes.
		/// </summary>
		public override void Configure(Container funqContainer)
		{
			ASPSecurityKitConfiguration.Configure(this, funqContainer);

			// To serialize as yyyy-mm-ddTh:m:s.fffffZ dateTime.toString("o")
			JsConfig.DateHandler = DateHandler.ISO8601;

			// for kind.unspecified to be assumed as utc
			JsConfig.AssumeUtc = true;

			// to forbid datetime.toLocal() after deserialization and to always serialize as utc (latter isn't confirmed).
			JsConfig.AlwaysUseUtc = true;

			SetConfig(new HostConfig { UseCamelCase = true });

			this.Plugins.Add(new ValidationFeature());
		}

	}
}