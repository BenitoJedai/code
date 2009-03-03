using System.Threading;
using System;

using ScriptCoreLib;


namespace OrcasJavaConsoleApplication.source.java
{
    [Script]
    public class Program
    {
		public static string Text { get; set; }


        public static void Main(string[] args)
        {
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

            // doubleclicking on the jar will not show the console
			unchecked
			{
				sbyte a = (sbyte)0x7F;
				sbyte b = (sbyte)0xFF;

				short ax = (short)((a & 0xff) << 8);
				short bx = (short)((b & 0xff) << 0);

				Console.WriteLine("ax: " + ax);
				Console.WriteLine("bx: " + bx);
			}

			// ax: 32512
			// bx: 255

			//ax: 32512
			//bx: 255

            Text = "Hello World";

            var dot = "..";

            for (int i = 0; i < 2; i++)
            {
                Thread.Sleep(500);

                Console.Write(dot);
                Console.Beep();
            }

            Console.WriteLine(Text);
        }
    }
}
