using android.view;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.widget
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/widget/PopupWindow.java
    // http://developer.android.com/reference/android/widget/PopupWindow.html

    [Script(IsNative = true)]
    public class PopupWindow
    {
        public PopupWindow(View contentView, int width, int height)
        {
            // X:\jsc.svn\examples\java\android\forms\AndroidFormsActivity\AndroidFormsActivity\ApplicationActivity.cs

        }

        public void showAsDropDown(View anchor, int xoff, int yoff, int gravity) { }

        public void setContentView(View contentView) { }

        public void setFocusable(bool focusable) { }
        public void setOutsideTouchable(bool touchable) { }

        public void dismiss() { }
    }
}
