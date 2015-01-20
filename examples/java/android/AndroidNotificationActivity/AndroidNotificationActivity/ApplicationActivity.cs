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
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;

namespace AndroidNotificationActivity.Activities
{


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207
        // how can we shot this from NDK?


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            // Camera PTP

            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            var b = new Button(this);

            b.setText("Notify!");
            int counter = 0;

            // ScriptCoreLib.Ultra ?
            b.AtClick(
                delegate
                {
                    counter++;
                    var nm = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);


                    // see http://developer.android.com/reference/android/app/Notification.html
                    var notification = new Notification(
                        android.R.drawable.star_on,
                        "The text that flows by in the status bar when the notification first activates.",
                         java.lang.System.currentTimeMillis()
                    );

                    var notificationIntent = new Intent(this, typeof(ApplicationActivity).ToClass());
                    var contentIntent = PendingIntent.getActivity(this, 0, notificationIntent, 0);


                    notification.setLatestEventInfo(
                        this,
                        "The title that goes in the expanded entry.",
                        "The text that goes in the expanded entry.",
                        contentIntent);

                    notification.defaults |= Notification.DEFAULT_VIBRATE;
                    notification.defaults |= Notification.DEFAULT_LIGHTS;
                    // http://androiddrawableexplorer.appspot.com/
                    nm.notify(counter, notification);
                }
            );

            ll.addView(b);

            this.setContentView(sv);

            // X:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\ScriptCoreLib.Android\Shader.cs

            // Error	1	'AndroidNotificationActivity.Activities.ApplicationActivity' does not contain a definition for 'ShowLongToast' and no extension method 'ShowLongToast' accepting a first argument of type 'AndroidNotificationActivity.Activities.ApplicationActivity' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\java\android\AndroidNotificationActivity\AndroidNotificationActivity\ApplicationActivity.cs	80	18	AndroidNotificationActivity
            //this.ShowLongToast("http://jsc-solutions.net");
            this.ShowToast("http://jsc-solutions.net");


        }




    }
}
