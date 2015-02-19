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
    //  System.Attribute for Void .ctor() used at

    //[armeabi-v7a] SharedLibrary  : libHybridOculusVrActivity.so
    // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\3rdParty\turbojpeg.h
    //X:/opensource/android-ndk-r10c/toolchains/arm-linux-androideabi-4.6/prebuilt/windows-x86_64/bin/../lib/gcc/arm-linux-androideabi/4.6/../../../../arm-linux-androideabi/bin/ld.exe: ./obj/local/armeabi-v7a/objs/HybridOculusVrActivity/VrCommon.o: in function OVR::WriteJpeg(char const*, unsigned char const*, int, int):jni/VrCommon.cpp:257: error: undefined reference to 'tjInitCompress'

    // OVR::VrAppInterface
    public partial class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {

        //D/VrActivity(12725): HybridOculusVrActivity.OVRJVM.ApplicationActivity@41defac0 onCreate()
        //D/VrActivity(12725): rate = 44100
        //D/VrActivity(12725): size = 512
        //D/VrActivity(12725): username = guest
        //D/VrActivity(12725): action:android.intent.action.MAIN
        //D/VrActivity(12725): type:null
        //D/VrActivity(12725): uri:null
        //I/System.Console(12725): enter  HybridOculusVrActivity.OVRJVM ApplicationActivity onCreate
        //D/VrActivity(12725): HybridOculusVrActivity.OVRJVM.ApplicationActivity@41defac0 onStart()
        //D/VrActivity(12725): HybridOculusVrActivity.OVRJVM.ApplicationActivity@41defac0 onResume()
        //W/App     (12725): 0x0 Java_com_oculusvr_vrlib_VrActivity_nativeResume


        [Script(NoDecoration = true)]
        static void android_main(android_native_app_glue.android_app state)
        {
            // http://supersegfault.com/three-ways-to-set-up-ndk-apps/

            // jsc is not printing the target name?
            //var loc0 = state;

            // roslyn confuses jsc?

            android_native_app_glue.app_dummy();



            log.__android_log_print(
                log.android_LogPriority.ANDROID_LOG_INFO, 
                "xNativeActivity", "exit HybridOculusVrActivity");



        }
    }
}
