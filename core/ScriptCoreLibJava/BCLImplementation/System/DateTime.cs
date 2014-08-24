using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/datetime.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\DateTime.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\DateTime.cs


    [Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime // : __NativeDateTime
    {
        public global::java.util.Calendar InternalValue;

        public DateTimeKind Kind { get; set; }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return (DayOfWeek)(InternalValue.get(global::java.util.Calendar.DAY_OF_WEEK) - 1);
            }
        }

        public __DateTime()
            : this(-1, -1, -1, -1, -1, -1)
        {

        }

        public __DateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.InternalValue = global::java.util.Calendar.getInstance();

            if (year == -1)
            {

            }
            else
            {
                this.InternalValue.set(year, month - 1, day, hour, minute, second);
            }
        }

        public __DateTime(long ticks)
            : this(ticks, DateTimeKind.Local)
        {

        }

        public __DateTime(long ticks, DateTimeKind kind)
        {
            this.InternalValue = global::java.util.Calendar.getInstance();
            this.InternalValue.setTimeInMillis((ticks - ticks_1970_1_1) / TimeSpan.TicksPerMillisecond);

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
            return TimeZone.CurrentTimeZone.ToLocalTime(this);
        }

        public DateTime ToUniversalTime()
        {
            return TimeZone.CurrentTimeZone.ToUniversalTime(this);
        }


        public static DateTime Now
        {
            get
            {

                return (DateTime)(object)new __DateTime();
            }
        }

        public int Second { get { return this.InternalValue.get(global::java.util.Calendar.SECOND); } }
        public int Minute { get { return this.InternalValue.get(global::java.util.Calendar.MINUTE); } }
        public int Hour { get { return this.InternalValue.get(global::java.util.Calendar.HOUR_OF_DAY); } }
        public int Day { get { return this.InternalValue.get(global::java.util.Calendar.DAY_OF_MONTH); } }
        public int Month { get { return this.InternalValue.get(global::java.util.Calendar.MONTH) + 1; } }
        public int Year { get { return this.InternalValue.get(global::java.util.Calendar.YEAR); } }

        public const long ticks_1970_1_1 = 621355968000000000;


        public long Ticks
        {
            get
            {
                // conversion needed

                var ms = this.InternalValue.getTimeInMillis();

                return ms * TimeSpan.TicksPerMillisecond + ticks_1970_1_1;
            }
        }

        public static int DaysInMonth(int year, int month)
        {
            return ScriptCoreLib.Shared.BCLImplementation.System.DateTimeDaysInMonth.DaysInMonth(year, month);           
        }

        public static bool IsLeapYear(int year)
        {
            return ScriptCoreLib.Shared.BCLImplementation.System.DateTimeDaysInMonth.IsLeapYear(year);           
        }

        public static TimeSpan operator -(__DateTime d1, __DateTime d2)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\TimeSpan.cs

            return TimeSpan.FromTicks(d1.Ticks - d2.Ticks);
        }

        public DateTime AddMinutes(double value)
        {
            long ticks = TimeSpan.TicksPerMinute;
            return new DateTime((long)Math.Floor(this.Ticks + ticks * value));
        }

        public DateTime AddDays(double value)
        {
            long ticks = TimeSpan.TicksPerDay;
            return new DateTime((long)Math.Floor(this.Ticks + ticks * value));
        }

        public DateTime AddMonths(int value)
        {
            long ticks = TimeSpan.TicksPerDay;

            if(value > 0)
            {
                double tempTicks = 0;
                for(int i = 0; i < value; i++)
                {
                    tempTicks += (double)DateTime.DaysInMonth(this.Year, this.Month + i) * ticks;
                }
                return new DateTime((long)Math.Floor(this.Ticks + tempTicks));
            }
            else 
            {
                double tempTicks = 0;
                for (int i = -1; i >= value; i--)
                {
                    tempTicks += (double)DateTime.DaysInMonth(this.Year, this.Month + i) * ticks;
                }
                return new DateTime((long)Math.Floor(this.Ticks - tempTicks));
            }
            //return new DateTime((long)Math.Floor(this.Ticks + (double)DateTime.DaysInMonth(this.Year, this.Month) * ticks));
        }

        #region Operators
        public static bool operator <=(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTimeInMillis() <= b.InternalValue.getTimeInMillis())
                return true;
            return false;
        }
        public static bool operator >=(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTimeInMillis() >= b.InternalValue.getTimeInMillis())
                return true;
            return false;
        }
        public static bool operator ==(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTimeInMillis() == b.InternalValue.getTimeInMillis())
                return true;
            return false;
        }
        public static bool operator !=(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTimeInMillis() != b.InternalValue.getTimeInMillis())
                return true;
            return false;
        }
        public static bool operator >(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTimeInMillis() > b.InternalValue.getTimeInMillis())
                return true;
            return false;
        }
        public static bool operator <(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTimeInMillis() < b.InternalValue.getTimeInMillis())
                return true;
            return false;
        }
        #endregion
        
        public string ToString(string format)
        {
            if (format == "ddMMMyyyyHHmmss")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(GetMonthString(this.Month).Substring(0, 3).PadLeft(3, '0'));
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                w.Append(this.Hour.ToString().PadLeft(2, '0'));
                w.Append(this.Minute.ToString().PadLeft(2, '0'));
                w.Append(this.Second.ToString().PadLeft(2, '0'));

                return w.ToString();
            }
            else if (format == "dd.MMM.yyyy HH:mm:ss")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(".");
                w.Append(GetMonthString(this.Month).Substring(0, 3).PadLeft(3, '0'));
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
            else if (format == "ddMMyyyyHHmmss")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(this.Month.ToString().PadLeft(2, '0'));
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                w.Append(this.Hour.ToString().PadLeft(2, '0'));
                w.Append(this.Minute.ToString().PadLeft(2, '0'));
                w.Append(this.Second.ToString().PadLeft(2, '0'));
                return w.ToString();
            }
            else if (format == "dd.MM.yyyy HH:mm:ss")
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
            else if (format == "ddMMMMyyyyHHmmss")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(GetMonthString(this.Month));
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                w.Append(this.Hour.ToString().PadLeft(2, '0'));
                w.Append(this.Minute.ToString().PadLeft(2, '0'));
                w.Append(this.Second.ToString().PadLeft(2, '0'));
                return w.ToString();
            }
            else if (format == "dd.MMMM.yyyy HH:mm:ss")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(".");
                w.Append(GetMonthString(this.Month));
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
            else if (format == "ddMMMMyyyy")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(GetMonthString(this.Month));
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                return w.ToString();
            }
            else if (format == "dd.MMMM.yyyy")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(".");
                w.Append(GetMonthString(this.Month));
                w.Append(".");
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                return w.ToString();
            }
            else if (format == "ddMMMyyyy")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(GetMonthString(this.Month).Substring(0, 3).PadLeft(3, '0'));
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                return w.ToString();
            }
            else if (format == "dd.MMM.yyyy")
            {
                var w = new StringBuilder();
                w.Append(this.Day.ToString().PadLeft(2, '0'));
                w.Append(".");
                w.Append(GetMonthString(this.Month).Substring(0, 3).PadLeft(3, '0'));
                w.Append(".");
                w.Append(this.Year.ToString().PadLeft(4, '0'));
                return w.ToString();
            }
            else if (format == "MMMM")
            {
                return GetMonthString(this.Month);
            }
            else if (format == "MMM")
            {
                return GetMonthString(this.Month).Substring(0, 3);
            }
            else
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            var w = new StringBuilder();

            w.Append((this.Day + "").PadLeft(2, '0'));
            w.Append(".");
            w.Append((this.Month + "").PadLeft(2, '0'));
            w.Append(".");
            w.Append(this.Year);
            w.Append(" ");
            w.Append((this.Hour + "").PadLeft(2, '0'));
            w.Append(":");
            w.Append((this.Minute + "").PadLeft(2, '0'));
            w.Append(":");
            w.Append((this.Second + "").PadLeft(2, '0'));

            return w.ToString();
        }

        private string GetMonthString(int month)
        {
            if (month < 1)
                throw new Exception("ArgumentOutOfRange_Month");
            if (month > 12)
                throw new Exception("ArgumentOutOfRange_Month");

            var arr = new[] {
                "January", "February", "March", "April", "May", "June", "July", "August", "September",
                "October","November","December"
                };
            return arr[month - 1];
        }

    }
}
