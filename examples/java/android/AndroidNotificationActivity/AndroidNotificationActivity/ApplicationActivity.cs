using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;

namespace AndroidNotificationActivity.Activities
{
    public class ApplicationActivity : Activity
    {
 


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            Button b = new Button(this);

            b.setText("Notify!");
            int counter = 0;

            b.AtClick(
                delegate
                {
                    counter++;
                    NotificationManager nm = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);


                    // see http://developer.android.com/reference/android/app/Notification.html
                    Notification notification = new Notification(
                        android.R.drawable.star_on,
                        "The text that flows by in the status bar when the notification first activates.",
                         java.lang.System.currentTimeMillis()
                    );

                    Intent notificationIntent = new Intent(this, typeof(ApplicationActivity).ToClass());
                    PendingIntent contentIntent = PendingIntent.getActivity(this, 0, notificationIntent, 0);


                    notification.setLatestEventInfo(
                        this,
                        "The title that goes in the expanded entry.",
                        "The text that goes in the expanded entry.",
                        contentIntent);

                    notification.defaults |= Notification.DEFAULT_VIBRATE;
                    notification.defaults |= Notification.DEFAULT_LIGHTS;
                    // http://androiddrawableexplorer.appspot.com/
                    nm.notify(counter, notification);
                }
            );

            ll.addView(b);

            this.setContentView(sv);

            this.ShowLongToast("http://jsc-solutions.net");


        }


  

    }
}
