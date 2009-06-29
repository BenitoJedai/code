using System.Threading;
using System;

using ScriptCoreLib;
using jni;


namespace NativePluginExample.source.java
{
	[Script]
	public class Program
	{
		public static string Text;

		public static void Main(string[] args)
		{
			// doubleclicking on the jar will not show the console


			Console.WriteLine("This program will load jni native dll - " + CPtrLibrary.LibraryPath);

			TestLIBC();
		}



		private static void TestLIBC()
		{
			string libc = "msvcrt.dll";

			var printf = new CFunc(libc, "printf");

			printf.callInt("via [msvcrt.dll] printf();\r\n");



		}

	}
}
