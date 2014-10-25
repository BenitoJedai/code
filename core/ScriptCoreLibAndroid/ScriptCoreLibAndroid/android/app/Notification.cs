using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;
using java.lang;

namespace android.app
{
    // http://developer.android.com/reference/android/app/Notification.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/Notification.java

    [Script(IsNative = true)]
    public class Notification
    {
        // tested by
        // X:\jsc.svn\examples\java\android\AndroidNotificationActivity\AndroidNotificationActivity\ApplicationActivity.cs

        public int flags;
        public int defaults;

        public static int FLAG_NO_CLEAR;
        public static int FLAG_AUTO_CANCEL;
        public static int DEFAULT_SOUND;

        public Notification(int icon, string tickerText, long when)
        { 
        }

        public void setLatestEventInfo(Context context, string contentTitle, string contentText, PendingIntent contentIntent)
        {
        }

        // members and types are to be extended by jsc at release build
    }

}
