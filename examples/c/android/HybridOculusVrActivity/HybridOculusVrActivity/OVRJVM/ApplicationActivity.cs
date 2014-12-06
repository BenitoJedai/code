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
    // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRJVM\ApplicationActivity.cs
    // "X:\opensource\ovr_mobile_sdk_20141111\VrNative\VrTemplate\src\oculus\MainActivity.java"

    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141206
    public class ApplicationActivity : Activity, ScriptCoreLib.Android.IAssemblyReferenceToken
    {
        // if we were to include AssetsLibrary,
        // how can we force non merge/ script ?


        // VrActivity



        // wo dont yet have the cool dispatcher for arm
        // X:\jsc.svn\core\ScriptCoreLibJava.jni\web\bin\jnistb10\dispatch.c

        // X:\jsc.svn\core\ScriptCoreLibJava.jni\BCLImplementation\ScriptCoreLib\Shared\PlatformInvocationServices.cs
        // could we load any .so on will?

        protected override void onCreate(android.os.Bundle value)
        {

            // why call the base?
            base.onCreate(value);

            Console.WriteLine("enter  HybridOculusVrActivity.OVRJVM ApplicationActivity onCreate");
            var tv = new TextView(this);
            tv.setText(stringFromJNI());
            setContentView(tv);



            //I/System.Console( 8778): enter  HybridOculusVrActivity.OVRJVM ApplicationActivity onCreate
            //I/System.Console( 8778): stringFromJNI: from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI


            // can we get into the native world?
            //Console.WriteLine("stringFromJNI: " + stringFromJNI());
        }


        static ApplicationActivity()
        {
            // LOCAL_MODULE    := HybridOculusVrActivity
            // X:\jsc.svn\examples\c\android\Test\HybridOculusVrActivity\HybridOculusVrActivity\staging\jni\Android.mk

            java.lang.System.loadLibrary("HybridOculusVrActivity");
        }

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
