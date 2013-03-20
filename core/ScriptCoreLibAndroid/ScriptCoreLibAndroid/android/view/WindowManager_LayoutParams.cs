using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // http://developer.android.com/reference/android/view/WindowManager.LayoutParams.html
    [Script(IsNative = true)]
    public class WindowManager_LayoutParams : ViewGroup.LayoutParams
    {
        public const int FLAG_HARDWARE_ACCELERATED = 16777216;
        public static int FLAG_NOT_TOUCH_MODAL;
        public static int FLAG_WATCH_OUTSIDE_TOUCH;
        public static int FLAG_NOT_FOCUSABLE;

        public int flags;

        public int x;
        public int y;
    }
}
