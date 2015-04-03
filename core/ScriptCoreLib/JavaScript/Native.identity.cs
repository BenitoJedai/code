using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Threading.Tasks;



namespace ScriptCoreLib.JavaScript
{

    // C# 6 shall import this static type and make members available!
    public static partial class Native
    {
		// we should send idenity with the thread jumps?
		// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTab\ChromeExtensionHopToTab\Application.cs

		// X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs
		// x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs
		// X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs
		// X:\jsc.svn\examples\javascript\Test\TestCryptoUIThreadIdentityKeyPair\TestCryptoUIThreadIdentityKeyPair\Application.cs
		// X:\jsc.svn\examples\javascript\Test\TestEncryptedPrivateFields\TestEncryptedPrivateFields\ApplicationWebService.cs
		// X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs

		// can we binary encrypt and sign our data uploads?



		// http://jim.com/security/replacing_TCP.html
		// You will notice that the server only allocates memory and does heavy computation *after*
		// the client has successfully performed proof of work and shown that it is indeed capable 
		// of receiving data sent to the advertised network address.
		[Obsolete("experimental. allows us to sign/encrypt our data uploads for our session.")]
        // Error	89	A static readonly field cannot be assigned to (except in a static constructor or a variable initializer)	X:\jsc.svn\core\ScriptCoreLib\JavaScript\Native.identity.cs	57	25	ScriptCoreLib
        public static Task<KeyPair> identity { get; private set; }

        // the server can now track the client
        // by keystore identity.
        // able to sign the stacktrace?

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\Cryptography\CryptoStream.cs



        #region __initialize_identity
        static void __initialize_identity()
        {
            // X:\jsc.svn\examples\javascript\android\Test\TestAndroidCryptoKeyGenerate\TestAndroidCryptoKeyGenerate\Application.cs

            if (Native.crypto != null)
                if (Native.crypto.subtle != null)
                {
                    // do we even have crypto capability?


                    // I/chromium( 6625): [INFO:CONSOLE(1751)] "Uncaught TypeError: Cannot call method 'generateKey' of undefined", source: http://127.0.0.1:14272/view-source (1751)

                    try
                    {
                        var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });

                        // http://social.msdn.microsoft.com/Forums/en-US/d12a2c2e-22b0-44ab-bab5-8202a0c8edcc/rsa-signature-with-rsassapkcs1v15?forum=csharpgeneral
                        // Asymmetric private keys should never be stored verbatim or in plain text on the local computer. If you need to store a private key, you should use a key container. 
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140831



                        Native.identity = Native.crypto.subtle.generateKeyAsync(
                                new
                            {
                                name = "RSASSA-PKCS1-v1_5",

                                // for SSL we seem to need to use SHA1 tho?
                                hash = new { name = "SHA-256" },


                                modulusLength = 2048,
                                publicExponent,

                                //  RsaHashedKeyGenParams: hash: Algorithm: Not an object
                            },
                                false,
                            //new[] { "encrypt", "decrypt" }
                                new[] { "sign", "verify" }
                            );
                    }
                    catch
                    {
                        // no crypto?
                    }
                }






        }
        #endregion


    }




}
