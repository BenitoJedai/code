using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.widget
{
    // http://developer.android.com/reference/android/widget/Toast.html
    [Script(IsNative = true)]
    public  class Toast
    {
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
