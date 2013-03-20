using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.widget;
using ScriptCoreLib;
using android.util;

namespace android.content.res
{
    // http://developer.android.com/reference/android/content/res/Resources.html
    [Script(IsNative = true)]
    public class Resources
    {
        // members and types are to be extended by jsc at release build

        public DisplayMetrics getDisplayMetrics()
        {
            throw null;

        }

        public virtual AssetManager getAssets()
        {
            return default(AssetManager);
        }
    }
}
