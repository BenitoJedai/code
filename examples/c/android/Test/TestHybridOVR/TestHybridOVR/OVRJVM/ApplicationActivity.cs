using android.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib;

[assembly: ScriptTypeFilter(ScriptType.Java, typeof(TestHybridOVR.OVRJVM.ApplicationActivity))]

// notice the different namespace
namespace TestHybridOVR.OVRJVM
{
    // "X:\opensource\android-ndk-r10c\samples\hello-jni\libs\armeabi-v7a\libhello-jni.so"

    //BUILD FAILED
    //C:\util\android-sdk-windows\tools\ant\build.xml:720: The following error occurred while executing this line:
    //C:\util\android-sdk-windows\tools\ant\build.xml:734: Unable to find a javac compiler;
    //com.sun.tools.javac.Main is not on the classpath.
    //Perhaps JAVA_HOME does not point to the JDK.
    //It is currently set to "C:\Program Files (x86)\Java\jre7"



    //BUILD FAILED
    //C:\util\android-sdk-windows\tools\ant\build.xml:653: The following error occurred while executing this line:
    //C:\util\android-sdk-windows\tools\ant\build.xml:659: X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\bin\Debug\staging\src does not exist.


    //[gettarget] Project Target:   Android 2.2
    //[gettarget] API level:        8
    //[gettarget] WARNING: Attribute minSdkVersion in AndroidManifest.xml (9) is higher than the project target API level (8)



    //W/dalvikvm( 8478): Exception Ljava/lang/UnsatisfiedLinkError; thrown while initializing LTestHybridOVR/OVRJVM/ApplicationActivity;
    //W/dalvikvm( 8478): Class init failed in newInstance call (LTestHybridOVR/OVRJVM/ApplicationActivity;)
    //D/AndroidRuntime( 8478): Shutting down VM
    //W/dalvikvm( 8478): threadid=1: thread exiting with uncaught exception (group=0x41670ba8)
    //E/AndroidRuntime( 8478): FATAL EXCEPTION: main
    //E/AndroidRuntime( 8478): Process: TestHybridOVR.OVRJVM, PID: 8478
    //E/AndroidRuntime( 8478): java.lang.UnsatisfiedLinkError: Couldn't load TestHybridOVR from loader dalvik.system.PathClassLoader[DexPathList[[zip file "/data/app/TestHybridOVR.OVRJVM-1.apk"],nativeLibraryDirectories=[/data/app-lib/TestHybridOVR.OVRJVM-1, /vendor/lib, /system/lib]]]: findLibrary returned null


    // "X:\opensource\android-ndk-r10c\samples\hello-jni\src\com\example\hellojni\HelloJni.java"
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141206
    public class ApplicationActivity : Activity, ScriptCoreLib.Android.IAssemblyReferenceToken
    {
        // Failure [INSTALL_PARSE_FAILED_MANIFEST_MALFORMED]

        // wo dont yet have the cool dispatcher for arm
        // X:\jsc.svn\core\ScriptCoreLibJava.jni\web\bin\jnistb10\dispatch.c

        // X:\jsc.svn\core\ScriptCoreLibJava.jni\BCLImplementation\ScriptCoreLib\Shared\PlatformInvocationServices.cs
        // could we load any .so on will?

        protected override void onCreate(android.os.Bundle value)
        {
            // why call the base?
            base.onCreate(value);

            //I/System.Console( 8778): enter  TestHybridOVR.OVRJVM ApplicationActivity onCreate
            //I/System.Console( 8778): stringFromJNI: from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI


            Console.WriteLine("enter  TestHybridOVR.OVRJVM ApplicationActivity onCreate");
            // can we get into the native world?
            Console.WriteLine("stringFromJNI: " + stringFromJNI());
        }


        static ApplicationActivity()
        {
            // LOCAL_MODULE    := TestHybridOVR
            // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\staging\jni\Android.mk

            java.lang.System.loadLibrary("TestHybridOVR");
        }

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }
}

namespace TestHybridOVR.OVRNDK
{
    using ScriptCoreLibNative.SystemHeaders;

    public partial class xNativeActivity
    {
        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI(
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
