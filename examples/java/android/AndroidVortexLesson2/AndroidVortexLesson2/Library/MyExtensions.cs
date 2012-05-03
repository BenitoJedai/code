using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.widget;
using ScriptCoreLib;

namespace AndroidVortexLesson2.Library
{
    [Script]
    public static class MyExtensions
    {
        public static void setText(this TextView e, string value)
        {
            // this cast will work on JVM
            e.setText((java.lang.CharSequence)(object)value);
        }



    }
}
