using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using android.content;
using java.util;
using android.provider;
using android.text.format;
using java.text;

namespace AndroidCalendarIntentActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // inspired by 
        // http://mobile.tutsplus.com/tutorials/android/android-essentials-adding-events-to-the-user%E2%80%99s-calendar/
        // http://www.techrepublic.com/blog/app-builder/programming-with-the-android-40-calendar-api-the-good-the-bad-and-the-ugly/825
        // http://stackoverflow.com/questions/8881851/how-can-i-add-a-new-phone-calendar-to-android
        // http://stackoverflow.com/questions/7859005/how-to-read-and-edit-android-calendar-events-using-the-new-android-4-0-ice-cream

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            new Button(this)
                .WithText("Create Event!")
                .AttachTo(ll)
                .AtClick(
                    b =>
                    {
                        b.setText("Done!");

                        // http://developer.android.com/reference/android/provider/CalendarContract.EventsColumns.html#DESCRIPTION

                        Intent calIntent = new Intent(Intent.ACTION_INSERT);
                        calIntent.setType("vnd.android.cursor.item/event");
                        calIntent.putExtra("title", "My House Party");
                        calIntent.putExtra("eventLocation", "My Beach House");
                        calIntent.putExtra("description", "A Pig Roast on the Beach");

                        GregorianCalendar calDate = new GregorianCalendar(2012, 7, 15);
                        calIntent.putExtra(CalendarContract.EXTRA_EVENT_ALL_DAY, true);
                        calIntent.putExtra(CalendarContract.EXTRA_EVENT_BEGIN_TIME,
                             calDate.getTimeInMillis());
                        calIntent.putExtra(CalendarContract.EXTRA_EVENT_END_TIME,
                             calDate.getTimeInMillis());





                        //ttdw


                        calIntent.putExtra("accessLevel", 0x00000002);
                        calIntent.putExtra("availability", 0x00000000);

                        calIntent.putExtra("rrule", "FREQ=WEEKLY;COUNT=10;WKST=SU;BYDAY=TU,TH");

                        startActivity(calIntent);
                    }
            );

            var COLS = new[]
            { 
                "title",
                "dtstart"
            };


            var at = new TextView(this).AttachTo(ll);

            at.setText("at " + CalendarContract.Events.CONTENT_URI.ToString());

            // from t in calendars
            // select t.title

            var mCursor = this.getContentResolver().query(
                CalendarContract.Events.CONTENT_URI, COLS, null, null, null
            );

            mCursor.moveToLast();

            var tv = new TextView(this).AttachTo(ll);
            tv.setText("n/a");
            Action update =
                delegate
                {
                    var title = "";
                    var start = "";
                    var w = "";

                    //Format df = android.text.format.DateFormat.getDateFormat(this);
                    //Format tf = android.text.format.DateFormat.getTimeFormat(this);

                    //try
                    //{
                    title = mCursor.getString(0);
                    start = ((object)mCursor.getLong(1)).ToString();


                    w += title;
                    w += " at ";
                    w += start;

                    tv.setText(w);
                    //}
                    //catch
                    //{
                    //    tv.setText("n/a error");

                    //    throw;
                    //}



                };

            Button prev, next = null;

            prev = new Button(this)
                  .WithText("Prev")
                  .AttachTo(ll)
                  .AtClick(
                      b =>
                      {
                          if (!mCursor.isFirst())
                          {
                              mCursor.moveToPrevious();
                              next.setEnabled(true);
                          }
                          else
                              b.setEnabled(false);

                          update();
                      }
            );

            next = new Button(this)
                .WithText("Next")
                .AttachTo(ll)
                .AtClick(
                    b =>
                    {

                        if (!mCursor.isLast())
                        {
                            mCursor.moveToNext();
                            prev.setEnabled(true);
                        }
                        else
                            b.setEnabled(false);

                        update();
                    }
          );

            update();


            this.setContentView(sv);
        }


    }


}
