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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLMD2MorphtargetExample;
using WebGLMD2MorphtargetExample.Design;
using WebGLMD2MorphtargetExample.HTML.Pages;

namespace WebGLMD2MorphtargetExample
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
            // http://oos.moxiecode.com/js_webgl/md2_converter/MD2_converter.js
            // http://oos.moxiecode.com/js_webgl/md2_morphtarget_example/

            var camz = 3000.0;

            var SHADOW_MAP_WIDTH = 1024;
            var SHADOW_MAP_HEIGHT = 512;

            var scene = new THREE.Scene();
            scene.fog = new THREE.FogExp2(0x111111, 0.00098);

            //  fov, aspect, near, far )
            var camera = new THREE.PerspectiveCamera(
                60,
                Native.window.aspect,
                1,
                10000
            );

            camera.position.z = camz;
            camera.position.y = 100;
            camera.lookAt(new THREE.Vector3(0, -94, 0));
            scene.add(camera);

            var THREE_FlatShading = 1;

            // Ground
            var planeGeometry = new THREE.PlaneGeometry(10000, 10000);
            var planeMaterial_args = new
            {
                map = THREE.ImageUtils.loadTexture(
                        new HTML.Images.FromAssets._4585_v4().src
                    ),

                // WebGL: INVALID_ENUM: texParameter: invalid parameter
                shading = THREE_FlatShading,
                ambient = 0x666666,
                color = 0xffffff,
                specular = 0x666666,
                shininess = 1000000
            };



            var planeMaterial = new THREE.MeshPhongMaterial(planeMaterial_args);


            planeMaterial.map.repeat.x = 30;
            planeMaterial.map.repeat.y = 30;
            planeMaterial.map.wrapS = THREE.RepeatWrapping;
            planeMaterial.map.wrapT = THREE.RepeatWrapping;


            var plane = new THREE.Mesh(planeGeometry, planeMaterial);
            //plane.rotation.x = -Math.PI/2;
            plane.rotation.y = -Math.PI * 0.75;
            plane.position.y = -94;
            plane.castShadow = false;
            plane.receiveShadow = true;
            scene.add(plane);

            // Lights
            var ambient = new THREE.AmbientLight(0x333333);
            scene.add(ambient);

            var light = new THREE.SpotLight(0xffffff, 1.0);
            light.position.set(100, 350, 140);
            light.target.position.set(0, -94, 0);

            light.castShadow = true;

            light.shadowCameraNear = 1;
            light.shadowCameraFar = camera.far;
            light.shadowCameraFov = 50;

            light.shadowMapBias = 0.000001;
            light.shadowMapDarkness = 0.15;

            light.shadowMapWidth = SHADOW_MAP_WIDTH;
            light.shadowMapHeight = SHADOW_MAP_HEIGHT;

            scene.add(light);

            // Model

            var material = new THREE.MeshPhongMaterial(
                new
            {
                map = THREE.ImageUtils.loadTexture(
                        new HTML.Images.FromAssets.blade_black().src
                    ),
                ambient = 0x999999,
                color = 0xffffff,
                specular = 0xffffff,
                shininess = 25,
                morphTargets = true
            }
            );


            #region tris_md2
            new THREE.JSONLoader().load(
                new global::WebGLMD2MorphtargetExample.Data.tris_md2().Content.src,
                 new Action<object>(
                    async geometry =>
                    {
                        var mesh = new THREE.MorphAnimMesh(geometry, material);
                        mesh.rotation.y = -Math.PI / 2;
                        mesh.scale.set(4, 4, 4);
                        mesh.duration = 1000 * 20;
                        mesh.castShadow = true;
                        mesh.receiveShadow = false;

                        scene.add(mesh);

                        // renderer
                        var renderer = new THREE.WebGLRenderer(new { antialias = false });
                        //renderer.setClearColorHex(0x111111, 1);
                        //renderer.setClearColorHex(0x111111);
                        renderer.setSize();
                        renderer.domElement.AttachToDocument();
                        renderer.domElement.style.SetLocation(0, 0);
                        renderer.domElement.style.backgroundColor = "black";



                        renderer.shadowMapEnabled = true;
                        renderer.shadowMapSoft = true;



                        var clock = new Stopwatch();
                        clock.Start();

                        while (true)
                        {
                            double delta = clock.ElapsedMilliseconds;
                            clock.Restart();

                            //if (delta > 1000 || delta == 0)
                            //{
                            //    delta = 1000 / 60.0;
                            //}

                            //Console.WriteLine(new { delta });

                            mesh.updateAnimation(delta);

                            camz += (450 - camz) / 20.0;
                            camera.position.z = camz;


                            renderer.render(scene, camera);

                            //await Native.window.requestAnimationFrameAsync;
                            await Native.window.async.onframe;
                        };
                    }
                )
            );
            #endregion








        }

    }
}
