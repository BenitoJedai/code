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
using WebGLGodRay;
using WebGLGodRay.Design;
using WebGLGodRay.HTML.Pages;
using WebGLRah66Comanche;
using WebGLRah66Comanche.Library;

namespace WebGLGodRay
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
            // http://threejs.org/examples/#webgl_postprocessing_godrays
            // view-source:file:///X:/opensource/github/three.js/examples/webgl_postprocessing_godrays.html

            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.Clear();


            var sunPosition = new THREE.Vector3(0, 1000, -1000);
            var screenSpacePosition = new THREE.Vector3();

            var mouseX = 0;
            var mouseY = 0;

            var windowHalfX = Native.window.Width / 2;
            var windowHalfY = Native.window.Height / 2;

            //var postprocessing = { enabled : true };

            var orbitRadius = 200;

            var bgColor = 0x000511;
            var sunColor = 0xffee00;

            var camera = new THREE.PerspectiveCamera(70, Native.window.aspect, 1, 3000);
            camera.position.z = 200;

            var scene = new THREE.Scene();

            //

            var materialDepth = new THREE.MeshDepthMaterial();


            #region tree
            // X:\jsc.svn\examples\javascript\WebGL\WebGLGodRay\WebGLGodRay\Application.cs

            var materialScene = new THREE.MeshBasicMaterial(new { color = 0x000000, shading = THREE.FlatShading });
            var loader = new THREE.JSONLoader();

            // http://stackoverflow.com/questions/16539736/do-not-use-system-runtime-compilerservices-dynamicattribute-use-the-dynamic
            // https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.dynamicattribute%28v=vs.110%29.aspx
            //System.Runtime.CompilerServices.DynamicAttribute

            loader.load(

                new Models.tree().Content.src,

                new Action<THREE.Geometry>(
                xgeometry =>
                {

                    var treeMesh = new THREE.Mesh(xgeometry, materialScene);
                    treeMesh.position.set(0, -150, -150);

                    var tsc = 400;
                    treeMesh.scale.set(tsc, tsc, tsc);

                    treeMesh.matrixAutoUpdate = false;
                    treeMesh.updateMatrix();


                    treeMesh.AttachTo(scene);

                }
                )
                );
            #endregion

            // sphere

            var geo = new THREE.SphereGeometry(1, 20, 10);

            var sphereMesh = new THREE.Mesh(geo, materialScene);

            var sc = 20;
            sphereMesh.scale.set(sc, sc, sc);

            scene.add(sphereMesh);

            var renderer = new THREE.WebGLRenderer(new { antialias = false });

            renderer.setClearColor(bgColor);
            //renderer.setPixelRatio(window.devicePixelRatio);
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.domElement.AttachToDocument();


            renderer.autoClear = false;
            renderer.sortObjects = false;


            var postprocessing_scene = new THREE.Scene();

            var postprocessing_camera = new THREE.OrthographicCamera(Native.window.Width / -2, Native.window.Width / 2, Native.window.Height / 2, Native.window.Height / -2, -10000, 10000);
            postprocessing_camera.position.z = 100;

            postprocessing_scene.add(postprocessing_camera);

            var pars = new { minFilter = THREE.LinearFilter, magFilter = THREE.LinearFilter, format = THREE.RGBFormat };

            var postprocessing_rtTextureColors = new THREE.WebGLRenderTarget(Native.window.Width, Native.window.Height, pars);

            // Switching the depth formats to luminance from rgb doesn't seem to work. I didn't
            // investigate further for now.
            // pars.format = THREE.LuminanceFormat;

            // I would have this quarter size and use it as one of the ping-pong render
            // targets but the aliasing causes some temporal flickering

            var postprocessing_rtTextureDepth = new THREE.WebGLRenderTarget(Native.window.Width, Native.window.Height, pars);

            // Aggressive downsize god-ray ping-pong render targets to minimize cost

            var w = Native.window.Width / 4;
            var h = Native.window.Height / 4;
            var postprocessing_rtTextureGodRays1 = new THREE.WebGLRenderTarget(w, h, pars);
            var postprocessing_rtTextureGodRays2 = new THREE.WebGLRenderTarget(w, h, pars);

            // god-ray shaders

            // X:\jsc.svn\market\synergy\THREE\THREE\opensource\gihtub\three.js\build\THREE.ShaderGodRays.idl
            // these are special <script src="js/ShaderGodRays.js"></script>
            var godraysGenShader = THREE.ShaderGodRays["godrays_generate"] as dynamic;
            var postprocessing_godrayGenUniforms = THREE.UniformsUtils.clone(godraysGenShader.uniforms);
            var postprocessing_materialGodraysGenerate = new THREE.ShaderMaterial(new
            {

                uniforms = postprocessing_godrayGenUniforms,
                vertexShader = godraysGenShader.vertexShader,
                fragmentShader = godraysGenShader.fragmentShader

            });

            var godraysCombineShader = THREE.ShaderGodRays["godrays_combine"] as dynamic;
            var postprocessing_godrayCombineUniforms = THREE.UniformsUtils.clone(godraysCombineShader.uniforms);
            var postprocessing_materialGodraysCombine = new THREE.ShaderMaterial(new
            {

                uniforms = postprocessing_godrayCombineUniforms,
                vertexShader = godraysCombineShader.vertexShader,
                fragmentShader = godraysCombineShader.fragmentShader

            });

            var godraysFakeSunShader = THREE.ShaderGodRays["godrays_fake_sun"] as dynamic;
            var postprocessing_godraysFakeSunUniforms = THREE.UniformsUtils.clone(godraysFakeSunShader.uniforms);
            var postprocessing_materialGodraysFakeSun = new THREE.ShaderMaterial(new
            {

                uniforms = postprocessing_godraysFakeSunUniforms,
                vertexShader = godraysFakeSunShader.vertexShader,
                fragmentShader = godraysFakeSunShader.fragmentShader

            });

            postprocessing_godraysFakeSunUniforms.bgColor.value.setHex(bgColor);
            postprocessing_godraysFakeSunUniforms.sunColor.value.setHex(sunColor);

            postprocessing_godrayCombineUniforms.fGodRayIntensity.value = 0.75;

            var postprocessing_quad = new THREE.Mesh(
                new THREE.PlaneBufferGeometry(Native.window.Width, Native.window.Height),
                postprocessing_materialGodraysGenerate
            );
            postprocessing_quad.position.z = -9900;
            postprocessing_scene.add(postprocessing_quad);


            #region create field

            // THREE.PlaneGeometry: Consider using THREE.PlaneBufferGeometry for lower memory footprint.
            var planeGeometry = new THREE.PlaneGeometry(1000, 1000);
            //var planeMaterial = new THREE.MeshLambertMaterial(
            //    new
            //    {
            //        //map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.dirt_tx().src),
            //        color = 0xA26D41
            //        //color = 0xff0000
            //    }
            //);

            //planeMaterial.map.repeat.x = 300;
            //planeMaterial.map.repeat.y = 300;
            //planeMaterial.map.wrapS = THREE.RepeatWrapping;
            //planeMaterial.map.wrapT = THREE.RepeatWrapping;
            var plane = new THREE.Mesh(planeGeometry,
                    new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })

                );
            plane.castShadow = false;
            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                parent.add(plane);

                parent.position.y = -.5f * 100;

                parent.rotation.x = -Math.PI / 2;
                parent.scale.set(100, 100, 100);

                //scene.add(parent);
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




            #region Comanche
            new Comanche().Source.Task.ContinueWithResult(
                Comanche =>
                {

                    Comanche.position.y = 200;

                    //dae.position.z = 280;

                    Comanche.AttachTo(scene);

                    //scene.add(dae);
                    //oo.Add(Comanche);

                    // wont do it
                    //dae.castShadow = true;

                    // http://stackoverflow.com/questions/15492857/any-way-to-get-a-bounding-box-from-a-three-js-object3d
                    //var helper = new THREE.BoundingBoxHelper(dae, 0xff0000);
                    //helper.update();
                    //// If you want a visible bounding box
                    //scene.add(helper);

                    Comanche.children[0].children[0].children.WithEach(x => x.castShadow = true);


                    // the rotors?
                    Comanche.children[0].children[0].children.Last().children.WithEach(x => x.castShadow = true);


                    Comanche.scale.set(0.5, 0.5, 0.5);
                    //helper.scale.set(0.5, 0.5, 0.5);

                    var s2w = Stopwatch.StartNew();

                    Native.window.onframe += delegate
                    {
                        //dae.children[0].children[0].children.Last().al
                        //dae.children[0].children[0].children.Last().rotation.z = sw.ElapsedMilliseconds * 0.01;
                        //dae.children[0].children[0].children.Last().rotation.x = sw.ElapsedMilliseconds * 0.01;
                        //rotation.y = sw.ElapsedMilliseconds * 0.01;

                        Comanche.children[0].children[0].children.Last().rotation.y = s2w.ElapsedMilliseconds * 0.001;

                        //dae.children[0].children[0].children.Last().app
                    };
                }
            );
            #endregion




            var sw = Stopwatch.StartNew();

            var controls = new THREE.OrbitControls(camera, renderer.domElement);

            Native.window.onframe +=
                delegate
                {
                    //var time = IDate.now() / 4000;
                    var time = sw.ElapsedMilliseconds / 4000.0;

                    sphereMesh.position.x = orbitRadius * Math.Cos(time);
                    sphereMesh.position.z = orbitRadius * Math.Sin(time) - 100;



                    //controls.center.copy(blendMesh.position);
                    //controls.center.y += radius * 2.0;
                    controls.update();

                    //var camOffset = camera.position.clone().sub(controls.center);
                    //camOffset.normalize().multiplyScalar(750);
                    camera.position = controls.center.clone();


                    //camera.position.x += (mouseX - camera.position.x) * 0.036;
                    //camera.position.y += (-(mouseY) - camera.position.y) * 0.036;

                    //camera.lookAt(scene.position);


                    // Find the screenspace position of the sun

                    screenSpacePosition.copy(sunPosition).project(camera);

                    screenSpacePosition.x = (screenSpacePosition.x + 1) / 2;
                    screenSpacePosition.y = (screenSpacePosition.y + 1) / 2;

                    // Give it to the god-ray and sun shaders

                    postprocessing_godrayGenUniforms["vSunPositionScreenSpace"].value.x = screenSpacePosition.x;
                    postprocessing_godrayGenUniforms["vSunPositionScreenSpace"].value.y = screenSpacePosition.y;

                    postprocessing_godraysFakeSunUniforms["vSunPositionScreenSpace"].value.x = screenSpacePosition.x;
                    postprocessing_godraysFakeSunUniforms["vSunPositionScreenSpace"].value.y = screenSpacePosition.y;

                    // -- Draw sky and sun --

                    // Clear colors and depths, will clear to sky color

                    renderer.clearTarget(postprocessing_rtTextureColors, true, true, false);

                    // Sun render. Runs a shader that gives a brightness based on the screen
                    // space distance to the sun. Not very efficient, so i make a scissor
                    // rectangle around the suns position to avoid rendering surrounding pixels.

                    var sunsqH = 0.74 * Native.window.Height; // 0.74 depends on extent of sun from shader
                    var sunsqW = 0.74 * Native.window.Height; // both depend on height because sun is aspect-corrected

                    screenSpacePosition.x *= Native.window.Width;
                    screenSpacePosition.y *= Native.window.Height;

                    renderer.setScissor(screenSpacePosition.x - sunsqW / 2, screenSpacePosition.y - sunsqH / 2, sunsqW, sunsqH);
                    renderer.enableScissorTest(true);

                    postprocessing_godraysFakeSunUniforms["fAspect"].value = Native.window.aspect;

                    postprocessing_scene.overrideMaterial = postprocessing_materialGodraysFakeSun;
                    renderer.render(postprocessing_scene, postprocessing_camera, postprocessing_rtTextureColors);

                    renderer.enableScissorTest(false);

                    // -- Draw scene objects --

                    // Colors

                    scene.overrideMaterial = null;
                    renderer.render(scene, camera, postprocessing_rtTextureColors);

                    // Depth

                    scene.overrideMaterial = materialDepth;
                    renderer.render(scene, camera, postprocessing_rtTextureDepth, true);

                    // -- Render god-rays --

                    // Maximum length of god-rays (in texture space [0,1]X[0,1])

                    var filterLen = 1.0;

                    // Samples taken by filter

                    var TAPS_PER_PASS = 6.0;

                    // Pass order could equivalently be 3,2,1 (instead of 1,2,3), which
                    // would start with a small filter support and grow to large. however
                    // the large-to-small order produces less objectionable aliasing artifacts that
                    // appear as a glimmer along the length of the beams

                    // pass 1 - render into first ping-pong target

                    var pass = 1.0;
                    var stepLen = filterLen * Math.Pow(TAPS_PER_PASS, -pass);

                    postprocessing_godrayGenUniforms["fStepSize"].value = stepLen;
                    postprocessing_godrayGenUniforms["tInput"].value = postprocessing_rtTextureDepth;

                    postprocessing_scene.overrideMaterial = postprocessing_materialGodraysGenerate;

                    renderer.render(postprocessing_scene, postprocessing_camera, postprocessing_rtTextureGodRays2);

                    // pass 2 - render into second ping-pong target

                    pass = 2.0;
                    stepLen = filterLen * Math.Pow(TAPS_PER_PASS, -pass);

                    postprocessing_godrayGenUniforms["fStepSize"].value = stepLen;
                    postprocessing_godrayGenUniforms["tInput"].value = postprocessing_rtTextureGodRays2;

                    renderer.render(postprocessing_scene, postprocessing_camera, postprocessing_rtTextureGodRays1);

                    // pass 3 - 1st RT

                    pass = 3.0;
                    stepLen = filterLen * Math.Pow(TAPS_PER_PASS, -pass);

                    postprocessing_godrayGenUniforms["fStepSize"].value = stepLen;
                    postprocessing_godrayGenUniforms["tInput"].value = postprocessing_rtTextureGodRays1;

                    renderer.render(postprocessing_scene, postprocessing_camera, postprocessing_rtTextureGodRays2);

                    // final pass - composite god-rays onto colors

                    postprocessing_godrayCombineUniforms["tColors"].value = postprocessing_rtTextureColors;
                    postprocessing_godrayCombineUniforms["tGodRays"].value = postprocessing_rtTextureGodRays2;

                    postprocessing_scene.overrideMaterial = postprocessing_materialGodraysCombine;

                    renderer.render(postprocessing_scene, postprocessing_camera);
                    postprocessing_scene.overrideMaterial = null;


                };



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

            var ze = new ZeProperties();

            ze.Show();

            ze.treeView1.Nodes.Clear();

            ze.Add(() => renderer);
            ze.Add(() => controls);
            ze.Add(() => scene);
            ze.Left = 0;


        }

    }
}
