using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using java.lang;
using ScriptCoreLib;
using android.text;

namespace android.widget
{

    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/widget/TextView.java
    // http://developer.android.com/reference/android/widget/TextView.html
    [Script(IsNative = true)]
    public class TextView : View
    {
        public TextView(Context c)
            : base(c)
        {

        }
        public virtual void setText(CharSequence value)
        {

        }

        public virtual void setText(string value)
        {

        }


        //public void setBackgroundColor(int color)
        //{ }

        public void setTextColor(int color)
        {

        }

        public void setShadowLayer(float radius, float dx, float dy, int color)
        {
        }


        public void addTextChangedListener(TextWatcher watcher)
        {
        }

        // members and types are to be extended by jsc at release build
    }
}
