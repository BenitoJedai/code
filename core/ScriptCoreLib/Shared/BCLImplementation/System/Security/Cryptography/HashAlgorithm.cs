using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
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
