using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using java.lang;
using ScriptCoreLib;

namespace android.widget
{
    // http://developer.android.com/reference/android/widget/EditText.html
    [Script(IsNative = true)]
    public class EditText : TextView
    {
        public EditText(Context c)
            : base(c)
        {

        }

    

        // members and types are to be extended by jsc at release build
    }
}
