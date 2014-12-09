using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.nfc.tech
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/nfc/tech/IsoDep.java

    [Script(IsNative = true)]
    public class IsoDep : BasicTagTechnology
    {
        public void setTimeout(int timeout)
        {
        }

        public sbyte[] transceive(sbyte[] data)
        {
            return null;
        }

        public static IsoDep get(Tag tag)
        {
            return null;
        }
    }
}
