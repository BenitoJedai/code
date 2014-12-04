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
    public class KeyEvent : InputEvent
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\keycodes.cs

        // http://developer.android.com/reference/android/view/KeyEvent.Callback.html
        [Script(IsNative = true)]
        public interface Callback
        {

        }

        public int getKeyCode()
        {
            throw null;
        }
        public int getAction()
        {
            throw null;
        }

        // members and types are to be extended by jsc at release build
        public static int ACTION_UP;

        public const int KEYCODE_BACK = 4;
    }
}
