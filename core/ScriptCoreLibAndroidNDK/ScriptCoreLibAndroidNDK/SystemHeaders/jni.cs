using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\jni.h"

    // or should this be a marker attribute instead?
    [Script(IsNative = true, Header = "jni.h", IsSystemHeader = true)]
    public interface jni_h
    {
    }


    // typedef jobject         jstring;
    //typedef void*           jobject;
    [Script(IsNative = true, PointerName = "jobject")]
    public class jobject : jni_h
    {
        // look we just defined an object?
        // the name already is the pointer?
    }

    [Script(IsNative = true, PointerName = "jstring")]
    public class jstring : jobject
    {
        // look we just defined an object?
        // the name already is the pointer?
    }


    [Script(IsNative = true, PointerName = "jclass")]
    public class jclass : jobject
    {
        // look we just defined an object?
        // the name already is the pointer?
    }


    [Script(IsNative = true, PointerName = "jlong")]
    public class jlong : jni_h
    {
        // long
    }

    //typedef const struct JNINativeInterface* JNIEnv;
    [Script(IsNative = true, PointerName = "JNIEnv")]
    public class JNIEnv : jni_h
    {
        // used by 
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRNDK\xNativeActivity.cs

        // jint        (*GetVersion)(JNIEnv *);

        //jstring     (*NewStringUTF)(JNIEnv*, const char*);

        // this is actualy a field to function pointer?
        
        // return  (void*)NewStringUTF((void*)env, (void*)env, (char*)"from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");
        // c does not have instance methods!

        [Script(IsNative = true)]
        public delegate jstring NewStringUTFDelegate(ref JNIEnv env, string value);

        public NewStringUTFDelegate NewStringUTF;

    }

    [Script(IsNative = true)]
    public class jni : jni_h
    {
        // X:\jsc.internal.git\compiler\jsc.internal\jsc.internal.library\Desktop\JVM\JVMLauncher.cs


        // full circle



        //typedef const struct JNIInvokeInterface* JavaVM;
        [Script(IsNative = true)]
        public class JavaVM
        {
            //jint        (*GetEnv)(JavaVM*, void**, jint);
        }

    }

}
