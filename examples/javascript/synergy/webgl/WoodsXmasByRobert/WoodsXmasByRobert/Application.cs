using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WoodsXmasByRobert.Design;
//using WoodsXmasByRobert.Design.References;
using WoodsXmasByRobert.HTML.Images.FromAssets;
using WoodsXmasByRobert.HTML.Pages;
using ScriptCoreLib.Shared.Lambda;

namespace WoodsXmasByRobert
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page = null)
        {
            // ! start big, then resize to small





            var ScreenWidth = Native.Screen.width;
            var ScreenHeight = Native.Screen.height;

            Console.WriteLine(
                new { ScreenWidth, ScreenHeight, Native.window.Width, Native.window.Height }
            );


            #region workaround for ThreeJS/chrome webgl upscale bug
            // workaround for not knowing how to tell three js to upscale correctly..
            // X:\jsc.svn\examples\javascript\Test\TestNestedIFrameForMoreWidth\TestNestedIFrameForMoreWidth\Application.cs
            // instead of reloading full app
            // could we run part of it instead?
            // like let jsc know that this sub application should be reloadable?
            // this will be like threading
            // the outer code wil just stop doing anything
            // and the inner app will take over.

            var ApplyWorkaround = false;
            var location = "";

            try
            {
                location = Native.Document.location.href;
                //var pl = Native.Window.parent.document.location;

                if (Native.window.Width < Native.screen.width)
                    ApplyWorkaround = true;
            }
            catch
            {

            }

            if (ApplyWorkaround)
            {
                #region make sure the url looks different to make iframe actually load
                Native.window.parent.With(
                    parent =>
                    {
                        // http://stackoverflow.com/questions/5934538/is-there-a-limitation-on-an-iframe-containing-another-iframe-with-the-same-url

                        var parentlocation = "";

                        try
                        {
                            parentlocation = parent.document.location.href;
                        }
                        catch
                        {
                            // parent from another origin
                        }

                        Console.WriteLine(new { parentlocation });

                        if (parentlocation.TakeUntilIfAny("#") == location.TakeUntilIfAny("#"))
                        {
                            var withouthash = location.TakeUntilIfAny("#");
                            var onlyhash = location.SkipUntilOrEmpty("#");

                            withouthash += "?";

                            if (onlyhash != "")
                            {
                                withouthash += "#" + onlyhash;
                            }

                            location = withouthash;
                        }
                    }
                );
                #endregion

                #region ApplyWorkaround

                // this check only looks for default screen width
                // what about height and secondary screens?
                Console.WriteLine("will prepare... " + location);

                var iframe = new IHTMLIFrame
                {
                    frameBorder = "0",
                    allowFullScreen = true
                };

                iframe.style.minWidth = Native.Screen.width + "px";
                iframe.style.minHeight = Native.Screen.height + "px";

                iframe.style.position = IStyle.PositionEnum.absolute;
                iframe.style.left = "0px";
                iframe.style.top = "0px";
                iframe.style.width = "100%";
                iframe.style.height = "100%";

                Native.Document.body.Clear();
                Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

                Native.window.onmessage +=
                   e =>
                   {
                       Console.WriteLine("Native.Window.onmessage " + new { e.data });

                       // pure trickery :P
                       if ((string)e.data == "WoodsXmasByRobert.loaded")
                       {
                           iframe.style.minWidth = "";
                           iframe.style.minHeight = "";
                       }
                   };

                iframe.onload +=
                    delegate
                    {
                        if (iframe.src != location)
                            return;

                        Native.window.requestAnimationFrame +=
                          delegate
                          {
                              Console.WriteLine("reload done! " + new { location, iframe.src });
                              iframe.contentWindow.postMessage("ready yet?");
                          };
                    };

                Native.window.requestAnimationFrame +=
                    delegate
                    {
                        Console.WriteLine("will reload... " + location);
                        iframe.AttachToDocument();
                        iframe.src = location;
                    };
                #endregion


                return;
            }




            #endregion


            new AppReferences().With(
                References =>
                {

                    var source = new[]
                    {
                        References.Three,
                        //incompatible
                        // new THREELibrary.opensource.gihtub.three.js.build.three().Content,

                        References.Tween,
                        References.CopyShader,
                        References.FilmShader,
                        References.VignetteShader,
                        References.EffectComposer,
                        References.MaskPass,
                        References.RenderPass,
                        References.ShaderPass,
                        References.FilmPass,
                    };

                    var yield = source.ForEach(
                         (SourceScriptElement, i, MoveNext) =>
                         {
                             //Console.WriteLine("will load: " + SourceScriptElement.src);
                             SourceScriptElement.AttachToDocument().onload +=
                                 delegate
                                 {
                                     //Console.WriteLine("loaded: " + SourceScriptElement.src);
                                     MoveNext();
                                 };
                         }
                     );

                    yield(
                         delegate
                         {
                             //Console.WriteLine("will load WoodsXmasByRobert");

                             InitializeContent();
                         }
                     );

                }
            );

        }

        private static void InitializeContent()
        {
            Action __WoodsXmasByRobert_loaded = null;

            Console.WriteLine(
                new { Native.window.opener, Native.window.parent }
            );


            Native.window.parent.With(
                parent =>
                {
                    parent.postMessage("WoodsXmasByRobert.preparing");
                    Console.WriteLine("WoodsXmasByRobert.preparing");

                    __WoodsXmasByRobert_loaded = delegate
                    {
                        Console.WriteLine("will post WoodsXmasByRobert.loaded");

                        __WoodsXmasByRobert_loaded = null;

                        parent.postMessage("WoodsXmasByRobert.loaded");
                    };
                }
            );


            //<!-- Snow flakes -->

            new IHTMLScript { type = "x-shader/x-vertex", id = "vertexshader", innerText = new Shaders.particlesVertexShader().ToString() }.AttachToDocument();
            new IHTMLScript { type = "x-shader/x-fragment", id = "fragmentshader", innerText = new Shaders.particlesFragmentShader().ToString() }.AttachToDocument();

            var w = Native.window;

            dynamic window = w;



            // http://stackoverflow.com/questions/4923136/why-doesnt-firefox-support-mp3-file-format-in-audio
            //            Timestamp: 12/28/2012 1:22:05 PM
            //Warning: HTTP "Content-Type" of "audio/mpeg3" is not supported. Load of media resource http://192.168.1.100:27248/assets/WoodsXmasByRobert/unfiltered_mix.mp3 failed.
            //Source File: http://192.168.1.100:27248/
            //Line: 0




            #region snd
            var snd = new HTML.Audio.FromAssets.unfiltered_mix { volume = 0.9 };

            window.snd = snd;


            Native.window.onfocus +=
                delegate
                {
                    Console.WriteLine("WoodsXmasByRobert onfocus");
                    snd.volume = 0.9;
                };


            Native.window.onblur +=
                delegate
                {
                    Console.WriteLine("WoodsXmasByRobert onblur");
                    snd.volume = 0.1;

                    // if we are also not visible anymore
                    // and animations frame stop
                    // we should stop all sound
                };

            #endregion




            var canvas = new IHTMLCanvas();

            object webglRenderer_args = new object().With(
                 (dynamic a) =>
                 {
                     a.clearColor = 0x000000;
                     a.clearAlpha = 1.0;
                     a.preserveDrawingBuffer = true;
                     a.canvas = canvas;
                 }
             );

            var webglRenderer = new THREE.WebGLRenderer(
                webglRenderer_args
            );



            webglRenderer.autoClear = false;

            //var canvas = (IHTMLCanvas)webglRenderer.domElement;



            canvas.AttachToDocument();

            webglRenderer.setSize(Native.window.Width, Native.window.Height);

            var camera = new THREE.PerspectiveCamera(75, Native.window.Width / Native.window.Height, 1, 100000);

            camera.position.z = 0;
            camera.position.x = 0;
            camera.position.y = 0;

            window.camera = camera;

            var cameraTarget = new THREE.Vector3();
            cameraTarget.z = -400;
            camera.lookAt(cameraTarget);

            window.cameraTarget = cameraTarget;

            var loadingImage = THREE.ImageUtils.loadTexture(new loading().src);
            var map = THREE.ImageUtils.loadTexture(new snowflake().src);
            var starImage = THREE.ImageUtils.loadTexture(new flare().src);

            window.loadingImage = loadingImage;
            window.map = map;
            window.starImage = starImage;

            #region cursor
            var cursor = new pointer();

            cursor.style.zIndex = 0x1000;
            cursor.Hide();
            cursor.AttachToDocument();

            dynamic style = Native.Document.body.style;
            //http://stackoverflow.com/questions/7849002/how-can-i-set-the-hotspot-to-the-center-of-a-custom-cursor
            // http://stackoverflow.com/questions/5649608/custom-css-cursors
            style.cursor = "url(" + cursor.src + ") 16 16,pointer";
            #endregion




            var mouseXpercent = 0.5;
            var mouseYpercent = 0.5;

            #region onmousemove

            var CursorX = 0;
            var CursorY = 0;

            Native.Document.onmousemove +=
                e =>
                {
                    if (Native.Document.pointerLockElement == Native.Document.body)
                    {
                        cursor.Show();
                        CursorX += e.movementX;
                        CursorY += e.movementY;
                    }
                    else
                    {
                        cursor.Hide();
                        CursorX = e.CursorX;
                        CursorY = e.CursorY;
                    }

                    // keep cursor in view
                    CursorX = CursorX.Max(0).Min(Native.window.Width);
                    CursorY = CursorY.Max(0).Min(Native.window.Height);

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        cursor.style.SetLocation(CursorX - 16, CursorY - 16);
                    }

                    var windowHalfX = Native.window.Width >> 1;
                    var windowHalfY = Native.window.Height >> 1;

                    var mouseX = (CursorX - windowHalfX);
                    var mouseY = (CursorY - windowHalfY);

                    mouseXpercent = mouseX / windowHalfX;
                    mouseYpercent = mouseY / windowHalfY;

                    window.mouseXpercent = mouseXpercent;
                    window.mouseYpercent = mouseYpercent;
                };
            #endregion

            Native.Document.onmousedown +=
                e =>
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Right)
                        Native.Document.body.requestPointerLock();
                };


            new AppCode().Content.AttachToDocument().onload +=
                delegate
                {
                    // ScriptCoreLib should define this event!
                    snd.addEventListener(
                        "loadeddata",
                        new Action(
                            delegate
                            {
                                new IFunction("window.checkLoadingDone();").apply(Native.window);
                            }
                        )
                    );


                    var scene = (THREE.Scene)(object)window.scene;

                    scene.add(camera);


                    #region Cloud
                    {
                        object args = new object().With(
                            (dynamic a) =>
                            {
                                a.map = THREE.ImageUtils.loadTexture(new cloud().src);
                                a.transparent = true;
                                a.opacity = 0.17;
                                a.fog = false;
                            }
                        );

                        var cloudPlane = new THREE.PlaneGeometry(12500, 1880);
                        var cloud = new THREE.Mesh(cloudPlane, new THREE.MeshBasicMaterial(args));
                        cloud.position.set(300, 5350, -4450);
                        cloud.lookAt(camera.position);
                        scene.add(cloud);
                        window.cloud = cloud;
                    }
                    #endregion

                    #region Sky
                    {
                        object args = new object().With(
                            (dynamic a) =>
                            {
                                a.map = THREE.ImageUtils.loadTexture(new sky().src);
                                a.opacity = 0.57;
                                a.fog = false;
                            }
                        );

                        var skyPlane = new THREE.PlaneGeometry(9000, 6000);
                        var sky = new THREE.Mesh(skyPlane, new THREE.MeshBasicMaterial(args));

                        sky.scale.set(4, 2.5, 2.5);
                        sky.position.set(0, 7500, -6000);
                        sky.lookAt(camera.position);
                        scene.add(sky);
                        window.sky = sky;
                    }
                    #endregion

                    #region moon
                    {
                        dynamic moon_material_args = new object();

                        moon_material_args.map = THREE.ImageUtils.loadTexture(new moon().src);
                        moon_material_args.transparent = true;
                        moon_material_args.opacity = 0.3;
                        moon_material_args.fog = false;
                        moon_material_args.blending = THREE.AdditiveBlending;

                        var moonPlane = new THREE.PlaneGeometry(1000, 1000);
                        var moon = new THREE.Mesh(moonPlane,
                            new THREE.MeshBasicMaterial(
                                (object)moon_material_args
                            )
                        );




                        moon.position.set(300, 4300, -4600);
                        moon.lookAt(camera.position);
                        scene.add(moon);
                        window.moon = moon;
                    }
                    #endregion






                    #region subtitleArray
                    var subtitleArray = (IArray<THREE.Mesh>)(object)window.subtitleArray;

                    var textPlane = new THREE.PlaneGeometry(512, 80);

                    new SubtitlesImages().Images.WithEach(
                        i =>
                        {

                            object args = new object().With(
                              (dynamic a) =>
                              {
                                  a.map = THREE.ImageUtils.loadTexture(i.src);
                                  a.transparent = true;
                                  a.depthTest = false;
                              }
                          );


                            var sub = new THREE.Mesh(textPlane, new THREE.MeshBasicMaterial(args));
                            sub.position.z = -800;
                            sub.position.y = -550;
                            sub.visible = false;
                            camera.add(sub);
                            subtitleArray.push(sub);
                        }
                     );

                    {
                        var endPlane = new THREE.PlaneGeometry(500, 100);

                        object args = new object().With(
                            (dynamic a) =>
                            {
                                a.map = THREE.ImageUtils.loadTexture(new xmas().src);
                                a.transparent = true;
                                a.opacity = 1.0;
                                a.depthTest = false;
                            }
                        );


                        var end = new THREE.Mesh(endPlane, new THREE.MeshBasicMaterial(args));
                        end.position.z = -400;
                        end.position.y = 100;
                        end.visible = false;
                        camera.add(end);
                        subtitleArray.push(end);

                        window.end = end;
                    }


                    new IFunction("window.setupSubtitles();").apply(Native.window);
                    #endregion

                    var particles = (THREE.ParticleSystem)(object)window.particles;
                    var bgSprite = (THREE.Sprite)(object)window.bgSprite;
                    var loadingSprite = (THREE.Sprite)(object)window.loadingSprite;
                    var pointLight = (THREE.PointLight)(object)window.pointLight;
                    var treeArray = (IArray<THREE.Mesh>)(object)window.treeArray;
                    var rockArray = (IArray<THREE.Mesh>)(object)window.rockArray;
                    var flowerArray = (IArray<THREE.Mesh>)(object)window.flowerArray;


                    var groundMesh1 = (THREE.Mesh)(object)window.groundMesh1;
                    var groundMesh2 = (THREE.Mesh)(object)window.groundMesh2;


                    var renderModel = (object)window.renderModel;
                    var effectFilm = (object)window.effectFilm;
                    var effectVignette = (object)window.effectVignette;
                    var effectCopy = (object)window.effectCopy;


                    var composer = new THREE.EffectComposer(webglRenderer);
                    composer.addPass(renderModel);
                    composer.addPass(effectFilm);
                    composer.addPass(effectVignette);
                    composer.addPass(effectCopy);





                    var speedEffector_value = (int)new IFunction("return window.speedEffector.value;").apply(Native.window);

                    #region load
                    Action<string, Action<object>> load =
                        (src, yield) =>
                        {
                            new THREE.JSONLoader().load(
                               src,
                               IFunction.OfDelegate(yield)
                           );
                        };
                    #endregion

                    #region sled
                    load(
                        new WoodsXmasByRobert.Design.models.sleigh().Content.src,
                        geometry =>
                        {
                            Console.WriteLine("got sled!");

                            var sled = new THREE.Mesh(
                                geometry,
                                new THREE.MeshFaceMaterial()
                            );

                            var scale = 4;
                            sled.scale.set(scale, scale, scale);

                            sled.rotation.y = -Math.PI / 2;
                            sled.position.y = -290;
                            sled.position.z = -80;

                            scene.add(sled);

                            window.sled = sled;

                            new IFunction("window.checkLoadingDone();").apply(Native.window);
                        }
                    );
                    #endregion


                    var random = new Random();

                    #region treeDead
                    load(
                        new WoodsXmasByRobert.Design.models.treeDead().Content.src,
                        tree1Geo =>
                        {
                            Console.WriteLine("got treeDead!");

                            load(
                                 new WoodsXmasByRobert.Design.models.treeEvergreenHigh().Content.src,
                                 tree2Geo =>
                                 {
                                     Console.WriteLine("got treeEvergreenHigh!");

                                     var gridSize = 500;

                                     for (var x = 0; x < 8; x++)
                                     {
                                         for (var z = 0; z < 12; z++)
                                         {
                                             var geo = tree2Geo;
                                             if (random.NextDouble() < 0.25)
                                                 if (x != 0 && x != 7)
                                                 {
                                                     geo = tree1Geo;
                                                 }

                                             var mesh = new THREE.Mesh(geo, new THREE.MeshFaceMaterial());
                                             var scale = 1.2 + random.NextDouble();
                                             mesh.scale.set(scale, scale * 2, scale);

                                             var posx = 0.0;

                                             if (x < 4)
                                             {
                                                 posx = (x * gridSize) - (gridSize * 4) - 100 + random.NextDouble() * 100 - 50;
                                             }
                                             else
                                             {
                                                 posx = (x * gridSize) - 1400 + random.NextDouble() * 100 - 50;
                                             };

                                             var posz = -(z * gridSize) + random.NextDouble() * 100 - 50;

                                             mesh.position.set(posx, -400 - (random.NextDouble() * 80), posz);

                                             mesh.rotation.set((random.NextDouble() * 0.2) - 0.1, random.NextDouble() * Math.PI, (random.NextDouble() * 0.2) - 0.1);

                                             scene.add(mesh);

                                             treeArray.push(mesh);

                                         }
                                     }

                                     new IFunction("window.checkLoadingDone();").apply(Native.window);
                                 }
                             );
                        }
                    );
                    #endregion


                    #region bird
                    load(
                        new WoodsXmasByRobert.Design.models.eagle().Content.src,
                        geometry =>
                        {
                            Console.WriteLine("got bird!");

                            dynamic args = new object();

                            args.color = 0x000000;
                            args.morphTargets = true;
                            args.fog = false;

                            var bird = new THREE.MorphAnimMesh(
                                geometry,
                                new THREE.MeshBasicMaterial(
                                    (object)args
                                )
                            );

                            bird.duration = 1000;

                            bird.scale.set(4, 4, 4);
                            bird.rotation.y = Math.PI;
                            bird.position.set(0, 3000, -1500);

                            scene.add(bird);


                            window.bird = bird;

                            new IFunction("window.checkLoadingDone();").apply(Native.window);
                        }
                    );
                    #endregion


                    #region rock
                    load(
                        new WoodsXmasByRobert.Design.models.rock().Content.src,
                        geometry =>
                        {
                            Console.WriteLine("got rock!");

                            var numOfRocks = 25;
                            for (var i = 0; i < numOfRocks; ++i)
                            {
                                dynamic args = new object();

                                args.color = 0x444444;

                                var mesh = new THREE.Mesh(geometry, new THREE.MeshLambertMaterial((object)args));

                                var scale = 1 + (random.NextDouble() * 0.5);

                                mesh.scale.set(scale, scale, scale);
                                mesh.rotation.set(0, random.NextDouble() * Math.PI, 0);
                                mesh.position.set((random.NextDouble() * 4000) - 2000, -400, (random.NextDouble() * 6000) - 6000);

                                if (mesh.position.x < 45)
                                    if (mesh.position.x > 0)
                                    {
                                        mesh.position.x += 450;
                                    }

                                if (mesh.position.x > -450)
                                    if (mesh.position.x < 0)
                                    {
                                        mesh.position.x -= 450;
                                    }

                                scene.add(mesh);

                                rockArray.push(mesh);
                            }


                            new IFunction("window.checkLoadingDone();").apply(Native.window);
                        }
                    );
                    #endregion

                    #region horse
                    load(
                        new WoodsXmasByRobert.Design.models.horse().Content.src,
                        geometry =>
                        {
                            Console.WriteLine("got horse!");

                            dynamic horse_material_args = new object();

                            horse_material_args.color = 0x090601;
                            horse_material_args.morphTargets = true;

                            var horse = new THREE.MorphAnimMesh(geometry,
                                new THREE.MeshLambertMaterial(
                                    (object)horse_material_args
                                )
                            );

                            horse.duration = 1000;

                            horse.scale.set(2.5, 1.8, 2);
                            horse.rotation.y = Math.PI;
                            horse.position.set(0, -350, -700);

                            scene.add(horse);
                            window.horse = horse;

                            //checkLoadingDone();

                            // Handles
                            var plane = new THREE.PlaneGeometry(700, 10, 40, 1);

                            var l = Math.Floor(plane.vertices.Length / 2.0);

                            for (var i = 0; i < l; i++)
                            {

                                var offset = Math.Sin(i / 14) * 100;

                                plane.vertices[i].y -= offset;
                                plane.vertices[i + 41].y -= offset;


                                plane.vertices[i].z -= (i / 5) + (offset * -1) / 8;
                                plane.vertices[i + 41].z += (i / 5) - (offset * -1) / 8;

                            }

                            dynamic material_args = new object();

                            material_args.color = 0x090601;
                            material_args.side = THREE.DoubleSide;

                            var material = new THREE.MeshBasicMaterial(
                                (object)material_args
                            );

                            var leftHandle = new THREE.Mesh(plane, material);
                            leftHandle.position.y = -120;
                            leftHandle.position.z = -350;
                            leftHandle.position.x = -30;
                            leftHandle.rotation.y = -(Math.PI / 2) + 0.075;
                            leftHandle.rotation.x = Math.PI * 2 - 0.075;

                            scene.add(leftHandle);
                            window.leftHandle = leftHandle;

                            var rightHandle = new THREE.Mesh(plane, material);
                            rightHandle.position.y = -120;
                            rightHandle.position.z = -350;
                            rightHandle.position.x = 30;
                            rightHandle.rotation.y = -(Math.PI / 2) - 0.075;
                            rightHandle.scale.z = -1;
                            rightHandle.rotation.x = Math.PI * 2 - 0.075;
                            scene.add(rightHandle);
                            window.rightHandle = rightHandle;

                            new IFunction("window.checkLoadingDone();").apply(Native.window);
                        }
                    );
                    #endregion


                    #region flowerLoaded
                    Action<object, bool> flowerLoaded = (geometry, halfScale) =>
                    {

                        var numOfFlowers = 20;

                        var half = Math.Floor(numOfFlowers / 2.0);

                        dynamic args = new object();

                        args.color = 0x444444;


                        for (var i = 0; i < half; ++i)
                        {
                            var mesh = new THREE.Mesh(geometry,
                                new THREE.MeshLambertMaterial(
                                    (object)args
                                )
                            );

                            var scale = 1 + (random.NextDouble() * 1);
                            if (halfScale)
                            {
                                scale *= 0.6;
                            }
                            mesh.scale.set(scale, scale, scale);
                            mesh.rotation.set((random.NextDouble() * 0.6) - 0.3, random.NextDouble() * Math.PI, (random.NextDouble() * 0.6) - 0.3);
                            mesh.position.set((random.NextDouble() * 1000) - 500, -310, (random.NextDouble() * 6000) - 6000);

                            if (mesh.position.x < 100)
                                if (mesh.position.x > 0)
                                {
                                    mesh.position.x += 100;
                                }

                            if (mesh.position.x > -100)
                                if (mesh.position.x < 0)
                                {
                                    mesh.position.x -= 100;
                                }

                            scene.add(mesh);

                            flowerArray.push(mesh);
                        }

                        new IFunction("window.checkLoadingDone();").apply(Native.window);
                    };
                    #endregion


                    #region weeds01
                    load(
                        new WoodsXmasByRobert.Design.models.weeds01().Content.src,
                        geometry =>
                        {
                            Console.WriteLine("got weeds01!");

                            flowerLoaded(geometry, false);
                        }
                    );
                    #endregion

                    #region glowbulb
                    load(
                        new WoodsXmasByRobert.Design.models.glowbulb().Content.src,
                        geometry =>
                        {
                            Console.WriteLine("got glowbulb!");

                            flowerLoaded(geometry, true);
                        }
                    );
                    #endregion


                    #region run
                    Action<double> run =
                        delta =>
                        {
                            // trees
                            var mesh = default(THREE.Mesh);

                            for (var i = 0; i < treeArray.length; ++i)
                            {

                                mesh = treeArray[i];

                                // respawn
                                if (mesh.position.z > camera.position.z + 700)
                                {
                                    mesh.position.z -= 6000;

                                    var scale = 1.2 + random.NextDouble();
                                    mesh.scale.set(scale, scale * 2, scale);
                                }

                            }

                            // rocks
                            for (var i = 0; i < rockArray.length; ++i)
                            {

                                mesh = rockArray[i];

                                // respawn
                                if (mesh.position.z > camera.position.z + 400)
                                {
                                    mesh.position.z -= 6000;
                                }

                            }

                            // flowers
                            for (var i = 0; i < flowerArray.length; ++i)
                            {

                                mesh = flowerArray[i];

                                // respawn
                                if (mesh.position.z > camera.position.z + 400)
                                {
                                    mesh.position.z -= 6000;
                                }

                            }

                            // ground respawn
                            if (groundMesh1.position.z - 10000 > camera.position.z)
                            {
                                groundMesh1.position.z -= 40000;
                            }
                            if (groundMesh2.position.z - 10000 > camera.position.z)
                            {
                                groundMesh2.position.z -= 40000;
                            }
                        };
                    #endregion

                    var oldTime = new IDate().getTime();

                    var r = 0.0;

                    bool disableNextFrame = false;

                    #region loop
                    Action loop = delegate
                    {
                        var allLoaded = (bool)(object)window.allLoaded;

                        var horse = (THREE.MorphAnimMesh)(object)window.horse;
                        var bird = (THREE.MorphAnimMesh)(object)window.bird;

                        var leftHandle = (THREE.Mesh)(object)window.leftHandle;
                        var rightHandle = (THREE.Mesh)(object)window.rightHandle;

                        var moon = (THREE.Mesh)(object)window.moon;
                        var cloud = (THREE.Mesh)(object)window.cloud;
                        var sky = (THREE.Mesh)(object)window.sky;
                        var sled = (THREE.Mesh)(object)window.sled;

                        //new IFunction("this.loop();").apply(Native.Window);

                        var time = new IDate().getTime();
                        var delta = time - oldTime;
                        oldTime = time;

                        if (double.IsNaN(delta))
                        {
                            delta = 1000 / 60;
                        }

                        var maxSpeed = delta / 2;

                        if (allLoaded)
                        {

                            r += delta / 2000;
                            run(delta);

                            var noise = random.NextDouble() * 2;

                            camera.position.x = (50 * Math.Cos(r * 2)) * speedEffector_value;
                            camera.position.y = (2 * Math.Sin(r * 12) - 100) * speedEffector_value;

                            camera.up.x = (Math.Sin(r * 12) / 50) * speedEffector_value;

                            cameraTarget.y += (((camera.position.y + 80) + noise - mouseYpercent * 120) - cameraTarget.y) / 20;
                            cameraTarget.x += (mouseXpercent * 400 - cameraTarget.x) / 20;

                            var speed = (delta / 2) * speedEffector_value;

                            camera.position.z -= speed;
                            cameraTarget.z -= speed;
                            camera.lookAt(cameraTarget);

                            pointLight.position.z -= speed;

                            if (moon != null)
                            {
                                moon.position.z -= speed;
                            }

                            if (cloud != null)
                            {
                                cloud.position.z -= speed;
                            }

                            if (sky != null)
                            {
                                sky.position.z -= speed;
                            }

                            #region sled
                            if (sled != null)
                            {
                                sled.position.z -= speed;
                                sled.position.x = camera.position.x;
                            }
                            #endregion


                            #region bird
                            if (bird != null)
                            {
                                bird.position.x = 200 * Math.Cos(r) + ((bird.position.z - camera.position.z) / 10);
                                bird.position.y = 4000 + (Math.Sin(r) * 300);
                                bird.position.z -= maxSpeed * 1.25;

                                if (bird.position.z < camera.position.z - 10000)
                                {
                                    bird.position.z = camera.position.z - 500;
                                }

                                bird.updateAnimation(delta);
                            }
                            #endregion


                            #region horse
                            if (horse != null)
                            {
                                horse.position.z -= speed;
                                horse.position.x = camera.position.x;

                                horse.updateAnimation(speed * 2);

                                leftHandle.position.z -= speed;
                                leftHandle.position.x = camera.position.x - 37;

                                rightHandle.position.z -= speed;
                                rightHandle.position.x = camera.position.x + 40;

                                leftHandle.scale.y = 1 - Math.Abs(Math.Sin(camera.position.z / 150)) / 4;
                                rightHandle.scale.y = leftHandle.scale.y;
                            }
                            #endregion

                            particles.position.z -= speed;

                            new IFunction("e", "window.uniforms.globalTime.value += e;").apply(Native.window, delta * 0.00015);
                            new IFunction("e", "window.uniforms.speed.value = e;").apply(Native.window, speed / maxSpeed);

                            new IFunction("window.runSubtitles();").apply(Native.window);


                            //disableNextFrame = true;

                            if (__WoodsXmasByRobert_loaded != null)
                                __WoodsXmasByRobert_loaded();
                        }
                        else
                        {
                            if (loadingSprite != null)
                            {
                                loadingSprite.position.set(Native.window.Width >> 1, Native.window.Height >> 1, 0);
                                loadingSprite.rotation -= 0.08;
                            }
                        }

                        if (bgSprite != null)
                        {
                            bgSprite.position.set(Native.window.Width >> 1, Native.window.Height >> 1, 0);
                        }


                        new IFunction("window.TWEEN.update();").apply(Native.window);

                        //if (has_gl)
                        {
                            webglRenderer.clear();
                            composer.render(0.01);
                        }
                    };
                    #endregion


                    #region animate
                    Native.window.onframe += delegate
                    {
                        if (disableNextFrame)
                            return;

                        loop();
                    };

                    #endregion




                    Action AtResize = delegate
                    {

                        // notify the renderer of the size change
                        webglRenderer.setSize(Native.window.Width, Native.window.Height);

                        // update the camera
                        camera.aspect = Native.window.Width / Native.window.Height;
                        camera.updateProjectionMatrix();
                    };


                    #region onresize
                    Native.window.onresize +=
                        delegate
                        {
                            AtResize();
                        };
                    #endregion

                    AtResize();


                };



            Native.Document.body.ondblclick +=
                delegate
                {
                    Native.Document.body.requestFullscreen();
                };


            Native.Document.body.onmousedown +=
                 e =>
                 {
                     if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                     {
                         if (Native.Document.pointerLockElement == Native.Document.body)
                         {
                             // cant requestFullscreen while pointerLockElement
                             Console.WriteLine("exitPointerLock");
                             Native.Document.exitPointerLock();
                             Native.Document.exitFullscreen();
                             return;
                         }

                         Native.Document.body.requestFullscreen();
                         Native.Document.body.requestPointerLock();
                     }
                 };

            Native.Document.oncontextmenu +=
                e =>
                {
                    e.preventDefault();
                };


        }


    }
}
