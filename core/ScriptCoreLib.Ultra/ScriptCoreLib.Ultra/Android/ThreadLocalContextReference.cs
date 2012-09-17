extern alias globalandroid;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android
{
    // move to ScriptCoreLibAndroid when SQLite is being enabled for WebActivity
    public static class ThreadLocalContextReference
    {
        public static globalandroid::android.content.Context CurrentContext;
    }
}
