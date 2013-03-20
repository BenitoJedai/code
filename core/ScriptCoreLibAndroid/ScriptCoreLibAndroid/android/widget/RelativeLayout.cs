using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // http://developer.android.com/reference/android/widget/RelativeLayout.html
    [Script(IsNative = true)]
    public class RelativeLayout : ViewGroup
    {
        // members and types are to be extended by jsc at release build

        // http://developer.android.com/reference/android/widget/RelativeLayout.LayoutParams.html
        [Script(IsNative = true)]
        public new class LayoutParams : MarginLayoutParams
        {
            public LayoutParams(int w, int h)
            {
            }
        }




        public RelativeLayout(Context c)
            : base(c)
        {

        }
    }
}
