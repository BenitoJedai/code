using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using WebGLDynamicTerrainTemplate.HTML.Pages;
using WebGLDynamicTerrainTemplate.Design;
using THREE = WebGLDynamicTerrainTemplate.Design.THREE;

namespace WebGLDynamicTerrainTemplate
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // inspired by http://alteredqualia.com/three/examples/webgl_terrain_dynamic.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            #region await Three.js then do InitializeContent
            new[]
            {
                new global::WebGLDynamicTerrainTemplate.Design.ThreeTerrain().Content,
                new global::WebGLDynamicTerrainTemplate.Design.ShaderTerrain().Content,
                new global::WebGLDynamicTerrainTemplate.Design.ShaderExtrasTerrain().Content,
                new global::WebGLDynamicTerrainTemplate.Design.PostprocessingTerrain().Content,
            }.ForEach(
                (SourceScriptElement, i, MoveNext) =>
                {
                    SourceScriptElement.AttachToDocument().onload +=
                        delegate
                        {
                            MoveNext();
                        };
                }
            )(
                delegate
                {
                    InitializeContent(page);
                }
            );
            #endregion


            style.Content.AttachToHead();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page = null)
        {
            #region make sure we atleast have our invisible DOM
            if (page == null)
                page = new HTML.Pages.DefaultPage();
            #endregion

            #region container
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#003366";
            container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            #endregion
           






            #region IsDisposed

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                //page.song.pause();
                page.song2.pause();

                container.Orphanize();
            };
            #endregion

            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

                //camera.aspect = Native.Window.Width / Native.Window.Height;
                //camera.updateProjectionMatrix();

                //renderer.setSize(Native.Window.Width, Native.Window.Height);
            };

            Native.Window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();
            #endregion

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


        }

        bool IsDisposed = false;

        public Action Dispose;

    }
}
