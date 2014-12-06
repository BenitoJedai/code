using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using HybridOculusVrActivity;

// we need two languages now
[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(HybridOculusVrActivity.OVRNDK.xNativeActivity))]


namespace HybridOculusVrActivity.OVRNDK
{
    //[armeabi-v7a] SharedLibrary  : libHybridOculusVrActivity.so
    // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\3rdParty\turbojpeg.h
    //X:/opensource/android-ndk-r10c/toolchains/arm-linux-androideabi-4.6/prebuilt/windows-x86_64/bin/../lib/gcc/arm-linux-androideabi/4.6/../../../../arm-linux-androideabi/bin/ld.exe: ./obj/local/armeabi-v7a/objs/HybridOculusVrActivity/VrCommon.o: in function OVR::WriteJpeg(char const*, unsigned char const*, int, int):jni/VrCommon.cpp:257: error: undefined reference to 'tjInitCompress'

    // OVR::VrAppInterface
    public partial class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        [Script(NoDecoration = true)]
        static void android_main(android_native_app_glue.android_app state)
        {
            // http://supersegfault.com/three-ways-to-set-up-ndk-apps/

            // jsc is not printing the target name?
            //var loc0 = state;

            // roslyn confuses jsc?

            android_native_app_glue.app_dummy();



            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "exit HybridOculusVrActivity");



        }
    }
}
