using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/view/WindowManager.java

    // http://developer.android.com/reference/android/view/WindowManager.html
    [Script(IsNative = true)]
    public interface WindowManager : ViewManager
    {
        // members and types are to be extended by jsc at release build

        Display getDefaultDisplay();
    }
}
