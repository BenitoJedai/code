using ScriptCoreLib;

namespace java.security
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/KeyPair.html
    // http://developer.android.com/reference/java/security/KeyPair.html
    [Script(IsNative = true)]
    public class KeyPair
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\KeyPair.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829
        // tested by?



        public PrivateKey getPrivate()
        {
            return default(PrivateKey);
        }

        public PublicKey getPublic()
        {
            return default(PublicKey);
        }
    }
}
