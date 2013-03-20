using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // http://developer.android.com/reference/android/view/ViewGroup.html
    [Script(IsNative = true)]
    public abstract class ViewGroup : View
    {
        [Script(IsNative = true)]
        public class LayoutParams
        {

        }


        // members and types are to be extended by jsc at release build

        public ViewGroup(Context c) : base(c)
        {

        }

        public virtual void addView(View v)
        { 
        }
    }
}
