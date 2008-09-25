using System.Threading;
using System;

using ScriptCoreLib;
using jni;


namespace OrcasNativeJavaConsoleApplication.source.java
{
    [Script]
    public class Program
    {
        public static string Text;

        public static void Main(string[] args)
        {
            // doubleclicking on the jar will not show the console

            Text = "Will test LIBC now, which can call __cdecl API";

            Console.WriteLine(Text);
            
            TestLIBC();
        }

   

        private static void TestLIBC()
        {
            string libc = "msvcrt.dll";

            var printf = new CFunc(libc, "printf");

            int ires = printf.callInt(
                "\n<output from printf(): Running %s, eh?>\n", "macOS");

            Console.WriteLine("printf() returned " + ires);


            /* Little more complicated.  Firstly, ctime() takes a "int *" which
               points to an int containing the elapsed seconds since the epoch.
               So we will malloc() a 4 byte chunk with C's malloc, and initialize
               it the value we just got from time().  Secondly, ctime() returns a
               string, so be aware of that. */
            var timePtr = new CMalloc(4);
            timePtr.setInt(0, ires);
            var ctime = new CFunc(libc, "ctime");
            var returnedString = ctime.callCPtr(timePtr);
            Console.WriteLine("\nctime() reports " + returnedString.getString(0));
            /* We malloc()ed something from C heap, so we have to free() it. */
            timePtr.free();


            ///* Read first word from stdin with scanf(). */
            //Console.WriteLine("\nPlease type something and then hit <return>");
            //var scanf = new CFunc(libc, "scanf");
            //var cbuf = new CMalloc(128);
            //ires = scanf.callInt("%s", cbuf);
            //Console.WriteLine("scanf() says first word you typed is \"" +
            //           cbuf.getString(0) + "\"");
            ///* malloc()ed memory must be freed! */
            //cbuf.free();


            /* Caculate C's sin(2.0) with Math.sin(2.0). */
            var sin = new CFunc(libc, "sin");
            double dres = sin.callDouble(2.0);
            Console.WriteLine("\nC's  sin(2.0) = " + dres);
            Console.WriteLine("Math.sin(2.0) = " + Math.Sin(2.0));


            /* clock().  Takes no arguments. */
            var clock = new CFunc(libc, "clock");
            Console.WriteLine("\nclock() returned " + clock.callInt(0));
        }

    }
}
