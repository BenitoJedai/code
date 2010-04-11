using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreatingXElements
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(
				DocumentBuilder.Create().ToString()
			);

			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
		}
	}
}
