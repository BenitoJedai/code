#define FWebGLHZBlendCharacter

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
            Native.body.style.backgroundColor = "black";
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
            var target = new THREE.Vector3(0, 0, 0);

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



            var renderTarget = new THREE.WebGLRenderTarget(
                   Native.window.Width, Native.window.Height,
                   new
                   {
                       minFilter = THREE.LinearFilter,
                       magFilter = THREE.LinearFilter,
                       format = THREE.RGBAFormat,
                       stencilBufer = false
                   }
               );

            //var composer = new THREE.EffectComposer(renderer, renderTarget);
            //var renderModel = new THREE.RenderPass(scene, camera);
            //composer.addPass(renderModel);

            //#region vblur
            //var hblur = new THREE.ShaderPass(THREE.HorizontalTiltShiftShader);
            //var vblur = new THREE.ShaderPass(THREE.VerticalTiltShiftShader);

            ////var bluriness = 6.0;
            //var bluriness = 4.0;

            //// Show Details	Severity	Code	Description	Project	File	Line
            ////Error CS0656  Missing compiler required member 'Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create' WebGLTiltShift Application.cs  183

            //(hblur.uniforms as dynamic).h.value = bluriness / Native.window.Width;
            //(vblur.uniforms as dynamic).v.value = bluriness / Native.window.Height;

            //(hblur.uniforms as dynamic).r.value = 0.5;
            //(vblur.uniforms as dynamic).r.value = 0.5;
            ////vblur.renderToScreen = true;

            //composer.addPass(hblur);
            //composer.addPass(vblur);
            //#endregion

            // Uncaught TypeError: renderer.setSize is not a function
            // Uncaught TypeError: renderer.getClearColor is not a function

            var effect = new THREE.OculusRiftEffect(
                renderer,

                // how to get the vblur into oculus effect?

                //renderModel,
                //composer,
                //renderTarget,
                new
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

                if (i % 2 == 0)
                {
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

            }
            #endregion


            #region HZCannon
            new HeatZeekerRTSOrto.HZCannon().Source.Task.ContinueWithResult(
                async cube =>
                {
                    // https://github.com/mrdoob/three.js/issues/1285
                    //cube.children.WithEach(c => c.castShadow = true);

                    cube.traverse(
                        new Action<THREE.Object3D>(
                            child =>
                            {
                                // does it work? do we need it?
                                //if (child is THREE.Mesh)

                                child.castShadow = true;
                                //child.receiveShadow = true;

                            }
                        )
                    );

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
                    var sw2 = Stopwatch.StartNew();



                    scene.add(cube);
                    //interactiveObjects.Add(cube);

                    // offset is wrong
                    //while (true)
                    //{
                    //    await Native.window.async.onframe;

                    //    cube.rotation.y = Math.PI * 0.0002 * sw2.ElapsedMilliseconds;

                    //}
                }
            );
            #endregion


            #region HZCannon
            new HeatZeekerRTSOrto.HZCannon().Source.Task.ContinueWithResult(
                async cube =>
                {
                    // https://github.com/mrdoob/three.js/issues/1285
                    //cube.children.WithEach(c => c.castShadow = true);

                    cube.traverse(
                        new Action<THREE.Object3D>(
                            child =>
                            {
                                // does it work? do we need it?
                                //if (child is THREE.Mesh)

                                child.castShadow = true;
                                //child.receiveShadow = true;

                            }
                        )
                    );

                    // um can edit and continue insert code going back in time?
                    cube.scale.x = 10.0;
                    cube.scale.y = 10.0;
                    cube.scale.z = 10.0;



                    //cube.castShadow = true;
                    //dae.receiveShadow = true;

                    //cube.position.x = -100;
                    cube.position.y = 400;

                    ////cube.position.y = (cube.scale.y * 50) / 2;
                    //cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;



                    // if i want to rotate, how do I do it?
                    //cube.rotation.z = random() + Math.PI;
                    //cube.rotation.x = random() + Math.PI;
                    var sw2 = Stopwatch.StartNew();



                    scene.add(cube);
                    //interactiveObjects.Add(cube);

                    // offset is wrong
                    //while (true)
                    //{
                    //    await Native.window.async.onframe;

                    //    cube.rotation.y = Math.PI * 0.0002 * sw2.ElapsedMilliseconds;

                    //}
                }
            );
            #endregion


            #region HZBunker
            new HeatZeekerRTSOrto.HZBunker().Source.Task.ContinueWithResult(
                     cube =>
                     {
                         // https://github.com/mrdoob/three.js/issues/1285
                         //cube.children.WithEach(c => c.castShadow = true);
                         cube.castShadow = true;

                         cube.traverse(
                             new Action<THREE.Object3D>(
                                 child =>
                                 {
                                     // does it work? do we need it?
                                     //if (child is THREE.Mesh)
                                     child.castShadow = true;
                                     //child.receiveShadow = true;

                                 }
                             )
                         );

                         // um can edit and continue insert code going back in time?
                         cube.scale.x = 10.0;
                         cube.scale.y = 10.0;
                         cube.scale.z = 10.0;

                         //cube.castShadow = true;
                         //dae.receiveShadow = true;

                         cube.position.x = -1000;
                         //cube.position.y = (cube.scale.y * 50) / 2;
                         cube.position.z = 0;

                         scene.add(cube);
                     }
                 );
            #endregion


            var lon0 = -45.0;
            var lon1 = 0.0;

            var lon = new sum(
                 () => lon0,
                 () => lon1
             );

            var lat0 = 0.0;
            var lat1 = 0.0;

            // or could we do it with byref or pointers?
            var lat = new sum(
                () => lat0,
                () => lat1
            );

            var phi = 0.0;
            var theta = 0.0;

            //var controls = new THREE.OrbitControls(camera);

            Native.window.onframe +=
                delegate
                {
                    ////var delta = clock.getDelta();

                    //controls.update();



                    var scale = 1.0;
                    var delta = clock.getDelta();
                    var stepSize = delta * scale;

                    if (stepSize > 0)
                    {
                        //characterController.update(stepSize, scale);
                        //gui.setSpeed(blendMesh.speed);

                        THREE.AnimationHandler.update(stepSize);
                    }

                    //camera.position = controls.center.clone();

                    //if (Native.document.pointerLockElement == Native.document.body)
                    //    lon += 0.00;
                    //else
                    //    lon += 0.01;

                    //var lat2 = Math.Max(-85, Math.Min(85, lat));
                    var lat2 = lat;

                    //Native.document.title = new { lon, lat }.ToString();
                    //Native.document.title = new { lon0 }.ToString();


                    phi = THREE.Math.degToRad(90 - lat2);
                    theta = THREE.Math.degToRad(lon);

                    target.x = camera.position.x + (500 * Math.Sin(phi) * Math.Cos(theta));
                    target.y = camera.position.y + (500 * Math.Cos(phi));
                    target.z = camera.position.z + (500 * Math.Sin(phi) * Math.Sin(theta));


                    //controls.update();
                    //camera.position = controls.center.clone();


                    camera.lookAt(target);

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

            // gamma -0 .. -90

            var compassHeadingOffset = 0.0;
            var compassHeadingInitialized = 0;

            #region compassHeading
            // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeading\TestCompassHeading\Application.cs
            Native.window.ondeviceorientation +=
              dataValues =>
              {
                  // Convert degrees to radians
                  var alphaRad = dataValues.alpha * (Math.PI / 180);
                  var betaRad = dataValues.beta * (Math.PI / 180);
                  var gammaRad = dataValues.gamma * (Math.PI / 180);

                  // Calculate equation components
                  var cA = Math.Cos(alphaRad);
                  var sA = Math.Sin(alphaRad);
                  var cB = Math.Cos(betaRad);
                  var sB = Math.Sin(betaRad);
                  var cG = Math.Cos(gammaRad);
                  var sG = Math.Sin(gammaRad);

                  // Calculate A, B, C rotation components
                  var rA = -cA * sG - sA * sB * cG;
                  var rB = -sA * sG + cA * sB * cG;
                  var rC = -cB * cG;

                  // Calculate compass heading
                  var compassHeading = Math.Atan(rA / rB);

                  // Convert from half unit circle to whole unit circle
                  if (rB < 0)
                  {
                      compassHeading += Math.PI;
                  }
                  else if (rA < 0)
                  {
                      compassHeading += 2 * Math.PI;
                  }

                  /*
                  Alternative calculation (replacing lines 99-107 above):

                    var compassHeading = Math.atan2(rA, rB);

                    if(rA < 0) {
                      compassHeading += 2 * Math.PI;
                    }
                  */

                  // Convert radians to degrees
                  compassHeading *= 180 / Math.PI;

                  // Compass heading can only be derived if returned values are 'absolute'

                  // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeadingWithReset\TestCompassHeadingWithReset\Application.cs

                  //Native.document.body.innerText = new { compassHeading }.ToString();
                  if (compassHeadingInitialized > 0)
                  {
                      lon1 = compassHeading - compassHeadingOffset;
                  }
                  else
                  {
                      compassHeadingOffset = compassHeading;
                      compassHeadingInitialized++;
                  }

              };
            #endregion

            #region gamma
            Native.window.ondeviceorientation +=
                //e => Native.body.innerText = new { e.alpha, e.beta, e.gamma }.ToString();
                //e => lon = e.gamma;
                e => lat1 = e.gamma;
            #endregion



            #region camera rotation
            var old = new { clientX = 0, clientY = 0 };

            Native.document.body.ontouchstart +=
                e =>
                {
                    var n = new { e.touches[0].clientX, e.touches[0].clientY };
                    old = n;
                };

            Native.document.body.ontouchmove +=
                    e =>
                    {
                        var n = new { e.touches[0].clientX, e.touches[0].clientY };

                        e.preventDefault();

                        lon0 += (n.clientX - old.clientX) * 0.2;
                        lat0 -= (n.clientY - old.clientY) * 0.2;

                        old = n;
                    };


            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        lon0 += e.movementX * 0.1;
                        lat0 -= e.movementY * 0.1;

                        //Console.WriteLine(new { lon, lat, e.movementX, e.movementY });
                    }
                };


            Native.document.body.onmouseup +=
              e =>
              {
                  //drag = false;
                  e.preventDefault();
              };

            Native.document.body.onmousedown +=
                e =>
                {
                    //e.CaptureMouse();

                    //drag = true;
                    e.preventDefault();
                    Native.document.body.requestPointerLock();

                };


            Native.document.body.ondblclick +=
                e =>
                {
                    e.preventDefault();

                    Console.WriteLine("requestPointerLock");
                };

            #endregion


        }

    }

    // http://stackoverflow.com/questions/32664/is-there-a-constraint-that-restricts-my-generic-method-to-numeric-types
    class sum //<T>
    {
        public static implicit operator double (sum that)
        {
            return that.i.Sum(x => x());
        }


        Func<double>[] i;
        public sum(params Func<double>[] i)
        {
            this.i = i;
        }

        //public sum(params ref double[] i)
        //{
        //}
    }

}
