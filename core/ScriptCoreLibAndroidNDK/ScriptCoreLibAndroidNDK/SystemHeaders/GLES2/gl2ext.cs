﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.GLES2
{
    // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm64\usr\include\GLES2\gl2ext.h"

    [Script(IsNative = true, Header = "GLES2/gl2ext.h", IsSystemHeader = true)]
    public interface gl2ext_h : gl2_h
    {
    }

    [Script(IsNative = true)]
    public class gl2ext : gl2ext_h
    {
        // how do we get the ref in the correct order?
        // use interface?

        //#include <EGL/eglplatform.h>
        //#include <EGL/egl.h>
        //#include <GLES2/gl2platform.h>
        //#include <GLES2/gl2ext.h>
        //#include <GLES2/gl2.h>

        // vs

        //#include <GLES2/gl2platform.h>
        //#include <GLES2/gl2.h>
        //#include <GLES2/gl2ext.h>


        // http://www.phonesdevelopers.com/1729225/
        // http://stackoverflow.com/questions/24875322/are-vertex-array-objects-supported-in-android-opengl-es-2-0-using-extensions

        // http://docs.nvidia.com/gameworks/index.html#technologies/mobile/native_android_opengles.htm%3FTocPath%3DTechnologies%7CMobile%2520Technologies%7CNative%2520Development%2520on%2520NVIDIA%25C2%25A0Android%2520Devices%7C_____3

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android_native_app_glue.cs

        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\xNativeActivity.cs


        //       X:/opensource/android-ndk-r10c/platforms/android-21/arch-arm64/usr/include/GLES2/gl2ext.h:81:45: error: unknown type name 'GLenum'
        //typedef void (GL_APIENTRY* GLDEBUGPROCKHR)(GLenum source, GLenum type,GLuint id, GLenum severity,GLsizei length,const GLchar* message,const void* userParam);
        //                                            ^

    }

}
