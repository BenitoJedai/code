using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.os;
using android.widget;
using ScriptCoreLib;

namespace android.view
{
    [Script(IsNative = true)]
    public class ContextThemeWrapper : ContextWrapper
    {
        // members and types are to be extended by jsc at release build

        public void setTheme(int resid)
        { }
    }
}
