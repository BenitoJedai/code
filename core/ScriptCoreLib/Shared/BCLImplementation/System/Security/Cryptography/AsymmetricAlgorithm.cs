using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
	// http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/asymmetricalgorithm.cs

	[Script(Implements = typeof(global::System.Security.Cryptography.AsymmetricAlgorithm))]
	public abstract class __AsymmetricAlgorithm : IDisposable
	{
		// used by
		// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSA.cs
		// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\X509Certificates\X509Certificate2.cs

		public abstract string ToXmlString(bool includePrivateParameters);

		// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
		public virtual int KeySize { get; set; }

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion

		public static implicit operator global::System.Security.Cryptography.AsymmetricAlgorithm(__AsymmetricAlgorithm e)
		{
			return (global::System.Security.Cryptography.AsymmetricAlgorithm)(object)e;

		}
	}
}
