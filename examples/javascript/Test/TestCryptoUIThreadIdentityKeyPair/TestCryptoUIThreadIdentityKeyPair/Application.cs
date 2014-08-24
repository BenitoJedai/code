using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestCryptoUIThreadIdentityKeyPair;
using TestCryptoUIThreadIdentityKeyPair.Design;
using TestCryptoUIThreadIdentityKeyPair.HTML.Pages;
using System.Diagnostics;

namespace TestCryptoUIThreadIdentityKeyPair
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\Cryptography\X509Certificates\PublicKey.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

            #region secure origin
            new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

            if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
            {
                new IHTMLAnchor
                {
                    href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
                    innerText = "open as secure origin!"
                }.AttachToDocument();

                return;
            }
            #endregion

            // jsc needs to learn SSL for dev server!


            var sw = Stopwatch.StartNew();

            new IHTMLPre { new { sw.ElapsedMilliseconds } }.AttachToDocument();

            new { }.With(
                async delegate
            {
                var key = await Native.identity;

                //new IHTMLPre {
                new IHTMLCode {


                    // can we do a shadow dom syntax highlighter/ interactive thingy?
                    new {
                                       sw.ElapsedMilliseconds,

                                       privateKey = new {
                                           key.privateKey,

                                           key.privateKey.type,
                                           key.privateKey.extractable,
                                           key.privateKey.algorithm,
                                           key.privateKey.usages
                                       },


                                       publicKey = new {
                                           key.publicKey,

                                           key.publicKey.type,
                                           key.publicKey.extractable,
                                           key.publicKey.algorithm,
                                           key.publicKey.usages
                                       }

                    } }.AttachToDocument();

                // {{ ElapsedMilliseconds = 2177, privateKey = [object CryptoKey], publicKey = [object CryptoKey], type = public, extractable = true, algorithm = [object Object], usages = verify }}

                // {{ ElapsedMilliseconds = 1931, 
                // privateKey = {{ privateKey = [object CryptoKey], type = private, extractable = false, algorithm = [object Object], usages = sign }}, 
                // publicKey = {{ publicKey = [object CryptoKey], type = public, extractable = true, algorithm = [object Object], usages = verify }} }}


                // do we know how to read the public key yet, in order to send it to server,
                // have something encrypted for us, and to get it back?

                // {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 1712 }}
                // {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 919 }}
                // {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 1755 }}
                // {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 4298 }}
                // {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 535 }}

            }
            );


        }

    }
}
