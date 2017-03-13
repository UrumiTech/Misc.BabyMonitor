using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using UrumiTech.Misc.BabyMonitorLogic;
using UrumiTech.Misc.BabyMonitorService;

namespace UrumiTech.RaspberryPi.BabyMonitor
{
	public class AutoFacConfig
	{
		public AutoFacConfig()
		{
		}

		public static void SetupContainer()
		{
			var builder = new ContainerBuilder();
			var config = new HttpConfiguration();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<BabyTemperatureManager>().As<IBabyTemperatureManager>().InstancePerRequest();
			builder.RegisterType<TemperatureService>().As<ITemperatureService>().InstancePerRequest();
			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			GlobalConfiguration.Configuration.DependencyResolver = config.DependencyResolver;
		}
	}
}
