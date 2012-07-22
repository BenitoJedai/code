using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.widget
{
    [Script(IsNative = true)]
    public   class CheckBox : CompoundButton 
    {
        // members and types are to be extended by jsc at release build

        public CheckBox(Context c)
            : base(c)
        {

        }
    }
}
