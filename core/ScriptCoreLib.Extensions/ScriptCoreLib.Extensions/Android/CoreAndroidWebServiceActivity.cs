using android.app;
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
    }
}
