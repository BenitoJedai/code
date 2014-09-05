using java.math;
using ScriptCoreLib;

namespace java.security
{
    // http://developer.android.com/reference/java/security/interfaces/RSAKey.html
    [Script(IsNative = true)]
    public interface RSAKey
    {
        BigInteger getModulus();
    }
}
