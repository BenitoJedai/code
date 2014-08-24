using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsa.cs
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Security.Cryptography/RSAParameters.cs

	[Script(Implements = typeof(global::System.Security.Cryptography.RSAParameters))]
	public class __RSAParameters
	{
        // X:\jsc.svn\examples\javascript\forms\Test\TestRSACryptoServiceProvider\TestRSACryptoServiceProvider\ApplicationControl.cs

		public byte[] D;
		public byte[] DP;
		public byte[] DQ;
		public byte[] Exponent;
		public byte[] InverseQ;
		public byte[] Modulus;
		public byte[] P;
		public byte[] Q;






	}
}
