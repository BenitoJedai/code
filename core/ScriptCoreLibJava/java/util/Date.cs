using ScriptCoreLib;

namespace java.util
{

    [Script(IsNative = true)]
    public class Date
    {
        /// <summary>
        /// Allocates a Date object and initializes it so that it represents the time at which it was allocated, measured to the nearest millisecond.
        /// </summary>
        public Date()
        {
        }

        #region methods
        /// <summary>
        /// Tests if this date is after the specified date.
        /// </summary>
        public bool after(Date when)
        {
            return default(bool);
        }

        /// <summary>
        /// Tests if this date is before the specified date.
        /// </summary>
        public bool before(Date when)
        {
            return default(bool);
        }

        /// <summary>
        /// Return a copy of this object.
        /// </summary>
        public object clone()
        {
            return default(object);
        }

        /// <summary>
        /// Compares two Dates for ordering.
        /// </summary>
        public int compareTo(Date anotherDate)
        {
            return default(int);
        }

        /// <summary>
        /// Compares this Date to another Object.
        /// </summary>
        public int compareTo(object o)
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.DAY_OF_MONTH)</code>.</I>
        /// </summary>
        public int getDate()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.DAY_OF_WEEK)</code>.</I>
        /// </summary>
        public int getDay()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.HOUR_OF_DAY)</code>.</I>
        /// </summary>
        public int getHours()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.MINUTE)</code>.</I>
        /// </summary>
        public int getMinutes()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.MONTH)</code>.</I>
        /// </summary>
        public int getMonth()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.SECOND)</code>.</I>
        /// </summary>
        public int getSeconds()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the number of milliseconds since January 1, 1970, 00:00:00 GMT represented by this <tt>Date</tt> object.
        /// </summary>
        public long getTime()
        {
            return default(long);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>-(Calendar.get(Calendar.ZONE_OFFSET) + Calendar.get(Calendar.DST_OFFSET)) / (60 * 1000)</code>.</I>
        /// </summary>
        public int getTimezoneOffset()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.get(Calendar.YEAR) - 1900</code>.</I>
        /// </summary>
        public int getYear()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>DateFormat.parse(String s)</code>.</I>
        /// </summary>
        public static long parse(string s)
        {
            return default(long);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(Calendar.DAY_OF_MONTH, int date)</code>.</I>
        /// </summary>
        public void setDate(int date)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(Calendar.HOUR_OF_DAY, int hours)</code>.</I>
        /// </summary>
        public void setHours(int hours)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(Calendar.MINUTE, int minutes)</code>.</I>
        /// </summary>
        public void setMinutes(int minutes)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(Calendar.MONTH, int month)</code>.</I>
        /// </summary>
        public void setMonth(int month)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(Calendar.SECOND, int seconds)</code>.</I>
        /// </summary>
        public void setSeconds(int seconds)
        {
        }

        /// <summary>
        /// Sets this <tt>Date</tt> object to represent a point in time that is  <tt>time</tt> milliseconds after January 1, 1970 00:00:00 GMT.
        /// </summary>
        public void setTime(long time)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(Calendar.YEAR, year + 1900)</code>.</I>
        /// </summary>
        public void setYear(int year)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>DateFormat.format(Date date)</code>, using a GMT <code>TimeZone</code>.</I>
        /// </summary>
        public string toGMTString()
        {
            return default(string);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>DateFormat.format(Date date)</code>.</I>
        /// </summary>
        public string toLocaleString()
        {
            return default(string);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>As of JDK version 1.1, replaced by <code>Calendar.set(year + 1900, month, date, hrs, min, sec)</code> or <code>GregorianCalendar(year + 1900, month, date, hrs, min, sec)</code>, using a UTC <code>TimeZone</code>, followed by <code>Calendar.getTime().getTime()</code>.</I>
        /// </summary>
        public static long UTC(int year, int month, int date, int hrs, int min, int sec)
        {
            return default(long);
        }

        #endregion

    }
}
