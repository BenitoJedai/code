using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLCelShader.HTML.Pages;

namespace WebGLCelShader
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.Shared.Drawing;
    using WebGLCelShader.Shaders;
    using WebGLCelShader.Library;
    using System.Collections.Generic;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        /* Source: http://www.ro.me/tech/demos/6/index.html
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            #region ThreeExtras
            new ThreeExtras().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           new Target().Content.With(
                                source2 =>
                                {
                                }
                            ).AttachToDocument();
                       };

               }
           ).AttachToDocument();
            #endregion


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page)
        {
            page.PageContainer.style.color = Color.Blue;

            var size = 500;

            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;
            #endregion

            #region gl - Initialise WebGL


            var gl = default(WebGLRenderingContext);

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                Native.Window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");
            }
            #endregion



            var gl_viewportWidth = size;
            var gl_viewportHeight = size;


            var h = 1f;
            var r1 = .5f;
            var r2 = .2f;
            var nPhi = 500;


            var prog = gl.createProgram();


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));

                    return null;
                }

                return shader;
            };
            #endregion

            var vs = createShader(new GeometryVertexShader());
            var fs = createShader(new GeometryFragmentShader());

            if (vs == null || fs == null) throw new InvalidOperationException("shader failed");

            gl.attachShader(prog, vs);
            gl.attachShader(prog, fs);


            gl.linkProgram(prog);
            gl.useProgram(prog);






            var c = 0;




            #region tick - new in lesson 03
            var tick = default(Action);

            tick = delegate
            {
                c++;



                Native.Document.title = "" + c;

             

                Native.Window.requestAnimationFrame += tick;
            };

            tick();
            #endregion
        }

    }


}
