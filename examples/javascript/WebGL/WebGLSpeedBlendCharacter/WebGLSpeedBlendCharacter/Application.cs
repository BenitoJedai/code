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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLSpeedBlendCharacter;
using WebGLSpeedBlendCharacter.Design;
using WebGLSpeedBlendCharacter.HTML.Images.FromAssets;
using WebGLSpeedBlendCharacter.HTML.Pages;

namespace WebGLSpeedBlendCharacter
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // Could not connect to the feed specified at 'http://my.jsc-solutions.net/nuget'. P


        public Application(IApp page)
        {
            // http://www.realitymeltdown.com/WebGL3/character-controller.html

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150127
            //Native.css
            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;

            //Error CS0246  The type or namespace name 'THREE' could not be found(are you missing a using directive or an assembly reference?)	WebGLSpeedBlendCharacter Application.cs  46
            // used by, for?
            var clock = new THREE.Clock();
            //var keys = new { LEFT = 37, UP = 38, RIGHT = 39, DOWN = 40, A = 65, S = 83, D = 68, W = 87 };


            var scene = new THREE.Scene();
            var skyScene = new THREE.Scene();
            scene.fog = new THREE.Fog(0xB0CAE1, 1000, 20000);
            scene.add(new THREE.AmbientLight(0xaaaaaa));

            var lightOffset = new THREE.Vector3(0, 1000, 1000.0);

            var light = new THREE.DirectionalLight(0xffffff, 1.5);
            light.position.copy(lightOffset);
            light.castShadow = true;

            var xlight = light as dynamic;
            xlight.shadowMapWidth = 4096;
            xlight.shadowMapHeight = 2048;
            xlight.shadowDarkness = 0.5;
            xlight.shadowCameraNear = 10;
            xlight.shadowCameraFar = 10000;
            xlight.shadowBias = 0.00001;
            xlight.shadowCameraRight = 4000;
            xlight.shadowCameraLeft = -4000;
            xlight.shadowCameraTop = 4000;
            xlight.shadowCameraBottom = -4000;
            xlight.shadowCameraVisible = true;

            scene.add(light);

            var renderer = new THREE.WebGLRenderer(new { antialias = true, alpha = false });
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.autoClear = false;
            renderer.shadowMapEnabled = true;
            renderer.shadowMapType = THREE.PCFSoftShadowMap;

            renderer.domElement.AttachToDocument();

            // this will mess up
            // three.OrbitControls.js
            // onMouseMove

            //new IStyle(renderer.domElement)
            //{
            //    position = IStyle.PositionEnum.absolute,
            //    left = "0px",
            //    top = "0px",
            //    right = "0px",
            //    bottom = "0px",
            //};


            //new { }.With(
            //    async delegate
            //    {

            //        await Native.window.async.onresize;

            //        // if the reload were fast, then we could actually do that:D
            //        Native.document.location.reload();

            //    }
            //);





            #region create field

            // THREE.PlaneGeometry: Consider using THREE.PlaneBufferGeometry for lower memory footprint.
            //var planeGeometry = new THREE.PlaneGeometry(1000, 1000);
            //var planeMaterial = new THREE.MeshLambertMaterial(
            //    new
            //    {
            //        map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.dirt_tx().src),
            //        color = 0xffffff
            //    }
            //);

            //planeMaterial.map.repeat.x = 300;
            //planeMaterial.map.repeat.y = 300;
            //planeMaterial.map.wrapS = THREE.RepeatWrapping;
            //planeMaterial.map.wrapT = THREE.RepeatWrapping;
            //var plane = new THREE.Mesh(planeGeometry, planeMaterial);
            //plane.castShadow = false;
            //plane.receiveShadow = true;


            //{

            //    var parent = new THREE.Object3D();
            //    parent.add(plane);
            //    parent.rotation.x = -Math.PI / 2;
            //    parent.scale.set(100, 100, 100);

            //    scene.add(parent);
            //}

            var random = new Random();
            var meshArray = new List<THREE.Mesh>();
            var geometry = new THREE.CubeGeometry(1, 1, 1);

            for (var i = 1; i < 100; i++)
            {

                var ii = new THREE.Mesh(geometry, new THREE.MeshLambertMaterial(
                    new
                    {
                        color = (Convert.ToInt32(0xffffff * random.NextDouble()))
                    }));
                ii.position.x = i % 2 * 500 - 2.5f;

                // raise it up
                ii.position.y = .5f * 100;
                ii.position.z = -1 * i * 400;
                ii.castShadow = true;
                ii.receiveShadow = true;
                //ii.scale.set(100, 100, 100 * i);
                ii.scale.set(100, 100 * i, 100);


                meshArray.Add(ii);

                scene.add(ii);

            }
            #endregion





            var blendMesh = new THREE.SpeedBlendCharacter();


            blendMesh.load(
                new Models.marine_anims().Content.src,
                new Action(
                    delegate
                    {
                        // buildScene

                        //blendMesh.rotation.y = Math.PI * -135 / 180;
                        blendMesh.castShadow = true;
                        // we cannot scale down we want our shadows
                        //blendMesh.scale.set(0.1, 0.1, 0.1);

                        scene.add(blendMesh);

                        //var characterController = new THREE.CharacterController(blendMesh);

                        // run
                        blendMesh.setSpeed(1.0);

                        var radius = blendMesh.geometry.boundingSphere.radius;


                        Native.document.title = new { radius }.ToString();


                        var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        camera.position.set(0.0, radius * 3, radius * 3.5);

                        var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        skyCamera.position.set(0.0, radius * 3, radius * 3.5);

                        var controls = new THREE.OrbitControls(camera);
                        //controls.noPan = true;


                        var loader = new THREE.JSONLoader();
                        loader.load(new Models.ground().Content.src,
                            new Action<THREE.Geometry, THREE.Material[]>(

                            (xgeometry, materials) =>
                            {

                                var ground = new THREE.Mesh(xgeometry, materials[0]);
                                ground.scale.set(20, 20, 20);
                                ground.receiveShadow = true;
                                ground.castShadow = true;
                                scene.add(ground);

                            }
                            )
                         );

                        #region  createSky

                        var urls = new[] {
                            new px().src, new nx().src,
                            new py().src, new ny().src,
                            new pz().src, new nz().src,
                         };

                        var textureCube = THREE.ImageUtils.loadTextureCube(urls);

                        dynamic shader = THREE.ShaderLib["cube"];
                        shader.uniforms["tCube"].value = textureCube;

                        // We're inside the box, so make sure to render the backsides
                        // It will typically be rendered first in the scene and without depth so anything else will be drawn in front
                        var material = new THREE.ShaderMaterial(new
                        {
                            fragmentShader = shader.fragmentShader,
                            vertexShader = shader.vertexShader,
                            uniforms = shader.uniforms,
                            depthWrite = false,
                            side = THREE.BackSide
                        });

                        // THREE.CubeGeometry has been renamed to THREE.BoxGeometry
                        // The box dimension size doesn't matter that much when the camera is in the center.  Experiment with the values.
                        var skyMesh = new THREE.Mesh(new THREE.CubeGeometry(10000, 10000, 10000, 1, 1, 1), material);
                        //skyMesh.renderDepth = -10;


                        skyScene.add(skyMesh);
                        #endregion

                        #region onframe
                        Native.window.onframe +=
                            delegate
                            {
                                var scale = 1.0;
                                var delta = clock.getDelta();
                                var stepSize = delta * scale;

                                if (stepSize > 0)
                                {
                                    //characterController.update(stepSize, scale);
                                    //gui.setSpeed(blendMesh.speed);

                                    THREE.AnimationHandler.update(stepSize);
                                    blendMesh.updateSkeletonHelper();
                                }

                                controls.center.copy(blendMesh.position);
                                controls.center.y += radius * 2.0;
                                controls.update();

                                var camOffset = camera.position.clone().sub(controls.center);
                                camOffset.normalize().multiplyScalar(750);
                                camera.position = controls.center.clone().add(camOffset);

                                skyCamera.rotation.copy(camera.rotation);



                                renderer.clear();
                                renderer.render(skyScene, skyCamera);
                                renderer.render(scene, camera);
                            };
                        #endregion

                        new { }.With(
                              async delegate
                              {
                                    do
                                  {
                                      camera.aspect = Native.window.aspect;
                                      camera.updateProjectionMatrix();
                                      renderer.setSize(Native.window.Width, Native.window.Height);

                                    } while (await Native.window.async.onresize);
                                }
                          );

                      
                    }
                )
           );




        }

    }
}
