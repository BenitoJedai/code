using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.content.pm
{
    // http://developer.android.com/reference/android/content/pm/PackageManager.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/content/pm/PackageManager.java
    [Script(IsNative = true)]
    public abstract class PackageManager
    {
        public abstract PackageInfo getPackageInfo(String packageName, int flags);
    }
}
