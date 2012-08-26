using java.lang;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    [Script(IsNative = true)]
    public interface Menu
    {
        void clear();

        MenuItem add(CharSequence value);
    }
}
