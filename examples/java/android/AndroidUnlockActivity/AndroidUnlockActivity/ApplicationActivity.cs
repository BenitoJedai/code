using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using android.app;
using android.hardware;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using android.content.pm;
using android.content;

namespace AndroidUnlockActivity.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class AndroidUnlockActivity : Activity
    {
        // inspired by http://baroqueworksdev.blogspot.com/2012/09/how-to-handle-screen-onoff-and-keygurad.html



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            this.setContentView(sv);


            new Button(this).WithText("register").AttachTo(ll).AtClick(
                btn =>
                {
                    btn.setEnabled(false);

                    // get KeyGuardManager
                    var mKeyguard = (KeyguardManager)getSystemService(Context.KEYGUARD_SERVICE);

                    var mReceiver = new MyBroadcastReceiver();

                    mReceiver.AtReceive +=
                        (Context context, Intent intent)  =>
                        {
                            #region Notify
                            int counter = 0;
                            Action<string> Notify =
                                Title =>
                                {
                                    counter++;

                                    var nm = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);

                                    // see http://developer.android.com/reference/android/app/Notification.html
                                    var notification = new Notification(
                                        android.R.drawable.star_on,
                                        Title,
                                         java.lang.System.currentTimeMillis()
                                    );

                                    // ToClass is like GetTypeInfo
                                    var notificationIntent = new Intent(this, typeof(AndroidUnlockActivity).ToClass());
                                    var contentIntent = PendingIntent.getActivity(this, 0, notificationIntent, 0);


                                    notification.setLatestEventInfo(
                                        this,
                                        Title,
                                        "",
                                        contentIntent);

                                    // http://stackoverflow.com/questions/10402686/how-to-have-led-light-notification
                                    notification.defaults |= Notification.DEFAULT_VIBRATE;
                                    notification.defaults |= Notification.DEFAULT_SOUND;
                                    //notification.defaults |= Notification.DEFAULT_LIGHTS;
                                    notification.defaults |= Notification.FLAG_SHOW_LIGHTS;
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

                            var action = intent.getAction();
                            if (action == Intent.ACTION_SCREEN_OFF)
                            {
                                // Screen is off
                                Notify("ACTION_SCREEN_OFF");
                            }
                            else if (action == Intent.ACTION_SCREEN_ON)
                            {
                                // Intent.ACTION_USER_PRESENT will be broadcast when the screen
                                // is
                                // unlocked.

                                // if API Level 16
                                /*
                                 * if(mKeyguard.isKeyguardLocked()){ // the keyguard is
                                 * currently locked. Log.e("","ACTION_SCREEN_ON : locked"); }
                                 */
                                if (mKeyguard.inKeyguardRestrictedInputMode())
                                {
                                    // the keyguard is currently locked.
                                    Notify("ACTION_SCREEN_ON : locked");
                                }
                                else
                                {
                                    // unlocked
                                    Notify("ACTION_SCREEN_ON : unlocked");
                                }

                            }
                            else if (action == Intent.ACTION_USER_PRESENT)
                            {
                                // The user has unlocked the screen. Enabled!
                                Notify("ACTION_USER_PRESENT");

                               
                            }
                        };


                    // IntetFilter with Action
                    IntentFilter intentFilter = new IntentFilter();
                    intentFilter.addAction(Intent.ACTION_SCREEN_OFF);
                    intentFilter.addAction(Intent.ACTION_SCREEN_ON);
                    intentFilter.addAction(Intent.ACTION_USER_PRESENT);// Keyguard is GONE

                    // register BroadcastReceiver and IntentFilter
                    registerReceiver(mReceiver, intentFilter);
                }
            );

            //this.ShowToast("http://jsc-solutions.net");
        }


        class MyBroadcastReceiver : BroadcastReceiver
        {
            public event Action<Context, Intent> AtReceive;

            public override void onReceive(Context context, Intent intent) 
            {
                if (AtReceive != null)
                    AtReceive(context, intent);
            }
        }

    }


}
