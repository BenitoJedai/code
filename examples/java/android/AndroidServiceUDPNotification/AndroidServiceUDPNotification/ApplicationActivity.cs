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
using AndroidServiceUDPNotification.Activities;
//using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using android.net.wifi;

namespace AndroidServiceUDPNotification.Activities
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
            startservice.setText("Start Service");
            startservice.AtClick(
                delegate
            {
                startservice.setEnabled(false);
                //this.ShowToast("startservice_onclick");

                //var intent = new Intent(this, NotifyService.Class);
                var intent = new Intent(this, typeof(NotifyService).ToClass());
                this.startService(intent);

                // http://developer.android.com/reference/android/app/Activity.html#recreate%28%29
                //this.recreate();
                this.finish();
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

                // I/System.Console(17713): { cn = AndroidServiceUDPNotification.Activities.NotifyService }
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
            #region Notify
            int counter = 0;
            Action<string, string, string> Notify =
                (Title, link, search) =>
                {
                    counter++;

                    var nm = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);


                    //var nn = new NotificationCompat
                    // see http://developer.android.com/reference/android/app/Notification.html
                    var notification = new Notification(
                        android.R.drawable.star_on,
                        Title,
                         java.lang.System.currentTimeMillis()
                    );

                    // ToClass is like GetTypeInfo
                    //var xmyIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse("http://youtube.com"));

                    //http://stackoverflow.com/questions/9860456/search-a-specific-string-in-youtube-application-from-my-app

                    // http://grokbase.com/t/gg/android-developers/123s02429a/use-marquee-on-notification-bar

                    Intent xmyIntent = new Intent(Intent.ACTION_SEARCH);
                    xmyIntent.setPackage("com.google.android.youtube");
                    xmyIntent.putExtra("query", search);

                    // https://code.google.com/p/android/issues/detail?id=82065
                    // http://stackoverflow.com/questions/11939018/scrolling-text-in-notification
                    xmyIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                    //startActivity(intent);

                    //var xmyIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse(link));


 //[javac] W:\src\JVMCLRBroadcastLogger\__AndroidMulticast.java:165: error: bad operand type __Func_2< __f__AnonymousType_109_1_2<__NetworkInterface, Boolean>,__IEnumerable_1 < __UnicastIPAddressInformation >> for unary operator '!'
 //     [javac]             if (!__AndroidMulticast.CS___9__CachedAnonymousMethodDelegate10)
 //[javac] ^

                    var xpendingIntent
                      = PendingIntent.getActivity(
                          getBaseContext(),
                        0,
                        xmyIntent,
                        Intent.FLAG_ACTIVITY_NEW_TASK);


                    notification.setLatestEventInfo(
                        this,
                        Title,
                       Title,
                        xpendingIntent);

                    
                    // http://stackoverflow.com/questions/10402686/how-to-have-led-light-notification
                    notification.defaults |= Notification.DEFAULT_VIBRATE;
                    //notification.defaults |= Notification.DEFAULT_SOUND;
                    //notification.defaults |= Notification.DEFAULT_LIGHTS;
                    notification.defaults |= Notification.FLAG_SHOW_LIGHTS;


                    //new Notification.BigTextStyle(
                    
                    // http://androiddrawableexplorer.appspot.com/
                    nm.notify(counter, notification);

                    //context.ToNotification(
                    //      Title: Title,
                    //      Content: Title,

                    //      id: (int)java.lang.System.currentTimeMillis(),
                    //        icon: android.R.drawable.star_on,
                    //      uri: "http://my.jsc-solutions.net"
                    //  );
                };
            #endregion

            var intentFilter = new IntentFilter();
            intentFilter.addAction(ACTION);
            registerReceiver(notifyServiceReceiver, intentFilter);

            //            0001 02000006 AndroidServiceUDPNotification.AndroidActivity::< module >.SHA1c9cbee88a1edabb97eb411ca262280fe2fa18dd1@229018826
            //{ OwnerMethod = Int32 onStartCommand(android.content.Intent, Int32, Int32), DeclaringType = AndroidServiceUDPNotification.Activities.NotifyService }
            //            {
            //                exc = System.Security.VerificationException: Operation could destabilize the runtime.
            //                 at System.RuntimeMethodHandle.GetMethodBody(IRuntimeMethodInfo method, RuntimeType declaringType)
            //               at System.Reflection.RuntimeMethodInfo.GetMethodBody()
            //               at jsc.ILBlock..ctor(MethodBase SourceMethod) in x:\jsc.internal.git\compiler\jsc\CodeModel\ILBlock.cs:line 349

            Notify("awaiting for tv...", "http://youtube.com", "music");

            // http://developer.android.com/reference/android/net/wifi/WifiManager.html
            // http://developer.android.com/reference/android/net/wifi/WifiManager.html#createMulticastLock(java.lang.String)
            ((WifiManager)this.getSystemService(Context.WIFI_SERVICE)).createWifiLock(WifiManager.WIFI_MODE_FULL_HIGH_PERF, "ApplicationActivity").acquire();
            ((WifiManager)this.getSystemService(Context.WIFI_SERVICE)).createMulticastLock("ApplicationActivity").acquire();

            new JVMCLRBroadcastLogger.__AndroidMulticast(
                AtData:
                xmlstring =>
                {
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs

                    var xml = XElement.Parse(xmlstring);

                    if (xml.Value.StartsWith("Visit me at "))
                    {
                        // what about android apps runnning on SSL?
                        // what about preview images?
                        // do we get localhost events too?

                        var n = xml.Attribute("n");

                        var uri = "http://" + xml.Value.SkipUntilOrEmpty("Visit me at ");
                        var link = uri + "/results?search_query=" + n.Value;

                        Notify(n.Value, link, n.Value);
                    }
                }
            );



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


            //internal compiler error at method
            // assembly: Y:\staging\clr\AndroidServiceUDPNotification.AndroidActivity.dll at X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\bin\Release
            // type: AndroidServiceUDPNotification.Activities.NotifyService+NotifyServiceReceiver, AndroidServiceUDPNotification.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // method: onReceive
            // Java : invalid if block at
            // assembly: Y:\staging\clr\AndroidServiceUDPNotification.AndroidActivity.dll
            // type: AndroidServiceUDPNotification.Activities.NotifyService+NotifyServiceReceiver, AndroidServiceUDPNotification.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000f
            //  method:Void onReceive(android.content.Context, android.content.Intent)

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

