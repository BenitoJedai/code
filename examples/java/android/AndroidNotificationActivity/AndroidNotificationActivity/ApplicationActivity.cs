using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using AndroidNotificationActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

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

            b.setOnClickListener(
                new _onclick { that = this }
            );
            
            ll.addView(b);

            this.setContentView(sv);

            this.ShowLongToast("http://jsc-solutions.net");

          
        }


        class _onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;

            int counter;

            public void onClick(View v)
            {
                counter++;

                NotificationManager nm = (NotificationManager)that.getSystemService(Activity.NOTIFICATION_SERVICE);


                // see http://developer.android.com/reference/android/app/Notification.html
                Notification notification = new Notification(
                    android.R.drawable.star_on,
                    (CharSequence)(object)"The text that flows by in the status bar when the notification first activates.",
                     java.lang.System.currentTimeMillis()
                );

                notification.setLatestEventInfo(
                    that,
                    (CharSequence)(object)"The title that goes in the expanded entry.",
                    (CharSequence)(object)"The text that goes in the expanded entry.",
                    null);


                // http://androiddrawableexplorer.appspot.com/
                nm.notify(counter, notification);
            }
        }

    }
}
