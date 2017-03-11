using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace UrumiTech.Misc.BabyMonitorService
{
	public class TemperatureService : ITemperatureService
	{
		StringBuilder outputBuilder;
		StringBuilder errorBuilder;
		public TemperatureService()
		{
		}

		public async Task<string> GetTemperature()
		{
			
			Process proc = CreateTemperPollProcess();
			proc.Start();

			return await Task.Run(() => Readline(proc));
		}

		public string Readline(Process proc)
		{ 
			string line;
			while (!proc.StandardOutput.EndOfStream)
			{
				line = proc.StandardOutput.ReadLine();
				// do something with line
			}

			//Found 1 devices
			//Device #0: 26.1°C 78.9°F
			return line;

		}

		private Process CreateTemperPollProcess()
		{
			return new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "sudo",
					Arguments = "temper-poll",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
		}
}
}
