using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLeaveWithReturn
{
    class Program
    {
        static void Main(string[] args)
        {
            XMain(args);

        }
        static object XMain(string[] args)
        {
            // IL_00ec:  leave      IL_00f1
            //  }  // end handler
            //  IL_00f1:  leave      IL_00fd
            //  IL_00f6:  leave      IL_00fb
            //}  // end handler
            //IL_00fb:  ldnull
            //IL_00fc:  throw
            //IL_00fd:  nop


            try
            {
                if (args.Length == 2)
                    throw null;

                return null;
            }
            catch
            {
                if (args.Length == 1)
                    return null;

                try
                {

                    return null;
                }
                catch
                {
                    //Debugger.Break();

                    return null;
                }
            }

            Console.WriteLine("no trap");

            return null;
        }
    }
}
