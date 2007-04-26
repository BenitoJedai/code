using ScriptCoreLib;

namespace java.util
{

    [Script(IsNative = true)]
    public abstract class TimeZone
    {
        #region methods
        /// <summary>
        /// Creates a copy of this <code>TimeZone</code>.
        /// </summary>
        public object clone()
        {
            return default(object);
        }

        /// <summary>
        /// Gets all the available IDs supported.
        /// </summary>
        public static string[] getAvailableIDs()
        {
            return default(string[]);
        }

        /// <summary>
        /// Gets the available IDs according to the given time zone offset.
        /// </summary>
        public static string[] getAvailableIDs(int rawOffset)
        {
            return default(string[]);
        }

        /// <summary>
        /// Gets the default <code>TimeZone</code> for this host.
        /// </summary>
        public static TimeZone getDefault()
        {
            return default(TimeZone);
        }

        /// <summary>
        /// Returns a name of this time zone suitable for presentation to the user in the default locale.
        /// </summary>
        public string getDisplayName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a name of this time zone suitable for presentation to the user in the default locale.
        /// </summary>
        public string getDisplayName(bool daylight, int style)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a name of this time zone suitable for presentation to the user in the specified locale.
        /// </summary>
        public string getDisplayName(bool daylight, int style, Locale locale)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a name of this time zone suitable for presentation to the user in the specified locale.
        /// </summary>
        public string getDisplayName(Locale locale)
        {
            return default(string);
        }

        /// <summary>
        /// Returns the amount of time to be added to local standard time to get local wall clock time.
        /// </summary>
        public int getDSTSavings()
        {
            return default(int);
        }

        /// <summary>
        /// Gets the ID of this time zone.
        /// </summary>
        public string getID()
        {
            return default(string);
        }

        /// <summary>
        /// Gets the time zone offset, for current date, modified in case of daylight savings.
        /// </summary>
        public abstract int getOffset(int era, int year, int month, int day, int dayOfWeek, int milliseconds);

        /// <summary>
        /// Returns the offset of this time zone from UTC at the specified date.
        /// </summary>
        public int getOffset(long date)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the amount of time in milliseconds to add to UTC to get standard time in this time zone.
        /// </summary>
        public abstract int getRawOffset();

        /// <summary>
        /// Gets the <code>TimeZone</code> for the given ID.
        /// </summary>
        public static TimeZone getTimeZone(string ID)
        {
            return default(TimeZone);
        }

        /// <summary>
        /// Returns true if this zone has the same rule and offset as another zone.
        /// </summary>
        public bool hasSameRules(TimeZone other)
        {
            return default(bool);
        }

        /// <summary>
        /// Queries if the given date is in daylight savings time in this time zone.
        /// </summary>
        public abstract bool inDaylightTime(Date date);

        /// <summary>
        /// Sets the <code>TimeZone</code> that is returned by the <code>getDefault</code> method.
        /// </summary>
        public static void setDefault(TimeZone zone)
        {
        }

        /// <summary>
        /// Sets the time zone ID.
        /// </summary>
        public void setID(string ID)
        {
        }

        /// <summary>
        /// Sets the base time zone offset to GMT.
        /// </summary>
        public abstract void setRawOffset(int offsetMillis);

        /// <summary>
        /// Queries if this time zone uses daylight savings time.
        /// </summary>
        public abstract bool useDaylightTime();

        #endregion


    }
}
