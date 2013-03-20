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
    // http://developer.android.com/reference/android/content/Intent.html
    [Script(IsNative = true)]
    public class Intent
    {
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

        public Intent(Context packageContext, Class cls)
        { }

        public Intent(string action, android.net.Uri uri)
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

        public virtual Intent setAction(string value)
        {
            throw null;
        }
    }
}
