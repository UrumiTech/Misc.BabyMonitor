using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace UrumiTech.RaspberryPi.BabyMonitor
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			AutoFacConfig.SetupContainer();
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			GlobalConfiguration.Configuration.EnsureInitialized();

		}
	}
}
