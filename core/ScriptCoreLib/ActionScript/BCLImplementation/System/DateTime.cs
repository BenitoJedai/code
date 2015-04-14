using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/datetime.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/DateTime.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/DateTime.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/DateTime.cs
    // https://github.com/sq/JSIL/blob/master/Proxies/DateTime.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\DateTime.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\DateTime.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\DateTime.cs

    [Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime
    {
        internal Date InternalValue;


        public __DateTime()
            : this(-1, -1, -1, -1, -1, -1)
        {

        }

        public __DateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.InternalValue = new Date();

            if (year == -1)
            {

            }
            else
            {
                this.InternalValue.setFullYear(year, month - 1, day);
                this.InternalValue.setHours(hour, minute, second, 0);
            }
        }

        public static __DateTime Now
        {
            get
            {
                return new __DateTime();
            }
        }

        public override string ToString()
        {
            var w = new StringBuilder();

            w.Append(this.Day.ToString().PadLeft(2, '0'));
            w.Append(".");
            w.Append(this.Month.ToString().PadLeft(2, '0'));
            w.Append(".");
            w.Append(this.Year.ToString().PadLeft(4, '0'));
            w.Append(" ");
            w.Append(this.Hour.ToString().PadLeft(2, '0'));
            w.Append(":");
            w.Append(this.Minute.ToString().PadLeft(2, '0'));
            w.Append(":");
            w.Append(this.Second.ToString().PadLeft(2, '0'));

            return w.ToString();
        }

        public long Ticks
        {
            get
            {
                // conversion needed

                var ms = (long)this.InternalValue.getTime();

                return ms * TicksPerMillisecond + ticks_1970_1_1;
            }
        }

        private const long TicksPerMillisecond = 0x10000;

        public const long ticks_1970_1_1 = 621355968000000000;



        public int Second { get { return Convert.ToInt32(this.InternalValue.getSeconds()); } }
        public int Minute { get { return Convert.ToInt32(this.InternalValue.getMinutes()); } }
        public int Hour { get { return Convert.ToInt32(this.InternalValue.getHours()); } }
        public int Day { get { return Convert.ToInt32(this.InternalValue.getDate()); } }
        public int Month { get { return Convert.ToInt32(this.InternalValue.getMonth()) + 1; } }
        public int Year { get { return Convert.ToInt32(this.InternalValue.getFullYear()); } }

        public static TimeSpan operator -(__DateTime d1, __DateTime d2)
        {
            // tested by
            // X:\jsc.svn\examples\actionscript\Test\TestDateTimeToTimeSpan\TestDateTimeToTimeSpan\ApplicationCanvas.cs
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\TimeSpan.cs

            return TimeSpan.FromMilliseconds(d1.InternalValue.getTime() - d2.InternalValue.getTime());
        }






        public static implicit operator DateTime(__DateTime e)
        {
            return (DateTime)(object)e;
        }
        public static implicit operator __DateTime(DateTime e)
        {
            return (__DateTime)(object)e;
        }

        public DateTime ToLocalTime()
        {
            //return TimeZone.CurrentTimeZone.ToLocalTime(this);
            return this;
        }

        public DateTime ToUniversalTime()
        {
            //return TimeZone.CurrentTimeZone.ToUniversalTime(this);
            return this;
        }
    }
}
