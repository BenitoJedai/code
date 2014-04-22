using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.widget;
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
            // http://android-developers.blogspot.com/2011/11/new-layout-widgets-space-and-gridlayout.html

            var r = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms);

            var u = new ApplicationControl();

            u.AttachTo(this);



            ////// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
            ////// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
            ////// http://developer.android.com/reference/android/webkit/WebView.html





            //this.ShowLongToast("http://jsc-solutions.net");

            //[javac] Compiling 527 source files to V:\bin\classes
            //[javac] V:\src\AndroidFormsActivity\ApplicationControl.java:129: error: cannot find symbol
            //[javac]         super.Dispose_06000006(disposing);
            //[javac]              ^
            //[javac]   symbol: method Dispose_06000006(boolean)
            //[javac] Note: V:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.
        }


    }
}
