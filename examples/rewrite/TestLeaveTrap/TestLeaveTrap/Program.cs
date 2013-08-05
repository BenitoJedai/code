using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLeaveTrap
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                goto __try;

            load:
                var x = "xxx";

                goto trap;
            __try:
                try
                {

                    Console.WriteLine();

                    goto load;
                }
                finally
                {
                    // release build will skip try block without this?
                    Console.WriteLine();
                }
            //throw null;

        trap:
                Console.WriteLine(value: x);
            }



            {
                goto __try;

            load:
                var x = "xxx";

                goto trap;
            __try:
                try
                {

                    Console.WriteLine();

                    goto load;
                }
                catch
                {
                    // release build will skip try block without this?
                    Console.WriteLine();
                    goto load;
                }
            //throw null;

trap:
                Console.WriteLine(value: x);
            }

            try
            {
                Console.WriteLine("try");
            }
            catch
            {
                Console.WriteLine("catch");
            }
            //finally
            //{
            //    Console.WriteLine("finally");
            //}

            //Unhandled Exception: System.Runtime.CompilerServices.RuntimeWrappedException: An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.
            //   at TestLeaveTrap.Program.Main(String[] args)

            Console.WriteLine("no trap");


            try
            {
                Console.WriteLine();
            }
            //catch
            //{
            //    Console.WriteLine();
            //}
            finally
            {
                Console.WriteLine();
            }

            //Unhandled Exception: System.Runtime.CompilerServices.RuntimeWrappedException: An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.
            //   at TestLeaveTrap.Program.Main(String[] args)

            Console.WriteLine("no trap via finally");


        }
    }
}
