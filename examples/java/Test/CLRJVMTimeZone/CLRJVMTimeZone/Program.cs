using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;

namespace CLRJVMTimeZone
{

    public class Program
    {
        public static void Main(string[] args)
        {
            //            java.lang.Object
            //{ Ticks = 72000000000, z = 0.02:00:00 }
            //{ utc = 26.12.2013 23:22:25, Kind = 1 }
            //{ loc = 27.12.2013 01:22:25, Kind = 2 }
            //running inside CLR


            // { Ticks = 72000000000, z = 02:00:00, utc = 12/26/2013 11:14:40 PM, loc = 12/27/2013 1:14:40 AM }
            Console.WriteLine(typeof(object));
            var now = DateTime.Now;
            var z = TimeZone.CurrentTimeZone.GetUtcOffset(now);

            //System.Object
            //{ Ticks = 72000000000, z = 02:00:00 }
            //{ utc = 12/26/2013 11:17:02 PM, Kind = Utc }
            //{ loc = 12/27/2013 1:17:02 AM, Kind = Local }
            //running inside CLR


            Console.WriteLine(
                new
                {
                    z.Ticks,
                    z,
                }
            );

            var utc = now.ToUniversalTime();
            Console.WriteLine(new { utc, utc.Kind });
            var loc = utc.ToLocalTime();
            Console.WriteLine(new { loc, loc.Kind });




            CLRProgram.CLRMain();
        }


    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.ReadKey();
        }
    }
}
