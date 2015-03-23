using ScriptCoreLib;
using java.net;
using java.io;

namespace java.security.cert
{
	// http://developer.android.com/reference/java/security/cert/CertificateFactory.html

	[Script(IsNative = true)]
	public class CertificateFactory
	{
		public static CertificateFactory getInstance(string type)
		{
			return default(CertificateFactory);
		}

		public Certificate generateCertificate(InputStream inStream)
		{
			return default(Certificate);
		}
	}

}
