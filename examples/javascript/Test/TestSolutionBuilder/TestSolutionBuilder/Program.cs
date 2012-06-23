using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSolutionBuilder
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
		}
	}
}
