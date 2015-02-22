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
using WebGLRah66Comanche.Library;

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
    public sealed class Application : ApplicationWebService
    {
        // new three broke it?
        // http://www.clicktorelease.com/code/perlin/explosion.html
        // http://www.webgl.com/2013/01/webgl-tutorial-vertex-displacement-with-a-noise-function-aka-fiery-explosion/


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            // http://inear.se/fireshader/




            // http://stackoverflow.com/questions/16765120/ashima-perlin-noise-shader-not-working-with-recent-versions-of-three-js

            //var container, renderer,  camera, mesh;
            var start = IDate.Now;
            var fov = 30;


            #region container
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#000000";
            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);
            #endregion


            var scene = new THREE.Scene();
            var bkgScene = new THREE.Scene();

            var camera = new THREE.PerspectiveCamera(fov, Native.window.aspect, 1, 10000);
            camera.position.z = 100;
            //camera.target = new THREE.Vector3(0, 0, 0);

            scene.add(camera);

            var bkgCamera = new THREE.OrthographicCamera(
                Native.window.Width / -2,
                Native.window.Width / 2,
                Native.window.Height / 2,
                Native.window.Height / -2,
                -10000,
                10000
            );

            bkgScene.add(bkgCamera);

            var bkgShader = new THREE.ShaderMaterial(
                new 
                {
                    uniforms = new 
                    {
                        tDiffuse = new 
                        {
                            type = "t",
                            value = THREE.ImageUtils.loadTexture(
                                new HTML.Images.FromAssets.bkg().src
                            )
                        },

                        resolution = new  { type = "v2", value = new THREE.Vector2(Native.window.Width, Native.window.Height) }
                    },

                    vertexShader = new Shaders.ExplosionVertexShader().ToString(),
                    //        fragmentShader: document.getElementById( 'fs_Gradient' ).textContent,
                    //fragmentShader = new Shaders.GradientFragmentShader().ToString(),
                    fragmentShader = new Shaders.ExplosionFragmentShader().ToString(),

                    depthWrite = false,
                    depthTest = false,
                    transparent = true
                }
            );

            var quad = new THREE.Mesh(new THREE.PlaneGeometry(Native.window.Width, Native.window.Height), bkgShader);
            quad.position.z = -100;
            quad.rotation.x = (float)Math.PI / 2;
            bkgScene.add(quad);


            var material = new THREE.ShaderMaterial(

                new 
                {
                    uniforms = new // material_uniforms
                    {
                        tExplosion = new  //uniforms_item
                        {
                            type = "t",
                            value =  THREE.ImageUtils.loadTexture(
                                new HTML.Images.FromAssets.explosion().src
                            )
                        },
                        time = new { type = "f", value = 0.0 },
                        weight = new { type = "f", value = 8.0 },
                    },

                    vertexShader = new Shaders.ExplosionVertexShader().ToString(),
                    fragmentShader = new Shaders.ExplosionFragmentShader().ToString(),
                    depthWrite = false,
                    depthTest = false,
                    transparent = true
                }
            );


            var mesh = new THREE.Mesh(new THREE.SphereGeometry(20, 200, 200), material);
            scene.add(mesh);

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.autoClear = false;

            container.appendChild(renderer.domElement);

          
            var lon = 0.0;
            var phi = 0.0;
            var theta = 0.0;
            var lat = 15.0;
            var isUserInteracting = false;


            var scale = 0.0;

            #region render

            Native.window.onframe += delegate
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

            };


            #endregion



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
                container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);


                renderer.setSize(Native.window.Width, Native.window.Height);

                camera.projectionMatrix.makePerspective(fov, Native.window.aspect, 1, 1100);

                //camera.aspect = Native.Window.Width / Native.Window.Height;
                //camera.updateProjectionMatrix();
            };

            Native.window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();
            #endregion



            var ze = new ZeProperties();

            ze.Show();
            ze.treeView1.Nodes.Clear();

            ze.Add(() => renderer);
            //ze.Add(() => controls);
            ze.Add(() => scene);
            ze.Left = 0;



        }

        bool IsDisposed = false;

        public Action Dispose;

    }
}
