using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WoodsXmasByRobert.Design;
using WoodsXmasByRobert.Design.References;
using WoodsXmasByRobert.HTML.Images.FromAssets;
using WoodsXmasByRobert.HTML.Pages;

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
            //<!-- Snow flakes -->

            new IHTMLScript { type = "x-shader/x-vertex", id = "vertexshader", innerText = new Shaders.particlesVertexShader().ToString() }.AttachToDocument();
            new IHTMLScript { type = "x-shader/x-fragment", id = "fragmentshader", innerText = new Shaders.particlesFragmentShader().ToString() }.AttachToDocument();

            var w = Native.Window;

            dynamic window = w;

            #region hint such that our assets stay around

            { ITexturesImages ref0; }
            { Audio ref0; }


            { Design.models.eagle ref1; }
            { Design.models.glowbulb ref1; }
            { Design.models.horse ref1; }
            { Design.models.rock ref1; }
            { Design.models.sleigh ref1; }
            { Design.models.treeDead ref1; }
            { Design.models.treeEvergreenHigh ref1; }
            { Design.models.weeds01 ref1; }

            #endregion

            var snd = new HTML.Audio.FromAssets.unfiltered_mix { volume = 0.9 };

            window.snd = snd;

            //var camera = (THREE_PerspectiveCamera)(object)window.camera;
            var camera = new THREE_PerspectiveCamera(75, Native.Window.Width / Native.Window.Height, 1, 100000);

            camera.position.z = 0;
            camera.position.x = 0;
            camera.position.y = 0;

            window.camera = camera;

            var cameraTarget = new THREE_Vector3();
            cameraTarget.z = -400;
            camera.lookAt(cameraTarget);

            window.cameraTarget = cameraTarget;

            var loadingImage = THREE.ImageUtils.loadTexture(new loading().src);
            var map = THREE.ImageUtils.loadTexture(new snowflake().src);
            var starImage = THREE.ImageUtils.loadTexture(new flare().src);

            window.loadingImage = loadingImage;
            window.map = map;
            window.starImage = starImage;

            dynamic style = Native.Document.body.style;

            style.cursor = "url(" + new pointer().src + "),pointer";

            //container.style.cursor = 'url(img/pointer.png),pointer';




            new AppCode().Content.AttachToDocument().onload +=
                delegate
                {
                    // ScriptCoreLib should define this event!
                    snd.addEventListener(
                        "loadeddata",
                        new Action(
                            delegate
                            {
                                new IFunction("window.checkLoadingDone();").apply(Native.Window);
                            }
                        )
                    );


                    var scene = (THREE_Scene)(object)window.scene;

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

                        var cloudPlane = new THREE_PlaneGeometry(12500, 1880);
                        var cloud = new THREE_Mesh(cloudPlane, new THREE_MeshBasicMaterial(args));
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

                        var skyPlane = new THREE_PlaneGeometry(9000, 6000);
                        var sky = new THREE_Mesh(skyPlane, new THREE_MeshBasicMaterial(args));

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

                        var moonPlane = new THREE_PlaneGeometry(1000, 1000);
                        var moon = new THREE_Mesh(moonPlane,
                            new THREE_MeshBasicMaterial(
                                (object)moon_material_args
                            )
                        );




                        moon.position.set(300, 4300, -4600);
                        moon.lookAt(camera.position);
                        scene.add(moon);
                        window.moon = moon;
                    }
                    #endregion

                    var webglRenderer = new THREE_WebGLRenderer(
                        new THREE_WebGLRenderer_args { clearColor = 0x000000, clearAlpha = 1.0 }
                    );

                    webglRenderer.setSize(Native.Window.Width, Native.Window.Height);
                    webglRenderer.autoClear = false;
                    webglRenderer.domElement.AttachToDocument();



                    #region onresize
                    Native.Window.onresize +=
                        delegate
                        {

                            // notify the renderer of the size change
                            webglRenderer.setSize(Native.Window.Width, Native.Window.Height);


                            // update the camera
                            camera.aspect = Native.Window.Width / Native.Window.Height;
                            camera.updateProjectionMatrix();
                        };
                    #endregion

                    #region subtitleArray
                    var subtitleArray = (IArray<THREE_Mesh>)(object)window.subtitleArray;

                    var textPlane = new THREE_PlaneGeometry(512, 80);

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


                            var sub = new THREE_Mesh(textPlane, new THREE_MeshBasicMaterial(args));
                            sub.position.z = -800;
                            sub.position.y = -550;
                            sub.visible = false;
                            camera.add(sub);
                            subtitleArray.push(sub);
                        }
                     );

                    {
                        var endPlane = new THREE_PlaneGeometry(500, 100);

                        object args = new object().With(
                            (dynamic a) =>
                            {
                                a.map = THREE.ImageUtils.loadTexture(new xmas().src);
                                a.transparent = true;
                                a.opacity = 1.0;
                                a.depthTest = false;
                            }
                        );


                        var end = new THREE_Mesh(endPlane, new THREE_MeshBasicMaterial(args));
                        end.position.z = -400;
                        end.position.y = 100;
                        end.visible = false;
                        camera.add(end);
                        subtitleArray.push(end);

                        window.end = end;
                    }


                    new IFunction("window.setupSubtitles();").apply(Native.Window);
                    #endregion

                    var particles = (THREE_ParticleSystem)(object)window.particles;
                    var bgSprite = (THREE_Sprite)(object)window.bgSprite;
                    var loadingSprite = (THREE_Sprite)(object)window.loadingSprite;
                    var pointLight = (THREE_PointLight)(object)window.pointLight;
                    var treeArray = (IArray<THREE_Mesh>)(object)window.treeArray;
                    var rockArray = (IArray<THREE_Mesh>)(object)window.rockArray;
                    var flowerArray = (IArray<THREE_Mesh>)(object)window.flowerArray;


                    var groundMesh1 = (THREE_Mesh)(object)window.groundMesh1;
                    var groundMesh2 = (THREE_Mesh)(object)window.groundMesh2;


                    var renderModel = (object)window.renderModel;
                    var effectFilm = (object)window.effectFilm;
                    var effectVignette = (object)window.effectVignette;
                    var effectCopy = (object)window.effectCopy;


                    var composer = new THREE_EffectComposer(webglRenderer);
                    composer.addPass(renderModel);
                    composer.addPass(effectFilm);
                    composer.addPass(effectVignette);
                    composer.addPass(effectCopy);





                    #region onmousemove
                    var mouseXpercent = 0.5;
                    var mouseYpercent = 0.5;

                    Native.Document.onmousemove +=
                        e =>
                        {
                            var windowHalfX = Native.Window.Width >> 1;
                            var windowHalfY = Native.Window.Height >> 1;

                            var mouseX = (e.CursorX - windowHalfX);
                            var mouseY = (e.CursorY - windowHalfY);

                            mouseXpercent = mouseX / windowHalfX;
                            mouseYpercent = mouseY / windowHalfY;

                            window.mouseXpercent = mouseXpercent;
                            window.mouseYpercent = mouseYpercent;
                        };
                    #endregion




                    var speedEffector_value = (int)new IFunction("return window.speedEffector.value;").apply(Native.Window);

                    #region load
                    Action<string, Action<object>> load =
                        (src, yield) =>
                        {
                            new THREE_JSONLoader().load(
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

                            var sled = new THREE_Mesh(
                                geometry,
                                new THREE_MeshFaceMaterial()
                            );

                            var scale = 4;
                            sled.scale.set(scale, scale, scale);

                            sled.rotation.y = -Math.PI / 2;
                            sled.position.y = -290;
                            sled.position.z = -80;

                            scene.add(sled);

                            window.sled = sled;

                            new IFunction("window.checkLoadingDone();").apply(Native.Window);
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

                                             var mesh = new THREE_Mesh(geo, new THREE_MeshFaceMaterial());
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

                                     new IFunction("window.checkLoadingDone();").apply(Native.Window);
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

                            var bird = new THREE_MorphAnimMesh(
                                geometry,
                                new THREE_MeshBasicMaterial(
                                    (object)args
                                )
                            );

                            bird.duration = 1000;

                            bird.scale.set(4, 4, 4);
                            bird.rotation.y = Math.PI;
                            bird.position.set(0, 3000, -1500);

                            scene.add(bird);


                            window.bird = bird;

                            new IFunction("window.checkLoadingDone();").apply(Native.Window);
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

                                var mesh = new THREE_Mesh(geometry, new THREE_MeshLambertMaterial((object)args));

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


                            new IFunction("window.checkLoadingDone();").apply(Native.Window);
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

                            var horse = new THREE_MorphAnimMesh(geometry,
                                new THREE_MeshLambertMaterial(
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
                            var plane = new THREE_PlaneGeometry(700, 10, 40, 1);

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

                            var material = new THREE_MeshBasicMaterial(
                                (object)material_args
                            );

                            var leftHandle = new THREE_Mesh(plane, material);
                            leftHandle.position.y = -120;
                            leftHandle.position.z = -350;
                            leftHandle.position.x = -30;
                            leftHandle.rotation.y = -(Math.PI / 2) + 0.075;
                            leftHandle.rotation.x = Math.PI * 2 - 0.075;

                            scene.add(leftHandle);
                            window.leftHandle = leftHandle;

                            var rightHandle = new THREE_Mesh(plane, material);
                            rightHandle.position.y = -120;
                            rightHandle.position.z = -350;
                            rightHandle.position.x = 30;
                            rightHandle.rotation.y = -(Math.PI / 2) - 0.075;
                            rightHandle.scale.z = -1;
                            rightHandle.rotation.x = Math.PI * 2 - 0.075;
                            scene.add(rightHandle);
                            window.rightHandle = rightHandle;

                            new IFunction("window.checkLoadingDone();").apply(Native.Window);
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
                            var mesh = new THREE_Mesh(geometry,
                                new THREE_MeshLambertMaterial(
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

                        new IFunction("window.checkLoadingDone();").apply(Native.Window);
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
                            var mesh = default(THREE_Mesh);

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

                    #region loop
                    Action loop = delegate
                    {
                        var allLoaded = (bool)(object)window.allLoaded;

                        var horse = (THREE_MorphAnimMesh)(object)window.horse;
                        var bird = (THREE_MorphAnimMesh)(object)window.bird;

                        var leftHandle = (THREE_Mesh)(object)window.leftHandle;
                        var rightHandle = (THREE_Mesh)(object)window.rightHandle;

                        var moon = (THREE_Mesh)(object)window.moon;
                        var cloud = (THREE_Mesh)(object)window.cloud;
                        var sky = (THREE_Mesh)(object)window.sky;
                        var sled = (THREE_Mesh)(object)window.sled;

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

                            if (sled != null)
                            {
                                sled.position.z -= speed;
                                sled.position.x = camera.position.x;
                            }

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

                            particles.position.z -= speed;

                            new IFunction("e", "window.uniforms.globalTime.value += e;").apply(Native.Window, delta * 0.00015);
                            new IFunction("e", "window.uniforms.speed.value = e;").apply(Native.Window, speed / maxSpeed);

                            new IFunction("window.runSubtitles();").apply(Native.Window);

                        }
                        else
                        {
                            if (loadingSprite != null)
                            {
                                loadingSprite.position.set(Native.Window.Width >> 1, Native.Window.Height >> 1, 0);
                                loadingSprite.rotation -= 0.08;
                            }
                        }

                        if (bgSprite != null)
                        {
                            bgSprite.position.set(Native.Window.Width >> 1, Native.Window.Height >> 1, 0);
                        }


                        new IFunction("window.TWEEN.update();").apply(Native.Window);

                        //if (has_gl)
                        {
                            webglRenderer.clear();
                            composer.render(0.01);
                        }
                    };
                    #endregion


                    #region animate
                    Action animate = null;

                    animate = delegate
                    {
                        Native.Window.requestAnimationFrame += animate;
                        loop();
                    };

                    animate();
                    #endregion

                };



            Native.Document.body.ondblclick +=
                delegate
                {
                    Native.Document.body.requestFullscreen();
                };



            Native.Document.oncontextmenu +=
                e =>
                {
                    e.preventDefault();
                };

            //Native.Document.onmousedown +=
            //    e =>
            //    {
            //        if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
            //            Native.Document.body.requestPointerLock();
            //    };
        }

    }
}
