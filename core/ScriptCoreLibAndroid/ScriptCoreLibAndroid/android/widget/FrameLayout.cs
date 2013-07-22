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
    public class FrameLayout : ViewGroup, ViewManager
    {
        // members and types are to be extended by jsc at release build

        public FrameLayout(Context c)
            : base(c)
        {

        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130722-new-release
        public virtual void addView(View view, ViewGroup.LayoutParams @params)
        {
        }

        public virtual void removeView(View view)
        {
        }

        public virtual void updateViewLayout(View view, ViewGroup.LayoutParams @params)
        {
        }
    }
}
