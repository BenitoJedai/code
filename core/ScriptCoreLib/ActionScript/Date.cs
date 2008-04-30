using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Date.html
    [Script(IsNative=true)]
    public sealed class Date
    {
        #region Properties
        /// <summary>
        /// The day of the month (an integer from 1 to 31) specified by a Date object according to local time.
        /// </summary>
        public double date { get; set; }

        /// <summary>
        /// The day of the month (an integer from 1 to 31) of a Date object according to universal time (UTC).
        /// </summary>
        public double dateUTC { get; set; }

        /// <summary>
        /// [read-only] The day of the week (0 for Sunday, 1 for Monday, and so on) specified by this Date according to local time.
        /// </summary>
        public double day { get; private set; }

        /// <summary>
        /// [read-only] The day of the week (0 for Sunday, 1 for Monday, and so on) of this Date according to universal time (UTC).
        /// </summary>
        public double dayUTC { get; private set; }

        /// <summary>
        /// The full year (a four-digit number, such as 2000) of a Date object according to local time.
        /// </summary>
        public double fullYear { get; set; }

        /// <summary>
        /// The four-digit year of a Date object according to universal time (UTC).
        /// </summary>
        public double fullYearUTC { get; set; }

        /// <summary>
        /// The hour (an integer from 0 to 23) of the day portion of a Date object according to local time.
        /// </summary>
        public double hours { get; set; }

        /// <summary>
        /// The hour (an integer from 0 to 23) of the day of a Date object according to universal time (UTC).
        /// </summary>
        public double hoursUTC { get; set; }

        /// <summary>
        /// The milliseconds (an integer from 0 to 999) portion of a Date object according to local time.
        /// </summary>
        public double milliseconds { get; set; }

        /// <summary>
        /// The milliseconds (an integer from 0 to 999) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double millisecondsUTC { get; set; }

        /// <summary>
        /// The minutes (an integer from 0 to 59) portion of a Date object according to local time.
        /// </summary>
        public double minutes { get; set; }

        /// <summary>
        /// The minutes (an integer from 0 to 59) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double minutesUTC { get; set; }

        /// <summary>
        /// The month (0 for January, 1 for February, and so on) portion of a Date object according to local time.
        /// </summary>
        public double month { get; set; }

        /// <summary>
        /// The month (0 [January] to 11 [December]) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double monthUTC { get; set; }

        /// <summary>
        /// The seconds (an integer from 0 to 59) portion of a Date object according to local time.
        /// </summary>
        public double seconds { get; set; }

        /// <summary>
        /// The seconds (an integer from 0 to 59) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double secondsUTC { get; set; }

        /// <summary>
        /// The number of milliseconds since midnight January 1, 1970, universal time, for a Date object.
        /// </summary>
        public double time { get; set; }

        /// <summary>
        /// [read-only] The difference, in minutes, between universal time (UTC) and the computer's local time.
        /// </summary>
        public double timezoneOffset { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// Returns the day of the month (an integer from 1 to 31) specified by a Date object according to local time.
        /// </summary>
        public double getDate()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the day of the week (0 for Sunday, 1 for Monday, and so on) specified by this Date according to local time.
        /// </summary>
        public double getDay()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the full year (a four-digit number, such as 2000) of a Date object according to local time.
        /// </summary>
        public double getFullYear()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the hour (an integer from 0 to 23) of the day portion of a Date object according to local time.
        /// </summary>
        public double getHours()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the milliseconds (an integer from 0 to 999) portion of a Date object according to local time.
        /// </summary>
        public double getMilliseconds()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the minutes (an integer from 0 to 59) portion of a Date object according to local time.
        /// </summary>
        public double getMinutes()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the month (0 for January, 1 for February, and so on) portion of this Date according to local time.
        /// </summary>
        public double getMonth()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the seconds (an integer from 0 to 59) portion of a Date object according to local time.
        /// </summary>
        public double getSeconds()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the number of milliseconds since midnight January 1, 1970, universal time, for a Date object.
        /// </summary>
        public double getTime()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the difference, in minutes, between universal time (UTC) and the computer's local time.
        /// </summary>
        public double getTimezoneOffset()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the day of the month (an integer from 1 to 31) of a Date object, according to universal time (UTC).
        /// </summary>
        public double getUTCDate()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the day of the week (0 for Sunday, 1 for Monday, and so on) of this Date according to universal time (UTC).
        /// </summary>
        public double getUTCDay()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the four-digit year of a Date object according to universal time (UTC).
        /// </summary>
        public double getUTCFullYear()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the hour (an integer from 0 to 23) of the day of a Date object according to universal time (UTC).
        /// </summary>
        public double getUTCHours()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the milliseconds (an integer from 0 to 999) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double getUTCMilliseconds()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the minutes (an integer from 0 to 59) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double getUTCMinutes()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the month (0 [January] to 11 [December]) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double getUTCMonth()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the seconds (an integer from 0 to 59) portion of a Date object according to universal time (UTC).
        /// </summary>
        public double getUTCSeconds()
        {
            return default(double);
        }

        /// <summary>
        /// [static] Converts a string representing a date into a number equaling the number of milliseconds elapsed since January 1, 1970, UTC.
        /// </summary>
        public static double parse(string date)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the day of the month, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setDate(double day)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the year, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setFullYear(double year, double month, double day)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the hour, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setHours(double hour, double minute, double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the milliseconds, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setMilliseconds(double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the minutes, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setMinutes(double minute, double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the month and optionally the day of the month, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setMonth(double month, double day)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the seconds, according to local time, and returns the new time in milliseconds.
        /// </summary>
        public double setSeconds(double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the date in milliseconds since midnight on January 1, 1970, and returns the new time in milliseconds.
        /// </summary>
        public double setTime(double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the day of the month, in universal time (UTC), and returns the new time in milliseconds.
        /// </summary>
        public double setUTCDate(double day)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the year, in universal time (UTC), and returns the new time in milliseconds.
        /// </summary>
        public double setUTCFullYear(double year, double month, double day)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the hour, in universal time (UTC), and returns the new time in milliseconds.
        /// </summary>
        public double setUTCHours(double hour, double minute, double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the milliseconds, in universal time (UTC), and returns the new time in milliseconds.
        /// </summary>
        public double setUTCMilliseconds(double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the minutes, in universal time (UTC), and returns the new time in milliseconds.
        /// </summary>
        public double setUTCMinutes(double minute, double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the month, and optionally the day, in universal time(UTC) and returns the new time in milliseconds.
        /// </summary>
        public double setUTCMonth(double month, double day)
        {
            return default(double);
        }

        /// <summary>
        /// Sets the seconds, and optionally the milliseconds, in universal time (UTC) and returns the new time in milliseconds.
        /// </summary>
        public double setUTCSeconds(double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// Returns a string representation of the day and date only, and does not include the time or timezone.
        /// </summary>
        public string toDateString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a String representation of the day and date only, and does not include the time or timezone.
        /// </summary>
        public string toLocaleDateString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a String representation of the day, date, time, given in local time.
        /// </summary>
        public string toLocaleString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a String representation of the time only, and does not include the day, date, year, or timezone.
        /// </summary>
        public string toLocaleTimeString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a String representation of the time and timezone only, and does not include the day and date.
        /// </summary>
        public string toTimeString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a String representation of the day, date, and time in universal time (UTC).
        /// </summary>
        public string toUTCString()
        {
            return default(string);
        }

        /// <summary>
        /// [static] Returns the number of milliseconds between midnight on January 1, 1970, universal time, and the time specified in the parameters.
        /// </summary>
        public static double UTC(double year, double month, double date, double hour, double minute, double second, double millisecond)
        {
            return default(double);
        }

        /// <summary>
        /// [static] Returns the number of milliseconds between midnight on January 1, 1970, universal time, and the time specified in the parameters.
        /// </summary>
        public static double UTC(double year, double month, double date, double hour, double minute, double second)
        {
            return default(double);
        }

        /// <summary>
        /// [static] Returns the number of milliseconds between midnight on January 1, 1970, universal time, and the time specified in the parameters.
        /// </summary>
        public static double UTC(double year, double month, double date, double hour, double minute)
        {
            return default(double);
        }

        /// <summary>
        /// [static] Returns the number of milliseconds between midnight on January 1, 1970, universal time, and the time specified in the parameters.
        /// </summary>
        public static double UTC(double year, double month, double date, double hour)
        {
            return default(double);
        }

        /// <summary>
        /// [static] Returns the number of milliseconds between midnight on January 1, 1970, universal time, and the time specified in the parameters.
        /// </summary>
        public static double UTC(double year, double month, double date)
        {
            return default(double);
        }

        /// <summary>
        /// [static] Returns the number of milliseconds between midnight on January 1, 1970, universal time, and the time specified in the parameters.
        /// </summary>
        public static double UTC(double year, double month)
        {
            return default(double);
        }

        /// <summary>
        /// Returns the number of milliseconds since midnight January 1, 1970, universal time, for a Date object.
        /// </summary>
        public double valueOf()
        {
            return default(double);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new Date object that holds the specified date and time.
        /// </summary>
        public Date(object yearOrTimevalue, double month, double date, double hour, double minute, double second, double millisecond)
        {
        }

        /// <summary>
        /// Constructs a new Date object that holds the specified date and time.
        /// </summary>
        public Date(object yearOrTimevalue, double month, double date, double hour, double minute, double second)
        {
        }

        /// <summary>
        /// Constructs a new Date object that holds the specified date and time.
        /// </summary>
        public Date(object yearOrTimevalue, double month, double date, double hour, double minute)
        {
        }

        /// <summary>
        /// Constructs a new Date object that holds the specified date and time.
        /// </summary>
        public Date(object yearOrTimevalue, double month, double date, double hour)
        {
        }

        /// <summary>
        /// Constructs a new Date object that holds the specified date and time.
        /// </summary>
        public Date(object yearOrTimevalue, double month, double date)
        {
        }

        /// <summary>
        /// Constructs a new Date object that holds the specified date and time.
        /// </summary>
        public Date(object yearOrTimevalue, double month)
        {
        }


        
        #endregion

        public Date(long ticks)
        {
        }

        public Date()
        {
        }
    }
}
