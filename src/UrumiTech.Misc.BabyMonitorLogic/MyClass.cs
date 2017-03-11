using System;
using System.Diagnostics;

namespace UrumiTech.Misc.BabyMonitorLogic
{
	public class Mplayer
	{
		Process mplayer;

		public Mplayer(string path, string pipeName, int panelId)
		{
			String args = "";
			mplayer = new Process();
			mplayer.StartInfo.UseShellExecute = false;
			mplayer.StartInfo.RedirectStandardInput = true;
			mplayer.StartInfo.FileName = path;
			args = @"\\.\pipe\" + pipeName + " -demuxer +h264es -fps 120 -nosound -cache 512";
			args += " -nofs -noquiet -identify -slave ";
			args += " -nomouseinput -sub-fuzziness 1 ";
			args += " -vo direct3d, -ao dsound  -wid ";
			args += panelId;
			mplayer.StartInfo.Arguments = args;
		}

		public void Start()
		{
			mplayer.Start();
		}

		public void End()
		{
			mplayer.Kill();
		}
	}
}
