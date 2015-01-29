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
using WebGLBokehWithGeometry;
using WebGLBokehWithGeometry.Design;
using WebGLBokehWithGeometry.HTML.Pages;
using WebGLSpeedBlendCharacter.HTML.Images.FromAssets;

namespace WebGLBokehWithGeometry
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
            // view-source:http://alteredqualia.com/three/examples/webgl_postprocessing_ssao.html
            // http://threejs.org/examples/#webgl_materials_cubemap_dynamic

            // X:\jsc.svn\examples\javascript\WebGL\WebGLDepthOfField\WebGLDepthOfField\Application.cs
            // http://threejs.org/examples/#webgl_postprocessing_dof
            // "X:\opensource\github\three.js\examples\webgl_postprocessing_dof.html"

            { WebGLHZBlendCharacter.HTML.Pages.TexturesImages ref0; }

            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.Clear();

            var clock = new THREE.Clock();

            var radius = 100;
            var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
            skyCamera.position.set(0.0, radius * 3, radius * 3.5);

            var camera = new THREE.PerspectiveCamera(80, Native.window.aspect, 1, 5000);
            camera.position.z = 200;

            var skyScene = new THREE.Scene();
            var scene = new THREE.Scene();



            //scene.fog = new THREE.Fog(0xA26D41, 1000, 20000);

            //scene.add(new THREE.AmbientLight(0xaaaaaa));
            scene.add(new THREE.AmbientLight(0xffffff));

            var lightOffset = new THREE.Vector3(0, 1000, 1000.0);

            var light = new THREE.DirectionalLight(0xffffff, 1.0);
            //var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 1.5);
            light.position.copy(lightOffset);
            light.castShadow = true;

            var xlight = light as dynamic;
            xlight.shadowMapWidth = 4096;
            xlight.shadowMapHeight = 2048;

            xlight.shadowDarkness = 0.3;
            //xlight.shadowDarkness = 0.5;

            xlight.shadowCameraNear = 10;
            xlight.shadowCameraFar = 10000;
            xlight.shadowBias = 0.00001;
            xlight.shadowCameraRight = 4000;
            xlight.shadowCameraLeft = -4000;
            xlight.shadowCameraTop = 4000;
            xlight.shadowCameraBottom = -4000;
            //xlight.shadowCameraVisible = true;

            scene.add(light);


            var renderer = new THREE.WebGLRenderer(new { antialias = false });
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.domElement.AttachToDocument();
            renderer.sortObjects = false;
            renderer.autoClear = false;
            renderer.shadowMapEnabled = true;
            renderer.shadowMapType = THREE.PCFSoftShadowMap;

            //var material_depth = new THREE.MeshDepthMaterial();





            #region create field

            // THREE.PlaneGeometry: Consider using THREE.PlaneBufferGeometry for lower memory footprint.
            var planeGeometry = new THREE.PlaneGeometry(1000, 1000);
            var plane = new THREE.Mesh(planeGeometry,
                    new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })

                );
            plane.castShadow = false;
            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                parent.add(plane);
                parent.rotation.x = -Math.PI / 2;
                parent.scale.set(100, 100, 100);

                scene.add(parent);
            }

            var random = new Random();
            var meshArray = new List<THREE.Mesh>();
            var geometry = new THREE.CubeGeometry(1, 1, 1);
            var sw = Stopwatch.StartNew();

            for (var i = 1; i < 5; i++)
            {

                //THREE.MeshPhongMaterial
                var ii = new THREE.Mesh(geometry,


                    new THREE.MeshPhongMaterial(new { ambient = 0x000000, color = 0xA06040, specular = 0xA26D41, shininess = 1 })

                    //new THREE.MeshLambertMaterial(
                    //new
                    //{
                    //    color = (Convert.ToInt32(0xffffff * random.NextDouble())),
                    //    specular = 0xffaaaa,
                    //    ambient= 0x050505, 
                    //})

                    );
                ii.position.x = i % 4 * 500 - 2.5f;

                // raise it up
                ii.position.y = .5f * 100;
                ii.position.z = -1 * i * 300;
                ii.castShadow = true;
                ii.receiveShadow = true;
                //ii.scale.set(100, 100, 100 * i);
                ii.scale.set(100, 100 * i, 100);


                meshArray.Add(ii);

                scene.add(ii);

                var _i = i;

                var blendMesh = new THREE.SpeedBlendCharacter();
                blendMesh.load(
                    new WebGLHZBlendCharacter.Models.marine_anims().Content.src,
                    new Action(
                        delegate
                        {
                            // buildScene
                            //blendMesh.rotation.y = Math.PI * -135 / 180;
                            blendMesh.castShadow = true;
                            // we cannot scale down we want our shadows
                            //blendMesh.scale.set(0.1, 0.1, 0.1);

                            blendMesh.position.x = (_i + 2) % 4 * 500 - 2.5f;

                            // raise it up
                            //blendMesh.position.y = .5f * 100;
                            blendMesh.position.z = -1 * _i * 300;


                            var xtrue = true;
                            // run
                            blendMesh.setSpeed(1.0);

                            // will in turn call THREE.AnimationHandler.play( this );
                            blendMesh.run.play();
                            // this wont help. bokah does not see the animation it seems.
                            blendMesh.run.update(1);

                            blendMesh.showSkeleton(!xtrue);

                            scene.add(blendMesh);


                            Native.window.onframe +=
                             delegate
                             {

                                 blendMesh.rotation.y = Math.PI * 0.0002 * sw.ElapsedMilliseconds;



                                 ii.rotation.y = Math.PI * 0.0002 * sw.ElapsedMilliseconds;

                             };

                        }
                    )
                );

            }
            #endregion


            // maskpass
            // THREE.EffectComposer relies on THREE.CopyShader
            var composer = new THREE.EffectComposer(renderer);

            var renderPass = new THREE.RenderPass(scene, camera);
            composer.addPass(renderPass);

            // THREE.BokehPass relies on THREE.BokehShader
            var bokehPass = new THREE.BokehPass(scene, camera, new
            {
                focus = 1.0,
                aperture = 0.025,
                maxblur = 1.0,

                width = Native.window.Width,
                height = Native.window.Height
            });

            bokehPass.renderToScreen = true;
            composer.addPass(bokehPass);

            renderer.autoClear = false;

            var controls = new THREE.OrbitControls(camera);


            Native.document.onclick +=
                delegate
                {
                    // THREE.AnimationHandler.add() has been deprecated.

                    // how to make it work with bokah?
                    //THREE.AnimationHandler.update(1);

                };



            #region onframe
            Native.window.onframe +=
                delegate
                {
                    controls.update();


                    var scale = 1.0;
                    var delta = clock.getDelta();
                    var stepSize = delta * scale;

                    if (stepSize > 0)
                    {
                        //characterController.update(stepSize, scale);
                        //gui.setSpeed(blendMesh.speed);

                        //THREE.AnimationHandler.update(stepSize);
                    }


                    camera.position = controls.center.clone();


                    skyCamera.rotation.copy(camera.rotation);

                    composer.render(0.1);

                };

            #endregion


            new { }.With(
                  async delegate
                  {
                      //while (true)
                      do
                      {
                          camera.aspect = Native.window.aspect;
                          camera.updateProjectionMatrix();
                          renderer.setSize(Native.window.Width, Native.window.Height);

                          // convert to bool?
                      } while (await Native.window.async.onresize);
                      //} while (await Native.window.async.onresize != null);

                  }
              );

        }

    }
}
