using ScriptCoreLib;

namespace java.util
{

    [Script(IsNative = true)]
    public class GregorianCalendar : Calendar
    {

        #region methods
        /// <summary>
        /// Adds the specified (signed) amount of time to the given time field, based on the calendar's rules.
        /// </summary>
        public override void add(int field, int amount)
        {
        }

        /// <summary>
        /// Converts UTC as milliseconds to time field values.
        /// </summary>
        protected override void computeFields()
        {
        }

        /// <summary>
        /// Overrides Calendar Converts time field values to UTC as milliseconds.
        /// </summary>
        protected override void computeTime()
        {
        }

       
        /// <summary>
        /// Returns highest minimum value for the given field if varies.
        /// </summary>
        public override int getGreatestMinimum(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Gets the Gregorian Calendar change date.
        /// </summary>
        public Date getGregorianChange()
        {
            return default(Date);
        }

        /// <summary>
        /// Returns lowest maximum value for the given field if varies.
        /// </summary>
        public override int getLeastMaximum(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Returns maximum value for the given field.
        /// </summary>
        public override int getMaximum(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Returns minimum value for the given field.
        /// </summary>
        public override int getMinimum(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Determines if the given year is a leap year.
        /// </summary>
        public bool isLeapYear(int year)
        {
            return default(bool);
        }

        /// <summary>
        /// Adds or subtracts (up/down) a single unit of time on the given time field without changing larger fields.
        /// </summary>
        public override void roll(int field, bool up)
        {
        }



        /// <summary>
        /// Sets the GregorianCalendar change date.
        /// </summary>
        public void setGregorianChange(Date date)
        {
        }

        #endregion

    }
}
