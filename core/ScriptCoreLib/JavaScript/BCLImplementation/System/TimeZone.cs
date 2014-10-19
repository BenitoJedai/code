using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/timezone.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/TimeZone.cs

    [Script(Implements = typeof(global::System.TimeZone))]
    internal class __TimeZone
    {
        // http://www.w3schools.com/jsref/jsref_utc.asp
        [Script(OptimizedCode = "return Date.UTC(2014,02,30) - new Date(2014,02,30).getTime();")]
        public long InternalGetOffset()
        {
            //Date.UTC(2012,02,30) - new Date(2012,02,30).getTime()
            //10800000
            return 0;
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

            return new __DateTime
            {
                Kind = DateTimeKind.Local,
                InternalValue = new IDate(x.InternalValue.getTime() + InternalGetOffset())
            };
        }

        public virtual DateTime ToUniversalTime(DateTime time)
        {
            if (time.Kind == DateTimeKind.Utc)
                return time;

            __DateTime x = time;

            return new __DateTime
            {
                Kind = DateTimeKind.Utc,
                InternalValue = new IDate(x.InternalValue.getTime() - InternalGetOffset())
            };
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
