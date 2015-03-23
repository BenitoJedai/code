using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.X509Certificates
{
	// http://referencesource.microsoft.com/#System/security/system/security/cryptography/x509/x509certificate2.cs

	[Script(Implements = typeof(global::System.Security.Cryptography.X509Certificates.PublicKey))]
	public class __PublicKey
	{
		// X:\jsc.svn\examples\javascript\Test\TestCryptoUIThreadIdentityKeyPair\TestCryptoUIThreadIdentityKeyPair\Application.cs
		// X:\jsc.svn\examples\java\hybrid\crypto\JVMCLRRSADuplex\JVMCLRRSADuplex\Program.cs

		// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\X509Certificates\X509Certificate2.cs

		public AsymmetricAlgorithm Key { get; set; }

		public static implicit operator global::System.Security.Cryptography.X509Certificates.PublicKey(__PublicKey e)
		{
			return (global::System.Security.Cryptography.X509Certificates.PublicKey)(object)e;
		}
	}
}
