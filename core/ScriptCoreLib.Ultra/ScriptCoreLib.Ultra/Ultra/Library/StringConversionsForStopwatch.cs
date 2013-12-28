using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Data;
using ScriptCoreLib.Extensions;
using System.Diagnostics;
using ScriptCoreLib.Shared.BCLImplementation.System.Diagnostics;
namespace ScriptCoreLib.Library
{
    public static class StringConversionsForStopwatch
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224

        public static string ConvertToString(Stopwatch e)
        {
            if (e == null)
                return null;


            // http://www.w3schools.com/tags/tag_th.asp
            var x = new XElement("Stopwatch",

                new XAttribute("ElapsedMilliseconds", "" + e.ElapsedMilliseconds),
                new XAttribute("IsRunning", "" + e.IsRunning)

            );

            Console.WriteLine("StringConversionsForStopwatch.ConvertToString " + new { e.ElapsedMilliseconds, e.IsRunning });

            return x.ToString();
        }


        public static string DateTimeConvertToString(DateTime e)
        {
            var x = e;
            Console.WriteLine("DateTimeConvertToString " + new { x });

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131226-datetime
            return "" + DateTimeConvertToInt64(e);
        }

        public const long TicksPerMillisecond = 10000;
        public const long ticks_1970_1_1 = 621355968000000000;


        public static long DateTimeConvertToInt64(DateTime e)
        {
            var ticks = e.ToUniversalTime().Ticks;

            // for SQLite
            var TotalMilliseconds = (long)(
                // jsc, why dont i get just the long
                (ticks - ticks_1970_1_1) / (double)TicksPerMillisecond
                );


            return TotalMilliseconds;
        }

        public static DateTime DateTimeConvertFromString(string e)
        {
            Console.WriteLine("DateTimeConvertFromString " + new { e });

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131226-datetime
            return DateTimeConvertFromInt64(
                long.Parse(e)
            );
        }

        public static DateTime DateTimeConvertFromObject(object e)
        {
            Console.WriteLine("DateTimeConvertFromObject " + new { e });

            //        Convert.ToInt64("0")	0	long
            //        Convert.ToInt64(default(string))	0	long
            //+		Convert.ToInt64("")	'Convert.ToInt64("")' threw an exception of type 'System.FormatException'	long {System.FormatException}

            if (e == null)
                return DateTime.Now;

            if (e is long)
            {
                // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs

                return DateTimeConvertFromInt64((long)(e));
            }

            var s = e as string;

            if (s != null)
            {
                if (s == "")
                    return DateTime.Now;
            }

            return DateTimeConvertFromInt64(Convert.ToInt64(s));
        }

        public static DateTime DateTimeConvertFromInt64(long TotalMilliseconds)
        {
            // for SQLite
            var ticks = TotalMilliseconds * TicksPerMillisecond + ticks_1970_1_1;

            //Additional information: Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.

            //if (DateTime.MaxValue.Ticks)
            var value = new DateTime(ticks: ticks, kind: DateTimeKind.Utc);

            Console.WriteLine("DateTimeConvertFromInt64 " + new { value.Kind, value });

            return value;
        }


        public static Stopwatch ConvertFromString(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;


            // DataTable.ReadXML?
            var x = XElement.Parse(e);

            var ElapsedMilliseconds = Convert.ToInt64(x.Attribute("ElapsedMilliseconds").Value);
            var IsRunning = Convert.ToBoolean(x.Attribute("IsRunning").Value);

            Console.WriteLine("StringConversionsForStopwatch.ConvertFromString CreateStopwatchAtElapsed " + new { ElapsedMilliseconds, IsRunning, x });

            //StringConversionsForStopwatch.ConvertFromString CreateStopwatchAtElapsed { ElapsedMilliseconds = 329, IsRunning = True }
            //enter CLR_StringConversionsForStopwatchExtensions.StopwatchAtElapsed. this shall only happen in clr. { IsAttached = True }
            //StringConversionsForStopwatch.ConvertFromString { ElapsedMilliseconds = 0, IsRunning = False }

            // can we also wiretransfer TimeSpan yet?
            var n = StopwatchExtensions.CreateStopwatchAtElapsed(
                TimeSpan.FromMilliseconds(ElapsedMilliseconds)
            );

            if (!IsRunning)
                n.Stop();

            Console.WriteLine("StringConversionsForStopwatch.ConvertFromString " + new { n.ElapsedMilliseconds, n.IsRunning });

            return n;
        }



    }



    [Script(Implements = typeof(StopwatchExtensions))]
    public static class __StopwatchExtensions
    {
        public static Stopwatch CreateStopwatchAtElapsed(TimeSpan t)
        {
            // enter StringConversionsForStopwatchExtensions.StopwatchAtElapsed

            Console.WriteLine("enter __StopwatchExtensions.StopwatchAtElapsed");

            //enter __StopwatchExtensions.StopwatchAtElapsed
            //__StopwatchExtensions.StopwatchAtElapsed { InternalStart = 24.12.2013 12:13:45, t = 0.00:00:00 }
            //__StopwatchExtensions.StopwatchAtElapsed { InternalStart = { Ticks = 635234768256210000 }, Ticks = -137990144 }
            //__StopwatchExtensions.StopwatchAtElapsed { Elapsed = -1.-1:-1:-14, ticks = 635234768394200200 }

            //__StopwatchExtensions.StopwatchAtElapsed { InternalStart = 24.12.2013 12:17:06, t = 0.00:00:00 }
            //__StopwatchExtensions.StopwatchAtElapsed { InternalStart = { Ticks = 635234770269410000 }, Ticks = 1110000 }
            //__StopwatchExtensions.StopwatchAtElapsed { Elapsed = 0.00:00:00, ticks = 635234770268300000 }

            var n = new __Stopwatch();

            // this will cost 1ms?
            //Console.WriteLine("__StopwatchExtensions.StopwatchAtElapsed " + new { n.InternalStart, t });
            //Console.WriteLine("__StopwatchExtensions.StopwatchAtElapsed " + new { InternalStart = new { n.InternalStart.Ticks }, t.Ticks });

            //// cant to this just yet?
            ////n.InternalStart -= t;



            var ticks = n.InternalStart.Ticks - t.Ticks;

            n.InternalStart = new DateTime(
                ticks: ticks
            );

            n.IsRunning = true;

            Console.WriteLine("__StopwatchExtensions.StopwatchAtElapsed " + new
            {
                n.Elapsed,
                n.ElapsedMilliseconds,
                n.IsRunning,
                ticks
            });

            return (Stopwatch)(object)n;
        }
    }
}
