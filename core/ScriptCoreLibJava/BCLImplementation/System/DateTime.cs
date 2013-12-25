﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime
    {
        internal global::java.util.Calendar InternalValue;

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

        public __DateTime(long ticks)
        {
            this.InternalValue = global::java.util.Calendar.getInstance();
            this.InternalValue.setTimeInMillis((ticks - ticks_1970_1_1) / TimeSpan.TicksPerMillisecond);

        }


        public static TimeSpan operator -(__DateTime d1, __DateTime d2)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\TimeSpan.cs

            return TimeSpan.FromTicks(d1.Ticks - d2.Ticks);
        }

        public DateTime AddMinutes(double value)
        {
            return new DateTime((long)Math.Floor(this.Ticks + TimeSpan.TicksPerMinute * value));
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
    }
}
