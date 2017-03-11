using System;
using System.Threading.Tasks;
using UrumiTech.Misc.BabyMonitorService;

namespace UrumiTech.Misc.BabyMonitorLogic
{
	public class BabyTemperatureManager:IBabyTemperatureMonitorManager
	{
		ITemperatureService _TemperatureService;

		public BabyTemperatureManager(ITemperatureService tempservice)
		{
			_TemperatureService = tempservice;
		}

		public async Task<string> GetTemperature()
		{
			return await _TemperatureService.GetTemperature();
		}
	}
}
