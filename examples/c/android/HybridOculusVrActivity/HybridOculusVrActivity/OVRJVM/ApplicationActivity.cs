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
    //X:\opensource\ovr_mobile_sdk_20141111\VRLib\src\com\oculusvr\vrlib\PassThroughCamera.java:49: error: cannot find symbol
    //                if ( BuildConfig.DEBUG && ( appPtr != appPtr_ ) && ( appPtr != 0 ) )
    //                     ^

    // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRJVM\ApplicationActivity.cs
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrTemplate\src\oculus\MainActivity.java"
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrScene\src\com\oculusvr\vrscene\MainActivity.java"

    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141206
    public class ApplicationActivity : Activity, ScriptCoreLib.Android.IAssemblyReferenceToken
    //public class ApplicationActivity : com.oculusvr.vrlib.VrActivity, ScriptCoreLib.Android.IAssemblyReferenceToken
    {
        public com.oculusvr.vrlib.VrActivity ref0;

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
            //this.appPtr = 0;

            //nativeResume(appPtr);
        }

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
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }
}

namespace HybridOculusVrActivity.OVRNDK
{
    using ScriptCoreLibNative.SystemHeaders;

    public partial class xNativeActivity
    {
        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI(
            ref JNIEnv env,
            jobject thiz)
        {

            var n = env.NewStringUTF;

            // look almost the same file!

            var v = n(ref env, "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");

            return v;
        }
    }
}
