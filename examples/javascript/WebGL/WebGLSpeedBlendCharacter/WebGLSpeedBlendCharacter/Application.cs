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
            //light.shadowMapWidth = 4096;
            //light.shadowMapHeight = 2048;
            //light.shadowDarkness = 0.5;
            //light.shadowCameraNear = 10;
            //light.shadowCameraFar = 10000;
            //light.shadowBias = 0.00001;
            //light.shadowCameraRight = 4000;
            //light.shadowCameraLeft = -4000;
            //light.shadowCameraTop = 4000;
            //light.shadowCameraBottom = -4000;
            //light.shadowCameraVisible = true;

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

            var blendMesh = new THREE.SpeedBlendCharacter();


            blendMesh.load(
                //"models/marine/marine_anims.js",
                new Models.marine_anims().Content.src,
                new Action(
                    delegate
                    {
                        // buildScene

                        blendMesh.rotation.y = Math.PI * -135 / 180;
                        //blendMesh.castShadow = true;

                        scene.add(blendMesh);

                        //var characterController = new THREE.CharacterController(blendMesh);

                        var radius = blendMesh.geometry.boundingSphere.radius;

                        var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        camera.position.set(0.0, radius * 3, radius * 3.5);

                        var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        skyCamera.position.set(0.0, radius * 3, radius * 3.5);

                        var controls = new THREE.OrbitControls(camera);
                        //controls.noPan = true;

#if ground
                        var loader = new THREE.JSONLoader();
                        loader.load("models/objects/ground.js",
                            new Action<THREE.Geometry, THREE.Material[]>(
                                (geometry, materials) =>
                                {
                                    var ground = new THREE.Mesh(geometry, materials[0]);
                                    ground.scale.set(20, 20, 20);
                                    ground.receiveShadow = true;
                                    ground.castShadow = true;
                                    scene.add(ground);

                                }
                            )
                        );
    #endif


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
                                controls.center.y += blendMesh.geometry.boundingSphere.radius * 2.0;
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


                    }
                )
           );




        }

    }
}
