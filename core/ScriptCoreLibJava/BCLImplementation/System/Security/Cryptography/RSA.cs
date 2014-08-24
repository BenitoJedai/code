using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Security.Cryptography;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsa.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/corlib/System.Security.Cryptography/RSA.cs


	[Script(Implements = typeof(global::System.Security.Cryptography.RSA))]
	internal abstract class __RSA : __AsymmetricAlgorithm
	{
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\Cryptography\RSA.cs

		public abstract void ImportParameters(RSAParameters parameters);

		public abstract RSAParameters ExportParameters(bool includePrivateParameters);
 

 


		public override string ToXmlString(bool includePrivateParameters)
		{
			RSAParameters parameters = this.ExportParameters(includePrivateParameters);
			StringBuilder builder = new StringBuilder();
			builder.Append("<RSAKeyValue>");
			builder.Append("<Modulus>" + Convert.ToBase64String(parameters.Modulus) + "</Modulus>");
			builder.Append("<Exponent>" + Convert.ToBase64String(parameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				builder.Append("<P>" + Convert.ToBase64String(parameters.P) + "</P>");
				builder.Append("<Q>" + Convert.ToBase64String(parameters.Q) + "</Q>");
				builder.Append("<DP>" + Convert.ToBase64String(parameters.DP) + "</DP>");
				builder.Append("<DQ>" + Convert.ToBase64String(parameters.DQ) + "</DQ>");
				builder.Append("<InverseQ>" + Convert.ToBase64String(parameters.InverseQ) + "</InverseQ>");
				builder.Append("<D>" + Convert.ToBase64String(parameters.D) + "</D>");
			}
			builder.Append("</RSAKeyValue>");
			return builder.ToString();
		}


	}
}
