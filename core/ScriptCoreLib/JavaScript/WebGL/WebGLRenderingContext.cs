using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/WebGLRenderingContext.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/canvas/nsIDOMWebGLRenderingContext.idl

    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLRenderingContextBase.cpp
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLRenderingContextBase.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLRenderingContext.idl

    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/WebGLRenderingContext/

    [Script(HasNoPrototype = true, InternalConstructor = true,

        // to support as operator for 
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\Extensions\INodeConvertible.cs

        // E/Web Console(  485): Uncaught ReferenceError: WebGLRenderingContext is not defined at http://192.168.43.1:22126/view-source:22331
        //  c = ( function () { var c$26 = b; return (c$26 instanceof WebGLRenderingContext ? c$26 : null); } )();
        // var __this = this;
        // X:\jsc.svn\examples\javascript\Test\TestMissingNativeIsInstance\TestMissingNativeIsInstance\Application.cs

        ExternalTarget = "WebGLRenderingContext")]
    public class WebGLRenderingContext : INodeConvertible<IHTMLCanvas>
    // : WebGLRenderingContextBase
    {
        // http://webglstats.com/

        // cpu, gpu, hpu
        // http://en.wikipedia.org/wiki/Holographic_processing_unit

        // http://www.tamats.com/blog/?p=604#more-604
        // http://www.tamats.com/blog/?p=488


        // http://support.apple.com/en-us/HT4623
        // http://toucharcade.com/2014/09/18/ios-8-webgl-demo-quake-iii/
        // ipad2 should support webgl too!
        // http://blog.tojicode.com/2014/07/bringing-vr-to-chrome.html#comment-form


        // 20141228
        // cool projects:
        // http://lo-th.github.io/3d.city/index.html
        // http://webgl.nu/

        // "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\GLContext.cpp"

        // when can we do WebGL from a worker?
        // https://www.khronos.org/webgl/public-mailing-list/archives/1306/msg00050.html
        // http://stackoverflow.com/questions/7844886/using-webgl-from-inside-a-web-worker-is-it-possible-how
        // https://bugzilla.mozilla.org/show_bug.cgi?id=709490
        // http://kripken.github.io/webgl-worker/webGLClient.js
        // http://wiki.whatwg.org/wiki/WorkerCanvas
        // https://code.google.com/p/chromium/issues/detail?id=245884
        // http://wiki.whatwg.org/wiki/CanvasInWorkers
        // http://philogb.github.io/blog/2012/11/04/web-workers-extension/
        // http://philogb.github.io/blog/2013/12/04/dotjs/


        // see also
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\CanvasRenderingContext2D.cs

        // HTML assets compile to custom elements. for them we have a special interface
        // to support AttachTo. lets have it here too.
        public IHTMLCanvas canvas;

        [Script(DefineAsStatic = true)]
        IHTMLCanvas INodeConvertible<IHTMLCanvas>.InternalAsNode()
        {
            // cannot call this yet via interface invoke! would jsc need to inline such methods into caller?

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\Extensions\INodeConvertible.cs


            // tested by
            // X:\jsc.svn\examples\java\webgl\Test\TestInstancedANGLE\TestInstancedANGLE\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130720
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140812
            return this.canvas;
        }

        // dictionary?
        [Script]
        sealed class __preserveDrawingBuffer
        {
            // tested by X:\jsc.svn\examples\javascript\WebGLSpiral\WebGLSpiral\Application.cs

            public bool alpha;
            public bool preserveDrawingBuffer;
            public bool antialias;

        }


        #region Constructor

        public WebGLRenderingContext(
            )
        {
            // InternalConstructor
        }

        static WebGLRenderingContext InternalConstructor(

            )
        {
            // tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs

            var canvas = new IHTMLCanvas();
            var context = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            return context;
        }

        #endregion

        #region Constructor

        public WebGLRenderingContext(
            bool alpha = false,
            bool preserveDrawingBuffer = false,
            bool antialias = false
            )
        {
            // InternalConstructor
        }

        static WebGLRenderingContext InternalConstructor(

             bool alpha,
            bool preserveDrawingBuffer,
            bool antialias = false


            )
        {
            // tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGAnonymous\WebGLSVGAnonymous\Application.cs

            var canvas = new IHTMLCanvas();
            var context = (WebGLRenderingContext)canvas.getContext("experimental-webgl",

                new __preserveDrawingBuffer
                {
                    alpha = alpha,
                    preserveDrawingBuffer = preserveDrawingBuffer,
                    antialias = antialias

                }
                );

            return context;
        }

        #endregion


        public const uint FRAGMENT_SHADER = 0x8B30;
        public const uint VERTEX_SHADER = 0x8B31;

        public WebGLProgram createProgram()
        {
            return default(WebGLProgram);
        }

        public void attachShader(WebGLProgram p, WebGLShader s)
        {
        }

        public void deleteShader(WebGLShader s)
        {
        }

        public void compileShader(WebGLShader s)
        {
        }

        public void shaderSource(WebGLShader s, string e)
        {
        }

        public WebGLShader createShader(uint s)
        {
            return default(WebGLShader);
        }

        public void bufferData(uint target, ArrayBufferView data, uint usage)
        {

        }

        public WebGLUniformLocation getUniformLocation(WebGLProgram p, string name)
        {
            return null;
        }


        public void uniform1f(WebGLUniformLocation location, float x)
        {
        }

        public void uniform2f(WebGLUniformLocation location, float x, float y)
        {
        }

        public void uniform3f(WebGLUniformLocation location, float x, float y, float z)
        {
        }


        //[Script(DefineAsStatic = true)]
        //IHTMLCanvas INodeConvertible<IHTMLCanvas>.InternalAsNode()
        //{
        //    // see also X:\jsc.svn\core\ScriptCoreLib\JavaScript\Extensions\INodeConvertible.cs
        //    throw new NotImplementedException();
        //}

        public object getExtension(string name)
        {
            // X:\jsc.svn\examples\javascript\WorkerMD5Experiment\WorkerMD5Experiment\Application.cs

            //gl.getExtension("WEBGL_debug_renderer_info");
            return null;
        }

        public object getParameter(uint pname)
        {
            return null;
        }
    }
}
