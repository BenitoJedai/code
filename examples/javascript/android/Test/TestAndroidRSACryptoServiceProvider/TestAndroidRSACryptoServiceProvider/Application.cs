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
using TestAndroidRSACryptoServiceProvider;
using TestAndroidRSACryptoServiceProvider.Design;
using TestAndroidRSACryptoServiceProvider.HTML.Pages;

namespace TestAndroidRSACryptoServiceProvider
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
            // can we do the same with onclick already?
            new IHTMLButton { "encrypt" }.AttachToDocument().WhenClicked(
                async btn =>
            {
                var data = Encoding.UTF8.GetBytes("hello world");

                var enc = await this.Encrypt(data);

                // shall we show the encrypted bytes?
                // what about encrypte png/gif?
                // what about doing this in the worker thread?

                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

                foreach (var item in enc.bytes)
                {
                    new IHTMLCode { " 0x" + item.ToString("x2") }.AttachToDocument();
                }

                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();


                // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs

                var decrypt = new IHTMLButton { "decrypt" }.AttachToDocument();

                await decrypt.async.onclick;

                decrypt.Orphanize();

                var xdata = await this.Decrypt(enc);

                var xstring = Encoding.UTF8.GetString(xdata);

                new IHTMLPre { new { xstring } }.AttachToDocument();


            }
            );
        }

    }
}
