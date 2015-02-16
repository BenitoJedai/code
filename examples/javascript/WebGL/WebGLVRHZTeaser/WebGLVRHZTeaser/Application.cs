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
using WebGLVRHZTeaser;
using WebGLVRHZTeaser.Design;
using WebGLVRHZTeaser.HTML.Pages;

namespace WebGLVRHZTeaser
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
            // 
            Native.document.body.style.margin = "0px";
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.document.body.Clear();

            double SCREEN_WIDTH = Native.window.Width;
            double SCREEN_HEIGHT = Native.window.Height;

            #region scene
            var scene = new THREE.Scene();
            var clock = new THREE.Clock();

            var sceneRenderTarget = new THREE.Scene();
            var cameraOrtho = new THREE.OrthographicCamera(
                (int)SCREEN_WIDTH / -2,
                (int)SCREEN_WIDTH / 2,
                (int)SCREEN_HEIGHT / 2,
                (int)SCREEN_HEIGHT / -2,
                -100000,
                100000
            );

            cameraOrtho.position.z = 100;
            sceneRenderTarget.add(cameraOrtho);

            var camera = new THREE.PerspectiveCamera(

                //40,
                20,
                //10,

                Native.window.aspect, 2,

                // how far out do we want to zoom?
                200000
                //9000
                );
            camera.position.set(-1200, 800, 1200);

            scene.add(camera);
            //scene.add(new THREE.AmbientLight(0x212121));

            //var spotLight = new THREE.SpotLight(0xffffff, 1.15);
            //spotLight.position.set(500, 2000, 0);
            //spotLight.castShadow = true;
            //scene.add(spotLight);

            //var pointLight = new THREE.PointLight(0xff4400, 1.5);
            //pointLight.position.set(0, 0, 0);
            //scene.add(pointLight);


            //scene.add(new THREE.AmbientLight(0xaaaaaa));
            scene.add(new THREE.AmbientLight(0xffffff));
            #endregion


            #region light
            //var light = new THREE.DirectionalLight(0xffffff, 1.0);
            var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 1.5);
            //var lightOffset = new THREE.Vector3(0, 1000, 2500.0);
            var lightOffset = new THREE.Vector3(
                2000,
                700,

                // lower makes longer shadows 
                700.0
                );
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

            xlight.shadowCameraVisible = true;

            scene.add(light);
            #endregion



            var renderer = new THREE.WebGLRenderer(
                new
                {

                    // http://stackoverflow.com/questions/20495302/transparent-background-with-three-js
                    alpha = true,
                    preserveDrawingBuffer = true,
                    antialias = true
                }

                );
            renderer.setSize(1920, 1080);
            renderer.domElement.AttachToDocument();
            renderer.shadowMapEnabled = true;
            renderer.shadowMapType = THREE.PCFSoftShadowMap;


            var effect = new THREE.OculusRiftEffect(
                renderer, new
                {
                    worldScale = 100,

                    //HMD
                }
                );

            effect.setSize(1920, 1080);


            #region create field

            // THREE.PlaneGeometry: Consider using THREE.PlaneBufferGeometry for lower memory footprint.

            // could we get some film grain?
            var planeGeometry = new THREE.CubeGeometry(512, 512, 1);
            var plane = new THREE.Mesh(planeGeometry,
                    new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })

                );
            //plane.castShadow = false;
            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                parent.add(plane);
                parent.rotation.x = -Math.PI / 2;
                parent.scale.set(10, 10, 10);

                scene.add(parent);
            }

            var random = new Random();
            var meshArray = new List<THREE.Mesh>();
            var geometry = new THREE.CubeGeometry(1, 1, 1);
            var sw = Stopwatch.StartNew();

            for (var i = 3; i < 9; i++)
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
                ii.position.x = i % 7 * 200 - 2.5f;

                // raise it up
                ii.position.y = .5f * 100;
                ii.position.z = -1 * i * 100;
                ii.castShadow = true;
                ii.receiveShadow = true;
                //ii.scale.set(100, 100, 100 * i);
                ii.scale.set(100, 100 * i, 100);


                meshArray.Add(ii);

                scene.add(ii);
#if FWebGLHZBlendCharacter
                #region SpeedBlendCharacter
                var _i = i;
                { WebGLHZBlendCharacter.HTML.Pages.TexturesImages ref0; }

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

                            blendMesh.position.x = (_i + 2) % 7 * 200 - 2.5f;

                            // raise it up
                            //blendMesh.position.y = .5f * 100;
                            blendMesh.position.z = -1 * _i * 100;


                            var xtrue = true;
                            // run
                            blendMesh.setSpeed(1.0);

                            // will in turn call THREE.AnimationHandler.play( this );
                            //blendMesh.run.play();
                            // this wont help. bokah does not see the animation it seems.
                            //blendMesh.run.update(1);

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
                #endregion
#endif

            }
            #endregion


            var controls = new THREE.OrbitControls(camera);

            Native.window.onframe +=
                delegate
                {
                    ////var delta = clock.getDelta();

                    controls.update();



                    var scale = 1.0;
                    var delta = clock.getDelta();
                    var stepSize = delta * scale;

                    if (stepSize > 0)
                    {
                        //characterController.update(stepSize, scale);
                        //gui.setSpeed(blendMesh.speed);

                        THREE.AnimationHandler.update(stepSize);
                    }

                    camera.position = controls.center.clone();

                    //composer.render(0.1);
                    //renderer.render(scene, camera);
                    effect.render(scene, camera);
                };


            new { }.With(
                     async delegate
                     {
                         retry:

                         var s = (double)Native.window.Width / 1920.0;


                         Native.document.body.style.transform = "scale(" + s + ")";
                         Native.document.body.style.transformOrigin = "0% 0%";

                         await Native.window.async.onresize;
                         goto retry;
                     }
                   );

        }

    }
}
