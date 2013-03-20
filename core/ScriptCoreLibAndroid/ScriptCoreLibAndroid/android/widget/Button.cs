using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.widget
{
    // http://developer.android.com/reference/android/widget/Button.html
    [Script(IsNative = true)]
    public class Button : TextView
    {
        // members and types are to be extended by jsc at release build

        public Button(Context c)
            : base(c)
        {

        }

        public void setHeight(int pixels)
        { }
        public void setWidth(int pixels)
        {
        }
    }
}
