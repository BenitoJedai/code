using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/ViewGroup.html
    [Script(IsNative = true)]
    public abstract class ViewGroup : View
    {

        // http://developer.android.com/reference/android/view/ViewGroup.LayoutParams.html
        [Script(IsNative = true)]
        public class LayoutParams
        {
            public static int FILL_PARENT;
            public static int WRAP_CONTENT;

            public int height;
            public int width;
        }

        // http://developer.android.com/reference/android/view/ViewGroup.MarginLayoutParams.html
        [Script(IsNative = true)]
        public class MarginLayoutParams : LayoutParams
        {

            public void setMargins(int left, int top, int right, int bottom)
            { }
        }

        // members and types are to be extended by jsc at release build

        public ViewGroup(Context c)
            : base(c)
        {

        }

        public virtual void addView(View v)
        {
        }

        public virtual bool onInterceptTouchEvent(MotionEvent ev)
        {
            throw null;
        }

        public int getChildCount()
        {
            throw null;
        }
    }
}
