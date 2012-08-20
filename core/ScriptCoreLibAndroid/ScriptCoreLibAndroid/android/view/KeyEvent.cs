using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/KeyEvent.html
    [Script(IsNative = true)]
    public class KeyEvent 
    {
        // members and types are to be extended by jsc at release build

        public const int KEYCODE_BACK = 4;
    }
}
