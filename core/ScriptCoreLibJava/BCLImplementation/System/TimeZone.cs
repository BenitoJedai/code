using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/timezone.cs


    [Script(Implements = typeof(global::System.TimeZone))]
    internal class __TimeZone
    {
        public long InternalGetOffset()
        {
            // http://stackoverflow.com/questions/11399491/java-timezone-offset
            // http://docs.oracle.com/javase/7/docs/api/java/util/TimeZone.html
            var tz = java.util.TimeZone.getDefault();

            return tz.getRawOffset();
        }

        public TimeSpan GetUtcOffset(DateTime time)
        {

            return TimeSpan.FromMilliseconds(InternalGetOffset());
        }



        public virtual DateTime ToLocalTime(DateTime time)
        {
            if (time.Kind == DateTimeKind.Local)
                return time;

            __DateTime x = time;

            var n = new __DateTime
            {
                Kind = DateTimeKind.Local
            };

            n.InternalValue = global::java.util.Calendar.getInstance();
            n.InternalValue.setTimeInMillis((x.Ticks - __DateTime.ticks_1970_1_1) / TimeSpan.TicksPerMillisecond + InternalGetOffset());

            return n;
        }

        public virtual DateTime ToUniversalTime(DateTime time)
        {
            if (time.Kind == DateTimeKind.Utc)
                return time;

            __DateTime x = time;

            var n = new __DateTime
            {
                Kind = DateTimeKind.Utc
            };

            n.InternalValue = global::java.util.Calendar.getInstance();
            n.InternalValue.setTimeInMillis((x.Ticks - __DateTime.ticks_1970_1_1) / TimeSpan.TicksPerMillisecond - InternalGetOffset());

            return n;
        }

        public static TimeZone CurrentTimeZone
        {
            get
            {

                return (TimeZone)(object)new __TimeZone();
            }
        }
    }
}
