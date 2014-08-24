using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/Crypto.idl
    // http://msdn.microsoft.com/en-us/library/ie/dn280995(v=vs.85).aspx

    [Script(HasNoPrototype = true)]
    public class Crypto
    {
        // can we now start sending
        // readonly objects from server to client
        // with private data serialized and encrypted for later use?
        // X:\jsc.svn\examples\javascript\Test\TestEncryptedPrivateFields\TestEncryptedPrivateFields\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\Test\TestCryptoUIThreadIdentityKeyPair\TestCryptoUIThreadIdentityKeyPair\Application.cs


        public SubtleCrypto subtle;
    }
}
