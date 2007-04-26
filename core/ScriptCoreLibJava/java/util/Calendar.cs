using ScriptCoreLib;

namespace java.util
{

    [Script(IsNative = true)]
    public abstract class Calendar
    {
        public const int YEAR = 1;
        public const int MONTH = 2;
        

        public const int DAY_OF_MONTH = 5;
        public const int DAY_OF_YEAR = 6;
        
        #region methods
        /// <summary>
        /// Date Arithmetic function.
        /// </summary>
        public abstract void add(int field, int amount);

        /// <summary>
        /// Compares the time field records.
        /// </summary>
        public bool after(object when)
        {
            return default(bool);
        }

        /// <summary>
        /// Compares the time field records.
        /// </summary>
        public bool before(object when)
        {
            return default(bool);
        }

        /// <summary>
        /// Clears the values of all the time fields.
        /// </summary>
        public void clear()
        {
        }

        /// <summary>
        /// Clears the value in the given time field.
        /// </summary>
        public void clear(int field)
        {
        }

        /// <summary>
        /// Overrides Cloneable
        /// </summary>
        public object clone()
        {
            return default(object);
        }

        /// <summary>
        /// Fills in any unset fields in the time field list.
        /// </summary>
        protected void complete()
        {
        }

        /// <summary>
        /// Converts  the current millisecond time value <code>time</code> to field values in <code>fields[]</code>.
        /// </summary>
        protected abstract void computeFields();

        /// <summary>
        /// Converts the current field values in <code>fields[]</code> to the millisecond time value <code>time</code>.
        /// </summary>
        protected abstract void computeTime();

        /// <summary>
        /// Gets the value for a given time field.
        /// </summary>
        public int get(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Return the maximum value that this field could have, given the current date.
        /// </summary>
        public int getActualMaximum(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Return the minimum value that this field could have, given the current date.
        /// </summary>
        public int getActualMinimum(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Gets the list of locales for which Calendars are installed.
        /// </summary>
        public static Locale[] getAvailableLocales()
        {
            return default(Locale[]);
        }

        /// <summary>
        /// Gets what the first day of the week is; e.g., Sunday in US, Monday in France.
        /// </summary>
        public int getFirstDayOfWeek()
        {
            return default(int);
        }

        /// <summary>
        /// Gets the highest minimum value for the given field if varies.
        /// </summary>
        public abstract int getGreatestMinimum(int field);

        /// <summary>
        /// Gets a calendar using the default time zone and locale.
        /// </summary>
        public static Calendar getInstance()
        {
            return default(Calendar);
        }

        /// <summary>
        /// Gets a calendar using the default time zone and specified locale.
        /// </summary>
        public static Calendar getInstance(Locale aLocale)
        {
            return default(Calendar);
        }

        /// <summary>
        /// Gets a calendar using the specified time zone and default locale.
        /// </summary>
        public static Calendar getInstance(TimeZone zone)
        {
            return default(Calendar);
        }

        /// <summary>
        /// Gets a calendar with the specified time zone and locale.
        /// </summary>
        public static Calendar getInstance(TimeZone zone, Locale aLocale)
        {
            return default(Calendar);
        }

        /// <summary>
        /// Gets the lowest maximum value for the given field if varies.
        /// </summary>
        public abstract int getLeastMaximum(int field);

        /// <summary>
        /// Gets the maximum value for the given time field.
        /// </summary>
        public abstract int getMaximum(int field);

        /// <summary>
        /// Gets what the minimal days required in the first week of the year are; e.g., if the first week is defined as one that contains the first day of the first month of a year, getMinimalDaysInFirstWeek returns 1.
        /// </summary>
        public int getMinimalDaysInFirstWeek()
        {
            return default(int);
        }

        /// <summary>
        /// Gets the minimum value for the given time field.
        /// </summary>
        public abstract int getMinimum(int field);

        /// <summary>
        /// Gets this Calendar's current time.
        /// </summary>
        public Date getTime()
        {
            return default(Date);
        }

        /// <summary>
        /// Gets this Calendar's current time as a long.
        /// </summary>
        public long getTimeInMillis()
        {
            return default(long);
        }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        public TimeZone getTimeZone()
        {
            return default(TimeZone);
        }

        /// <summary>
        /// Gets the value for a given time field.
        /// </summary>
        protected int internalGet(int field)
        {
            return default(int);
        }

        /// <summary>
        /// Tell whether date/time interpretation is to be lenient.
        /// </summary>
        public bool isLenient()
        {
            return default(bool);
        }

        /// <summary>
        /// Determines if the given time field has a value set.
        /// </summary>
        public bool isSet(int field)
        {
            return default(bool);
        }

        /// <summary>
        /// Time Field Rolling function.
        /// </summary>
        public abstract void roll(int field, bool up);

        /// <summary>
        /// Time Field Rolling function.
        /// </summary>
        public void roll(int field, int amount)
        {
        }

        /// <summary>
        /// Sets the time field with the given value.
        /// </summary>
        public void set(int field, int value)
        {
        }

        /// <summary>
        /// Sets the values for the fields year, month, and date.
        /// </summary>
        public void set(int year, int month, int date)
        {
        }

        /// <summary>
        /// Sets the values for the fields year, month, date, hour, and minute.
        /// </summary>
        public void set(int year, int month, int date, int hour, int minute)
        {
        }

        /// <summary>
        /// Sets the values for the fields year, month, date, hour, minute, and second.
        /// </summary>
        public void set(int year, int month, int date, int hour, int minute, int second)
        {
        }

        /// <summary>
        /// Sets what the first day of the week is; e.g., Sunday in US, Monday in France.
        /// </summary>
        public void setFirstDayOfWeek(int value)
        {
        }

        /// <summary>
        /// Specify whether or not date/time interpretation is to be lenient.
        /// </summary>
        public void setLenient(bool lenient)
        {
        }

        /// <summary>
        /// Sets what the minimal days required in the first week of the year are; For example, if the first week is defined as one that contains the first day of the first month of a year, call the method with value 1.
        /// </summary>
        public void setMinimalDaysInFirstWeek(int value)
        {
        }

        /// <summary>
        /// Sets this Calendar's current time with the given Date.
        /// </summary>
        public void setTime(Date date)
        {
        }

        /// <summary>
        /// Sets this Calendar's current time from the given long value.
        /// </summary>
        public void setTimeInMillis(long millis)
        {
        }

        /// <summary>
        /// Sets the time zone with the given time zone value.
        /// </summary>
        public void setTimeZone(TimeZone value)
        {
        }

        #endregion


    }
}
