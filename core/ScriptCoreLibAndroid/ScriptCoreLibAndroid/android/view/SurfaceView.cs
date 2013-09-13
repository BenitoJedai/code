using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // http://developer.android.com/reference/android/view/SurfaceView.html
    [Script(IsNative = true)]
    public class SurfaceView : View
    {
        public SurfaceView(Context c)
            : base(c)
        {

        }
    }
}
