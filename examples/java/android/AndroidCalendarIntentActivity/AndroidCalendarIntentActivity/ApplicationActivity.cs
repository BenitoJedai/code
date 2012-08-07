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

namespace AndroidCalendarIntentActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // inspired by http://mobile.tutsplus.com/tutorials/android/android-essentials-adding-events-to-the-user%E2%80%99s-calendar/

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this);
            ll.addView(b);
            b.setText("Create Event!");
            b.AtClick(
                v =>
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



                    calIntent.putExtra("accessLevel", 0x00000002);
                    calIntent.putExtra("availability", 0x00000000);

                    calIntent.putExtra("rrule", "FREQ=WEEKLY;COUNT=10;WKST=SU;BYDAY=TU,TH");

                    startActivity(calIntent);
                }
            );



            this.setContentView(sv);
        }


    }


}
