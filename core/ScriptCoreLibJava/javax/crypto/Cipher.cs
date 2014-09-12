using java.security;
using ScriptCoreLib;

namespace javax.crypto
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/javax/crypto/Cipher.html
    // http://developer.android.com/reference/javax/crypto/Cipher.html

    [Script(IsNative = true)]
    public class Cipher
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyGenerate\JVMCLRCryptoKeyGenerate\Program.cs

        public static readonly int ENCRYPT_MODE = 1;
        public static readonly int DECRYPT_MODE = 2;

        [System.Obsolete("not recommended")]
        public static Cipher getInstance(string transformation, string provider)
        {
            // http://docs.oracle.com/javase/7/docs/technotes/guides/security/SunProviders.html

            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
            return default(Cipher);
        }

        public static Cipher getInstance(string transformation)
        {

            return default(Cipher);

        }


        public sbyte[] doFinal(sbyte[] input)
        {

            return default(sbyte[]);
        }


        public void init(int opmode, Key key)
        {

        }
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829
        // tested by?

    }
}
