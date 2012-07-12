using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UltraRaycast6
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			new global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram
			{
				PrimaryApplication = typeof(Application),

                //Verbose = true,

                //mxmlc = new FileInfo(@"C:\util\flex4\bin\mxmlc.exe"),
                //flashplayer = new FileInfo(@"C:\util\flex_sdk_4.6.0.23201B\runtimes\player\11.1\win\FlashPlayerDebugger.exe"),

			}.Launch();
		}
	}
}
