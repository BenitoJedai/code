using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsa.cs
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/corlib/System.Security.Cryptography/RSAParameters.cs

	[Script(Implements = typeof(global::System.Security.Cryptography.RSAParameters))]
	internal class __RSAParameters
	{
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
