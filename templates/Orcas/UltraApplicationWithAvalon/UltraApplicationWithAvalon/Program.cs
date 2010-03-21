using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace UltraApplicationWithAvalon
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			var w = new ApplicationCanvas
			{
				WebService = new UltraWebService()
			}.ToWindow();

			w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

			w.ShowDialog();

			// we should do this in release build?
			//global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
			//    typeof(Application)
			//);
		}
	}
}
