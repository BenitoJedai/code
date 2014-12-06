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

    // "X:\opensource\android-ndk-r10c\samples\hello-jni\src\com\example\hellojni\HelloJni.java"
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141206
    public class ApplicationActivity : Activity, ScriptCoreLib.Android.IAssemblyReferenceToken
    {
        // wo dont yet have the cool dispatcher for arm
        // X:\jsc.svn\core\ScriptCoreLibJava.jni\web\bin\jnistb10\dispatch.c

        // X:\jsc.svn\core\ScriptCoreLibJava.jni\BCLImplementation\ScriptCoreLib\Shared\PlatformInvocationServices.cs
        // could we load any .so on will?

        protected override void onCreate(android.os.Bundle value)
        {
            // why call the base?
            base.onCreate(value);


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
