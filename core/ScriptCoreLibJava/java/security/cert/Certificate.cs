using ScriptCoreLib;
using java.net;

namespace java.security.cert
{
    // http://docs.oracle.com/javase/7/docs/api/java/security/cert/Certificate.html
    // http://developer.android.com/reference/java/security/cert/Certificate.html

    [Script(IsNative = true)]
    public abstract class Certificate
    {
		//extension error: java.security.cert.Certificate
		//19d0:02:01 RewriteToAssembly error: System.InvalidOperationException: Some extension types have mismatching signatures.



		public abstract PublicKey getPublicKey();
		
    }

}
