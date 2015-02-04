using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Security.Cryptography.X509Certificates
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsa.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Security/Cryptography/rsa.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security.Cryptography/RSA.cs


    // FULL_AOT_RUNTIME
    // FEATURE_CORECLR
    [Script(Implements = typeof(global::System.Security.Cryptography.RSA))]
    internal class __RSA : __AsymmetricAlgorithm
    {
        // https://en.wikipedia.org/wiki/RSA_(cryptosystem)


        // transpose plain bytes against brutefore attacks!

        // https://www.youtube.com/watch?v=QyIkzr22bD8#t=665

        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSA.cs

        // X:\jsc.svn\examples\java\hybrid\JVMCLRRSA\JVMCLRRSA\Program.cs


        // tested by ?

        public override string ToXmlString(bool includePrivateParameters)
        {
            throw new NotImplementedException();
        }
    }
}
