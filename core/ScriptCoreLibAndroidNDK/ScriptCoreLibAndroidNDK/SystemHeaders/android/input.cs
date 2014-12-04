using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\opensource\android-ndk-r10c\platforms\android-9\arch-arm\usr\include\android\input.h"

    [Script(IsNative = true, Header = "android/input.h", IsSystemHeader = true)]
    public static class input
    {
        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        public enum AInputEventType
        {
            /* Indicates that the input event is a key event. */
            AINPUT_EVENT_TYPE_KEY = 1,

            /* Indicates that the input event is a motion event. */
            AINPUT_EVENT_TYPE_MOTION = 2
        };


        /* Get the input event type. */
        public static AInputEventType AInputEvent_getType(this AInputEvent @event) { return default(AInputEventType);}

        // AInputEvent
        [Script(IsNative = true)]
        public class AInputEvent
        {
            // a named pointer without data fields?
            // X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\xNativeActivity.cs
        }

          [Script(IsNative = true)]
        public class AInputQueue
        {
        }
        
    }

}
