using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(Console))]
    internal class __Console
    {
        public static void Beep()
        {
            global::java.awt.Toolkit.getDefaultToolkit().beep();
        }

        public static void Write(string p)
        {
            global::java.lang.JavaSystem.@out.print(p);
        }

        public static void WriteLine()
        {
            WriteLine("");
        }

		public static void WriteLine(object e)
		{
			WriteLine("" + e);
		}

        public static void WriteLine(string e)
        {
            global::java.lang.JavaSystem.@out.println(e);
        }

        public static string ReadLine()
        {
            string z = null;

            try
            {
                var r0 = new global::java.io.InputStreamReader(global::java.lang.JavaSystem.@in);
                var r1 = new global::java.io.BufferedReader(r0);


                z = r1.readLine();
            }
            catch (Exception)
            {
                
            }

            return z;
        }

    }
}
