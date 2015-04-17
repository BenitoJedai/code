using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/widget/AbsSpinner.java
    // http://developer.android.com/reference/android/widget/AbsSpinner.html
    [Script(IsNative = true)]
    public abstract class AbsSpinner : AdapterView<SpinnerAdapter>
    {
        // members and types are to be extended by jsc at release build
        public AbsSpinner(Context c)
            : base(c)
        {

        }

    }
}
