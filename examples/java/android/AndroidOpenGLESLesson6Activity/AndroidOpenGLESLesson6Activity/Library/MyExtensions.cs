using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.widget;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson6Activity.Library
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
