using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.nfc.tech
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/nfc/tech/BasicTagTechnology.java

    [Script(IsNative = true)]
    public abstract class BasicTagTechnology : TagTechnology
    {
        public static IsoDep get(Tag tag)
        {
            return null;
        }

        public void close()
        {
            throw new System.NotImplementedException();
        }


        public void connect()
        {
            throw new System.NotImplementedException();
        }

        public void reconnect()
        {
            throw new System.NotImplementedException();
        }

        public bool isConnected()
        {
            throw new System.NotImplementedException();
        }
    }
}
