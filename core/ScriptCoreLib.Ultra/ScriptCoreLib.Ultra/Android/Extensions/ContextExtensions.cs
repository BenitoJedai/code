extern alias globalandroid;

using globalandroid::android.content;
using globalandroid::android.widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Extensions
{
    public static class ContextExtensions
    {
        public static Context ShowToast(this Context c, string e)
        {
            if (c == null)
                return c;

            Toast.makeText(
                  c,
                  e,
                  Toast.LENGTH_SHORT
            ).show();

            return c;
        }
    }
}
