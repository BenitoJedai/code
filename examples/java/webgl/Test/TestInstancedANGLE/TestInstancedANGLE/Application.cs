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
using TestInstancedANGLE;
using TestInstancedANGLE.Design;
using TestInstancedANGLE.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;

namespace TestInstancedANGLE
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
            // http://webglstats.com/
            // for heat zeeker, can we use webgl, instanced geometry, collada databound to LAN synced data layer?
            // http://msdn.microsoft.com/en-us/library/ie/dn725046%28v=vs.85%29.aspx
            // would this mean we would have a nice isometric world?
            // http://www.khronos.org/registry/webgl/extensions/ANGLE_instanced_arrays/
            // ace of spades webgl lan?

            // https://code.google.com/p/chromium/issues/detail?id=288391
            // http://blog.tojicode.com/2013/07/webgl-instancing-with.html
            // http://media.tojicode.com/webgl-samples/instancing.html

            //new WebGLRenderingContext

            // Uncaught TypeError: undefined is not a function 
            var gl = new WebGLRenderingContext().AttachTo(Native.shadow);

            // var ext = gl.getExtension("ANGLE_instanced_arrays"); // Vendor prefixes may apply!
            var ext = gl.getExtension("ANGLE_instanced_arrays");

            Console.WriteLine(new { ext });



            // THREE.JS supports it? http://stackoverflow.com/questions/23172609/does-three-js-support-angle-instanced-arrays
            // https://github.com/mrdoob/three.js/issues/975


        }

    }
}
