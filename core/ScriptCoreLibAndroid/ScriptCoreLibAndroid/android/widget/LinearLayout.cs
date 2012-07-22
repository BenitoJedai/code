using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    [Script(IsNative = true)]
    public  class LinearLayout : ViewGroup
    {
        // members and types are to be extended by jsc at release build

        public LinearLayout(Context c)
            : base(c)
        {

        }
    }
}
