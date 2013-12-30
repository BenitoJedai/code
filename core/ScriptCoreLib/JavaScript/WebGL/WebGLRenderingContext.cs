using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    [Script(HasNoPrototype = true, InternalConstructor = true)]
    public class WebGLRenderingContext
    {
        // see also
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\CanvasRenderingContext2D.cs

        public IHTMLCanvas canvas;

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
    }
}
