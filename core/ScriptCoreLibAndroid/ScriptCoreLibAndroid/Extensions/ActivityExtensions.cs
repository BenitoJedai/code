using android.app;
using android.content;
using android.widget;
using java.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Extensions
{
    [Script]
    public static class ActivityExtensions
    {
        [Script]
        class f : java.lang.Runnable
        {
            public Action y;

            public void run()
            {
                y();
            }
        }


        // used by ?
        // X:\jsc.svn\examples\javascript\android\CallSetContentView\CallSetContentView\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\android\AndroidPINForm\AndroidPINForm\ApplicationWebService.cs

        public static Activity runOnUiThread(this Activity c, Action<Activity> y)
        {
            if (c == null)
                return c;


            if (y != null)
            {
                Runnable r = new f
                {
                    y = delegate { y(c); }
                };

                //Error	39	The best overloaded method match for 'android.app.Activity.runOnUiThread(java.lang.Runnable)' has some invalid arguments	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\Extensions\ActivityExtensions.cs	38	17	ScriptCoreLib.Ultra
                //Error	40	Argument 1: cannot convert from 'java.lang.Runnable [C:\util\jsc\bin\ScriptCoreLibAndroid.dll]' to 'java.lang.Runnable [c:\util\jsc\bin\ScriptCoreLibJava.dll]'	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\Extensions\ActivityExtensions.cs	38	33	ScriptCoreLib.Ultra

                c.runOnUiThread(r);
            }

            return c;
        }
    }
}
