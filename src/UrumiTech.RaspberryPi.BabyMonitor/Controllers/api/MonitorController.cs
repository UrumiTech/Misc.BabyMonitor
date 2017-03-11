using System.Web.Http;
using System.Web.Mvc;
using UrumiTech.Misc.BabyMonitorLogic;

namespace UrumiTech.RaspberryPi.BabyMonitor.Controllers
{
	public class MonitorController : ApiController
	{
		IBabyTemperatureMonitorManager _TemparatureManager;
		public MonitorController(IBabyTemperatureMonitorManager tempManager)
		{
			_TemparatureManager = tempManager;
		}



		
	}
}
