using System;
using android.app;
using android.content;
using android.net;
using android.util;
using android.widget;
using AndroidWifiNotificationActivity.Activities;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLibJava.Extensions;

namespace AndroidWifiNotificationActivity.Activities
{
	// http://android-er.blogspot.com/2011/04/start-service-to-send-notification.html
	// http://stackoverflow.com/questions/5365395/android-net-wifi-state-change-not-triggered-on-wifi-disconnect
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
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

            var btn = new Button(this);
            btn.setText("wifi");

            ll.addView(btn);

            {
                ConnectivityManager connManager = (ConnectivityManager)getSystemService(Context.CONNECTIVITY_SERVICE);
                NetworkInfo wifiNetInfo = connManager.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
                NetworkInfo mobileNetInfo = connManager.getNetworkInfo(ConnectivityManager.TYPE_MOBILE);


                if (wifiNetInfo != null)
                    if (wifiNetInfo.isConnectedOrConnecting())
                    {
                        btn.setText("we are connected via WiFi");
                    }

                if (mobileNetInfo != null)
                    if (mobileNetInfo.isAvailable())
                        if (mobileNetInfo.isConnected())
                        {
                            btn.setText(" we are connected via mobile data (GPRS, 3G, etc.)");
                        }
            }


            this.setContentView(sv);

            this.StartPendingAlarm(typeof(NotifyService), 1000 * 1, 0);


        }





    }

    public sealed class NotifyService : Service
    {


        public override android.os.IBinder onBind(Intent Intent0)
        {
            return null;
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            Log.wtf("AndroidWifiNotificationActivity", "onStartCommand");

            this.ToNotification(
                Title: "wifi",
                Content: "Toggle wifi to see notifications",

                id: (int)java.lang.System.currentTimeMillis(),
                icon: android.R.drawable.star_on,
                uri: "http://my.jsc-solutions.net"
            );


            #region Wifi
            {
                ConnectivityManager connManager = (ConnectivityManager)getSystemService(Context.CONNECTIVITY_SERVICE);
                NetworkInfo wifiNetInfo = connManager.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
                NetworkInfo mobileNetInfo = connManager.getNetworkInfo(ConnectivityManager.TYPE_MOBILE);

                if (wifiNetInfo != null)
                    if (wifiNetInfo.isConnectedOrConnecting())
                    {
                        this.ToNotification(
                           Title: "wifi!",
                              Content: "we are connected via WiFi",

                              id: (int)java.lang.System.currentTimeMillis(),
                                icon: android.R.drawable.star_on,
                              uri: "http://my.jsc-solutions.net"
                          );



                    }


                if (mobileNetInfo != null)
                    if (mobileNetInfo.isAvailable())
                        if (mobileNetInfo.isConnected())
                        {
                            this.ToNotification(
                               Title: "wifi!",
                               Content: "we are connected via mobile data (GPRS, 3G, etc.)",

                               id: (int)java.lang.System.currentTimeMillis(),
                                 icon: android.R.drawable.star_on,
                               uri: "http://my.jsc-solutions.net"
                           );



                        }
            }
            #endregion



            this.stopSelf();

            return 0;
        }





    }


}

namespace foo
{
    [IntentFilter(Action = android.net.wifi.WifiManager.SUPPLICANT_CONNECTION_CHANGE_ACTION)]
    public class AtConnectionChange : BroadcastReceiver
    {
        public override void onReceive(Context c, Intent i)
        {
            var that = c;

            Log.wtf("AndroidWifiNotificationActivity", "AtConnectionChange");

            c.StartPendingAlarm(typeof(NotifyService), 1000 * 7, 0);

        }
    }


}

