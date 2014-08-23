using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{

    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Environment.java

    [Script(IsNative = true)]
    public class Environment
    {
        // tested by
        // X:\jsc.svn\examples\javascript\android\AndroidEnvironmentWebActivity\AndroidEnvironmentWebActivity\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

        public static string DIRECTORY_DCIM = "DCIM";

        public static File getExternalStoragePublicDirectory(string type)
        {
            return default(File);
        }

    }
}
