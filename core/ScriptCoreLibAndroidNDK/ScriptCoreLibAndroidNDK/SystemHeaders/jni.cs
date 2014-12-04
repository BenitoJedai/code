using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\jni.h"

    [Script(IsNative = true, Header = "jni.h", IsSystemHeader = true)]
    public static class jni
    {
        // X:\jsc.internal.git\compiler\jsc.internal\jsc.internal.library\Desktop\JVM\JVMLauncher.cs


        // full circle

        //typedef const struct JNINativeInterface* JNIEnv;
        [Script(IsNative = true)]
        public class JNIEnv
        {
            // jint        (*GetVersion)(JNIEnv *);

        }

        //typedef const struct JNIInvokeInterface* JavaVM;
        [Script(IsNative = true)]
        public class JavaVM
        {
            //jint        (*GetEnv)(JavaVM*, void**, jint);
        }

    }

}
