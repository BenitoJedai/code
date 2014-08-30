using ScriptCoreLib;

namespace java.security
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/KeyPairGenerator.html
    // http://developer.android.com/reference/java/security/KeyPairGenerator.html
    [Script(IsNative = true)]
    public class KeyPairGenerator
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyGenerate\JVMCLRCryptoKeyGenerate\Program.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829


        public static KeyPairGenerator getInstance(string algorithm)
        {
            return default(KeyPairGenerator);
        }

        public void initialize(int keysize)
        {
        }

        public KeyPair generateKeyPair()
        {
            return default(KeyPair);
        
        }
    }
}
