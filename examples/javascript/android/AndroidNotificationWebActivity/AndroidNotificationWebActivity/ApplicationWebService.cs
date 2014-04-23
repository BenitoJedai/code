using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLibJava.Extensions;
using android.content;
using android.app;

namespace AndroidNotificationWebActivity
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // https://code.google.com/p/android/issues/detail?id=14869
            // seems to exist!
            var VMRuntime = Type.GetType("dalvik.system.VMRuntime");

            e += new { VMRuntime };

            Console.WriteLine(new { VMRuntime });



            var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            var intent = new Intent(c, typeof(foo.NotifyService).ToClass());

            intent.putExtra("data0", e);

            c.startService(intent);

            // http://stackoverflow.com/questions/7598835/alternative-of-vmruntime-getruntime-setminimumheapsize-in-gingerbread


            if (VMRuntime != null)
            {

                //Implementation not found for type import :
                //type: System.Type
                //method: System.Reflection.MethodInfo GetMethod(System.String)
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!


                //var VMRuntime_getRuntime = VMRuntime.GetMethod("getRuntime");
                var VMRuntime_getRuntime = VMRuntime.GetMethod("getRuntime", new Type[0]);

                Console.WriteLine(new { VMRuntime_getRuntime });

                // https://android.googlesource.com/platform/libcore/+/9edf43dfcc35c761d97eb9156ac4254152ddbc55/libdvm/src/main/java/dalvik/system/VMRuntime.java
                var r = VMRuntime_getRuntime.Invoke(null, new object[0]);

                Console.WriteLine(new { r });

                //I/System.Console( 8079): { VMRuntime = dalvik.system.VMRuntime }
                //I/System.Console( 8079): { VMRuntime_getRuntime = dalvik.system.VMRuntime getRuntime() }
                //I/System.Console( 8079): { r = dalvik.system.VMRuntime@41764740 }

            }


            // Send it back to the caller.
            y(e);
        }

    }


}

// namespace AndroidNotificationWebActivity rename causes a conflict in manifset, workaround:
namespace foo
{
    public sealed class NotifyService : Service
    {
        public const string ACTION = "NotifyServiceAction";

        //public const int RQS_STOP_SERVICE = 1;

        //NotifyServiceReceiver notifyServiceReceiver;

        public override void onCreate()
        {
            //notifyServiceReceiver = new NotifyServiceReceiver { that = this };

            base.onCreate();
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            IntentFilter intentFilter = new IntentFilter();
            intentFilter.addAction(ACTION);
            //registerReceiver(notifyServiceReceiver, intentFilter);


            // Send Notification
            var notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

            var Title = "Notification!";

            var TargetUri = "http://www.jsc-solutions.net";

            if (value0.hasExtra("data0"))
                Title = value0.getStringExtra("data0");

            if (value0.hasExtra("data1"))
                TargetUri = value0.getStringExtra("data1");


            var myNotification = new Notification(
                // http://docs.since2006.com/android/2.1-drawables.php
                android.R.drawable.ic_menu_view,
                Title,
                java.lang.JavaSystem.currentTimeMillis()
                //java.lang.System.currentTimeMillis()
            );

            var context = getApplicationContext();

            var myIntent = new Intent(Intent.ACTION_VIEW,
                android.net.Uri.parse(TargetUri));

            var pendingIntent = PendingIntent.getActivity(
                getBaseContext(),
                0, myIntent,
                Intent.FLAG_ACTIVITY_NEW_TASK
            );

            myNotification.defaults |= Notification.DEFAULT_SOUND;
            myNotification.flags |= Notification.FLAG_AUTO_CANCEL;
            myNotification.setLatestEventInfo(context,
                    Title,
                    TargetUri,
               pendingIntent);
            notificationManager.notify(1, myNotification);


            this.stopSelf();

            return 0;
        }

        public override void onDestroy()
        {
            //this.unregisterReceiver(notifyServiceReceiver);
            base.onDestroy();
        }



        public override android.os.IBinder onBind(Intent value)
        {
            return null;
        }


    }

}