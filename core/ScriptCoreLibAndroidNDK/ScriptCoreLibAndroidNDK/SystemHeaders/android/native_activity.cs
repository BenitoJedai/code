using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
//using ScriptCoreLibNative.BCLImplementation.System;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\opensource\android-ndk-r10c\platforms\android-9\arch-arm\usr\include\android\native_activity.h"

    [Script(IsNative = true, Header = "android/native_activity.h", IsSystemHeader = true)]
    public static class native_activity
    {
        // The android.app.NativeActivity java class and the corresponding native struct ANativeActivity 
        // defined in <android/native_activity.h> frees applications from having to write java glue code, 
        // as well as providing native input event support not available otherwise.



        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        [Script(IsNative = true)]
        public delegate void ANativeActivityCallbacks_onResume(ANativeActivity activity);

        [Script(IsNative = true)]
        public delegate void ANativeActivityCallbacks_onPause(ANativeActivity activity);



        [Script(IsNative = true)]
        public class ANativeActivityCallbacks
        {
            // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\native_activity.cs

            public ANativeActivityCallbacks_onResume onResume;
            public ANativeActivityCallbacks_onPause onPause;
        }

        [Script(IsNative = true)]
        public class ANativeActivity
        {
            public ANativeActivityCallbacks callbacks;

            public jni.JavaVM vm;

            // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRNDK\xNativeActivity.cs
            public JNIEnv env;

            // typedef void*           jobject;
            public object clazz;

            //jobject clazz;
            public string internalDataPath;
            public string externalDataPath;

            public asset_manager.AAssetManager assetManager;
        }

        //    ANativeActivityCallbacks 


        // static field we need to set or redefine?
        // extern ANativeActivity_createFunc ANativeActivity_onCreate;
    }

}
