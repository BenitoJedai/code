using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Controls
{

    [Script]
    public class MonthCalendar
    {
        // Summary:
        //     Specifies the day of the week.
        public enum Day
        {
            // Summary:
            //     The day Monday.
            Monday = 0,
            //
            // Summary:
            //     The day Tuesday.
            Tuesday = 1,
            //
            // Summary:
            //     The day Wednesday.
            Wednesday = 2,
            //
            // Summary:
            //     The day Thursday.
            Thursday = 3,
            //
            // Summary:
            //     The day Friday.
            Friday = 4,
            //
            // Summary:
            //     The day Saturday.
            Saturday = 5,
            //
            // Summary:
            //     The day Sunday.
            Sunday = 6,
            //
            // Summary:
            //     A default day of the week specified by the application.
            Default = 7,
        }

        public IHTMLElement Control = new IHTMLDiv();

        /// <summary>Returns the number of days in the specified month and year.</summary>
        /// <returns>The number of days in month for the specified year.For example, if month equals 2 for February, the return value is 28 or 29 depending upon whether year is a leap year.</returns>
        /// <param name="month">The month (a number ranging from 1 to 12). </param>
        /// <param name="year">The year. </param>
        public static int DaysInMonth(int year, int month)
        {
            if ((month < 1) || (month > 12))
            {
                throw new ScriptException("ArgumentOutOfRange_Month");
            }
            int[] numArray = IsLeapYear(year) ? DaysToMonth366 : DaysToMonth365;
            return (numArray[month] - numArray[month - 1]);
        }



        /// <summary>Returns an indication whether the specified year is a leap year.</summary>
        /// <returns>true if year is a leap year; otherwise, false.</returns>
        /// <param name="year">A 4-digit year. </param>
        public static bool IsLeapYear(int year)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ScriptException("ArgumentOutOfRange_Year");
            }
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



        private static int[] DaysToMonth365;
        private static int[] DaysToMonth366;

        static T[] ToArray<T>(params T[] e)
        {
            return e;
        }

        static MonthCalendar()
        {
            DaysToMonth365 = ToArray( 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 );
            DaysToMonth366 = ToArray( 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 );

        }

        static int DayOfYear()
        {

        }

        public MonthCalendar()
        {
            Control.style.border = "1px solid blue";


            var d = IDate.Now;

            
            Console.WriteLine("Current year: " + d.getFullYear());
            Console.WriteLine("Current month: " + d.getMonth());
            Console.WriteLine("Current days in month: " + DaysInMonth(d.getFullYear(), d.getMonth()));
            Console.WriteLine("Current date: " + d.getDate());
            Console.WriteLine("Current time ticks: " + d.getTime());
            
            //  1175424705484

            // 1175424885531
            // 63311032466828

            Control.appendChild("This Control was created at " + IDate.Now.toLocaleString());

        }
    }
}
