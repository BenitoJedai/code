using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using AndroidFormsActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Extensions.Android;

namespace AndroidFormsActivity.Activities
{
    public class ApplicationActivity : Activity
    {


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);


            InitializeContent();
        }

        private void InitializeContent()
        {
            var r = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken);

            var u = new ApplicationControl();

            u.AttachTo(this);

           

            ////// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
            ////// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
            ////// http://developer.android.com/reference/android/webkit/WebView.html





            this.ShowLongToast("http://jsc-solutions.net");
        }


    }
}
