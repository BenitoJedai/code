using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android
{
    [Script]
    public static class ThreadLocalContextReference
    {
        // tested by ?

        // this is a workaround until WebActivity finds a better way.
        public static global::android.content.Context CurrentContext;
    }
}
