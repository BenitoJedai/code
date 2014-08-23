using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/SurfaceView.java
    // http://developer.android.com/reference/android/view/SurfaceView.html

    [Script(IsNative = true)]
    public class SurfaceView : View
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

        public SurfaceView(Context c)
            : base(c)
        {

        }


        public SurfaceHolder getHolder()
        {
            return default(SurfaceHolder);
        }



    }
}
