using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.widget
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/widget/Toast.java

    // http://developer.android.com/reference/android/widget/Toast.html
    [Script(IsNative = true)]
    public  class Toast
    {
        public static readonly int LENGTH_SHORT = 0;

        // members and types are to be extended by jsc at release build

        public static Toast makeText(Context context, string text, int duration = 0)
        {
            return default(Toast);
        }

        public void show()
        {

        }
    }
}
