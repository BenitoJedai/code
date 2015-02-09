﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.GLES2
{
    // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm64\usr\include\GLES2\gl2platform.h"

    [Script(IsNative = true, Header = "GLES2/gl2platform.h", IsSystemHeader = true)]
    public interface gl2platform_h : gl2ext_h
    {

    }

    [Script(IsNative = true)]
    public class gl2platform : gl2platform_h
    {
        //#include <EGL/eglplatform.h>
        //#include <EGL/egl.h>
        //#include <GLES2/gl2platform.h>
        //#include <GLES2/gl2ext.h>
        //#include <GLES2/gl2.h>

        // http://docs.nvidia.com/gameworks/index.html#technologies/mobile/native_android_opengles.htm%3FTocPath%3DTechnologies%7CMobile%2520Technologies%7CNative%2520Development%2520on%2520NVIDIA%25C2%25A0Android%2520Devices%7C_____3

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android_native_app_glue.cs

        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\xNativeActivity.cs
    }

}
