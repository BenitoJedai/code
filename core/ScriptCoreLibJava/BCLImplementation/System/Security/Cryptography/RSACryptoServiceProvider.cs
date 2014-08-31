using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Security.Cryptography;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsacryptoserviceprovider.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security.Cryptography/RSACryptoServiceProvider.cs
    // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx

    [Script(Implements = typeof(global::System.Security.Cryptography.RSACryptoServiceProvider))]
    internal class __RSACryptoServiceProvider : __RSA
    {
        // http://www.windows-tech.info/13/edd925e63a09c709.php

        // If anyone out there is planning on using the .Net implementation of RSA for cross application/platform development for the 
        // exchange of RSA encrypted data, I would urge you don't unless you have solid knowledge of, or the capacity (mental, time, financial) 
        // for many hours of studying information on topics including: The RSA algorithm itself, RSACryptoServiceProvider and it's limitations,
        // RSAParameter, byte arrays (manipulations and conversions), big-endian encoding, UTF-8 encoding, big integer classes, OAEP, PKCS1v15
        // padding and signature schemes, to name a few.






        // x:\jsc.svn\examples\javascript\forms\test\testrsacryptoserviceprovider\testrsacryptoserviceprovider\applicationcontrol.cs

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
