using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class SubtleCrypto
    {
        // http://www.w3.org/TR/WebCryptoAPI/

        // http://www.w3.org/2012/webcrypto/wiki/images/b/bc/Webrtc.pdf

        //IPromise<any> generateKey(AlgorithmIdentifier algorithm,
        //                   boolean extractable,
        //                   KeyUsage[] keyUsages);



        // KeyUsage
        //A type of operation that may be performed using a key. The recognized key usage values are "encrypt", "decrypt", "sign", "verify", "deriveKey", "deriveBits", "wrapKey" and "unwrapKey".

        // http://www.w3.org/TR/WebCryptoAPI/#dfn-AlgorithmIdentifier
        public IPromise<object> generateKey(object algorithm,
                           bool extractable,
                           string[] keyUsages)
        {
            // how does that help us on client side data layer?
            // tested by ?
            // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx

            return null;
        }
    }
}
