using System;
using System.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLSpiral.HTML.Pages;
using WebGLSpiral.Shaders;

namespace WebGLSpiral
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using System.Xml.Linq;
    using System.Text;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService, ISurface
    {
        // 01. http://www.brainjam.ca/stackoverflow/webglspiral.html

        #region This example shall implement a Rotating Spiral
        // 02. Build this empty project to verify jsc does its thing.
        // 03. Running this project shows up as a web page
        // 04. Start looking at "view-source:http://www.brainjam.ca/stackoverflow/webglspiral.html"
        // 05. Extract fragment shader
        // 06. Save work and commit to svn.
        // 07. Convert shader code into .NET language
        // 08. Notice that float literals require suffix "f" unless we start supporting double in GLSL?
        // 09. Notice that uniforms and attributes are to be marked as .NET attributes
        // 10. Notice that not all operators may be defined ing ScriptCoreLib GLSL
        // 11. Fix ScriptCoreLib GLSL to support required shader operations
        // 12. Save all and commit.
        // 13. List javascript methods to be implemented
        // 14. Port javascript into C#
        // 15. Define WebGL type aliases
        // 16. Notice that C# anonymous types are immutable
        // 17. Notice that ScriptCoreLib defines IDate instead of Date
        // 18. Port "init" function
        // 19. Notice that we defined our shader source as string const
        // 20. Port "createProgram" function
        // 21. Port "createShader" function
        // 22. Port "onWindowResize" function
        // 23. Port "loop" function
        // 24. Save work and commit
        // 25. Clear jsc cache due to ScriptCoreLib update
        // 26. Run the project to see if there are any defects.
        // 27. Make canvas fullscreen/ fulldocument.
        // 28. Test, save, commit
        // 29. Enable PHP server in release build
        // 30. Test with Android Firefox 4
        // 31. Integrate with .frag and .vert files to generate types into AssetsLibrary
        // 32. Add AssetsLibrary pre build event
        // 33. Make sure JSC creates classes for frag and vert files
        // 34. Share SpuralSurface with android implementation
        #endregion


        #region ISurface
        public event Action onframe;

        public event Action<int, int> onresize;

        public event Action<gl> onsurface;
        #endregion

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            InitializeContent();
        }



        private void InitializeContent()
        {



            //          public bool alpha = false;
            //public bool preserveDrawingBuffer = true;

            var gl = new WebGLRenderingContext(alpha: false, preserveDrawingBuffer: true);

            var canvas = gl.canvas.AttachToDocument();

            canvas.style.SetLocation(0, 0);



            #region Dispose
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion


            var s = new SpiralSurface(this);

            this.onsurface(gl);

            #region AtResize
            Action AtResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.window.Width;
                canvas.height = Native.window.Height;

                this.onresize(Native.window.Width, Native.window.Height);
            };

            AtResize();

            Native.window.onresize += delegate
            {
                AtResize();
            };
            #endregion



            Native.window.onframe += delegate
            {
                if (IsDisposed)
                    return;

                this.onframe();


            };


            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();
                };
            #endregion

            #region newicon
            Func<string> newicon = delegate
            {
                var icon = canvas.toDataURL("image/png");

                Native.Document.getElementsByTagName("link").AsEnumerable().ToList().WithEach(
                    e =>
                    {
                        var link = (IHTMLLink)e;

                        if (link.rel == "icon")
                        {
                            if (link.type == "image/png")
                            {

                                link.href = icon;
                            }
                            else
                            {
                                link.Orphanize();
                            }
                        }
                    }
                );

                return icon;
            };
            #endregion


            Native.document.onmousemove +=
                e =>
                {
                    s.ucolor_1 = e.CursorX / Native.window.Width;
                    s.ucolor_2 = e.CursorY / Native.window.Height;
                };

            Action speed_AtResize = delegate
            {
                //s.speed = Native.Screen.width - Native.Screen.width;
            };

            Native.window.onresize +=
                delegate
                {
                    speed_AtResize();
                };

            speed_AtResize();

            Native.document.body.onclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    newicon();
                };

            @"Spiral".ToDocumentTitle();

            new IHTMLAnchor { "drag me" }.AttachToDocument().With(
                dragme =>
                {
                    dragme.style.position = IStyle.PositionEnum.@fixed;
                    dragme.style.left = "1em";
                    dragme.style.bottom = "1em";

                    dragme.AllowToDragAsApplicationPackage();
                }
            );
        }

        public Action Dispose;
    }


}
