using android.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib;
using android.widget;

[assembly: ScriptTypeFilter(ScriptType.Java, typeof(HybridOculusVrActivity.OVRJVM.ApplicationActivity))]

// notice the different namespace
namespace HybridOculusVrActivity.OVRJVM
{
    // "X:\opensource\ovr_mobile_sdk_20141111\sdcard\oculus\tuscany.ovrscene"

    //X:\opensource\ovr_mobile_sdk_20141111\VRLib\src\com\oculusvr\vrlib\PassThroughCamera.java:49: error: cannot find symbol
    //                if ( BuildConfig.DEBUG && ( appPtr != appPtr_ ) && ( appPtr != 0 ) )
    //                     ^

    // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRJVM\ApplicationActivity.cs
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrTemplate\src\oculus\MainActivity.java"
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrScene\src\com\oculusvr\vrscene\MainActivity.java"

    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141206
    //public class ApplicationActivity : Activity, ScriptCoreLib.Android.IAssemblyReferenceToken
    public class ApplicationActivity : com.oculusvr.vrlib.VrActivity, ScriptCoreLib.Android.IAssemblyReferenceToken
    {
        // Just as a side note, you can actually run the app on the Note 4 without having a VR Gear.
        // Just change the manifest android:value="vr_only" to android:value="vr_dual" and the message will be gone. You still need to have a valid signature file nevertheless.

        // https://forums.oculus.com/viewtopic.php?f=67&t=17244
        // The way it is designed, you never see a VR image outside of the Gear headset, or a 2D image inside it. 
        // When you launch a VR app it just tells you to put on the Gear and doesn't show anything on the screen.
        // Home is a bit of an exception because it does have an out-of-VR store UI as well, but it won't install 
        // on current firmware.

        //public com.oculusvr.vrlib.VrActivity ref0;

        //W/JniUtils(10537): enter ovr_GetGlobalClassReference
        //W/JniUtils(10537): com/oculusvr/vrlib/VrLib
        //W/JniUtils(10537): fail ovr_GetGlobalClassReference
        //W/JniUtils(10537): FindClass( com/oculusvr/vrlib/VrLib ) failed

        // jint JNI_OnLoad( JavaVM* vm, void* reserved )
        // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\App.cpp
        // ovr_OnLoad( vm );
        // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\VrApi\VrApi.cpp
        // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\VrApi\JniUtils.cpp
        // VrLibClass = ovr_GetGlobalClassReference( jni, "com/oculusvr/vrlib/VrLib" );

        // i think it crashes because we do not have the things java source linked with us

        //I/DEBUG   (  122):     #05  pc 000a46d0  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (ovr_GetGlobalClassReference(_JNIEnv*, char const*)+112)
        //I/DEBUG   (  122):     #06  pc 00093364  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (ovr_OnLoad+100)
        //I/DEBUG   (  122):     #07  pc 000b9004  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (JNI_OnLoad+48)
        //I/DEBUG   (  122):     #08  pc 00050005  /system/lib/libdvm.so (dvmLoadNativeCode(char const*, Object*, char**)+468)


        // if we were to include AssetsLibrary,
        // how can we force non merge/ script ?


        // VrActivity



        // wo dont yet have the cool dispatcher for arm
        // X:\jsc.svn\core\ScriptCoreLibJava.jni\web\bin\jnistb10\dispatch.c

        // X:\jsc.svn\core\ScriptCoreLibJava.jni\BCLImplementation\ScriptCoreLib\Shared\PlatformInvocationServices.cs
        // could we load any .so on will?

        protected override void onCreate(android.os.Bundle value)
        {
            base.onCreate(value);

            //E/AndroidRuntime(13031): android.util.SuperNotCalledException: Activity {HybridOculusVrActivity.OVRJVM/HybridOculusVrActivity.OVRJVM.ApplicationActivity} did not call through to super.onCreate()
            //E/AndroidRuntime(13031):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2150)
            //E/AndroidRuntime(13031):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2233)
            //E/AndroidRuntime(13031):        at android.app.ActivityThread.access$800(ActivityThread.java:135)
            //E/AndroidRuntime(13031):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1196)
            //E/AndroidRuntime(13031):        at android.os.Handler.dispatchMessage(Handler.java:102)
            //E/AndroidRuntime(13031):        at android.os.Looper.loop(Looper.java:136)
            //E/AndroidRuntime(13031):        at android.app.ActivityThread.main(ActivityThread.java:5001)

            // why call the base?

            Console.WriteLine("enter  HybridOculusVrActivity.OVRJVM ApplicationActivity onCreate");

            var tv = new TextView(this);
            tv.setText(stringFromJNI());
            setContentView(tv);

            // if we do not set it we are going to crash.
            //var appPtr = nativeSetAppInterface(ref0);

            // https://forums.oculus.com/viewtopic.php?f=67&t=17790
            this.appPtr = nativeSetAppInterface(this);

            //nativeResume(appPtr);
        }

        // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\GlGeometry.cpp


//I/DEBUG   (  122):     #01  pc 000adf30  /data/app-lib/HybridOculusVrActivity.OVRJVM-2/libHybridOculusVrActivity.so (OVR::GlGeometry::Create(OVR::VertexAttribs const&, OVR::Array<unsigned short, OVR::ArrayDefaultPolicy> const&)+88)
//I/DEBUG   (  122):     #02  pc 000af96c  /data/app-lib/HybridOculusVrActivity.OVRJVM-2/libHybridOculusVrActivity.so (OVR::BuildTesselatedQuad(int, int)+1100)
//I/DEBUG   (  122):     #03  pc 000bb0ac  /data/app-lib/HybridOculusVrActivity.OVRJVM-2/libHybridOculusVrActivity.so (OVR::AppLocal::InitGlObjects()+376)
//I/DEBUG   (  122):     #04  pc 000bb230  /data/app-lib/HybridOculusVrActivity.OVRJVM-2/libHybridOculusVrActivity.so (OVR::AppLocal::InitVrThread()+196)
//I/DEBUG   (  122):     #05  pc 000bdbf0  /data/app-lib/HybridOculusVrActivity.OVRJVM-2/libHybridOculusVrActivity.so (OVR::AppLocal::VrThreadFunction()+48)
//I/DEBUG   (  122):     #06  pc 000be48c  /data/app-lib/HybridOculusVrActivity.OVRJVM-2/libHybridOculusVrActivity.so (OVR::AppLocal::ThreadStarter(void*)+4)

        protected override void onResume()
        {
            base.onResume();
        }

        //override on


        static ApplicationActivity()
        {
            Console.WriteLine("enter  HybridOculusVrActivity.OVRJVM ApplicationActivity cctor");

            // LOCAL_MODULE    := HybridOculusVrActivity
            // X:\jsc.svn\examples\c\android\Test\HybridOculusVrActivity\HybridOculusVrActivity\staging\jni\Android.mk

            java.lang.System.loadLibrary("HybridOculusVrActivity");
        }

        //appPtr = nativeSetAppInterface( this );       
        //public static native long nativeSetAppInterface( VrActivity act ); 

        [Script(IsPInvoke = true)]
        public static long nativeSetAppInterface(com.oculusvr.vrlib.VrActivity act) { return default(long); }



        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }
}

namespace HybridOculusVrActivity.OVRNDK
{
    using ScriptCoreLibNative.SystemHeaders;
    using ScriptCoreLibNative.SystemHeaders.android;

    [Script(IsNative = true, Header = "OvrApp.h")]
    public class OvrApp
    {
        //jni/OvrApp.cpp: In function 'char* cxxGetString()':
        //jni/OvrApp.cpp:18:9: error: deprecated conversion from string constant to 'char*' [-Werror=write-strings]
        //cc1plus.exe: all warnings being treated as errors

        public static string cxxGetString() { return default(string); }
        //jlong cxxSetAppInterface(JNIEnv* jni, jclass clazz, jobject activity);
        public static jlong cxxSetAppInterface(ref JNIEnv jni, jclass c, jobject activity) { return (jlong)(object)0; }
    }

    public partial class xNativeActivity
    {


        //jlong Java_oculus_MainActivity_nativeSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity )

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jlong Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_nativeSetAppInterface(
            ref JNIEnv env,
            jclass clazz,
            jobject activity)
        {
            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_nativeSetAppInterface");

            //jni/HybridOculusVrActivity.dll.c: In function 'Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_nativeSetAppInterface':
            //jni/HybridOculusVrActivity.dll.c:64:5: error: format not a string literal and no format arguments [-Werror=format-security]

            //android_native_app_glue.app_dummy();
            //var x = OvrApp.cxxGetString();

            //android_native_app_glue.app_dummy();
            //log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", x);

            //      return (new OvrApp())->SetActivity( jni, clazz, activity );


            //I/System.Console(13696): enter  HybridOculusVrActivity.OVRJVM ApplicationActivity onCreate
            //I/xNativeActivity(13696): enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI
            //I/xNativeActivity(13696): enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_nativeSetAppInterface

            // http://stackoverflow.com/questions/7281441/elegantly-call-c-from-c

            //long u = 0;

            return OvrApp.cxxSetAppInterface(
                ref env,
                clazz,
                activity
           );
        }



        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI(
            ref JNIEnv env,
            jobject thiz)
        {
            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI");

            var n = env.NewStringUTF;

            // look almost the same file!

            var x = OvrApp.cxxGetString();

            //var v = n(ref env, "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");
            var v = n(ref env, x);

            return v;
        }
    }
}
