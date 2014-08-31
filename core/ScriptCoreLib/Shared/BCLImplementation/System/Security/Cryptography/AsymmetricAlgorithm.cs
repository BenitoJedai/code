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

		public abstract string ToXmlString(bool includePrivateParameters);

        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
        public virtual int KeySize { get; set; }

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
