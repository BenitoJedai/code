using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script()]
    public class DateTimeDaysInMonth
    {
        private static readonly int[] DaysToMonth366;
        private static readonly int[] DaysToMonth365;
        
        static DateTimeDaysInMonth ()
	    {
            DaysToMonth365 = __ArrayDummy(0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365);
            DaysToMonth365[0] = 0;
            DaysToMonth366 = __ArrayDummy(0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366);
            DaysToMonth366[0] = 0;
	    }

        static T[] __ArrayDummy<T>(params T[] e)
        {
            return e;
        }

         public static int DaysInMonth(int year, int month)
        {
            if (month < 1)
                throw new Exception("ArgumentOutOfRange_Month");
            if (month > 12)
                throw new Exception("ArgumentOutOfRange_Month");

            int[] numArray = DaysToMonth365;

            if (IsLeapYear(year)) numArray = DaysToMonth366;

            return (numArray[month] - numArray[month - 1]);
        }

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
