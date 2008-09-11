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
            // doubleclicking on the jar will not show the console

            Text = "Hello World";

            var dot = "...";

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
