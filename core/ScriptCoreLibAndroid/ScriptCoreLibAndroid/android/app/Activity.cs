using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;

namespace android.app
{
    [Script(IsNative = true)]
    public class Activity : ContextThemeWrapper
    {
        // members and types are to be extended by jsc at release build

        protected virtual void setContentView(View savedInstanceState)
        { 
        }

        protected virtual void onCreate(Bundle savedInstanceState)
        { 
        
        }

    }
}
