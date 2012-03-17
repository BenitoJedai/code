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
using WebGLInvade.HTML.Pages;
using WebGLInvade.Library;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Drawing;
using WebGLInvade.Library;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;

namespace WebGLInvade
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    using THREE = WebGLInvade.Library.THREE;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Source: http://www.webspaceinvader.com/2011/09/21/first-try-at-webgl/
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            new[]
            {
                new global::WebGLInvade.Library.Three().Content,
                new global::WebGLInvade.Library.ShaderExtras().Content,
                new global::WebGLInvade.Library.postprocessing.EffectComposer().Content,
                new global::WebGLInvade.Library.postprocessing.ShaderPass().Content,
                new global::WebGLInvade.Library.postprocessing.MaskPass().Content,
                new global::WebGLInvade.Library.postprocessing.RenderPass().Content,
                new global::WebGLInvade.Library.postprocessing.FilmPass().Content,
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





            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }



        void InitializeContent(IDefaultPage page = null)
        {

            var SCREEN_WIDTH = Native.Window.Width;
            var SCREEN_HEIGHT = Native.Window.Height;

            //var ,stats;


            //var mesh, zmesh, geometry;

            var mouseX = 0;
            var mouseY = 0;

            var windowHalfX = SCREEN_WIDTH / 2;
            var windowHalfY = SCREEN_HEIGHT / 2;
            var lastUpdate = new IDate().getTime();




            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

            var camera = new THREE.Camera(75, SCREEN_WIDTH / SCREEN_HEIGHT, 1, 100000);
            camera.position.z = 50;
            camera.updateMatrix();

            var scene = new THREE.Scene();

            // LIGHTS

            var ambient = new THREE.AmbientLight(0x222222);
            ambient.position.z = -300;
            scene.addLight(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(-1, 0, 1);
            directionalLight.position.normalize();
            scene.addLight(directionalLight);

            var dLight = new THREE.DirectionalLight(0xffeedd);
            dLight.position.set(1, 0, 1);
            dLight.position.normalize();
            scene.addLight(dLight);

            // init the WebGL renderer and append it to the Dom
            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            renderer.autoClear = false;

            renderer.domElement.AttachTo(container);

            #region createScene
            Action<object, f, f, f, f> createScene = (geometry, x, y, z, b) =>
            {

                var zmesh = new THREE.Mesh(geometry, new THREE.MeshFaceMaterial());
                zmesh.position.x = x;
                zmesh.position.z = y;
                zmesh.position.y = z;
                zmesh.scale.x = 5f;
                zmesh.scale.y = 5f;
                zmesh.scale.z = 5f;
                zmesh.overdraw = true;
                zmesh.updateMatrix();
                scene.addObject(zmesh);
            };
            #endregion


            var loader = new THREE.JSONLoader();

            Action<object> callbackMale = (geometry) => { createScene(geometry, 90, 50, 0, 105); };

            loader.load(
                new THREE.JSONLoaderArguments
                {
                    model = new invade().Content.src,
                    callback = IFunction.OfDelegate(callbackMale)
                }
            );

            // postprocessing

            var renderModel = new THREE.RenderPass(scene, camera);
            var effectFilm = new THREE.FilmPass(0.35, 0.50, 2048, false); //( 0.35, 0.75, 2048, false );

            effectFilm.renderToScreen = true;

            var composer = new THREE.EffectComposer(renderer);

            composer.addPass(renderModel);
            composer.addPass(effectFilm);

            #region IsDisposed
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                renderer.domElement.Orphanize();
            };
            #endregion


            #region getFrametime
            Func<long> getFrametime = () =>
            {

                var now = new IDate().getTime();
                var tdiff = (now - lastUpdate) / 1000;
                lastUpdate = now;
                return tdiff;
            };
            #endregion

            #region render
            Action render = delegate
            {
                var delta = getFrametime();
                camera.position.x += (mouseX - camera.position.x) * .05f;
                camera.position.y += (-mouseY - camera.position.y) * .05f;
                camera.updateMatrix();

                //renderer.render( scene, camera );
                renderer.clear();
                composer.render(delta);
            };
            #endregion

            #region onmousemove
            Native.Document.onmousemove +=
                e =>
                {
                    mouseX = (e.CursorX - windowHalfX);
                    mouseY = (e.CursorY - windowHalfY);
                };
            #endregion

            #region animate
            Action animate = null;

            animate = () =>
            {
                if (IsDisposed)
                    return;

                render();


                Native.Window.requestAnimationFrame += animate;
            };

            Native.Window.requestAnimationFrame += animate;
            #endregion

            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

                camera.aspect = Native.Window.Width / Native.Window.Height;
                camera.updateProjectionMatrix();

                renderer.setSize(Native.Window.Width, Native.Window.Height);
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

        public Action Dispose;

    }


}
