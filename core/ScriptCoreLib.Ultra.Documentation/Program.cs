using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Documentation
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
		}
	}
}
