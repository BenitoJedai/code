using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.widget;
using ScriptCoreLib;

namespace android.content.res
{
    [Script(IsNative = true)]
    public class Resources 
    {
        // members and types are to be extended by jsc at release build

        public virtual AssetManager getAssets()
        {
            return default(AssetManager);
        }
    }
}
