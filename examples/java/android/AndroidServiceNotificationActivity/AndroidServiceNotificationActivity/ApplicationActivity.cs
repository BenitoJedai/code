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
using AndroidServiceNotificationActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidServiceNotificationActivity.Activities
{
    // http://android-er.blogspot.com/2011/04/start-service-to-send-notification.html

    public class ApplicationActivity : Activity
    {
        // http://stackoverflow.com/questions/6274141/trigger-background-service-at-a-specific-time-in-android
        // http://stackoverflow.com/questions/7144908/how-is-an-intent-service-declared-in-the-android-manifest
        // http://developer.android.com/guide/topics/manifest/service-element.html


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
            startservice.setOnClickListener(
                new startservice_onclick { that = this }
            );
            ll.addView(startservice);
            #endregion

            #region stopservice
            var stopservice = new Button(this);
            stopservice.setText("Stop Service");
            stopservice.setOnClickListener(
                new stopservice_onclick { that = this }
            );
            ll.addView(stopservice);
            #endregion

            this.setContentView(sv);

            this.ShowToast("http://jsc-solutions.net");


        }


        class startservice_onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;

            public void onClick(View v)
            {
                that.ShowToast("startservice_onclick");

                var intent = new Intent(that, NotifyService.Class);
                that.startService(intent);

            }
        }

        class stopservice_onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;


            public void onClick(View v)
            {
                that.ShowToast("stopservice_onclick");

                Intent intent = new Intent();
                intent.setAction(NotifyService.ACTION);
                intent.putExtra("RQS", NotifyService.RQS_STOP_SERVICE);
                that.sendBroadcast(intent);

            }
        }


    }

    public sealed class NotifyService : Service
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return AndroidServiceNotificationActivity.Activities.NotifyService.class;")]
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
                (CharSequence)(object)"Notification!",
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
                    (CharSequence)(object)"The title that goes in the expanded entry.",
                    (CharSequence)(object)"The text that goes in the expanded entry.",
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
                int rqs = i.getIntExtra("RQS", 0);

                if (rqs == RQS_STOP_SERVICE)
                    that.stopSelf();
            }
        }
    }


}
