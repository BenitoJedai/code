using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDateTimeStruct
{
    public class Class1
    {
        public const long TicksPerMillisecond = 10000;
        public const long ticks_1970_1_1 = 621355968000000000;


        public static DateTime DateTimeConvertFromInt64(long TotalMilliseconds)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversionsForStopwatch.cs

            // for SQLite
            var ticks = TotalMilliseconds * TicksPerMillisecond + ticks_1970_1_1;

            //Additional information: Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.

            //if (DateTime.MaxValue.Ticks)
            var value = new DateTime(ticks: ticks, kind: DateTimeKind.Utc);

            //Console.WriteLine("DateTimeConvertFromInt64 " + new { value.Kind, value });

            return value;
        }
    }
}
