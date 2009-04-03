using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace OrcasNativeApplication
{
	[Script(Implements = typeof(global::System.Console))]
	internal class __Console
	{
		public static void Beep(int f, int d)
		{
			windows_h.Beep(f, d);
		}

		public static void Write(char c)
		{
			stdio_h.putchar(c);
		}

		public static void WriteLine(string e)
		{
			stdio_h.puts(e);

		}
	}

}
