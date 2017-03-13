using System;
using System.Threading.Tasks;
using System.Web.Http;
using UrumiTech.Misc.BabyMonitorLogic;

namespace UrumiTech.RaspberryPi.BabyMonitor.Controllers
{
	public class MonitorController : ApiController
	{
		IBabyTemperatureManager _TemperatureManager;
		public MonitorController(IBabyTemperatureManager tempManager)
		{
			_TemperatureManager = tempManager;
		}


		public async Task<IHttpActionResult> GetTemperature()
		{

			try 
			{ 
				return Ok(await _TemperatureManager.GetTemperature()); 
			}
			catch (Exception ex)
			{
				return BadRequest( await ReturnError(ex));
			}
		}

		public async Task<IHttpActionResult> GetTestString()
		{
			try 
			{ 
				return Ok(await _TemperatureManager.RunTest()); 
			}
			catch (Exception ex) 
			{ 
				return BadRequest(await ReturnError(ex)); 
			}
		}


		private Task<string> ReturnError(Exception ex) 
		{ 
			return Task.Run(()=> string.Format("Error Message: {0}", ex.Message));
		}


	}
}
