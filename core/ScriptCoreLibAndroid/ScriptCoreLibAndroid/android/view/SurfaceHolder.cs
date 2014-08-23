using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // http://developer.android.com/reference/android/view/SurfaceHolder.html
    [Script(IsNative = true)]
    public interface SurfaceHolder
    {
        void setType(int type) ;
        void addCallback(SurfaceHolder_Callback callback);
    }

    [Script(IsNative = true)]
    public interface SurfaceHolder_Callback
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

    }
}
