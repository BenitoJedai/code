using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/widget/LinearLayout.java
    // http://developer.android.com/reference/android/widget/LinearLayout.html
    [Script(IsNative = true)]
    public class LinearLayout : ViewGroup
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

        // members and types are to be extended by jsc at release build

        public static int VERTICAL;


        public LinearLayout(Context c)
            : base(c)
        {

        }

        public void setOrientation(int o)
        {
        }

        public void setGravity(int gravity)
        {
        }
    }
}
