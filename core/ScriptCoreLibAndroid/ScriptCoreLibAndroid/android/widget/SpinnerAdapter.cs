using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/widget/SpinnerAdapter.java
    // http://developer.android.com/reference/android/widget/SpinnerAdapter.html
    [Script(IsNative = true)]
    public interface SpinnerAdapter : Adpter
    {
        // members and types are to be extended by jsc at release build

    }
}
