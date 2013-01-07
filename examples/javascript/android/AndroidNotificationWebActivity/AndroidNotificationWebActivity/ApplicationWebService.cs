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
            var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            var intent = new Intent(c, typeof(foo.NotifyService).ToClass());

            intent.putExtra("data0", e);

            c.startService(intent);

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

            if (value0.hasExtra("data0"))
                Title = value0.getStringExtra("data0");

            var myNotification = new Notification(
                android.R.drawable.star_on,
                Title,
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
                    "The title that goes in the expanded entry.",
                    "The text that goes in the expanded entry.",
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