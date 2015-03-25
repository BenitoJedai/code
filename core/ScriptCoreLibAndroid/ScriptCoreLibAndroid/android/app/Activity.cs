using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using java.lang;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/Activity.java
    // http://developer.android.com/reference/android/app/Activity.html
    // https://github.com/blue112/android-externs-haxe/blob/master/android/app/Activity.hx

    [Script(IsNative = true)]
    public class Activity : ContextThemeWrapper
    {
        // http://redmondmag.com/articles/2014/02/01/could-android-be-coming-to-windows.aspx

        // http://developer.android.com/reference/android/app/NativeActivity.html

        // http://static.oculus.com/sdk-downloads/documents/Oculus_Mobile_v0.4.0_SDK_Documentation.zip
        // http://static.oculus.com/sdk-downloads/documents/OculusMobile_SubmissionGuidelines.pdf
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141125

        // You must structure your main activity as shown below. In particular, note that your main
        //activity's intent filter should be sent to android.intent.category.INFO instead of the
        //more common android.intent.category.LAUNCHER. This is to ensure that your app
        //only appears in Oculus Home and can’t be launched from the phone's launcher. The only
        //exception to this rule is if you are creating an app that can operate both with and without
        //VR functionality. In that case, use the LAUNCHER category and replace vr_only with
        //vr_dual in the com.samsung.android.vr.application.mode meta data tag
        // X:\jsc.svn\market\synergy\oculus\com.oculusvr.vrlib\com.oculusvr.vrlib\ApplicationActivity.cs

        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\CoreAndroidWebServiceActivity.cs
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs


        public void setProgressBarIndeterminateVisibility(bool visible)
        { }

        public void addContentView(View view, ViewGroup.LayoutParams @params)
        {

        }

        protected virtual void onNewIntent(android.content.Intent value)
        {

        }

        public static int RESULT_OK;

        protected virtual void onResume()
        {
        }

        protected virtual void onPause()
        {
        }

        public virtual void onLowMemory()
        {

        }

        public void sendBroadcast(Intent intent)
        {
        }

        public void setResult(int resultCode, Intent data)
        {

        }

        public void finish()
        {
        }


        public Intent getIntent()
        {
            throw null;
        }

        public virtual bool dispatchKeyEvent(KeyEvent @event)
        {
            throw null;
        }

        protected virtual void onActivityResult(int arg0, int arg1, Intent arg2)
        {

        }

        // members and types are to be extended by jsc at release build

        // http://developer.android.com/reference/android/app/Activity.html#setContentView(android.view.View)
        public virtual void setContentView(View savedInstanceState)
        {
        }

        protected virtual void onCreate(Bundle savedInstanceState)
        {

        }

        public virtual bool onCreateOptionsMenu(Menu value)
        {
            return default(bool);

        }
        public virtual bool onPrepareOptionsMenu(Menu value)
        {
            return default(bool);
        }

        // http://developer.android.com/reference/android/app/Activity.html#requestWindowFeature(int)
        public virtual bool requestWindowFeature(int e)
        {
            return default(bool);
        }


        public virtual bool onKeyDown(int keyCode, KeyEvent @event)
        {
            return default(bool);
        }

        public virtual void setTitle(string e)
        {

        }

        public virtual Window getWindow()
        {
            return default(Window);
        }

        public void runOnUiThread(Runnable r)
        {
        }
    }
}
