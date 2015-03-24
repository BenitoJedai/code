using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.nfc.tech
{
    // http://developer.android.com/reference/android/nfc/tech/IsoDep.html#getHiLayerResponse%28@%29
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

        public int getMaxTransceiveLength()
        {
            return default(int);
        }

        public bool isExtendedLengthApduSupported()
        {
            return default(bool);
        }
    }
}
