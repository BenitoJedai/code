using android.app;
using android.widget;
using ScriptCoreLib.Android.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android
{
    // mark ourselves 
    [ApplicationMetaData(name = "CoreAndroidWebServiceActivity", value = "http://my.jsc-solutions.net")]
    public abstract class CoreAndroidWebServiceActivity : Activity
    {
        // tested by X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs


        //protected void onActivityResult(int requestCode, int resultCode,
        //                           Intent intent)

        #region ActivityResult
        public event Action<int, int, android.content.Intent> ActivityResult;

        protected override void onActivityResult(int requestCode, int resultCode, android.content.Intent intent)
        {
            base.onActivityResult(requestCode, resultCode, intent);

            if (ActivityResult != null)
                ActivityResult(requestCode, resultCode, intent);
        }
        #endregion



        public static FrameLayout InternalFloatContainer;

        #region AtResume
        public event Action AtResume;
        protected override void onResume()
        {
            Console.WriteLine("CoreAndroidWebServiceActivity onResume");

            base.onResume();

            if (AtResume != null)
                AtResume();
        }
        #endregion

        #region AtPause
        public event Action AtPause;
        protected override void onPause()
        {
            base.onPause();

            if (AtPause != null)
                AtPause();
        }
        #endregion

        #region AtNewIntent
        public event Action<android.content.Intent> AtNewIntent;
        protected override void onNewIntent(android.content.Intent value)
        {
            base.onNewIntent(value);

            if (AtNewIntent != null)
                AtNewIntent(value);
        }
        #endregion


        #region AtLowMemory
        public event Action AtLowMemory;

        public override void onLowMemory()
        {
            // http://developer.android.com/reference/android/content/ComponentCallbacks.html#onLowMemory()
            // http://stackoverflow.com/questions/2881139/outofmemoryerror-what-to-increase-and-how

            Console.WriteLine("\n\n onLowMemory");

            if (AtLowMemory != null)
                AtLowMemory();
        }
        #endregion

    }
}
