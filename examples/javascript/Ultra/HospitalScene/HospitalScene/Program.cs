using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace HospitalScene
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
#if DEBUG
			new MyCanvas().ToWindow().ShowDialog();
#else			
			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
#endif
		}
	}
}
