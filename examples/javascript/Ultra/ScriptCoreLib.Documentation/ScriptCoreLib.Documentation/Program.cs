using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Documentation.Documentation;

namespace ScriptCoreLib.Documentation
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			//var c = new Compilation();

			//foreach (var item in c.GetArchives())
			//{
			//    Console.WriteLine(item.Name);

			//    foreach (var item2 in item.GetAssemblies())
			//    {
			//        Console.WriteLine("  " + item2.Name);

			//    }
			//}

			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
		}
	}
}
