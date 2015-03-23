using java.math;
using ScriptCoreLib;

namespace java.security.spec
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/spec/RSAPublicKeySpec.html
    // http://developer.android.com/reference/java/security/spec/RSAPublicKeySpec.html
    [Script(IsNative = true)]
    public class RSAPublicKeySpec : KeySpec
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829

        public RSAPublicKeySpec(BigInteger modulus, BigInteger privateExponent)
        {


        }
    }
}
