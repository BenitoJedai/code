using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.w3.org/2012/webcrypto/WebCryptoAPI-20142503/Overview.html#dfn-KeyPair
    [Script(HasNoPrototype = true)]
    public class KeyPair
    {
        // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

          public CryptoKey publicKey;
          public CryptoKey privateKey;
    }
}
