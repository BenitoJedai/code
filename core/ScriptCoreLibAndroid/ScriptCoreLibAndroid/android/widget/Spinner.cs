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
    // http://developer.android.com/guide/topics/ui/controls/spinner.html
    // http://developer.android.com/reference/android/widget/Spinner.html

    [Script(IsNative = true)]
    public class Spinner : AbsSpinner
    {
         public Spinner(Context c)
            : base(c)
        {

        }

        //public void setAdapter(SpinnerAdapter adapter) { }

        // members and types are to be extended by jsc at release build

        public override void setAdapter(SpinnerAdapter adapter)
        {
        }
    }
}
