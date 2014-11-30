using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/NativeActivity.java
    // https://developer.android.com/reference/android/app/NativeActivity.html

    [Script(IsNative = true)]
    public class NativeActivity : Activity
    {
        // "X:\opensource\android-ndk-r10c\sources\android\native_app_glue\android_native_app_glue.c"
        // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\native_activity.h"
        // "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\JNIHelper.h"

        // members and types are to be extended by jsc at release build
    }
}
