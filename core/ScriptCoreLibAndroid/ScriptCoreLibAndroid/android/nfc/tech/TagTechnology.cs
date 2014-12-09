using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.nfc.tech
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/nfc/tech/TagTechnology.java

    [Script(IsNative = true)]
    public interface TagTechnology
    {


        void connect();

        void reconnect();

        bool isConnected();
    }
}
