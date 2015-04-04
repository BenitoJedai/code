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
    //jsc.jvmi { TargetAndroidRuntime = True, FullName = ScriptCoreLib.SwitchToCLRContextAttribute
    //}
    //1bc8:01:04 CreateToJARImportNatives { FileNameString = :jcsneape\\nri\yrdclsrciiyHbiOuuVAtvt\i\tgn.sesirr\uptjv\yrdclsrciiyAstLbayjr }
    //1bc8:01:04 CreateToJARImportNatives { CreateToJARImportNativesCandidate = :jcsneape\\nri\yrdclsrciiyHbiOuuVAtvt\i\tgn.sesirr\uptjv\yrdclsrciiyAstLbayjr }
    //1bc8:01:04 CreateToJARImportNatives Cache { FileNameString = :jcsneape\\nri\yrdclsrciiyHbiOuuVAtvt\i\tgn.sesirr\uptjv\yrdclsrciiyAstLbayjr, Input = :jcsneape\\nri\yrdclsrciiyHbiOuuVAtvt\i\tgn.sesirr\uptjv\yrdclsrciiyAstLbayjr }

    // "X:\opensource\ovr_mobile_sdk_20141111\sdcard\oculus\tuscany.ovrscene"

    //X:\opensource\ovr_mobile_sdk_20141111\VRLib\src\com\oculusvr\vrlib\PassThroughCamera.java:49: error: cannot find symbol
    //                if ( BuildConfig.DEBUG && ( appPtr != appPtr_ ) && ( appPtr != 0 ) )
    //                     ^

    // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRJVM\ApplicationActivity.cs
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrTemplate\src\oculus\MainActivity.java"
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrScene\src\com\oculusvr\vrscene\MainActivity.java"

    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141206
    //public class ApplicationActivity : Activity, ScriptCoreLib.Android.IAssemblyReferenceToken




    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "19")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : com.oculusvr.vrlib.VrActivity, ScriptCoreLib.Android.IAssemblyReferenceToken
    {
        // 
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

            android.content.Intent intent = getIntent();
            var commandString = com.oculusvr.vrlib.VrLib.getCommandStringFromIntent(intent);
            var fromPackageNameString = com.oculusvr.vrlib.VrLib.getPackageStringFromIntent(intent);
            var uriString = com.oculusvr.vrlib.VrLib.getUriStringFromIntent(intent);
            // why are we roundtriping intent details?


            // https://forums.oculus.com/viewtopic.php?f=67&t=17790
            this.appPtr = nativeSetAppInterface(this,
                    fromPackageNameString, commandString, uriString                
                );

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

            // PInvoke wont load implictly? 
            java.lang.System.loadLibrary("HybridOculusVrActivity");
        }

        //appPtr = nativeSetAppInterface( this );       
        //public static native long nativeSetAppInterface( VrActivity act ); 

        [Script(IsPInvoke = true)]
        public static long nativeSetAppInterface(com.oculusvr.vrlib.VrActivity act, 
            string fromPackageNameString, string commandString, string uriString) { return default(long); }



        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }
}

namespace HybridOculusVrActivity.OVRNDK
{
    using ScriptCoreLibNative.SystemHeaders;
    using ScriptCoreLibNative.SystemHeaders.android;

    // what is this good for? just to reference the header?
    // it is the cpp export header for our c code to consume. 
    [Script(IsNative = true, Header = "OvrApp.h")]
    public class OvrApp
    {
        //jni/OvrApp.cpp: In function 'char* cxxGetString()':
        //jni/OvrApp.cpp:18:9: error: deprecated conversion from string constant to 'char*' [-Werror=write-strings]
        //cc1plus.exe: all warnings being treated as errors

        public static string cxxGetString() { return default(string); }
        //jlong cxxSetAppInterface(JNIEnv* jni, jclass clazz, jobject activity);
        public static jlong cxxSetAppInterface(ref JNIEnv jni, jclass c, jobject activity, 
            jstring fromPackageNameString, jstring commandString, jstring uriString) { return (jlong)(object)0; }
    }

    public partial class xNativeActivity
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/android-mk

        //jlong Java_oculus_MainActivity_nativeSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity )

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jlong Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_nativeSetAppInterface(
            ref JNIEnv env,
            jclass clazz,
            jobject activity,

            jstring fromPackageNameString, jstring commandString, jstring uriString            
            )
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
                activity,


                // added by oculus050
                fromPackageNameString, commandString, uriString
           );
        }



        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI(
            ref JNIEnv env,
            jobject thiz)
        {
            log.__android_log_print(
                log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI"
            );

            var n = env.NewStringUTF;

            // look almost the same file!

            var x = OvrApp.cxxGetString();

            //var v = n(ref env, "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");
            var v = n(ref env, x);

            return v;
        }
    }
}

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/install

//I/GlUtils ( 3912): GL_MAX_FRAGMENT_UNIFORM_VECTORS = 512
//I/EyePostRender( 3912): EyePostRender::Init()
//I/PackageFiles( 3912): File 'res/raw/loading_indicator.png' not found in apk!
//V/VrLocale( 3912): font_name is not a valid resource id!!
//I/VrLocale( 3912): key not found, localized to 'efigs.fnt'
//I/PackageFiles( 3912): File 'res/raw/efigs.fnt' not found in apk!
//F/art     ( 3912): art/runtime/check_jni.cc:65] JNI DETECTED ERROR IN APPLICATION: the return type of CallObjectMethodV does not match void com.oculusvr.vrlib.VrActivity.setDefaultLocale()



//I/DEBUG   (  199): backtrace:
//I/DEBUG   (  199):     #00 pc 0003b79c  /system/lib/libc.so (tgkill+12)
//I/DEBUG   (  199):     #01 pc 000166e5  /system/lib/libc.so (pthread_kill+52)
//I/DEBUG   (  199):     #02 pc 000172f7  /system/lib/libc.so (raise+10)
//I/DEBUG   (  199):     #03 pc 00013b49  /system/lib/libc.so (__libc_android_abort+36)
//I/DEBUG   (  199):     #04 pc 00011fec  /system/lib/libc.so (abort+4)
//I/DEBUG   (  199):     #05 pc 002260d3  /system/lib/libart.so (art::Runtime::Abort()+170)
//I/DEBUG   (  199):     #06 pc 000a7359  /system/lib/libart.so (art::LogMessage::~LogMessage()+1360)
//I/DEBUG   (  199):     #07 pc 000b1471  /system/lib/libart.so (art::JniAbort(char const*, char const*)+1112)
//I/DEBUG   (  199):     #08 pc 000b19b5  /system/lib/libart.so (art::JniAbortF(char const*, char const*, ...)+68)
//I/DEBUG   (  199):     #09 pc 000b23d1  /system/lib/libart.so (_ZN3art11ScopedCheck8CheckSigEP10_jmethodIDPKcb.constprop.130+284)
//I/DEBUG   (  199):     #10 pc 000b9a81  /system/lib/libart.so (art::CheckJNI::CallObjectMethodV(_JNIEnv*, _jobject*, _jmethodID*, std::__va_list)+72)
//I/DEBUG   (  199):     #11 pc 00049460  /data/app/HybridOculusVrActivity.OVRJVM-1/lib/arm/libHybridOculusVrActivity.so (_JNIEnv::CallObjectMethod(_jobject*, _jmethodID*, ...)+36)
//I/DEBUG   (  199):     #12 pc 000c5924  /data/app/HybridOculusVrActivity.OVRJVM-1/lib/arm/libHybridOculusVrActivity.so (OVR::AppLocal::InitFonts()+224)
//I/DEBUG   (  199):     #13 pc 000c9b34  /data/app/HybridOculusVrActivity.OVRJVM-1/lib/arm/libHybridOculusVrActivity.so (OVR::AppLocal::VrThreadFunction()+524)
//I/DEBUG   (  199):     #14 pc 000cb164  /data/app/HybridOculusVrActivity.OVRJVM-1/lib/arm/libHybridOculusVrActivity.so (OVR::AppLocal::ThreadStarter(void*)+4)
//I/DEBUG   (  199):     #15 pc 00015ed3  /system/lib/libc.so (__pthread_start(void*)+30)
//I/DEBUG   (  199):     #16 pc 00013ea7  /system/lib/libc.so (__start_thread+6)
//I/DEBUG   (  199):
//I/DEBUG   (  199): Tombstone written to: /data/tombstones/tombstone_08

// http://developer.android.com/reference/android/content/Context.html#getExternalFilesDirs(java.lang.String)
//I/dalvikvm(22337): java.lang.NoSuchMethodError: android.app.Activity.getExternalFilesDirs
//I/dalvikvm(22337):      at com.oculusvr.vrlib.VrLib.getExternalStorageFilesDirAtIdx(VrLib.java:1138)
//I/dalvikvm(22337):      at com.oculusvr.vrlib.VrLib.getPrimaryExternalStorageFilesDir(VrLib.java:1174)
//I/dalvikvm(22337):      at HybridOculusVrActivity.OVRJVM.ApplicationActivity.nativeSetAppInterface(Native Method)
//I/dalvikvm(22337):      at HybridOculusVrActivity.OVRJVM.ApplicationActivity.onCreate(ApplicationActivity.java:52)
//I/dalvikvm(22337):      at android.app.Activity.performCreate(Activity.java:5133)
//I/dalvikvm(22337):      at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1087)
//I/dalvikvm(22337):      at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2175)
//I/dalvikvm(22337):      at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2261)
//I/dalvikvm(22337):      at android.app.ActivityThread.access$600(ActivityThread.java:141)
//I/dalvikvm(22337):      at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1256)
//I/dalvikvm(22337):      at android.os.Handler.dispatchMessage(Handler.java:99)
//I/dalvikvm(22337):      at android.os.Looper.loop(Looper.java:137)
//I/dalvikvm(22337):      at android.app.ActivityThread.main(ActivityThread.java:5103)
//I/dalvikvm(22337):      at java.lang.reflect.Method.invokeNative(Native Method)
//I/dalvikvm(22337):      at java.lang.reflect.Method.invoke(Method.java:525)
//I/dalvikvm(22337):      at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:737)
//I/dalvikvm(22337):      at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:553)
//I/dalvikvm(22337):      at dalvik.system.NativeStart.main(Native Method)

//I/DEBUG   (  122): backtrace:
//I/DEBUG   (  122):     #00  pc 00000000  <unknown>
//I/DEBUG   (  122):     #01  pc 000b9d34  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (OVR::GlGeometry::Create(OVR::VertexAttribs const&, OVR::Array<unsigned short, OVR::ArrayDefaultPolicy> const&)+88)
//I/DEBUG   (  122):     #02  pc 000bc578  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (OVR::BuildTesselatedQuad(int, int, bool)+1216)
//I/DEBUG   (  122):     #03  pc 000c7470  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (OVR::AppLocal::InitGlObjects()+480)
//I/DEBUG   (  122):     #04  pc 000c9a6c  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (OVR::AppLocal::VrThreadFunction()+324)
//I/DEBUG   (  122):     #05  pc 000cb164  /data/app-lib/HybridOculusVrActivity.OVRJVM-1/libHybridOculusVrActivity.so (OVR::AppLocal::ThreadStarter(void*)+4)
//I/DEBUG   (  122):     #06  pc 0000d228  /system/lib/libc.so (__thread_entry+72)
//I/DEBUG   (  122):     #07  pc 0000d3c0  /system/lib/libc.so (pthread_create+240)