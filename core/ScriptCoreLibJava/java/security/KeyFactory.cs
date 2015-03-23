using ScriptCoreLib;

namespace java.security
{
    // http://developer.android.com/reference/java/security/KeyFactory.html
    [Script(IsNative = true)]
    public class KeyFactory
    {
        public PublicKey generatePublic(KeySpec keySpec)
        {
            return default(PublicKey);
        }

        public static KeyFactory getInstance(string algorithm)
        { 
            return default(KeyFactory);
        }

    }
}
