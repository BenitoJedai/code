using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/ViewManager.html
    [Script(IsNative = true)]
    public interface ViewManager
    {
        // members and types are to be extended by jsc at release build

        void addView(View view, ViewGroup.LayoutParams @params);

        void removeView(View view);

        void updateViewLayout(View view, ViewGroup.LayoutParams @params);

    }
}
