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
using DropFileForMD5Experiment;
using DropFileForMD5Experiment.Design;
using DropFileForMD5Experiment.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Diagnostics;

namespace DropFileForMD5Experiment
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
            // if we were to change the dom or css here
            // it could be done preemptivly also on the server?
            // thus sending us a modified special version.

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

                        var md5 = bytes.ToMD5Bytes();
                        var md5hex = md5.ToHexString();

                        new IHTMLPre {
                            new { 
                            f.name, 
                            f.size,
                            md5hex,
                            s.ElapsedMilliseconds
                            }
                        }.AttachToDocument();

                        // { name = 1399704015818.jpg, size = 32368, md5hex = 3842c6c1385089570889b4ffca8dbf38, ElapsedMilliseconds = 32 }


                    }

                };
        }

    }
}
