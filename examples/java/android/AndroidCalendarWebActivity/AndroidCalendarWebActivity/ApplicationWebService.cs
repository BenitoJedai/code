using android.content;
using android.provider;
using java.util;
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

        public void CreateEvent(string Title, string Location, string Description, Action y)
        {
            // http://developer.android.com/reference/android/provider/CalendarContract.EventsColumns.html#DESCRIPTION

            Intent calIntent = new Intent(Intent.ACTION_INSERT);
            calIntent.setType("vnd.android.cursor.item/event");
            calIntent.putExtra("title", Title);
            calIntent.putExtra("eventLocation", Location);
            calIntent.putExtra("description", Description);

            GregorianCalendar calDate = new GregorianCalendar(2012, 7, 15);
            calIntent.putExtra(CalendarContract.EXTRA_EVENT_ALL_DAY, true);
            calIntent.putExtra(CalendarContract.EXTRA_EVENT_BEGIN_TIME,
                 calDate.getTimeInMillis());
            calIntent.putExtra(CalendarContract.EXTRA_EVENT_END_TIME,
                 calDate.getTimeInMillis());



            calIntent.putExtra("accessLevel", 0x00000002);
            calIntent.putExtra("availability", 0x00000000);

            calIntent.putExtra("rrule", "FREQ=WEEKLY;COUNT=10;WKST=SU;BYDAY=TU,TH");

            y();

            // well spawn another activity/thread
            ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.startActivity(calIntent);
        }

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

            var p = mCursor.getPosition();

            mCursor.moveToPosition(p - int.Parse(e));


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
