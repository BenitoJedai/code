using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Security.Cryptography;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsacryptoserviceprovider.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security.Cryptography/RSACryptoServiceProvider.cs
    // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx

    [Script(Implements = typeof(global::System.Security.Cryptography.RSACryptoServiceProvider))]
    internal class __RSACryptoServiceProvider : __RSA
    {
        // we should defenetly be doing something more around here...


        public override RSAParameters ExportParameters(bool includePrivateParameters)
        {
            // used by?

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
