using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using WebGLInvade.HTML.Pages;
using WebGLInvade.Library;

namespace WebGLInvade
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    //using THREE = WebGLInvade.Library.THREE;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /* Source: http://www.webspaceinvader.com/2011/09/21/first-try-at-webgl/
         */


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            var SCREEN_WIDTH = Native.window.Width;
            var SCREEN_HEIGHT = Native.window.Height;

            //var ,stats;


            //var mesh, zmesh, geometry;

            var mouseX = 0;
            var mouseY = 0;

            var windowHalfX = SCREEN_WIDTH / 2;
            var windowHalfY = SCREEN_HEIGHT / 2;
            var lastUpdate = new IDate().getTime();




            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

            var camera = new THREE.PerspectiveCamera(75, Native.window.aspect, 1, 100000);
            camera.position.z = 50;
            camera.updateMatrix();

            var scene = new THREE.Scene();

            // LIGHTS

            var ambient = new THREE.AmbientLight(0x222222);
            ambient.position.z = -300;
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(-1, 0, 1);
            directionalLight.position.normalize();
            scene.add(directionalLight);

            var dLight = new THREE.DirectionalLight(0xffeedd);
            dLight.position.set(1, 0, 1);
            dLight.position.normalize();
            scene.add(dLight);

            // init the WebGL renderer and append it to the Dom
            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            renderer.autoClear = false;

            renderer.domElement.AttachTo(container);

            #region createScene
            Action<object, f, f, f, f> createScene = (geometry, x, y, z, b) =>
            {
                
                //var zmesh = new THREE.Mesh(geometry, new THREE.MeshFaceMaterial());
                var zmesh = new THREE.Mesh(geometry);
                zmesh.position.x = x;
                zmesh.position.z = y;
                zmesh.position.y = z;
                zmesh.scale.x = 5f;
                zmesh.scale.y = 5f;
                zmesh.scale.z = 5f;
                //zmesh.overdraw = true;
                zmesh.updateMatrix();
                scene.add(zmesh);
            };
            #endregion

            // message: "Unexpected token /"
            var loader = new THREE.JSONLoader();

            Action<object> callbackMale = (geometry) => { createScene(geometry, 90, 50, 0, 105); };

            loader.load(
                    url: new invade().Content.src,
                    callback: IFunction.OfDelegate(callbackMale)
            );

            // postprocessing



            var composer = new THREE.EffectComposer(renderer);

            var renderModel = new THREE.RenderPass(scene, camera);
            composer.addPass(renderModel);


            var effectFilm = new THREE.FilmPass(0.35, 0.50, 2048, false); //( 0.35, 0.75, 2048, false );
            effectFilm.renderToScreen = true;
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
            Native.document.onmousemove +=
                e =>
                {
                    mouseX = (e.CursorX - windowHalfX);
                    mouseY = (e.CursorY - windowHalfY);
                };
            #endregion

            Native.window.onframe +=
            delegate
            {
                if (IsDisposed)
                    return;

                render();

            };


            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

                camera.aspect = Native.window.aspect;
                camera.updateProjectionMatrix();

                renderer.setSize(Native.window.Width, Native.window.Height);
            };

            Native.window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();
            #endregion

            #region requestFullscreen
            Native.document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.document.body.requestFullscreen();


                };
            #endregion

        }

        public Action Dispose;

    }


}
