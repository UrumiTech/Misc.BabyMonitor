using System;
using System.Threading.Tasks;

namespace UrumiTech.Misc.BabyMonitorLogic
{
	public interface IBabyTemperatureMonitorManager
	{
		Task<string> GetTemperature();
	}
}
