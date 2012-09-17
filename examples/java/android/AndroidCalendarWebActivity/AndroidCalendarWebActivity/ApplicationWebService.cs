using android.provider;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace AndroidCalendarWebActivity
{
    public delegate void WebMethod2Handler(string Location, string EventText);

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void GetEventText(string e, WebMethod2Handler y)
        {
            var COLS = new[]
            { 
                "title",
                "dtstart"
            };

            var mCursor = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getContentResolver().query(
                CalendarContract.Events.CONTENT_URI, COLS, null, null, null
            );

            mCursor.moveToLast();
            //mCursor.getPosition()

            var Location = CalendarContract.Events.CONTENT_URI.ToString();

            var EventText = "";

            var title = mCursor.getString(0);
            var start = ((object)mCursor.getLong(1)).ToString();


            EventText += title;
            EventText += " at ";
            EventText += start;

            mCursor.close();

            // Send it back to the caller.
            y(Location, EventText);
        }

    }
}
