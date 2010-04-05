using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublishingXAML.Data;

namespace PublishingXAML
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var p = new DrawingSource();

			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
		}
	}
}
