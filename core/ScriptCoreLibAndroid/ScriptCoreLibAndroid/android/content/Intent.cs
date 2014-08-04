using android.net;
using android.os;
using java.io;
using java.lang;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{

    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/Intent.java

    // http://developer.android.com/reference/android/content/Intent.html
    [Script(IsNative = true)]
    public class Intent
    {
        public string getStringExtra(string name)
        {
            return null;
        }

        public bool hasExtra(string name)
        {
            return false;
        }

        public Intent putExtra(string name, Class value)
        {
            throw null;
        }

        public Intent putExtra(string name, Bundle value)
        {
            throw null;
        }

        public Intent putExtra(string name, int value)
        {
            throw null;
        }

        public Intent putExtra(string name, string value)
        {
            throw null;
        }

        public Bundle getExtras()
        {
            throw null;
        }

        public const string ACTION_VIEW = "android.intent.action.VIEW";
        public static string ACTION_MAIN;
        public static string CATEGORY_LAUNCHER;
        public static int FLAG_ACTIVITY_NEW_TASK;
        public static int FLAG_ACTIVITY_RESET_TASK_IF_NEEDED;
        public static int FLAG_ACTIVITY_CLEAR_TOP;


        public string getAction() { throw null; }

        public Serializable getSerializableExtra(string name)
        {
            throw null;
        }

        public Bundle getBundleExtra(string name)
        {
            throw null;
        }

        public int getIntExtra(string name, int defaultValue)
        {
            throw null;
        }

        public Intent()
        { }


        public Intent(Context packageContext, Class cls)
        { }

        public Intent(string action, android.net.Uri uri)
        {

        }
        public Intent(string action)
        {

        }
        public static Intent createChooser(Intent arg0, string arg1)
        {
            return default(Intent);
        }

        public virtual Intent setData(Uri value)
        {
            throw null;
        }

        public virtual Intent setFlags(int value)
        {
            throw null;
        }

        public virtual Intent setAction(string value)
        {
            throw null;
        }

        public Intent setComponent(ComponentName c)
        {
            throw null;
        }


        public Intent addCategory(string c)
        {
            throw null;
        }
    }
}
