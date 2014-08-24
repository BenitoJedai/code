using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/SubtleCrypto.idl

    [Script(HasNoPrototype = true)]
    public class SubtleCrypto
    {
        // http://unmitigatedrisk.com/?p=470
        // http://tonyarcieri.com/imperfect-forward-secrecy-the-coming-cryptocalypse
        // if we were on android chrome.
        // could we make use of NFC TPM ?


        //In that regard, the normative parts of the specification are totally fine. While the spec doesn’t cover it, the APIs seem sufficiently abstract to allow them to easily map onto future encryption algorithms and trusted platform modules (TPMs) which could provide secure storage for encryption keys.
        // http://tonyarcieri.com/whats-wrong-with-webcrypto


        // http://www.w3.org/TR/WebCryptoAPI/

        // http://www.w3.org/2012/webcrypto/wiki/images/b/bc/Webrtc.pdf

        //IPromise<any> generateKey(AlgorithmIdentifier algorithm,
        //                   boolean extractable,
        //                   KeyUsage[] keyUsages);



        // KeyUsage
        //A type of operation that may be performed using a key. The recognized key usage values are "encrypt", "decrypt", "sign", "verify", "deriveKey", "deriveBits", "wrapKey" and "unwrapKey".

        // http://www.w3.org/TR/WebCryptoAPI/#dfn-AlgorithmIdentifier
        public IPromise<KeyPair> generateKey(
            object algorithm,
            bool extractable,
            string[] keyUsages)
        {
            // how does that help us on client side data layer?
            // tested by ?
            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

            // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx

            return null;
        }
    }

    [Script]
    public static class SubtleCryptoExtensions
    {
        // where to put the async definitions?
        // keep the original callback/Promise api also visible?

        // tested by
        // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs

        public static Task<KeyPair> generateKeyAsync(
            this SubtleCrypto that,

            object algorithm,
            bool extractable,
            string[] keyUsages
        )
        {
            var x = new TaskCompletionSource<KeyPair>();

            // Error	1	Keyword 'this' is not valid in a static property, static method, or static field initializer	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\SubtleCrypto.cs	59	13	ScriptCoreLib
            //this.generateKey();

            var promise = that.generateKey(algorithm, extractable, keyUsages);

            // we are taking a delegate of a BCL function, and then converting it to IFunction! nice.
            promise.then(x.SetResult);

            return x.Task;
        }
    }

}
