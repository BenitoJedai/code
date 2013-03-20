using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // http://developer.android.com/reference/android/widget/FrameLayout.html
    [Script(IsNative = true)]
    public class FrameLayout : ViewGroup
    {
        // members and types are to be extended by jsc at release build

        public FrameLayout(Context c) : base(c)
        {

        }
    }
}
