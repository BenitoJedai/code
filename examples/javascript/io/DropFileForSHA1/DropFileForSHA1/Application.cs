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
using DropFileForSHA1;
using DropFileForSHA1.Design;
using DropFileForSHA1.HTML.Pages;
using System.Diagnostics;

namespace DropFileForSHA1
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
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoSHA1\TestWebCryptoSHA1\Application.cs
            // X:\jsc.svn\examples\javascript\io\DropFileForMD5Experiment\DropFileForMD5Experiment\Application.cs

            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs
            // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyImport\TestWebCryptoKeyImport\Application.cs

            #region secure origin
            new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

            if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
            {
                // https://code.google.com/p/chromium/issues/detail?id=412681

                new IHTMLAnchor
                {
                    href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
                    innerText = "open as secure origin!"
                }.AttachToDocument();

                return;
            }
            #endregion



            Native.document.documentElement.ondragover +=
                 e =>
                {
                    //Console.WriteLine("ondragover");

                    e.stopPropagation();
                    e.preventDefault();

                    e.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.


                    page.body.style.backgroundColor = "cyan";


                    // this wont work
                    //e.dataTransfer.setDragImage(
                    //    new IHTMLDiv { 
                    //        "drop here"
                    //    }, 0, 0
                    //);

                };

            Native.document.documentElement.ondrop +=
                async e =>
                {
                    // X:\jsc.svn\examples\javascript\io\WebApplicationSelectingFile\WebApplicationSelectingFile\Application.cs

                    page.body.style.backgroundColor = "";

                    Console.WriteLine("ondrop");

                    e.stopPropagation();
                    e.preventDefault();
                    FileList x = e.dataTransfer.files; // FileList object.

                    for (uint i = 0; i < x.length; i++)
                    {
                        File f = x[i];

                        var s = Stopwatch.StartNew();


                        // Error	59	The call is ambiguous between the following methods 
                        // or properties: 'ScriptCoreLib.JavaScript.DOM.FileExtensions.readAsBytes(ScriptCoreLib.JavaScript.DOM.File)' and 'ScriptCoreLib.JavaScript.DOM.FileEntryAsyncExtensions.readAsBytes(ScriptCoreLib.JavaScript.DOM.File)'	X:\jsc.svn\examples\javascript\io\DropFileForMD5Experiment\DropFileForMD5Experiment\Application.cs	81	43	DropFileForMD5Experiment

                        var bytes = await f.readAsBytes();

                        //var md5 = bytes.ToMD5Bytes();

                        var a = new { name = "SHA-1" };
                        var value = await Native.crypto.subtle.digestAsync(a, bytes);


                        var valuehex = value.ToHexString();

                        new IHTMLPre {
                            new {
                            f.name,
                            f.size,
                            valuehex,
                            s.ElapsedMilliseconds
                            }
                        }.AttachToDocument();

                        // {{ name = egyptlab.jpg, size = 556219, valuehex = cb429d00658cf1a7ce663a46c508c00cf3a63874, ElapsedMilliseconds = 50 }}
                        // { name = 1399704015818.jpg, size = 32368, md5hex = 3842c6c1385089570889b4ffca8dbf38, ElapsedMilliseconds = 32 }


                    }

                };
        }

    }
}
