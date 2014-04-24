using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.text.method
{
    // http://developer.android.com/reference/android/text/method/TransformationMethod.html
    [Script(IsNative = true)]
    public interface TransformationMethod
    {
        // haha a mistake kept build server awake for a hour
        //extension error: android.text.method.TransformationMethod
        //00fc:02:01 RewriteToAssembly error: System.InvalidOperationException: Some extension types have mismatching signatures.
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvoke()


    }
}
