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
        public Application(IApp page)
        {
            //<!-- Snow flakes -->

            new IHTMLScript { type = "x-shader/x-vertex", id = "vertexshader", innerText = new Shaders.particlesVertexShader().ToString() }.AttachToDocument();
            new IHTMLScript { type = "x-shader/x-fragment", id = "fragmentshader", innerText = new Shaders.particlesFragmentShader().ToString() }.AttachToDocument();



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


            new AppCode().Content.AttachToDocument().onload +=
                delegate
                {
                    var w = Native.Window;

                    dynamic window = w;

                    var webglRenderer = (THREE_WebGLRenderer)(object)window.webglRenderer;
                    var camera = (THREE_PerspectiveCamera)(object)window.camera;


                    //var renderer = (THREE_WebGLRenderer)new IFunction("return this.webglRenderer;").apply(Native.Window);
                    //var camera = (THREE_PerspectiveCamera)new IFunction("return this.camera;").apply(Native.Window);

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


                    var scene = (THREE_Scene)(object)window.scene;
                    var composer = (THREE_EffectComposer)(object)window.composer;

                    var particles = (THREE_ParticleSystem)(object)window.particles;



                    var bgSprite = (THREE_Sprite)(object)window.bgSprite;
                    var loadingSprite = (THREE_Sprite)(object)window.loadingSprite;

                    var pointLight = (THREE_PointLight)(object)window.pointLight;

                    var cameraTarget = (THREE_Vector3)(object)window.cameraTarget;
                    var treeArray = (IArray<THREE_Mesh>)(object)window.treeArray;
                    var rockArray = (IArray<THREE_Mesh>)(object)window.rockArray;
                    var flowerArray = (IArray<THREE_Mesh>)(object)window.flowerArray;
                    var groundMesh1 = (THREE_Mesh)(object)window.groundMesh1;
                    var groundMesh2 = (THREE_Mesh)(object)window.groundMesh2;

                    var speedEffector_value = (int)new IFunction("return window.speedEffector.value;").apply(Native.Window);

                    #region sled
                    new THREE_JSONLoader().load(
                        new WoodsXmasByRobert.Design.models.sleigh().Content.src,
                        IFunction.OfDelegate(
                            new Action<object>(
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
                            )
                        )
                    );
                    #endregion

                    #region bird
                    new THREE_JSONLoader().load(
                        new WoodsXmasByRobert.Design.models.eagle().Content.src,
                        IFunction.OfDelegate(
                            new Action<object>(
                                geometry =>
                                {
                                    Console.WriteLine("got bird!");

                                    var bird = new THREE_MorphAnimMesh(
                                        geometry,
                                        new THREE_MeshBasicMaterial(
                                            new THREE_MeshBasicMaterial_args
                                            {
                                                color = 0x000000,
                                                morphTargets = true,
                                                fog = false
                                            }
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
                            )
                        )
                    );
                    #endregion

                    var random = new Random();

                    #region rock
                    new THREE_JSONLoader().load(
                        new WoodsXmasByRobert.Design.models.rock().Content.src,
                        IFunction.OfDelegate(
                            new Action<object>(
                                geometry =>
                                {
                                    Console.WriteLine("got rock!");

                                    var numOfRocks = 25;
                                    for (var i = 0; i < numOfRocks; ++i)
                                    {

                                        var mesh = new THREE_Mesh(geometry, new THREE_MeshLambertMaterial(new THREE_MeshBasicMaterial_args { color = 0x444444 }));

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
                            )
                        )
                    );
                    #endregion

                    #region horse
                    new THREE_JSONLoader().load(
                        new WoodsXmasByRobert.Design.models.horse().Content.src,
                        IFunction.OfDelegate(
                            new Action<object>(
                                geometry =>
                                {
                                    Console.WriteLine("got horse!");

                                    var numOfRocks = 25;
                                    for (var i = 0; i < numOfRocks; ++i)
                                    {

                                        var mesh = new THREE_Mesh(geometry, new THREE_MeshLambertMaterial(new THREE_MeshBasicMaterial_args { color = 0x444444 }));

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
                            )
                        )
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

        }

    }
}
