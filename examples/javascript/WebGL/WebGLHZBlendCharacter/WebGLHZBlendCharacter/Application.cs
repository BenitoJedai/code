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
using WebGLHZBlendCharacter;
using WebGLHZBlendCharacter.Design;
using WebGLHZBlendCharacter.HTML.Images.FromAssets;
using WebGLHZBlendCharacter.HTML.Pages;

namespace WebGLHZBlendCharacter
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // http://alteredqualia.com/three/examples/webgl_postprocessing_ssao.html
        // http://alteredqualia.com/three/examples/webgl_cars.html
        // http://alteredqualia.com/xg/examples/animation_physics_terrain.html

        // https://chrome.google.com/webstore/detail/webglhzblendcharacter/cgnjcccfcjhdnbfgjgllglbhfcgndmea

        // Could not connect to the feed specified at 'http://my.jsc-solutions.net/nuget'. P
        // https://github.com/dotnet/roslyn/issues/98


        //Icon image is missing.
        //At least one new-style screenshot or video is required.
        //Small tile image is missing.
        //Please select a Primary Category for your item.
        //Language is not selected.

        // 640x400
        // 128x128
        // 440x280

        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150128

            Console.WriteLine("enter WebGLHZBlendCharacter");

#if !DEBUG
            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                Console.WriteLine("invoke TheServerWithAppWindow.Invoke");
                ChromeTCPServer.TheServerWithAppWindow.Invoke(AppSource.Text);

                return;
            }
            #endregion
#endif


            { TexturesImages ref0; }

            // http://www.realitymeltdown.com/WebGL3/character-controller.html

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150127
            //Native.css
            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;

            //Error CS0246  The type or namespace name 'THREE' could not be found(are you missing a using directive or an assembly reference?)	WebGLHZBlendCharacter Application.cs  46
            // used by, for?
            var clock = new THREE.Clock();
            //var keys = new { LEFT = 37, UP = 38, RIGHT = 39, DOWN = 40, A = 65, S = 83, D = 68, W = 87 };


            var scene = new THREE.Scene();
            var skyScene = new THREE.Scene();

            scene.fog = new THREE.Fog(0xA26D41, 1000, 20000);

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

            for (var i = 1; i < 100; i++)
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


            #region HZCannon
            new HeatZeekerRTSOrto.HZCannon().Source.Task.ContinueWithResult(
                async cube =>
                {
                    // https://github.com/mrdoob/three.js/issues/1285
                    //cube.children.WithEach(c => c.castShadow = true);

                    //cube.traverse(
                    //    new Action<THREE.Object3D>(
                    //        child =>
                    //        {
                    //            // does it work? do we need it?
                    //            //if (child is THREE.Mesh)

                    //            child.castShadow = true;
                    //            //child.receiveShadow = true;

                    //        }
                    //    )
                    //);

                    // um can edit and continue insert code going back in time?
                    cube.scale.x = 10.0;
                    cube.scale.y = 10.0;
                    cube.scale.z = 10.0;



                    //cube.castShadow = true;
                    //dae.receiveShadow = true;

                    //cube.position.x = -100;

                    ////cube.position.y = (cube.scale.y * 50) / 2;
                    //cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;



                    // if i want to rotate, how do I do it?
                    //cube.rotation.z = random() + Math.PI;
                    //cube.rotation.x = random() + Math.PI;
                    var sw = Stopwatch.StartNew();



                    scene.add(cube);
                    //interactiveObjects.Add(cube);


                    while (true)
                    {
                        await Native.window.async.onframe;

                        cube.rotation.y = Math.PI * 0.0002 * sw.ElapsedMilliseconds;

                    }
                }
            );
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

                        var xtrue = true;
                        // run
                        blendMesh.setSpeed(1.0);
                        blendMesh.showSkeleton(!xtrue);

                        var radius = blendMesh.geometry.boundingSphere.radius;


                        Native.document.title = new { radius }.ToString();


                        var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        camera.position.set(0.0, radius * 3, radius * 3.5);

                        var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        skyCamera.position.set(0.0, radius * 3, radius * 3.5);

                        var controls = new THREE.OrbitControls(camera);
                        //controls.noPan = true;


                        //var loader = new THREE.JSONLoader();
                        //loader.load(new Models.ground().Content.src,
                        //    new Action<THREE.Geometry, THREE.Material[]>(

                        //    (xgeometry, materials) =>
                        //    {

                        //        var ground = new THREE.Mesh(xgeometry, materials[0]);
                        //        ground.scale.set(20, 20, 20);
                        //        ground.receiveShadow = true;
                        //        ground.castShadow = true;
                        //        scene.add(ground);

                        //    }
                        //    )
                        // );

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




                        ////var renderTarget = new THREE.WebGLRenderTarget((int)Native.window.Width, (int)Native.window.Height,
                        ////    new
                        ////    {
                        ////        minFilter = THREE.LinearFilter,
                        ////        magFilter = THREE.LinearFilter,
                        ////        format = THREE.RGBFormat,
                        ////        stencilBufer = false
                        ////    }
                        ////);

                        ////var composer = new THREE.EffectComposer(renderer, renderTarget);
                        ////composer.addPass(new THREE.RenderPass(skyScene, skyCamera));
                        ////composer.addPass(new THREE.RenderPass(scene, camera));

                        ////#region vblur
                        ////var hblur = new THREE.ShaderPass(THREE.HorizontalTiltShiftShader);
                        ////var vblur = new THREE.ShaderPass(THREE.VerticalTiltShiftShader);

                        ////var bluriness = 6;

                        ////// Show Details	Severity	Code	Description	Project	File	Line
                        //////Error CS0656  Missing compiler required member 'Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create' WebGLTiltShift Application.cs  183

                        ////(hblur.uniforms as dynamic).h.value = bluriness / Native.window.Width;
                        ////(vblur.uniforms as dynamic).v.value = bluriness / Native.window.Height;

                        ////(hblur.uniforms as dynamic).r.value = 0.5;
                        ////(vblur.uniforms as dynamic).r.value = 0.5;
                        ////vblur.renderToScreen = true;

                        ////composer.addPass(hblur);
                        ////composer.addPass(vblur);
                        ////#endregion

                        //composer.addPass(new THREE.RenderPass(scene, camera));
                        // 

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

                                //var camOffset = camera.position.clone().sub(controls.center);
                                //camOffset.normalize().multiplyScalar(750);
                                camera.position = controls.center.clone();
                                //camera.position = controls.center.clone().add(camOffset);

                                skyCamera.rotation.copy(camera.rotation);


                                //composer.render(0.1);

                                //renderer.clear();
                                renderer.render(skyScene, skyCamera);
                                renderer.render(scene, camera);
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
                )
           );




        }

    }
}
