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
using AndroidBootServiceNotificationActivity.Activities;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidBootServiceNotificationActivity.Activities
{
    // http://android-er.blogspot.com/2011/04/start-service-to-send-notification.html

    public class ApplicationActivity : Activity
    {
        // http://stackoverflow.com/questions/6274141/trigger-background-service-at-a-specific-time-in-android
        // http://stackoverflow.com/questions/7144908/how-is-an-intent-service-declared-in-the-android-manifest
        // http://developer.android.com/guide/topics/manifest/service-element.html

        //AtBootCompleted hack1;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);

            #region startservice
            var startservice = new Button(this);
            startservice.setText("Start Service to send Notification");
            startservice.AtClick(
                delegate
                {
                    this.ShowToast("startservice_onclick");

                    var intent = new Intent(this, NotifyService.Class);
                    this.startService(intent);
                }
            );
            ll.addView(startservice);
            #endregion

            #region stopservice
            var stopservice = new Button(this);
            stopservice.setText("Stop Service");
            stopservice.AtClick(
                delegate
                {
                    this.ShowToast("stopservice_onclick");

                    Intent intent = new Intent();
                    intent.setAction(NotifyService.ACTION);
                    intent.putExtra("RQS", NotifyService.RQS_STOP_SERVICE);
                    this.sendBroadcast(intent);
                }
            );
            ll.addView(stopservice);
            #endregion

            this.setContentView(sv);

            this.ShowToast("http://jsc-solutions.net");


        }



    }

    public sealed class NotifyService : Service
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return NotifyService.class;")]
            get
            {
                return null;
            }
        }

        public const string ACTION = "NotifyServiceAction";

        public const int RQS_STOP_SERVICE = 1;

        NotifyServiceReceiver notifyServiceReceiver;

        public override void onCreate()
        {
            notifyServiceReceiver = new NotifyServiceReceiver { that = this };

            base.onCreate();
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            IntentFilter intentFilter = new IntentFilter();
            intentFilter.addAction(ACTION);
            registerReceiver(notifyServiceReceiver, intentFilter);


            // Send Notification
            var notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

            var myNotification = new Notification(
                android.R.drawable.star_on,
                (CharSequence)(object)"Boot!!",
                java.lang.System.currentTimeMillis()
            );

            Context context = getApplicationContext();

            Intent myIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse("http://www.jsc-solutions.net"));

            PendingIntent pendingIntent
              = PendingIntent.getActivity(getBaseContext(),
                0, myIntent,
                Intent.FLAG_ACTIVITY_NEW_TASK);
            myNotification.defaults |= Notification.DEFAULT_SOUND;
            myNotification.flags |= Notification.FLAG_AUTO_CANCEL;
            myNotification.setLatestEventInfo(context,
                    "Boot!!",
                    "Proud to be a jsc developer :)",
               pendingIntent);
            notificationManager.notify(1, myNotification);


            return base.onStartCommand(value0, value1, value2);
        }

        public override void onDestroy()
        {
            this.unregisterReceiver(notifyServiceReceiver);
            base.onDestroy();
        }



        public override android.os.IBinder onBind(Intent value)
        {
            return null;
        }


        public class NotifyServiceReceiver : BroadcastReceiver
        {
            public NotifyService that;

            public override void onReceive(Context c, Intent i)
            {
                //android.content.IntentFilter
                //android.content.Intent.ACTION_BOOT_COMPLETED
                int rqs = i.getIntExtra("RQS", 0);

                if (rqs == RQS_STOP_SERVICE)
                    that.stopSelf();
            }
        }
    }


}

namespace foo
{


    // android.intent.action.BOOT_COMPLETED
    [IntentFilter(Action = Intent.ACTION_BOOT_COMPLETED)]
    //[IntentFilter(Action = "android.intent.action.BOOT_COMPLETED")]
    public class AtBootCompleted : BroadcastReceiver
    {
        public override void onReceive(Context c, Intent i)
        {
            var that = c;

            //that.ShowToast("AtBootCompleted");

            var intent = new Intent(that, NotifyService.Class);
            that.startService(intent);
        }
    }

    //[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    //sealed class IntentFilterAttribute : Attribute
    //{
    //    // jsc does not support properties yet? are they even allowed in java?

    //    public string Action;
    //}

}

