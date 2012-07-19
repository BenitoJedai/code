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
using AndroidAlarmServiceActivity.Library;
using foo;
using java.lang;
using java.util;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidAlarmServiceActivity.Activities
{
    // http://android-er.blogspot.com/2010/10/simple-example-of-alarm-service-using.html

    public class ApplicationActivity : Activity
    {
        // http://stackoverflow.com/questions/6274141/trigger-background-service-at-a-specific-time-in-android
        // http://stackoverflow.com/questions/7144908/how-is-an-intent-service-declared-in-the-android-manifest
        // http://developer.android.com/guide/topics/manifest/service-element.html


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {

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

            //this.ShowToast("http://jsc-solutions.net");


        }

        public PendingIntent pendingIntent;

        class startservice_onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;

            public void onClick(View v)
            {
                that.ShowToast("start");

                var myIntent = new Intent(that, MyAlarmService.Class);
                that.pendingIntent = PendingIntent.getService(that, 0, myIntent, 0);

                AlarmManager alarmManager = (AlarmManager)that.getSystemService(ALARM_SERVICE);

      

                alarmManager.set(AlarmManager.RTC, 1000 * 5, that.pendingIntent);

            }
        }

        class stopservice_onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;


            public void onClick(View v)
            {
                that.ShowToast("stop");

                AlarmManager alarmManager = (AlarmManager)that.getSystemService(ALARM_SERVICE);

                alarmManager.cancel(that.pendingIntent);

            }
        }


    }

}

namespace foo
{


    public sealed class MyAlarmService : Service
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return foo.MyAlarmService.class;")]
            get
            {
                return null;
            }
        }



        public override void onCreate()
        {
            this.ShowToast("onCreate");
            base.onCreate();
        }

        public override void onStart(Intent value0, int value1)
        {
            this.ShowToast("onStart");
            base.onStart(value0, value1);
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            this.ShowToast("onStartCommand");
            return base.onStartCommand(value0, value1, value2);
        }

        public override void onDestroy()
        {
            this.ShowToast("onDestroy");
            base.onDestroy();
        }



        public override android.os.IBinder onBind(Intent value)
        {
            this.ShowToast("onBind");
            return null;
        }

        public override bool onUnbind(Intent value)
        {
            this.ShowToast("onUnbind");
            return base.onUnbind(value);
        }


    }


}