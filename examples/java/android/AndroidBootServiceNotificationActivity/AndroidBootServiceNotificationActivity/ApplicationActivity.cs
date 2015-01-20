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
//using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Threading.Tasks;
using System.Threading;

namespace AndroidBootServiceNotificationActivity.Activities
{
    // http://android-er.blogspot.com/2011/04/start-service-to-send-notification.html

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://github.com/opersys/raidl


        // http://stackoverflow.com/questions/6274141/trigger-background-service-at-a-specific-time-in-android
        // http://stackoverflow.com/questions/7144908/how-is-an-intent-service-declared-in-the-android-manifest
        // http://developer.android.com/guide/topics/manifest/service-element.html

        //AtBootCompleted hack1;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);

            #region startservice
            var startservice = new Button(this);
            startservice.setText("Start Service to send Notification");
            startservice.AtClick(
                delegate
                {
                    startservice.setEnabled(false);
                    this.ShowToast("startservice_onclick");

                    //var intent = new Intent(this, NotifyService.Class);
                    var intent = new Intent(this, typeof(NotifyService).ToClass());
                    this.startService(intent);

                    // http://developer.android.com/reference/android/app/Activity.html#recreate%28%29
                    this.recreate();
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

                    var intent = new Intent();
                    intent.setAction(NotifyService.ACTION);
                    intent.putExtra("RQS", NotifyService.RQS_STOP_SERVICE);
                    this.sendBroadcast(intent);

                    // seems stop takes a while

                    //Task.Delay(100);

                    Thread.Sleep(30);

                    this.recreate();
                }
            );
            ll.addView(stopservice);
            #endregion

            stopservice.setEnabled(false);

            // http://stackoverflow.com/questions/12891903/android-check-if-my-service-is-running-in-the-background
            var m = (ActivityManager)this.getSystemService(Context.ACTIVITY_SERVICE);


            Console.WriteLine("getRunningServices");

            var s = m.getRunningServices(1000);

            Console.WriteLine("getRunningServices " + s.size());

            var se = Enumerable.Range(0, s.size()).Select(i => (android.app.ActivityManager.RunningServiceInfo)s.get(i));

            foreach (var ss in se)
            {


                var cn = ss.service.getClassName();

                Console.WriteLine(new { cn });

                // I/System.Console(17713): { cn = AndroidBootServiceNotificationActivity.Activities.NotifyService }
                if (cn == typeof(NotifyService).FullName)
                {

                    startservice.setEnabled(false);
                    stopservice.setEnabled(true);

                    // its running

                    // http://stackoverflow.com/questions/7170730/how-to-set-a-control-panel-for-my-service-in-android
                    // http://www.techques.com/question/1-7170730/How-to-set-a-control-panel-for-my-Service-in-Android
                    // http://alvinalexander.com/java/jwarehouse/android/core/java/android/app/ActivityManagerNative.java.shtml

#if XCONTROLPANEL
                    PendingIntent cp = m.getRunningServiceControlPanel(ss.service);

                    Console.WriteLine(new { cp });
                    if (cp != null)
                    {
                    #region cpb
                        var cpb = new Button(this);
                        cpb.setText("ServiceControlPanel");
                        cpb.AtClick(
                            delegate
                            {
                                //new Intent(
                                //PendingIntent.getActivity(
                                //startActivity(cp);

                                // http://iserveandroid.blogspot.com/2011/03/how-to-launch-pending-intent.html
                                Intent intent = new Intent();

                                try
                                {
                                    cp.send(this, 0, intent);
                                }
                                catch
                                {

                                    throw;
                                }

                            }
                        );
                        ll.addView(cpb);
                    }
                        #endregion
#endif



                }
            }

            this.setContentView(sv);

            this.ShowToast("http://jsc-solutions.net");


        }


    }

    public sealed class NotifyService : Service
    {
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
            var intentFilter = new IntentFilter();
            intentFilter.addAction(ACTION);
            registerReceiver(notifyServiceReceiver, intentFilter);


            // Send Notification
            var notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

            var myNotification = new Notification(
                android.R.drawable.star_on,
                //(CharSequence)(object)"Boot!!",
                "Boot!!",
                java.lang.System.currentTimeMillis()
            );

            var context = getApplicationContext();

            var myIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse("http://youtube.com"));

            var pendingIntent
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

            var intent = new Intent(that, typeof(NotifyService).ToClass());
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

