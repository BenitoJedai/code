using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    [Script(IsNative = true)]
    public interface MenuItem
    {
        MenuItem setIcon(int value);
        MenuItem setIntent(Intent value);
    }
}
