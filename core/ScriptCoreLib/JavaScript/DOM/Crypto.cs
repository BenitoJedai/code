﻿using System;
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
        // http://jim.com/security/cypherpunk_program.html
        // Cryptography is hard, cryptographic protocols that actually work are harder, and embedding those protocols invisibly in utilities that do useful things without the end user needing to know or think about cryptography considerably harder still: To solve this we need higher level tools which automatically apply known sound protocols to the particular case, so that good cryptography can be a routine and invisible part of good applications, without requiring as much thought as it now does.

        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\SHA1CryptographyExtensions.cs

        // can we now start sending
        // readonly objects from server to client
        // with private data serialized and encrypted for later use?
        // X:\jsc.svn\examples\javascript\Test\TestEncryptedPrivateFields\TestEncryptedPrivateFields\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\Test\TestCryptoUIThreadIdentityKeyPair\TestCryptoUIThreadIdentityKeyPair\Application.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestRSACryptoServiceProvider\TestRSACryptoServiceProvider\ApplicationControl.cs


        public SubtleCrypto subtle;
    }
}
