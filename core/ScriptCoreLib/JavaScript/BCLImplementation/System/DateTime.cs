using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime
    {
        public IDate Value;

        protected __DateTime()
        {

        }

        public __DateTime(long ticks)
        {
            if ((ticks < 0L) || (ticks > 3155378975999999999L))
            {
                throw new Exception("ArgumentOutOfRange_DateTimeBadTicks");
            }

            var ms = (ticks - ticks_1970_1_1) / TicksPerMillisecond;

            Value = new IDate(ms);
        }




        public __DateTime(int year, int month, int day)
        {
            Value = new IDate();
            Value.setFullYear(year);
            Value.setMonth(month);
            Value.setDate(day);
        }






        public static __DateTime Now
        {
            get
            {
                return new __DateTime { Value = new IDate() };
            }
        }

        private const long TicksPerMillisecond = 0x10000;


        public int Millisecond
        {
            get
            {
                return this.Value.getMilliseconds();
            }
        }

        public int Second
        {
            get
            {
                return this.Value.getSeconds();
            }
        }

        public int Minute
        {
            get
            {
                return this.Value.getMinutes();
            }
        }

        public int Hour
        {
            get
            {
                return this.Value.getHours();
            }
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return (DayOfWeek)this.Value.getDay();
            }
        }

        public int Day
        {
            get
            {
                return this.Value.getDate();
            }
        }

        public int Month
        {
            get
            {
                return this.Value.getMonth() + 1;
            }
        }

        public int Year
        {
            get
            {
                return this.Value.getFullYear();
            }
        }

        public long Ticks
        {
            get
            {
                // conversion needed

                var ms = this.Value.getTime();

                return ms * TicksPerMillisecond + ticks_1970_1_1;
            }
        }

        public const long ticks_1970_1_1 = 621355968000000000;


        public static int DaysInMonth(int year, int month)
        {
            if (month < 1)
                throw new Exception("ArgumentOutOfRange_Month");
            if (month > 12)
                throw new Exception("ArgumentOutOfRange_Month");

            int[] numArray = IsLeapYear(year) ? DaysToMonth366 : DaysToMonth365;

            return (numArray[month] - numArray[month - 1]);
        }

        static __DateTime()
        {
            DaysToMonth365 = __ArrayDummy(0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365);
            DaysToMonth366 = __ArrayDummy(0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366);
        }

        static T[] __ArrayDummy<T>(params T[] e)
        {
            return e;
        }

        private static readonly int[] DaysToMonth366;
        private static readonly int[] DaysToMonth365;





        public static bool IsLeapYear(int year)
        {
            if (year < 1)
                throw new Exception("ArgumentOutOfRange_Year");
            if (year > 0x270f)
                throw new Exception("ArgumentOutOfRange_Year");

            if ((year % 4) != 0)
            {
                return false;
            }
            if ((year % 100) == 0)
            {
                return ((year % 400) == 0);
            }
            return true;
        }





    }
}
