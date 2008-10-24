using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {

		public static void InternalWriteLine(string e)
		{
			InternalWrite(e + Environment.NewLine);
		}

		public static void InternalWrite(string e)
		{
			Native.echo(e);
		}
 
        public static void WriteLine(object e)
        {
			InternalWriteLine(e.ToString());
        }

        public static void WriteLine(string e)
        {
			InternalWriteLine(e);
        }

        public static void WriteLine()
        {
			InternalWriteLine("");
        }

		//public static void WriteLine(string e, object x)
		//{
		//    __BrowserConsole.WriteLine(string.Format(e, x));
		//}

        public static void Write(string e)
        {
            InternalWrite(e);
        }

        public static void Write(object e)
        {
			InternalWrite(e.ToString());
        }
    }
}
