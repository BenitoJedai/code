using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/View.html
    [Script(IsNative = true)]
    public class View
    {
        // http://developer.android.com/reference/android/view/View.OnTouchListener.html
        [Script(IsNative = true)]
        public interface OnTouchListener
        {
            bool onTouch(View v, MotionEvent @event);
        }

        // members and types are to be extended by jsc at release build

        public View(Context c)
        {

        }

        [Script(IsNative = true)]
        public interface OnClickListener
        {
            void onClick(View v);
        }

        public void setOnClickListener(OnClickListener h)
        {

        }
    }
}
