﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.w3.org/2012/webcrypto/WebCryptoAPI-20142503/Overview.html#dfn-Key
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/CryptoKey.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/CryptoKey.cpp

    // http://msdn.microsoft.com/en-us/library/ie/dn302313(v=vs.85).aspx

    [Script(HasNoPrototype = true)]
    // which is it?

    //public class Key
    public class CryptoKey
    {
        // x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\java\security\spec\RSAPublicKeySpec.cs

        // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

        public string type;
        public bool extractable;
        public string[] usages;
        public object algorithm;
    }
}