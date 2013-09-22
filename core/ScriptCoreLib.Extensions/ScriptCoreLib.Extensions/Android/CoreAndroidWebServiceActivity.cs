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

        public event Action<int, int, android.content.Intent> ActivityResult;

        protected override void onActivityResult(int requestCode, int resultCode, android.content.Intent intent)
        {
            base.onActivityResult(requestCode, resultCode, intent);

            if (ActivityResult != null)
                ActivityResult(requestCode, resultCode, intent);
        }


        public static FrameLayout InternalFloatContainer;


        public event Action AtResume;
        protected override void onResume()
        {
            base.onResume();

            if (AtResume != null)
                AtResume();
        }

        public event Action AtPause;
        protected override void onPause()
        {
            base.onPause();

            if (AtPause != null)
                AtPause();
        }

        public event Action<android.content.Intent> AtNewIntent;
        protected override void onNewIntent(android.content.Intent value)
        {
            base.onNewIntent(value);

            if (AtNewIntent != null)
                AtNewIntent(value);
        }

    }
}
