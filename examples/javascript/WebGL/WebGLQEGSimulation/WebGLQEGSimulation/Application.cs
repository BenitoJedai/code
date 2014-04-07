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
using WebGLQEGSimulation;
using WebGLQEGSimulation.Design;
using WebGLQEGSimulation.HTML.Pages;

namespace WebGLQEGSimulation
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // http://www.britishideas.com/2008/09/07/importing-dxf-files-into-inkscape/

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //  Open Office Draw can load a DXF file and save an SVG file. I’ve tried it and it worked – I was able to take a DXF file, convert to SVG, load into Inkscape, 
            // does not work with ink scape nor libre office.
            // revert to png?

            // https://docs.google.com/file/d/0B-UG88wHB6NOeVh6cHJ2eWpnWkU/edit
            // https://docs.google.com/file/d/0B-UG88wHB6NOczdMMk44aGRXRlE/edit

            //           img:nth-of-type(2) {
            //    border: 1px dashed red;

            //    margin-top: 100px;
            //    -webkit-transform: scale(0.38) rotate(22deg);
            //}

            // HTML animation for now, until we figure out how to do it in WebGL?!

            Native.document.body.style.backgroundColor = "blue";

            var s = new IStyle(Native.css[IHTMLElement.HTMLElementEnum.img][1])
            {
                border = "0px dashed blue"
            };

            // http://www.ladydragon.com/news2014/qeg.pdf
            // THE STATOR, or generator core, is made using 140 laminations of 
            // 24 gauge M19 C5 electrical steel
            //forming a stack of 3 - ½ inches, with a 4 pole configuration. 
            // Corresponding ROTOR with 2 poles. Both 
            //STATOR and ROTOR are tig welded in 4 places. 

            // http://2012portal.blogspot.com/2014/04/report-about-japan-and-taiwan.html
            // rename to CSS* experiment instead?
            Native.window.onframe +=
                e =>
                {
                    //-webkit-transform: scale(0.38) rotate(22deg);
                    // too slow?
                    s.transform = "scale(0.38) " + "rotate(" + e.counter + "deg)";
                };
        }

    }
}
