using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/hashalgorithm.cs
	[Script(Implements = typeof(global::System.Security.Cryptography.HashAlgorithm))]
	public abstract class __HashAlgorithm
	{
		public abstract byte[] InternalComputeHash(byte[] buffer);

		public byte[] ComputeHash(byte[] buffer)
		{
			return InternalComputeHash(buffer);
		}
	}
}
