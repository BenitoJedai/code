using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLFireballExplosion.Design;
using WebGLFireballExplosion.HTML.Pages;

namespace WebGLFireballExplosion
{

    sealed class material_uniforms
    {
        public uniforms_item
            tExplosion,
            time,
            weight
            ;


    }

    sealed class bkg_uniforms
    {
        public uniforms_item
            tDiffuse,
            resolution
            ;


    }


    sealed class uniforms_item
    {
        public object texture;
        public object value;
        public string type;
    }


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // http://www.clicktorelease.com/code/perlin/explosion.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {

            #region await Three.js then do InitializeContent
            new[]
            {
                new global::WebGLFireballExplosion.Design.Three().Content,
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

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page = null)
        {



            //var container, renderer,  camera, mesh;
            var start = IDate.Now;
            var fov = 30;


            #region container
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#000000";
            container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            #endregion


            var scene = new THREE.Scene();
            var bkgScene = new THREE.Scene();

            var camera = new THREE.PerspectiveCamera(fov, Native.Window.Width / Native.Window.Height, 1, 10000);
            camera.position.z = 100;
            camera.target = new THREE.Vector3(0, 0, 0);

            scene.add(camera);

            var bkgCamera = new THREE.OrthographicCamera(
                Native.Window.Width / -2, 
                Native.Window.Width / 2, 
                Native.Window.Height / 2, 
                Native.Window.Height / -2, 
                -10000, 
                10000
            );

            bkgScene.add(bkgCamera);

            var bkgShader = new THREE.ShaderMaterial(
                new THREE.ShaderMaterialArguments
                {
                    uniforms = new bkg_uniforms
                    {
                        tDiffuse = new uniforms_item
                        {
                            type = "t",
                            value = 0,
                            texture = __THREE.ImageUtils.loadTexture(
                                new HTML.Images.FromAssets.bkg().src
                            )
                        },

                        resolution = new uniforms_item { type = "v2", value = new THREE.Vector2(Native.Window.Width, Native.Window.Height) }
                    },

                    vertexShader = new Shaders.ExplosionVertexShader().ToString(),
                    //        fragmentShader: document.getElementById( 'fs_Gradient' ).textContent,
                     //fragmentShader = new Shaders.GradientFragmentShader().ToString(),
                     fragmentShader = new Shaders.ExplosionFragmentShader().ToString(),

                    depthWrite = false,

                }
            );

            var quad = new THREE.Mesh(new THREE.PlaneGeometry(Native.Window.Width, Native.Window.Height), bkgShader);
            quad.position.z = -100;
            quad.rotation.x = (float)Math.PI / 2;
            bkgScene.add(quad);


            var material = new THREE.ShaderMaterial(

                new THREE.ShaderMaterialArguments
                {
                    uniforms = new material_uniforms
                    {
                        tExplosion = new uniforms_item
                        {
                            type = "t",
                            value = 0,
                            texture = __THREE.ImageUtils.loadTexture(
                                new HTML.Images.FromAssets.explosion().src
                            )
                        },
                        time = new uniforms_item { type = "f", value = 0.0 },
                        weight = new uniforms_item { type = "f", value = 8.0 },
                    },

                    vertexShader = new Shaders.ExplosionVertexShader().ToString(),
                    fragmentShader = new Shaders.ExplosionFragmentShader().ToString(),

                }
            );


            var mesh = new THREE.Mesh(new THREE.SphereGeometry(20, 200, 200), material);
            scene.add(mesh);

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(Native.Window.Width, Native.Window.Height);
            renderer.autoClear = false;

            container.appendChild(renderer.domElement);

            //    container.addEventListener( 'mousedown', onMouseDown, false );
            //    container.addEventListener( 'mousemove', onMouseMove, false );
            //    container.addEventListener( 'mouseup', onMouseUp, false );
            //    container.addEventListener( 'mousewheel', onMouseWheel, false );
            //    container.addEventListener( 'DOMMouseScroll', onMouseWheel, false);
            //    window.addEventListener( 'resize', onWindowResize, false );

            //    stats = new Stats();
            //    stats.domElement.style.position = 'absolute';
            //    stats.domElement.style.top = '0px';
            //    //container.appendChild( stats.domElement );

            //var onMouseDownMouseX = 0, onMouseDownMouseY = 0,
            var lon = 0.0;
            // onMouseDownLon = 0,
            // onMouseDownLat = 0,
            var phi = 0.0;
            var theta = 0.0;
            var lat = 15.0;
            var isUserInteracting = false;


            var scale = 0.0;

            #region render
            Action render = null;

            render = delegate
            {

                ((material_uniforms)material.uniforms).time.value = .00025 * (IDate.Now - start);

                scale += .005;
                scale %= 2;

                lat = Math.Max(-85, Math.Min(85, lat));
                phi = (90 - lat) * Math.PI / 180;
                theta = lon * Math.PI / 180;

                camera.position.x = (float)(100 * Math.Sin(phi) * Math.Cos(theta));
                camera.position.y = (float)(100 * Math.Cos(phi));
                camera.position.z = (float)(100 * Math.Sin(phi) * Math.Sin(theta));

                //mesh.rotation.x += .012;
                //mesh.rotation.y += .01;
                camera.lookAt(scene.position);

                //    //renderer.render( bkgScene, bkgCamera );
                renderer.render(scene, camera);

                //    stats.update();

                Native.Window.requestAnimationFrame += render;
            };


            Native.Window.requestAnimationFrame += render;
            #endregion


            //function onWindowResize() {
            //    renderer.setSize( window.innerWidth, window.innerHeight );
            //    camera.projectionMatrix.makePerspective( fov, window.innerWidth / window.innerHeight, 1, 1100 );
            //}

            //function onMouseWheel( event ) {

            //    // WebKit

            //    if ( event.wheelDeltaY ) {

            //        fov -= event.wheelDeltaY * 0.01;

            //    // Opera / Explorer 9

            //    } else if ( event.wheelDelta ) {

            //        fov -= event.wheelDelta * 0.05;

            //    // Firefox

            //    } else if ( event.detail ) {

            //        fov += event.detail * 1.0;

            //    }

            //    camera.projectionMatrix.makePerspective( fov, window.innerWidth / window.innerHeight, 1, 1100 );

            //}



            //function onMouseDown( event ) {

            //    event.preventDefault();

            //    isUserInteracting = true;

            //    onPointerDownPointerX = event.clientX;
            //    onPointerDownPointerY = event.clientY;

            //    onPointerDownLon = lon;
            //    onPointerDownLat = lat;

            //}

            //function onMouseMove( e ) {

            //    if ( isUserInteracting ) {

            //        lon = ( e.clientX - onPointerDownPointerX ) * 0.1 + onPointerDownLon;
            //        lat = ( e.clientY - onPointerDownPointerY ) * 0.1 + onPointerDownLat;

            //    }

            //    //material.uniforms[ 'weight' ].value = e.pageX * 10.0 / window.innerWidth;;

            //}

            //function onMouseUp( event ) {

            //    isUserInteracting = false;

            //}



            #region IsDisposed

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;


                container.Orphanize();
            };
            #endregion






            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);


                renderer.setSize(Native.Window.Width, Native.Window.Height);

                camera.projectionMatrix.makePerspective(fov, Native.Window.Width / Native.Window.Height, 1, 1100);

                //camera.aspect = Native.Window.Width / Native.Window.Height;
                //camera.updateProjectionMatrix();
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

                    //AtResize();
                };
            #endregion





        }

        bool IsDisposed = false;

        public Action Dispose;

    }
}
