using android.os;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/IntentFilter.java

    // http://developer.android.com/reference/android/content/IntentFilter.html
    [Script(IsNative = true)]
    public class IntentFilter : Parcelable
    {
        public IntentFilter()
        {

        }

        public IntentFilter(string e)
        {

        }
        public void addAction(string e)
        { 
        }
    }
}
