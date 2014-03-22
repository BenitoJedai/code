extern alias globalandroid;

using android.app;
using android.content;
using android.widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Extensions
{
    public static class ActivityExtensions
    {
        class f : globalandroid::java.lang.Runnable
        {
            public Action y;

            public void run()
            {
                y();
            }
        }

        public static Activity runOnUiThread(this Activity c, Action<Activity> y)
        {
            if (c == null)
                return c;


            if (y != null)
                c.runOnUiThread(
                    new f
                    {
                        y = delegate { y(c); }
                    }
                );

            return c;
        }
    }
}
