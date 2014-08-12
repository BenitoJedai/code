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
            // http://webglsamples.googlecode.com/hg/google-io/2011/100-objects.html

            // https://twitter.com/schteppe/status/493346982279532545

            // http://floooh.github.io/oryol/DrawCallPerf.html
            // https://www.mail-archive.com/emscripten-discuss@googlegroups.com/msg01454.html
            // https://code.google.com/p/dart/issues/detail?id=11357
            // https://bugzilla.mozilla.org/show_bug.cgi?id=843673

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
            //var ANGLEInstancedArrays = gl.getExtension("ANGLE_instanced_arrays") as ANGLE_instanced_arrays;

            // when will jsc pass generic arguments along?
            //var ANGLEInstancedArrays = (ANGLE_instanced_arrays)gl.getExtension<ANGLE_instanced_arrays>();

            var ANGLEInstancedArrays = (ANGLE_instanced_arrays)gl.getExtension("ANGLE_instanced_arrays");
            // 0:63ms {{ ANGLEInstancedArrays = [object ANGLEInstancedArrays] }} 

            // any reason to provide extension methods as gl.drawArraysInstanced ?
            // X:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\Extensions\WebGLExtensions.cs

            //ANGLEInstancedArrays.drawArraysInstancedANGLE(

            //gl.drawArraysInstanced(

            Console.WriteLine(new { ANGLEInstancedArrays });
            // 0:270ms {{ ext = [object ANGLEInstancedArrays] }} 
            // 


            // THREE.JS supports it? http://stackoverflow.com/questions/23172609/does-three-js-support-angle-instanced-arrays
            // BufferedGeometry perhaps?

            // https://github.com/mrdoob/three.js/issues/975
            // can we have starling like 2d svg instancing now for webgl?
            // if we wanted to run physic on the background thearead,

            // would the jsc interfaces for webgl have enough knowledge to proxy via WorkerAPI?

            // http://www.browserleaks.com/webgl
            // https://github.com/kripken/emscripten/issues/2510

            // http://nullprogram.com/blog/2014/06/01/

        }

    }
}
