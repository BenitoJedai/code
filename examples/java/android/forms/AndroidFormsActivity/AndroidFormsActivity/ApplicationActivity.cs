using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Extensions.Android;
using System.Windows.Forms;
using ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms;

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

            u.button1.Click += delegate
            {
                //var temp = (__UserControl)(object)u;
                var popupView = new android.widget.LinearLayout(this);
                var dispWidth = getWindowManager().getDefaultDisplay().getWidth() - 60;

                var popupText = new TextView(this);
                popupText.setText("This is Popup Window!");
                popupText.setPadding(0, 0, 0, 20);
                popupText.setTextColor(-16711936);

                var popupFormsTextBox = new TextBox();
                popupFormsTextBox.PasswordChar = '*';
                ((__TextBox)(object)popupFormsTextBox).InternalBeforeSetContext(this);
                var t = ((__TextBox)(object)popupFormsTextBox).InternalGetElement();
                ((EditText)t).setWidth(dispWidth);
                

                var submitButt = new System.Windows.Forms.Button();
                submitButt.Text = "Submit";
                ((__Button)(object)submitButt).InternalBeforeSetContext(this);
                var b = ((__Button)(object)submitButt).InternalGetElement();

                var cancelButt = new System.Windows.Forms.Button();
                cancelButt.Text = "Cancel";
                ((__Button)(object)cancelButt).InternalBeforeSetContext(this);
                var cb = ((__Button)(object)cancelButt).InternalGetElement();

                popupView.addView(popupText);
                popupView.addView(t);
                popupView.addView(b);
                popupView.addView(cb);

                popupView.setOrientation(1);
                popupView.setBackgroundColor(-3355444);

                var popup = new android.widget.PopupWindow(popupView, dispWidth, 250);
                popup.setContentView(popupView);
                popup.setFocusable(true);
                popup.setOutsideTouchable(true);

                // E/AndroidRuntime( 4979): Caused by: java.lang.NoSuchMethodError: android.widget.PopupWindow.showAsDropDown
                popup.showAsDropDown(((__Button)(object)u.button1).InternalGetElement(), android.view.Gravity.CENTER, 40, 0);
                u.button1.Text = "Now popup must show!!";

                submitButt.Click += delegate
                {
                    submitButt.Text = ((EditText)t).getText().ToString();
                };

                cancelButt.Click += delegate
                {
                    popup.dismiss();
                };
            };



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
