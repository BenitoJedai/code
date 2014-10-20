using ScriptCoreLib.JavaScript.WebCrypto;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/SubtleCrypto.idl
    // https://code.google.com/p/chromium/issues/detail?id=412681

    [Script(HasNoPrototype = true)]
    public class SubtleCrypto
    {
        // http://research.microsoft.com/en-us/downloads/29f9385d-da4c-479a-b2ea-2a7bb335d727/default.aspx

        // https://bugzilla.mozilla.org/show_bug.cgi?id=1020598


        // !!! So we have to lower the cost of encrypted authenticated communications, 
        // so that people can simply encrypt and authenticate everything without needing to think about it. 
        // http://jim.com/security/replacing_TCP.html

        // Problems such as password-authenticated key agreement transaction to a banking site require something that resembles encrypted SCTP, analogous to the way that TLS is encrypted TCP, but nothing like that exists as yet. 

        // Trust is an application level issue, not a communication layer issue, but neither do we want each application to roll its own trust 
        // cryptography – which at present web servers are forced to do.

        // https://docs.google.com/document/d/184AgXzLAoUjQjrtNdbimceyXVYzrn3tGpf3xQGCN10g/edit
        // https://code.google.com/p/chromium/issues/detail?id=245025
        // https://code.google.com/p/chromium/issues/detail?id=379976&q=WebCrypto&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified

        // http://jim.com/security/generic_client_server_program.html

        // http://unmitigatedrisk.com/?p=470
        // http://tonyarcieri.com/imperfect-forward-secrecy-the-coming-cryptocalypse
        // if we were on android chrome.
        // could we make use of NFC TPM ?


        //In that regard, the normative parts of the specification are totally fine. While the spec doesn’t cover it, the APIs seem sufficiently abstract to allow them to easily map onto future encryption algorithms and trusted platform modules (TPMs) which could provide secure storage for encryption keys.
        // http://tonyarcieri.com/whats-wrong-with-webcrypto
        // http://www.w3.org/TR/WebCryptoAPI/
        // http://www.w3.org/2012/webcrypto/wiki/images/b/bc/Webrtc.pdf


        // [CallWith=ScriptState] Promise importKey(KeyFormat format, ArrayBuffer keyData, Dictionary algorithm, boolean extractable, KeyUsage[] keyUsages);
        // https://code.google.com/p/chromium/issues/detail?id=389314
        // http://src.chromium.org/viewvc/chrome/trunk/src/content/child/webcrypto/openssl/rsa_key_openssl.cc?revision=286409&pathrev=286409

        public IPromise<CryptoKey> importKey(string format,
            object keyData,
            object algorithm,
            bool extractable,
            string[] keyUsages

            )
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyImport\TestWebCryptoKeyImport\Application.cs
            return null;
        }

        //Promise<any> exportKey(KeyFormat format, CryptoKey key)
        public IPromise<object> exportKey(string format, CryptoKey key)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs

            return null;
        }


        // Promise encrypt(Dictionary algorithm, CryptoKey key, ArrayBuffer data);
        //public IPromise<byte[]> encrypt(object algorithm, CryptoKey key, byte[] data)
        public IPromise<ArrayBuffer> encrypt(object algorithm, CryptoKey key, byte[] data)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs

            return null;
        }


        public IPromise<ArrayBuffer> decrypt(object algorithm, CryptoKey key, byte[] data)
        //public Task<byte[]> decrypt(object algorithm, CryptoKey key, byte[] data)
        {
            // as jsc is transforming Delegate to IFunction
            // could we do the same for Promise to Task on return?

            return null;
        }


        //IPromise<any> generateKey(AlgorithmIdentifier algorithm,
        //                   boolean extractable,
        //                   KeyUsage[] keyUsages);



        // KeyUsage
        //A type of operation that may be performed using a key. The recognized key usage values are "encrypt", "decrypt", "sign", "verify", "deriveKey", "deriveBits", "wrapKey" and "unwrapKey".

        // http://www.w3.org/TR/WebCryptoAPI/#dfn-AlgorithmIdentifier
        public IPromise<KeyPair> generateKey(
            object algorithm,
            bool extractable,
            string[] keyUsages
            )
        {
            // how does that help us on client side data layer?
            // tested by ?
            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

            // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx

            return null;
        }

        // http://msdn.microsoft.com/en-us/library/ie/dn302328(v=vs.85).aspx
        //[CallWith=ScriptState] Promise digest(Dictionary algorithm, ArrayBuffer data);
        public IPromise<ArrayBuffer> digest(object algorithm, byte[] data)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoSHA1\TestWebCryptoSHA1\Application.cs

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

        //[Obsolete("workaround until jsc implicitly turns Promis into Task for return values.")]
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

        // what an ugly name. keep it?
        public static Task<JsonWebKey> exportJSONWebKeyAsync(
        this SubtleCrypto that,

        CryptoKey key
    )
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs

            var x = new TaskCompletionSource<JsonWebKey>();
            var promise = that.exportKey("jwk", key);

            promise.then(
                z => { x.SetResult((JsonWebKey)z); }
            );

            return x.Task;
        }


        // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs
        public static Task<byte[]> encryptAsync(
                this SubtleCrypto that,

                object algorithm, CryptoKey key, byte[] data
            )
        {
            var x = new TaskCompletionSource<byte[]>();
            var promise = that.encrypt(algorithm, key, data);

            promise.then(
                z => { x.SetResult(z); }
            );

            return x.Task;
        }

        public static Task<byte[]> decryptAsync(
        this SubtleCrypto that,

        object algorithm, CryptoKey key, byte[] data
    )
        {
            Console.WriteLine(
             "enter decryptAsync"
             );

            var x = new TaskCompletionSource<byte[]>();
            var promise = that.decrypt(algorithm, key, data);

            // android webview wont like .catch
            promise.@catch(
                err =>
                {// X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs

                    Console.WriteLine(
                        "decryptAsync " + new { err }
                        );
                }
            );


            promise.then(
                z => { x.SetResult(z); }
            );

            return x.Task;
        }


        public static Task<byte[]> digestAsync(
        this SubtleCrypto that,

        object algorithm,
        byte[] data
    )
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoSHA1\TestWebCryptoSHA1\Application.cs

            var x = new TaskCompletionSource<byte[]>();
            var promise = that.digest(algorithm, data);

            promise.then(
                z => { x.SetResult(z); }
            );

            return x.Task;
        }
    }

}
