using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.widget;
using ScriptCoreLib;
using android.content.res;
using android.view;

namespace android.content
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/ContextWrapper.java

    // http://developer.android.com/reference/android/content/ContextWrapper.html
    [Script(IsNative = true)]
    public class ContextWrapper : Context
    {
        // members and types are to be extended by jsc at release build

        public Context getBaseContext()
        {
            return null;
        }

        public override void setTheme(int resid)
        {
        }

        public override Context getApplicationContext()
        {
            throw new NotImplementedException();
        }
        public override Resources getResources()
        {
            return default(Resources);
        }

        public override object getSystemService(string name)
        {
            return default(object);
        }

        public override ComponentName startService(Intent service)
        {
            throw new NotImplementedException();
        }



        public override SharedPreferences getSharedPreferences(string arg0, int arg1)
        {
            throw new NotImplementedException();
        }

        public override void startActivity(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override ContentResolver getContentResolver()
        {
            throw new NotImplementedException();
        }

        public override Intent registerReceiver(BroadcastReceiver receiver, IntentFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
