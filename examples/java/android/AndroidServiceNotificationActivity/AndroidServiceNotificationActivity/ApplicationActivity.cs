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
                //var intent = new Intent(AndroidNotifyService.this, com.exercise.AndroidNotifyService.NotifyService.class);
                //AndroidNotifyService.this.startService(intent);

                that.ShowToast("startservice_onclick");
            }
        }

        class stopservice_onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;


            public void onClick(View v)
            {
                //Intent intent = new Intent();
                //intent.setAction(Activity.NotifyService.ACTION);
                //intent.putExtra("RQS", NotifyService.STOP_SERVICE);
                //intent.putExtra("RQS", NotifyService.RQS_STOP_SERVICE);
                //that.sendBroadcast(intent);

                that.ShowToast("stopservice_onclick");
            }
        }


    }

    public class NotifyService : Service
    {
        NotifyServiceReceiver notifyServiceReceiver;

        public override void onCreate()
        {
            notifyServiceReceiver = new NotifyServiceReceiver { that = this };

            base.onCreate();
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
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
            }
        }
    }


}
