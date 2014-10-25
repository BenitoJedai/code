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
using ConvertBlackToAlpha;
using ConvertBlackToAlpha.Design;
using ConvertBlackToAlpha.HTML.Pages;

namespace ConvertBlackToAlpha
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
            // i think jsc should tell, to swap the hdd out and refresh the current installation

            // X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs
            // X:\jsc.svn\examples\javascript\synergy\webgl\WebGLEarthByBjorn\WebGLEarthByBjorn\Application.cs
            // x:\jsc.svn\examples\javascript\async\colordisco\colordisco\application.cs


            Native.body.style.backgroundColor = "red";

            //new IHTMLCanvas(
            //new HTML.Images.FromAssets.galaxy_starfield().bytes

            // how can we create a new canvas and write old pixels?
            new { }.With(
                async scope =>
                {
                    var i = new HTML.Images.FromAssets.galaxy_starfield();

                    var bytes = await i.async.bytes;

                    for (int ii = 0; ii < bytes.Length; ii += 4)
                    {
                  
                        bytes[ii + 3] = (byte)(bytes[ii + 0]);

                        bytes[ii + 0] = 0xff;
                        bytes[ii + 1] = 0xff;
                        bytes[ii + 2] = 0xff;
                    }


                    new IHTMLPre { bytes[0].ToString("x2") }.AttachToDocument();
                    new IHTMLPre { bytes[1].ToString("x2") }.AttachToDocument();
                    new IHTMLPre { bytes[2].ToString("x2") }.AttachToDocument();

                    // alpha.
                    new IHTMLPre { bytes[3].ToString("x2") }.AttachToDocument();

                    var c = new CanvasRenderingContext2D(i.width, i.height);

                    c.bytes = bytes;

                    c.canvas.AttachToDocument();


                }
            );


        }

    }
}
