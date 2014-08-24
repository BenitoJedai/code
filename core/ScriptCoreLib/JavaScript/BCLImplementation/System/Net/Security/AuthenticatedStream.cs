using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net.Security
{
    // http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/AuthenticatedStream.cs
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/System/System.Net.Security/AuthenticatedStream.cs

    [Script(Implements = typeof(global::System.Net.Security.AuthenticatedStream))]
    internal abstract class __AuthenticatedStream : __Stream
    {
        // used by
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\Security\SslStream.cs


        public abstract bool IsMutuallyAuthenticated { get; set; }
    }
}
