extern alias globalandroid;
using globalandroid::android.content;
using globalandroid::android.app;
using globalandroid::android.widget;
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
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        // tested by X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs


        //protected void onActivityResult(int requestCode, int resultCode,
        //                           Intent intent)

        #region ActivityResult
        public event Action<int, int, Intent> ActivityResult;

        protected override void onActivityResult(int requestCode, int resultCode, Intent intent)
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

        // http://developer.android.com/guide/components/activities.html#ImplementingLifecycleCallbacks


        #region AtStop
        public event Action AtStop;
        protected override void onStop()
        {
            base.onStop();

            if (AtStop != null)
                AtStop();
        }
        #endregion


        #region AtNewIntent
        public event Action<Intent> AtNewIntent;
        protected override void onNewIntent(Intent value)
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
