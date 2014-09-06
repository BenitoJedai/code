using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace javax.crypto.spec
{
    // http://developer.android.com/reference/javax/crypto/spec/OAEPParameterSpec.html
    // http://docs.oracle.com/javase/7/docs/api/javax/crypto/spec/OAEPParameterSpec.html
    //    http://stackoverflow.com/questions/2930206/rsa-c-sharp-encrypt-java-decrypt

    [Script(IsNative = true)]
    public class OAEPParameterSpec
    {
        // http://security.stackexchange.com/questions/32050/what-specific-padding-weakness-does-oaep-address-in-rsa

        // this is useful how?

        // http://www.ietf.org/mail-archive/web/jose/current/msg04138.html
        // http://www.netmite.com/android/mydroid/1.5/dalvik/libcore/security/src/main/java/org/bouncycastle/jce/provider/JCERSACipher.java

        public static readonly OAEPParameterSpec DEFAULT;

    }
}
