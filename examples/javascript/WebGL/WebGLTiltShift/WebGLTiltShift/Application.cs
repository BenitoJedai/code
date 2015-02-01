using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using WebGLTiltShift.HTML.Pages;
using WebGLTiltShift.Design;
//using THREE = WebGLTiltShift.Design.THREE;
using System.Threading.Tasks;

namespace WebGLTiltShift
{
    using ScriptCoreLib.JavaScript.DOM.SVG;
    using System.Diagnostics;
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // inspired by http://alteredqualia.com/three/examples/webgl_terrain_dynamic.html
        // http://alteredqualia.com/three/examples/webgl_morphtargets_md2_control.html

        // Invalid version number in manifest: 40. Please make sure the newly uploaded package has a larger version in file manifest.json than the published package: 40.

        public Application(IDefault page = null)
        {
            // vr view, isometric view
            // http://skycraft.io/
            // http://www.mark-lundin.com/box/inception/

            // running as android tab
            //            I / chromium(25513): [INFO: CONSOLE(88580)] "THREE.WebGLRenderer", source: http://192.168.43.7:25796/view-source (88580)
            //I/chromium(25513): [INFO: CONSOLE(88791)] "Error creating WebGL context.", source: http://192.168.43.7:25796/view-source (88791)
            //I/chromium(25513): [INFO: CONSOLE(88795)] "Uncaught TypeError: Cannot read property 'getShaderPrecisionFormat' of null", source: http://192.168.43.7:25796/view-source (88795)

            // X:\opensource\github\three.js
            // WEBGL_compressed_texture_pvrtc extension not supported.

            // http://stackoverflow.com/questions/21673278/three-js-error-when-applying-texture

            //#if !DEBUG
            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;

            // I/chromium(18051): [INFO:CONSOLE(36608)] "Uncaught TypeError: Cannot read property 'socket' of undefined", source: http://192.168.43.7:24445/view-source (36608)

            if ((object)self_chrome != null)
            {
                object self_chrome_socket = self_chrome.socket;

                if (self_chrome_socket != null)
                {
                    // could we change the color of the window?

                    // https://developer.chrome.com/apps/manifest/icons
                    chrome.Notification.DefaultIconUrl = new WebGLHZBlendCharacter.HTML.Images.FromAssets.x128().src;

                    Console.WriteLine("invoke TheServerWithAppWindow.Invoke");
                    ChromeTCPServer.TheServerWithAppWindow.Invoke(WebGLTiltShift.HTML.Pages.DefaultSource.Text);

                    return;
                }

            }
            #endregion
            //#endif

            Native.document.body.style.margin = "0px";
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.document.body.Clear();




            #region svg cursor
            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                 new HeatZeekerRTS.HTML.Images.FromAssets.MyCursor().src,
                 r =>
                 {
                     // public static XElement AsXElement(this IElement e);
                     var svg = (ISVGSVGElement)(IElement)(r.responseXML.documentElement);


                     var cursor1 = svg.AsXElement();




                     cursor1
                         .Elements("g")
                         .Elements("path")
                         .Where(x => x.Attribute("id").Value == "path2985")
                         .WithEach(
                             path =>
                                 path.Attribute("style").Value = path.Attribute("style").Value.Replace("fill:#ffff00;", "fill:#00ff00;")
                         );

                     cursor1
                         .Elements("g")
                         .Elements("path")
                         .Where(x => x.Attribute("id").Value == "path2985-1")
                         .WithEach(
                             path =>
                                 path.Attribute("style").Value = path.Attribute("style").Value.Replace("fill:#d9d900;", "fill:#00df00;")
                         );

                     //.AttachToDocument();

                     Native.css.style.cursorImage = svg;


                     // this wont work no more?
                     new IStyle(Native.css[IHTMLElement.HTMLElementEnum.div].hover)
                     {
                         // last change was abut adding pointer
                         // jsc jit could atleast let us know how it looks like
                         //cursor = IStyle.CursorEnum.pointer

                         //cursorImage = new MyCursor()
                     };

                     //Native.document.documentElement.style.cursorImage = svg;
                     //Native.document.documentElement.style.cursorImage = cursor1;
                     //Native.document.documentElement.style.cursorElement = cursor1;
                     //Native.document.documentElement.style.cursorElement = cursor1.AsHTMLElement();

                     //public static IHTMLElement AttachToDocument(this XElement e);

                     //.AttachToHead();
                 }
            );
            #endregion


            //#region playmusic
            //new { }.With(
            //    async delegate
            //    {
            //        do
            //        {
            //            var music = new HeatZeekerRTS.HTML.Audio.FromAssets.crickets
            //            {
            //                volume = 0.1

            //                //loop = true,
            //                //controls = true
            //            }.AttachToHead();

            //            music.play();

            //            await music.async.onended;

            //            music.Orphanize();
            //        }
            //        while (true);
            //    }
            //);

            //#endregion


            double SCREEN_WIDTH = Native.window.Width;
            double SCREEN_HEIGHT = Native.window.Height;

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

            var scene = new THREE.Scene();
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


            var renderer = new THREE.WebGLRenderer(
                new
                {

                    // http://stackoverflow.com/questions/20495302/transparent-background-with-three-js
                    alpha = true,
                    preserveDrawingBuffer = true,
                    antialias = true
                }

                );
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.domElement.AttachToDocument();
            renderer.shadowMapEnabled = true;
            renderer.shadowMapType = THREE.PCFSoftShadowMap;


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

            }
            #endregion



            // "X:\opensource\github\three.js\examples\js\shaders\VerticalTiltShiftShader.js"
            // http://stackoverflow.com/questions/20899326/how-do-i-stop-effectcomposer-from-destroying-my-transparent-background

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

            var composer = new THREE.EffectComposer(renderer, renderTarget);
            var renderModel = new THREE.RenderPass(scene, camera);
            composer.addPass(renderModel);

            #region vblur
            var hblur = new THREE.ShaderPass(THREE.HorizontalTiltShiftShader);
            var vblur = new THREE.ShaderPass(THREE.VerticalTiltShiftShader);

            //var bluriness = 6.0;
            var bluriness = 4.0;

            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS0656  Missing compiler required member 'Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create' WebGLTiltShift Application.cs  183

            (hblur.uniforms as dynamic).h.value = bluriness / Native.window.Width;
            (vblur.uniforms as dynamic).v.value = bluriness / Native.window.Height;

            (hblur.uniforms as dynamic).r.value = 0.5;
            (vblur.uniforms as dynamic).r.value = 0.5;
            vblur.renderToScreen = true;

            composer.addPass(hblur);
            composer.addPass(vblur);
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



            #region YomotsuTPS

            var material = new THREE.MeshPhongMaterial(
                          new
                          {
                              map = THREE.ImageUtils.loadTexture(
                                  new WebGLYomotsuTPS.HTML.Images.FromAssets._1().src
                              ),
                              ambient = 0x999999,
                              color = 0xffffff,
                              specular = 0xffffff,
                              shininess = 25,
                              morphTargets = true
                          }
                      );




            var loader = new THREE.JSONLoader();
            var md2frames = new WebGLYomotsuTPS.Application.md2frames();

            var moveState_moving = false;
            var moveState_front = false;
            var moveState_Backwards = false;
            var moveState_left = false;
            var moveState_right = false;
            var moveState_speed = 4.0;
            var moveState_angle = 0.0;



            var player_motion = default(WebGLYomotsuTPS.Application.motion);
            loader.load(
                new global::WebGLYomotsuTPS.Design.droid().Content.src,
                    IFunction.OfDelegate(
                        new Action<object>(
                            xgeometry =>
                            {
                                var md2meshBody = new THREE.MorphAnimMesh(xgeometry, material);

                                // raise it up
                                md2meshBody.position.y = 100;

                                md2meshBody.rotation.y = (float)(-Math.PI / 2);
                                //md2meshBody.scale.set(.02, .02, .02);
                                md2meshBody.scale.set(4, 4, 4);
                                md2meshBody.castShadow = true;
                                //md2meshBody.receiveShadow = false;

                                #region player_motion
                                Action<WebGLYomotsuTPS.Application.motion> player_changeMotion = motion =>
                                {
                                    //Console.WriteLine(
                                    //    new { motion, md2frames.run, md2frames.stand });

                                    player_motion = motion;

                                    //    player.state = md2frames[motion][3].state;

                                    var animMin = motion.min;
                                    var animMax = motion.max;
                                    var animFps = motion.fps;

                                    md2meshBody.time = 0;
                                    md2meshBody.duration = (int)(
                                        1000 * ((animMax - animMin) / (double)animFps)
                                    );
                                    Native.document.title = new { animMin, animMax }.ToString();

                                    md2meshBody.setFrameRange(animMin, animMax);
                                };

                                player_changeMotion(md2frames.stand);
                                #endregion


                                #region move
                                Action move = delegate
                                {
                                    //            if(player.model.motion !== 'run' && player.model.state === 'stand'){

                                    //    changeMotion('run');

                                    //}

                                    //if(player.model.motion !== 'crwalk' && player.model.state === 'crstand'){

                                    //    changeMotion('crwalk');

                                    //}

                                    var speed = moveState_speed;

                                    //if(player.model.state === 'crstand'){speed *= .5;}

                                    //if(player.model.state === 'freeze') {speed *= 0;}



                                    var direction = moveState_angle;

                                    if (moveState_front && !moveState_left && !moveState_Backwards && !moveState_right) { direction += 0; }
                                    if (moveState_front && moveState_left && !moveState_Backwards && !moveState_right) { direction += 45; }
                                    if (!moveState_front && moveState_left && !moveState_Backwards && !moveState_right) { direction += 90; }
                                    if (!moveState_front && moveState_left && moveState_Backwards && !moveState_right) { direction += 135; }
                                    if (!moveState_front && !moveState_left && moveState_Backwards && !moveState_right) { direction += 180; }
                                    if (!moveState_front && !moveState_left && moveState_Backwards && moveState_right) { direction += 225; }
                                    if (!moveState_front && !moveState_left && !moveState_Backwards && moveState_right) { direction += 270; }
                                    if (moveState_front && !moveState_left && !moveState_Backwards && moveState_right) { direction += 315; }



                                    md2meshBody.rotation.y = (float)((direction - 90) * Math.PI / 180);

                                    md2meshBody.position.x -= (float)(Math.Sin(direction * Math.PI / 180.0) * speed * 10);
                                    md2meshBody.position.z -= (float)(Math.Cos(direction * Math.PI / 180.0) * speed * 10);

                                };
                                #endregion


                                //scene.add(player_model_objects);
                                //player_model_objects.add(md2meshBody);
                                scene.add(md2meshBody);

                                #region onkeydown
                                Native.document.onkeydown +=
                                    e =>
                                    {
                                        if (e.KeyCode == 67)
                                        {
                                            if (player_motion == md2frames.stand)
                                                player_changeMotion(md2frames.crstand);
                                            else if (player_motion == md2frames.crstand)
                                                player_changeMotion(md2frames.stand);

                                        }
                                        else if (e.KeyCode == 87)
                                        {
                                            moveState_front = true;
                                            moveState_Backwards = false;
                                        }
                                        else if (e.KeyCode == 83)
                                        {
                                            moveState_front = false;
                                            moveState_Backwards = true;
                                        }
                                        else if (e.KeyCode == 65)
                                        {
                                            moveState_left = true;
                                            moveState_right = false;
                                        }
                                        else if (e.KeyCode == 68)
                                        {
                                            moveState_left = false;
                                            moveState_right = true;
                                        }

                                        var isStand = player_motion == md2frames.stand;
                                        Console.WriteLine(
                                            new { e.KeyCode, moveState_front, moveState_Backwards, isStand }
                                            );

                                        if (moveState_front || moveState_Backwards || moveState_left || moveState_right)
                                            if (player_motion == md2frames.stand)
                                                player_changeMotion(md2frames.run);
                                            else if (player_motion == md2frames.crstand)
                                                player_changeMotion(md2frames.crwalk);
                                    };
                                #endregion

                                #region onkeyup
                                Native.document.onkeyup +=
                                    e =>
                                    {
                                        if (e.KeyCode == 87)
                                        {
                                            moveState_front = false;
                                        }
                                        else if (e.KeyCode == 83)
                                        {
                                            moveState_Backwards = false;
                                        }
                                        else if (e.KeyCode == 65)
                                        {
                                            moveState_left = false;
                                        }
                                        else if (e.KeyCode == 68)
                                        {
                                            moveState_right = false;
                                        }

                                    };
                                #endregion




                                #region loop

                                var yclock = Stopwatch.StartNew();

                                Native.window.onframe += delegate
                                {
                                    if (moveState_front || moveState_Backwards || moveState_left || moveState_right)
                                    {
                                        move();
                                    }
                                    else
                                        if (player_motion == md2frames.run)
                                        player_changeMotion(md2frames.stand);
                                    else if (player_motion == md2frames.crwalk)
                                        player_changeMotion(md2frames.crstand);



                                    //player_model_objects.position.x = player_position_x;
                                    //player_model_objects.position.y = player_position_y;
                                    //player_model_objects.position.z = player_position_z;


                                    #region model animation

                                    var delta = yclock.ElapsedMilliseconds * 0.001;
                                    yclock.Restart();

                                    var isEndFleame = (player_motion.max == md2meshBody.currentKeyframe);
                                    var isAction = player_motion.action;

                                    var x = (isAction && !isEndFleame);

                                    if (!isAction || x)
                                    {
                                        md2meshBody.updateAnimation(1000 * delta);
                                    }
                                    else if (player_motion.state == "freeze")
                                    {
                                        //dead...
                                    }
                                    else
                                    {
                                        player_changeMotion(player_motion);
                                    }
                                    #endregion

                                    //renderer.render(scene, camera);


                                };

                                #endregion
                            }
                        )));


            #endregion


            Console.WriteLine("? awaiting to go fullscreen");

            Action requestFullscreen = Native.document.body.requestFullscreen;
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs

            // webview virtual dispatch
            Native.window.onmessage +=
                e =>
                {
                    //await contentWindow.postMessageAsync("virtual webview.requestFullscreen");
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeWebviewFullscreen\ChromeWebviewFullscreen\Application.cs

                    if (e.data == "virtual webview.requestFullscreen")
                    {
                        requestFullscreen = delegate
                        {
                            Console.WriteLine("requestFullscreen");
                            //e.ports.

                            //e.ports.WithEach
                            e.postMessage("requestFullscreen");
                        };

                        return;
                    }

                    // ???
                    Native.document.body.innerText = new { e.data }.ToString();

                    //await contentWindow.postMessageAsync("virtual webview.requestFullscreen");
                };


            #region onkeyup
            Native.document.onkeyup +=
                e =>
                {
                    if (e.KeyCode == (int)System.Windows.Forms.Keys.F)
                    {
                        requestFullscreen();
                    }

                };
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

                    composer.render(0.1);
                };



            #region onresize
            new { }.With(
                async delegate
                {
                    //while (true)
                    do
                    {
                        // wont really work yet?
                        renderer.setSize(Native.window.Width, Native.window.Height);
                        renderTarget.setSize(Native.window.Width, Native.window.Height);
                        composer.setSize(Native.window.Width, Native.window.Height);

                        (hblur.uniforms as dynamic).h.value = bluriness / Native.window.Width;
                        (vblur.uniforms as dynamic).v.value = bluriness / Native.window.Height;


                        camera.aspect = Native.window.aspect;
                        camera.updateProjectionMatrix();

                        // convert to bool?
                    } while (await Native.window.async.onresize);
                    //} while (await Native.window.async.onresize != null);

                }
            );
            #endregion


        }


    }
}
