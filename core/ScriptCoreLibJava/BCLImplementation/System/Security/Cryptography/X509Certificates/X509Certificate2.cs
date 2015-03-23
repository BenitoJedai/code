using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.security;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using java.security.cert;
using java.io;
using java.security.interfaces;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.X509Certificates;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates
{
	// http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/x509certificates/x509certificate.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Security/Cryptography/X509Certificates/X509Certificate.cs
	// http://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.x509certificate(v=vs.110).aspx
	// https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Security.Cryptography.X509Certificates/X509Certificate.cs
	// x:\jsc.svn\core\scriptcorelib\javascript\bclimplementation\system\security\cryptography\x509certificates\x509certificate.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\X509Certificates\X509Certificate.cs


	[Script(Implements = typeof(global::System.Security.Cryptography.X509Certificates.X509Certificate2))]
	internal class __X509Certificate2 : __X509Certificate
	{
		// can we extract rsakey from .cer?

		public X509Certificate InternalElement;

		public __X509Certificate2(byte[] rawData)
		{
			var certFactory = CertificateFactory.getInstance("X.509");

			InputStream ins = new ByteArrayInputStream((sbyte[])(object)rawData);

			this.InternalElement = (X509Certificate)certFactory.generateCertificate(ins);
		}

		public global::System.Security.Cryptography.X509Certificates.PublicKey PublicKey
		{
			get
			{
				// will we need non RSA?
				var InternalRSAPublicKey = (RSAPublicKey)this.InternalElement.getPublicKey();

				var Key = new __RSACryptoServiceProvider
				{
					InternalRSAPublicKey = InternalRSAPublicKey
				};
			
				var value = new __PublicKey { Key = Key };

				return value;
			}
		}
	}
}
