using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        public static void WriteLine(object e)
        {
            Console.WriteLine(e.ToString());
        }

        public static void WriteLine(string e)
        {
            Console.WriteLine(e);
        }

        public static void Write(string e)
        {
            Console.Write(e);
        }
    }
}
