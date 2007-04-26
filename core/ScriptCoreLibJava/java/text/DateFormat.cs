using ScriptCoreLib;

using java.lang;
using java.util;

namespace java.text
{
    [Script(IsNative=true)]
    public abstract class DateFormat : Format
    {
        #region methods
        /// <summary>
        /// Overrides Cloneable
        /// </summary>
        public object clone()
        {
            return default(object);
        }

        /// <summary>
        /// Formats a Date into a date/time string.
        /// </summary>
        public string format(Date date)
        {
            return default(string);
        }

        /// <summary>
        /// Formats a Date into a date/time string.
        /// </summary>
        public abstract StringBuffer format(Date date, StringBuffer toAppendTo, FieldPosition fieldPosition);

        /// <summary>
        /// Overrides Format.
        /// </summary>
        public StringBuffer format(object obj, StringBuffer toAppendTo, FieldPosition fieldPosition)
        {
            return default(StringBuffer);
        }

        /// <summary>
        /// Gets the set of locales for which DateFormats are installed.
        /// </summary>
        public static Locale[] getAvailableLocales()
        {
            return default(Locale[]);
        }

        /// <summary>
        /// Gets the calendar associated with this date/time formatter.
        /// </summary>
        public Calendar getCalendar()
        {
            return default(Calendar);
        }

        /// <summary>
        /// Gets the date formatter with the default formatting style for the default locale.
        /// </summary>
        public static DateFormat getDateInstance()
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the date formatter with the given formatting style for the default locale.
        /// </summary>
        public static DateFormat getDateInstance(int style)
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the date formatter with the given formatting style for the given locale.
        /// </summary>
        public static DateFormat getDateInstance(int style, Locale aLocale)
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the date/time formatter with the default formatting style for the default locale.
        /// </summary>
        public static DateFormat getDateTimeInstance()
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the date/time formatter with the given date and time formatting styles for the default locale.
        /// </summary>
        public static DateFormat getDateTimeInstance(int dateStyle, int timeStyle)
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the date/time formatter with the given formatting styles for the given locale.
        /// </summary>
        public static DateFormat getDateTimeInstance(int dateStyle, int timeStyle, Locale aLocale)
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Get a default date/time formatter that uses the SHORT style for both the date and the time.
        /// </summary>
        public static DateFormat getInstance()
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the number formatter which this date/time formatter uses to format and parse a time.
        /// </summary>
        public NumberFormat getNumberFormat()
        {
            return default(NumberFormat);
        }

        /// <summary>
        /// Gets the time formatter with the default formatting style for the default locale.
        /// </summary>
        public static DateFormat getTimeInstance()
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the time formatter with the given formatting style for the default locale.
        /// </summary>
        public static DateFormat getTimeInstance(int style)
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the time formatter with the given formatting style for the given locale.
        /// </summary>
        public static DateFormat getTimeInstance(int style, Locale aLocale)
        {
            return default(DateFormat);
        }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        public TimeZone getTimeZone()
        {
            return default(TimeZone);
        }

        /// <summary>
        /// Tell whether date/time parsing is to be lenient.
        /// </summary>
        public bool isLenient()
        {
            return default(bool);
        }

        /// <summary>
        /// Parses text from the beginning of the given string to produce a date.
        /// </summary>
        public Date parse(string source)
        {
            return default(Date);
        }

        /// <summary>
        /// Parse a date/time string according to the given parse position.
        /// </summary>
        public abstract Date parse(string source, ParsePosition pos);

        /// <summary>
        /// Parses text from a string to produce a <code>Date</code>.
        /// </summary>
        public object parseObject(string source, ParsePosition pos)
        {
            return default(object);
        }

        /// <summary>
        /// Set the calendar to be used by this date format.
        /// </summary>
        public void setCalendar(Calendar newCalendar)
        {
        }

        /// <summary>
        /// Specify whether or not date/time parsing is to be lenient.
        /// </summary>
        public void setLenient(bool lenient)
        {
        }

        /// <summary>
        /// Allows you to set the number formatter.
        /// </summary>
        public void setNumberFormat(NumberFormat newNumberFormat)
        {
        }

        /// <summary>
        /// Sets the time zone for the calendar of this DateFormat object.
        /// </summary>
        public void setTimeZone(TimeZone zone)
        {
        }

        #endregion

    }
}
