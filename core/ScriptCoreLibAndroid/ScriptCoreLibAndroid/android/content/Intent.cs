using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
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
    }
}
