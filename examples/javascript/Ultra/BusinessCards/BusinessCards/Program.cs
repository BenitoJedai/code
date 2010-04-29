using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessCards
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			new global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram
			{
				DisableSplash = true,
				PrimaryApplication = typeof(Application)
			}.Launch();
		}
	}
}
