using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders.android;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\opensource\android-ndk-r10c\sources\android\native_app_glue\android_native_app_glue.c"
    // "X:\opensource\android-ndk-r10c\sources\android\native_app_glue\android_native_app_glue.h"


    // LOCAL_STATIC_LIBRARIES := android_native_app_glue
    [Script(IsNative = true, Header = "android_native_app_glue.h", IsSystemHeader = true)]
    public static class android_native_app_glue
    {
        // LOCAL_STATIC_LIBRARIES := android_native_app_glue


        // can the android app
        // ask for NFC chip and pin in order to download or decrypt the payload frame
        // application DRM
        // this would also mean the system knows who has activated parts of the application
        // for reactivation NFC is checked again
        // a loyalty/security scheme
        // if decrypted once, how to enforce time lock?



        public enum android_app_cmd
        {
            /**
             * Command from main thread: the AInputQueue has changed.  Upon processing
             * this command, android_app->inputQueue will be updated to the new queue
             * (or NULL).
             */
            APP_CMD_INPUT_CHANGED,

            /**
             * Command from main thread: a new ANativeWindow is ready for use.  Upon
             * receiving this command, android_app->window will contain the new window
             * surface.
             */
            APP_CMD_INIT_WINDOW,

            /**
             * Command from main thread: the existing ANativeWindow needs to be
             * terminated.  Upon receiving this command, android_app->window still
             * contains the existing window; after calling android_app_exec_cmd
             * it will be set to NULL.
             */
            APP_CMD_TERM_WINDOW,

            /**
             * Command from main thread: the current ANativeWindow has been resized.
             * Please redraw with its new size.
             */
            APP_CMD_WINDOW_RESIZED,

            /**
             * Command from main thread: the system needs that the current ANativeWindow
             * be redrawn.  You should redraw the window before handing this to
             * android_app_exec_cmd() in order to avoid transient drawing glitches.
             */
            APP_CMD_WINDOW_REDRAW_NEEDED,

            /**
             * Command from main thread: the content area of the window has changed,
             * such as from the soft input window being shown or hidden.  You can
             * find the new content rect in android_app::contentRect.
             */
            APP_CMD_CONTENT_RECT_CHANGED,

            /**
             * Command from main thread: the app's activity window has gained
             * input focus.
             */
            APP_CMD_GAINED_FOCUS,

            /**
             * Command from main thread: the app's activity window has lost
             * input focus.
             */
            APP_CMD_LOST_FOCUS,

            /**
             * Command from main thread: the current device configuration has changed.
             */
            APP_CMD_CONFIG_CHANGED,

            /**
             * Command from main thread: the system is running low on memory.
             * Try to reduce your memory use.
             */
            APP_CMD_LOW_MEMORY,

            /**
             * Command from main thread: the app's activity has been started.
             */
            APP_CMD_START,

            /**
             * Command from main thread: the app's activity has been resumed.
             */
            APP_CMD_RESUME,

            /**
             * Command from main thread: the app should generate a new saved state
             * for itself, to restore from later if needed.  If you have saved state,
             * allocate it with malloc and place it in android_app.savedState with
             * the size in android_app.savedStateSize.  The will be freed for you
             * later.
             */
            APP_CMD_SAVE_STATE,

            /**
             * Command from main thread: the app's activity has been paused.
             */
            APP_CMD_PAUSE,

            /**
             * Command from main thread: the app's activity has been stopped.
             */
            APP_CMD_STOP,

            /**
             * Command from main thread: the app's activity is being destroyed,
             * and waiting for the app thread to clean up and exit before proceeding.
             */
            APP_CMD_DESTROY,
        };

        [Script(IsNative = true)]
        public delegate void android_app_onAppCmd(android_app app, android_app_cmd cmd);

        [Script(IsNative = true)]
        public delegate int android_app_onInputEvent(android_app app, input.AInputEvent cmd);


        //[Script(IsNative = true, ExternalTarget = "android_app")]

        // jsc could figure out this for native types!
        //[Script(IsNative = true, HasNoPrototype = true, PointerName = "android_app*")]
        [Script(IsNative = true)]
        //[Script(PointerName = "PWNDCLASSEX", HasNoPrototype = true)]
        public class android_app
        {
            // ANativeActivity

            public object userData;

            //     void (*onAppCmd)(struct android_app* app, int32_t cmd);
            public android_app_onAppCmd onAppCmd;


            // Fill this in with the function to process input events.  At this point
            // the event has already been pre-dispatched, and it will be finished upon
            // return.  Return 1 if you have handled the event, 0 for any default
            // dispatching.
            //int32_t (*onInputEvent)(struct android_app* app, AInputEvent* event);
            public android_app_onInputEvent onInputEvent;

            public native_activity.ANativeActivity activity;

            //ALooper looper;

        }




        [Script(IsNative = true)]
        //void (*process)(struct android_app* app, struct android_poll_source* source);
        public delegate void android_poll_source_process(android_app app, android_poll_source source);

        [Script(IsNative = true)]
        public class android_poll_source
        {
            public android_poll_source_process process;
        }


        [Script(IsNative = true)]
        //[Script(PointerName = "PWNDCLASSEX", HasNoPrototype = true)]
        public class android_app<TuserData>
        {
            // ANativeActivity

            // generic in C ? would it work?
            public TuserData userData;

            //     void (*onAppCmd)(struct android_app* app, int32_t cmd);
            public android_app_onAppCmd onAppCmd;
        }

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        public static void app_dummy() { }


        // http://stackoverflow.com/questions/856636/effects-of-the-extern-keyword-on-c-functions
        // Technically, every function in a library public header is 'extern', 
        // however labeling them as such has very little to no benefit, depending on the compiler. 
        // Most compilers can figure that out on their own. As you see, those functions are actually defined somewhere else.

        [Obsolete("will jsc imply the extern keyword? look for [Script(NoDecoration = true)]")]
        //extern static void android_main(android_native_app_glue.android_app state);//; { }
        // actually. the user code cannot redefine the header. jsc wont be emitting the extern as that header file is part of the natives
        static void android_main(android_native_app_glue.android_app state) { }

        //X:/opensource/android-ndk-r10c/sources/android/native_app_glue/android_native_app_glue.h:343:13: note: previous declaration of 'android_main' was here
        // extern void android_main(struct android_app* app);
        //             ^
        //jni/TestNDK.dll.c:12:6: error: conflicting types for 'android_main'
        // void android_main(void* state)

    }

}
