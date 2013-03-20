using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/Display.html
    [Script(IsNative = true)]
    public class Display
    {
        // members and types are to be extended by jsc at release build

        public int getWidth()
        {
            throw null;
        }

        public int getHeight()
        {
            throw null;
        }
    }
}
