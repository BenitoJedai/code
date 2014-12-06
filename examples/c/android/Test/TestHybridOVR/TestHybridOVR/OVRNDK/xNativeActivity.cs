using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using TestHybridOVR;

// we need two languages now
[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(TestHybridOVR.OVRNDK.xNativeActivity))]


namespace TestHybridOVR.OVRNDK
{

    // OVR::VrAppInterface
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        //[arm64-v8a] Compile        : TestHybridOVR <= TestHybridOVR.dll.c
        //In file included from jni/TestHybridOVR.dll.h:20:0,
        //                 from jni/TestHybridOVR.dll.c:2:
        //X:/opensource/android-ndk-r10c/platforms/android-21/arch-arm64/usr/include/GLES2/gl2ext.h:40:33: error: expected ')' before '*' token
        // #define GL_APIENTRYP GL_APIENTRY*


        // http://docs.nvidia.com/gameworks/index.html#technologies/mobile/native_android_opengles.htm%3FTocPath%3DTechnologies%7CMobile%2520Technologies%7CNative%2520Development%2520on%2520NVIDIA%25C2%25A0Android%2520Devices%7C_____3
        // http://stackoverflow.com/questions/8828559/loading-3rd-party-shared-libraries-from-an-android-native-activity
        // http://stackoverflow.com/questions/6838397/how-do-i-load-my-own-java-class-in-c-on-android

        // "X:\opensource\android-ndk-r10c\samples\hello-jni\src\com\example\hellojni\HelloJni.java"

        // X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\xNativeActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestLibOVR\TestLibOVR\xNativeActivity.cs

        // X:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\ApplicatonActivity.cs

        // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrTemplate\jni\OvrApp.cpp"
        // "X:\jsc.svn\examples\c\android\Test\TestLibOVR\TestLibOVR\bin\Debug\staging\libs\armeabi-v7a\libJavaVr.so"

        //jlong Java_oculus_MainActivity_nativeSetAppInterface(JNIEnv* jni, jclass clazz, jobject activity)
        //{
        //    LOG("nativeSetAppInterface");
        //    return (new OvrApp())->SetActivity(jni, clazz, activity);
        //}


        // void* TestHybridOVR_OVRNDK_xNativeActivity_Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI(LPTestHybridOVR_OVRNDK_xNativeActivity __that, void* env, void* thiz)

        // jstring Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI(JNIEnv* env, jobject thiz)
        //  type not supported: ScriptCoreLibNative.SystemHeaders.JNIEnv& ; consider adding [ScriptAttribute]
        [Script(NoDecoration = true)]
        static jstring Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI(
            ref JNIEnv env,
            jobject thiz)
        {

            //jstring Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI(JNIEnv** env, jobject thiz)
            //{
            //    void* delegate0;

            //    delegate0 = ((jstring(*)(JNIEnv**, char*))(*env)->NewStringUTF);
            //    return  (jstring)NULL;
            //}


            //jni/TestHybridOVR.dll.c: In function 'Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI':
            //jni/TestHybridOVR.dll.c:62:49: error: request for member 'NewStringUTF' in something not a structure or union
            //     delegate0 = ((jstring(*)(JNIEnv*, char*))env->NewStringUTF);
            //                                                 ^

            // return (*env)->NewStringUTF(env, "Hello from JNI !  Compiled with ABI " ABI ".");

            //jni/TestHybridOVR.dll.c: In function 'Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI':
            //jni/TestHybridOVR.dll.c:61:37: warning: 'struct tag_JNIEnv' declared inside parameter list
            //     delegate0 = ((jstring(*)(struct tag_JNIEnv***, char*))(*env)->NewStringUTF);
            //                                     ^

            // Opcode not implemented: ldind.ref at TestHybridOVR.OVRNDK.xNativeActivity.Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI
            var n = env.NewStringUTF;

            var v = n(ref env, "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");

            return v;
            //return default(jstring);
        }

        //<!-- This .apk has no Java code itself, so set hasCode to false. -->
        //   <application android:label="@string/app_name" android:hasCode="false">
        [Script(NoDecoration = true)]
        static void android_main(android_native_app_glue.android_app state)
        {
            // http://supersegfault.com/three-ways-to-set-up-ndk-apps/

            // jsc is not printing the target name?
            //var loc0 = state;

            // roslyn confuses jsc?

            android_native_app_glue.app_dummy();



            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "exit TestHybridOVR");



        }
    }
}
