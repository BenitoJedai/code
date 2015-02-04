using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/datetime.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/DateTime.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/DateTime.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/DateTime.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\DateTime.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\DateTime.cs


    [Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime // : __NativeDateTime
    {
        public IDate InternalValue;

        public DateTimeKind Kind { get; set; }

        public __DateTime()
        {
            this.Kind = DateTimeKind.Local;
            // fix for default(DateTime).ToString
            // X:\jsc.svn\examples\javascript\Test\TestDateTime\TestDateTime\Program.cs
            this.InternalValue = new IDate();

        }

        public __DateTime(long ticks, DateTimeKind kind)
        {
            //if ((ticks < 0L) || (ticks > 3155378975999999999L))
            //{
            //    throw new Exception("ArgumentOutOfRange_DateTimeBadTicks");
            //}

            var ms = (ticks - ticks_1970_1_1) / TicksPerMillisecond;

            this.InternalValue = new IDate(ms);
            this.Kind = kind;
        }

        public __DateTime(long ticks)
            : this(ticks, DateTimeKind.Local)
        {

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


        // whoa. time travel? :)
        public const long TicksPerMillisecond = 10000;
        public const long ticks_1970_1_1 = 621355968000000000;


        public long Ticks
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224
                // conversion needed

                var TotalMilliseconds = this.InternalValue.getTime();

                return TotalMilliseconds * __DateTime.TicksPerMillisecond + __DateTime.ticks_1970_1_1;
            }
        }

        public DateTime Date
        {
            get
            {
                // X:\jsc.svn\examples\javascript\test\TestDate\TestDate\Application.cs
                var TotalMilliseconds = this.InternalValue.getTime();

                var x = (1000 * 60 * 60 * 24);

                var DateTotalMilliseconds = TotalMilliseconds / x;

                Console.WriteLine(new { DateTotalMilliseconds });

                var ms = DateTotalMilliseconds * x;

                return new __DateTime
                {
                    InternalValue = new IDate(ms),
                    Kind = this.Kind
                };
            }
        }


        public __DateTime(int year, int month, int day)
        {
            InternalValue = new IDate();
            InternalValue.setFullYear(year);
            InternalValue.setMonth(month - 1);
            InternalValue.setDate(day);
            InternalValue.setHours(0);
            InternalValue.setMinutes(0);
            InternalValue.setSeconds(0);
        }

        public __DateTime(int year, int month, int day, int hours, int minutes, int seconds)
        {
            InternalValue = new IDate();
            InternalValue.setFullYear(year);
            InternalValue.setMonth(month - 1);
            InternalValue.setDate(day);
            InternalValue.setHours(hours);
            InternalValue.setMinutes(minutes);
            InternalValue.setSeconds(seconds);
        }




        public static __DateTime Now
        {
            get
            {
                return new __DateTime { InternalValue = new IDate() };
            }
        }


        public int Millisecond
        {
            get
            {
                return this.InternalValue.getMilliseconds();
            }
        }


        public int Second
        {
            get
            {
                return this.InternalValue.getSeconds();
            }
        }

        public int Minute
        {
            get
            {
                return this.InternalValue.getMinutes();
            }
        }

        public int Hour
        {
            get
            {
                return this.InternalValue.getHours();
            }
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return (DayOfWeek)this.InternalValue.getDay();
            }
        }

        public int Day
        {
            get
            {
                return this.InternalValue.getDate();
            }
        }

        public int Month
        {
            get
            {
                return this.InternalValue.getMonth() + 1;
            }
        }

        public int Year
        {
            get
            {
                return this.InternalValue.getFullYear();
            }
        }




        public static int DaysInMonth(int year, int month)
        {
            //if (month < 1)
            //    throw new Exception("ArgumentOutOfRange_Month");
            //if (month > 12)
            //    throw new Exception("ArgumentOutOfRange_Month");

            //int[] numArray = DaysToMonth365;

            //if (IsLeapYear(year)) numArray = DaysToMonth366;

            //return (numArray[month] - numArray[month - 1]);

            return ScriptCoreLib.Shared.BCLImplementation.System.DateTimeDaysInMonth.DaysInMonth(year, month);

        }

        //static __DateTime()
        //{
        //    // compiler bug: zero valued elements are skipped

        //    DaysToMonth365 = __ArrayDummy(0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365);
        //    DaysToMonth365[0] = 0;
        //    DaysToMonth366 = __ArrayDummy(0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366);
        //    DaysToMonth366[0] = 0;

        //}

        //static T[] __ArrayDummy<T>(params T[] e)
        //{
        //    return e;
        //}

        //private static readonly int[] DaysToMonth366;
        //private static readonly int[] DaysToMonth365;





        public static bool IsLeapYear(int year)
        {
            //if (year < 1)
            //    throw new Exception("ArgumentOutOfRange_Year");
            //if (year > 0x270f)
            //    throw new Exception("ArgumentOutOfRange_Year");

            //if ((year % 4) != 0)
            //{
            //    return false;
            //}
            //if ((year % 100) == 0)
            //{
            //    return ((year % 400) == 0);
            //}
            //return true;

            return ScriptCoreLib.Shared.BCLImplementation.System.DateTimeDaysInMonth.IsLeapYear(year);
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

        //Tested by E:\jsc.svn\examples\javascript\Test\TestDateTimeFormat\TestDateTimeFormat\Application.cs
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


        public static TimeSpan operator -(__DateTime d1, __DateTime d2)
        {
            return TimeSpan.FromMilliseconds(
                d1.InternalValue.getTime() - d2.InternalValue.getTime()
            );
        }

        public static DateTime operator +(__DateTime d, TimeSpan t)
        {
            var ms = d.InternalValue.getTime() + t.TotalMilliseconds;

            return new __DateTime
            {
                InternalValue = new IDate(ms),
                Kind = d.Kind
            };
        }

        public DateTime AddDays(double value)
        {
            return this + TimeSpan.FromDays(value);
        }

        //Tested by : E:\jsc.svn\examples\javascript\Test\TestDateTimeOperators\TestDateTimeOperators\Application.cs
        #region Operators
        public static bool operator <=(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTime() <= b.InternalValue.getTime())
                return true;
            return false;
        }
        public static bool operator >=(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTime() >= b.InternalValue.getTime())
                return true;
            return false;
        }
        public static bool operator ==(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTime() == b.InternalValue.getTime())
                return true;
            return false;
        }
        public static bool operator !=(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTime() != b.InternalValue.getTime())
                return true;
            return false;
        }
        public static bool operator >(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTime() > b.InternalValue.getTime())
                return true;
            return false;
        }
        public static bool operator <(__DateTime a, __DateTime b)
        {
            if (a.InternalValue.getTime() < b.InternalValue.getTime())
                return true;
            return false;
        }
        #endregion

        private string GetMonthString(int month)
        {
            if (month == 1)
            {
                return "January";
            }
            else if (month == 2)
            {
                return "February";
            }
            else if (month == 3)
            {
                return "March";
            }
            else if (month == 4)
            {
                return "April";
            }
            else if (month == 5)
            {
                return "May";
            }
            else if (month == 6)
            {
                return "June";
            }
            else if (month == 7)
            {
                return "July";
            }
            else if (month == 8)
            {
                return "August";
            }
            else if (month == 9)
            {
                return "September";
            }
            else if (month == 10)
            {
                return "October";
            }
            else if (month == 11)
            {
                return "November";
            }
            else if (month == 12)
            {
                return "December";
            }
            return "";
        }
    }
}
