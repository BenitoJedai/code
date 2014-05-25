using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestSwitchRewriteDateTimeKind
{
    class Program
    {
        //public static XmlDateTimeSerializationMode ToSerializationMode(DateTimeKind kind)
        //{
        //    switch (kind)
        //    {
        //        case DateTimeKind.Local:
        //            return XmlDateTimeSerializationMode.Local;
        //        case DateTimeKind.Unspecified:
        //            return XmlDateTimeSerializationMode.Unspecified;
        //        case DateTimeKind.Utc:
        //            return XmlDateTimeSerializationMode.Utc;
        //        default:
        //            throw null;
        //    }
        //}

        public enum DateTimeZoneHandling
        {
            /// <summary>
            /// Treat as local time. If the <see cref="DateTime"/> object represents a Coordinated Universal Time (UTC), it is converted to the local time.
            /// </summary>
            Local,

            /// <summary>
            /// Treat as a UTC. If the <see cref="DateTime"/> object represents a local time, it is converted to a UTC.
            /// </summary>
            Utc,

            /// <summary>
            /// Treat as a local time if a <see cref="DateTime"/> is being converted to a string.
            /// If a string is being converted to <see cref="DateTime"/>, convert to a local time if a time zone is specified.
            /// </summary>
            Unspecified,

            /// <summary>
            /// Time zone information should be preserved when converting.
            /// </summary>
            RoundtripKind
        }

        private static DateTime SwitchToLocalTime(DateTime value)
        {
            switch (value.Kind)
            {
                case DateTimeKind.Unspecified:
                    return new DateTime(value.Ticks, DateTimeKind.Local);

                case DateTimeKind.Utc:
                    return value.ToLocalTime();

                case DateTimeKind.Local:
                    return value;
            }
            return value;
        }

        private static DateTime SwitchToUtcTime(DateTime value)
        {
            switch (value.Kind)
            {
                case DateTimeKind.Unspecified:
                    return new DateTime(value.Ticks, DateTimeKind.Utc);

                case DateTimeKind.Utc:
                    return value;

                case DateTimeKind.Local:
                    return value.ToUniversalTime();
            }
            return value;
        }

        internal static DateTime EnsureDateTime(DateTime value, DateTimeZoneHandling timeZone)
        {
            switch (timeZone)
            {
                case DateTimeZoneHandling.Local:
                    value = SwitchToLocalTime(value);
                    break;
                case DateTimeZoneHandling.Utc:
                    value = SwitchToUtcTime(value);
                    break;
                case DateTimeZoneHandling.Unspecified:
                    value = new DateTime(value.Ticks, DateTimeKind.Unspecified);
                    break;
                case DateTimeZoneHandling.RoundtripKind:
                    break;
                default:
                    throw new ArgumentException("Invalid date time handling value.");
            }

            return value;
        }

        static void Main(string[] args)
        {
            //var x = ToSerializationMode(DateTimeKind.Unspecified);

            var x = EnsureDateTime(DateTime.Now, DateTimeZoneHandling.Unspecified);

            Console.WriteLine(new { x });

        }
    }
}
