using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.w3.org/2012/webcrypto/WebCryptoAPI-20142503/Overview.html#dfn-Key
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/CryptoKey.idl
    // http://msdn.microsoft.com/en-us/library/ie/dn302313(v=vs.85).aspx

    [Script(HasNoPrototype = true)]
    //public class Key
    public class CryptoKey
    {
        // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

        public string type;
        public bool extractable;
        public string[] usages;
        public object algorithm;
    }
}
