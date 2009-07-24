using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Security.Cryptography;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
	[Script(Implements = typeof(global::System.Security.Cryptography.RSACryptoServiceProvider))]
	internal class __RSACryptoServiceProvider : __RSA
	{
		// we should defenetly be doing something more around here...


		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters)
				return (RSAParameters)(object)new __RSAParameters
				{
					D = InternalParameters.D,
					DP = InternalParameters.DP,
					DQ = InternalParameters.DQ,
					Exponent = InternalParameters.Exponent,
					InverseQ = InternalParameters.InverseQ,
					Modulus = InternalParameters.Modulus,
					P = InternalParameters.P,
					Q = InternalParameters.Q,
				};

			return (RSAParameters)(object)new __RSAParameters
			{
				Exponent = InternalParameters.Exponent,
				Modulus = InternalParameters.Modulus,
			};
		}

		RSAParameters InternalParameters;

		public override void ImportParameters(RSAParameters parameters)
		{
			InternalParameters = parameters;
		}
	}
}
