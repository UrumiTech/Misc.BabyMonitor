using System;
using System.Threading.Tasks;

namespace UrumiTech.Misc.BabyMonitorService
{
	public interface ITemperatureService
	{
		Task<string> GetTemperature();
	}
}
