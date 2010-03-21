using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace UltraApplicationWithAssets2
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{

			new MyCanvas().ToWindow().ShowDialog();

			//global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
			//    typeof(Application)
			//);
		}
	}
}
