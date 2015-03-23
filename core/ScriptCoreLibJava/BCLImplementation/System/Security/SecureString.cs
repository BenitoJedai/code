using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Security.Cryptography;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Security
{
    // https://msdn.microsoft.com/en-us/library/system.security.securestring%28v=vs.110%29.aspx
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Security/securestring.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security/SecureString.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\SecureString.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\SecureString.cs

    // FULL_AOT_RUNTIME
    // FEATURE_CORECLR
    [Script(Implements = typeof(global::System.Security.SecureString))]
    internal class __SecureString
    {
        // ah the fight against debuggers and memory snapshots
        // haha. SystemFunction041
        // could jsc encrypt all async state machines?

        // http://stackoverflow.com/questions/22645382/net-securestring-in-java
        // http://docs.oracle.com/cd/E23943_01/apirefs.1111/e24834/org/identityconnectors/common/security/GuardedString.html

        // do we need encrypted domain memory?

    }
}
