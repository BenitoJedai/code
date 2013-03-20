using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content.res;

namespace android.content
{
    // http://developer.android.com/reference/android/content/Context.html
    [Script(IsNative = true)]
    public abstract class Context
    {
        public static string NOTIFICATION_SERVICE;
        public static string WINDOW_SERVICE;

        // members and types are to be extended by jsc at release build

        public abstract Resources getResources();

        public abstract object getSystemService(string name);

        public abstract Context getApplicationContext();

        public abstract ComponentName startService(Intent service);
    }
}
