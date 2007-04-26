using ScriptCoreLib;

using java.lang;
using java.util;


namespace java.text
{
    [Script(IsNative=true)]
    public class SimpleDateFormat : DateFormat
    {
        public SimpleDateFormat(string format)
        {
        }

        #region methods
        /// <summary>
        /// Applies the given localized pattern string to this date format.
        /// </summary>
        public void applyLocalizedPattern(string pattern)
        {
        }

        /// <summary>
        /// Applies the given pattern string to this date format.
        /// </summary>
        public void applyPattern(string pattern)
        {
        }

        /// <summary>
        /// Creates a copy of this <code>SimpleDateFormat</code>.
        /// </summary>
        public object clone()
        {
            return default(object);
        }

        /// <summary>
        /// Formats the given <code>Date</code> into a date/time string and appends the result to the given <code>StringBuffer</code>.
        /// </summary>
        public override StringBuffer format(Date date, StringBuffer toAppendTo, FieldPosition pos)
        {
            return default(StringBuffer);
        }

        /// <summary>
        /// Formats an Object producing an <code>AttributedCharacterIterator</code>.
        /// </summary>
        //public AttributedCharacterIterator formatToCharacterIterator(object obj)
        //{
        //    return default(AttributedCharacterIterator);
        //}

        /// <summary>
        /// Returns the beginning date of the 100-year period 2-digit years are interpreted as being within.
        /// </summary>
        public Date get2DigitYearStart()
        {
            return default(Date);
        }

        /// <summary>
        /// Gets a copy of the date and time format symbols of this date format.
        /// </summary>
        //public DateFormatSymbols getDateFormatSymbols()
        //{
        //    return default(DateFormatSymbols);
        //}

        /// <summary>
        /// Parses text from a string to produce a <code>Date</code>.
        /// </summary>
        public override Date parse(string text, ParsePosition pos)
        {
            return default(Date);
        }

        /// <summary>
        /// Sets the 100-year period 2-digit years will be interpreted as being in to begin on the date the user specifies.
        /// </summary>
        public void set2DigitYearStart(Date startDate)
        {
        }

        /// <summary>
        /// Sets the date and time format symbols of this date format.
        /// </summary>
        //public void setDateFormatSymbols(DateFormatSymbols newFormatSymbols)
        //{
        //}

        /// <summary>
        /// Returns a localized pattern string describing this date format.
        /// </summary>
        public string toLocalizedPattern()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a pattern string describing this date format.
        /// </summary>
        public string toPattern()
        {
            return default(string);
        }

        #endregion

    }
}
