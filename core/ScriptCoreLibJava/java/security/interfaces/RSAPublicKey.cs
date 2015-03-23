using java.math;
using ScriptCoreLib;

namespace java.security.interfaces
{
    // http://developer.android.com/reference/java/security/interfaces/RSAPublicKey.html
    [Script(IsNative = true)]
    public interface RSAPublicKey : PublicKey, RSAKey
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs

        BigInteger getPublicExponent();

    }
}
