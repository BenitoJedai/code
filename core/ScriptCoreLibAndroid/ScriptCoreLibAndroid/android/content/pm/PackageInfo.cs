using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.content.pm
{
    // http://developer.android.com/reference/android/content/pm/PackageInfo.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/content/pm/PackageInfo.java
    [Script(IsNative = true)]
    public  class PackageInfo
    {
        public int versionCode;

    }
}
