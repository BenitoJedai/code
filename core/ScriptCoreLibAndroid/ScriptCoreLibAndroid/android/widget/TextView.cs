using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using java.lang;
using ScriptCoreLib;

namespace android.widget
{
    [Script(IsNative = true)]
    public  class TextView : View
    {
        public TextView(Context c)
            : base(c)
        {

        }
        public virtual void setText(CharSequence value)
        {
 
        }

        // members and types are to be extended by jsc at release build
    }
}
