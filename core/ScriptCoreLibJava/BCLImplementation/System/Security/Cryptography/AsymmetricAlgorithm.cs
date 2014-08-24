using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/asymmetricalgorithm.cs

	[Script(Implements = typeof(global::System.Security.Cryptography.AsymmetricAlgorithm))]
	internal abstract class __AsymmetricAlgorithm : IDisposable
	{
		public abstract string ToXmlString(bool includePrivateParameters);



		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
