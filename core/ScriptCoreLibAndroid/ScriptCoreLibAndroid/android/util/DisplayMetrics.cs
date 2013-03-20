using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.util
{
    // http://developer.android.com/reference/android/util/DisplayMetrics.html
    [Script(IsNative = true)]
    public class DisplayMetrics
    {
        public int widthPixels;
        public int heightPixels;
        public float density;

    }

}
