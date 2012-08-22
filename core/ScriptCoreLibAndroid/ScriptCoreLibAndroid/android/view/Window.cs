using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/Window.html
    [Script(IsNative = true)]
    public abstract class Window
    {
        // members and types are to be extended by jsc at release build

        public const int FEATURE_NO_TITLE = 1;

        public bool requestFeature(int featureId)
        {
            return default(bool);
        }
    }
}
