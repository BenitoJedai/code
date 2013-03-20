using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // http://developer.android.com/reference/android/content/Intent.html
    [Script(IsNative = true)]
    public class Intent
    {
        public const string ACTION_VIEW = "android.intent.action.VIEW";

        public Intent(string action, android.net.Uri uri)
        {

        }

        public static Intent createChooser(Intent arg0, string arg1)
        {
            return default(Intent);
        }

        public virtual Intent setData(Uri value)
        {
            throw null;
        }

        public virtual Intent setAction(string value)
        {
            throw null;
        }
    }
}
