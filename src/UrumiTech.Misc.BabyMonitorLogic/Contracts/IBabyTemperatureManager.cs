using System;
using System.Threading.Tasks;

namespace UrumiTech.Misc.BabyMonitorLogic
{
	public interface IBabyTemperatureManager
	{
		Task<string> GetTemperature();
		Task<string> RunTest();
	}
}
