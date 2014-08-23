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
    [Script(IsNative = true)]
    public class Activity : ContextThemeWrapper
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

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
