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
using WebGLAudi;
using WebGLAudi.Design;
using WebGLAudi.HTML.Pages;
using System.Diagnostics;

namespace WebGLAudi
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
            // Uncaught TypeError: Cannot read property 'nodes' of null
            // sketchup creates empty nodes
            // .//dae:scene/dae:instance_visual_scene

            // .dae download missing?
            //<scene>
            //    <instance_visual_scene url="#ID1" />

            // https://3dwarehouse.sketchup.com/model.html?id=7bf6249a031b5095ddd41159baaa3ad5
            // jsc, how to show dae?

            // Uncaught TypeError: Cannot read property 'input' of null
            // geometry: "ID690"
            //<geometry id="ID979">
            //        <mesh />
            //    </geometry>
            //    <geometry id="ID980">
            //        <mesh />
            //    </geometry>

            // X:\jsc.svn\examples\javascript\WebGL\WebGLNexus7\WebGLNexus7\Application.cs

            // X:\jsc.svn\examples\javascript\WebGL\WebGLGalaxyS\WebGLGalaxyS\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140801
            // jsc, when can we have compiled collada asssets?
            // svg we have why not collada..
            // https://3dwarehouse.sketchup.com/model.html?id=b3bdb20081c023accbd2ad75d6ff6a24
            // https://3dwarehouse.sketchup.com/collection.html?id=982aafab70c9aba140c287facf4f3262


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131110-dae

            var oo = new List<THREE.Object3D>();

            #region scene
            var window = Native.window;

            var camera = new THREE.PerspectiveCamera(
                45,
                window.aspect,
                1,
                2000
                );
            camera.position.z = 400;

            // scene

            var scene = new THREE.Scene();

            var ambient = new THREE.AmbientLight(0x303030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffffff);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);

            // WebGLRenderer preserveDrawingBuffer 
            var renderer = new THREE.WebGLRenderer(

                new
            {
                antialias = true,
                alpha = true,
                preserveDrawingBuffer = true
            }
            );

            // https://github.com/mrdoob/three.js/issues/3836
            //renderer.setClearColor(0xfffff, 1);

            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();
            renderer.domElement.style.SetLocation(0, 0);

            var canvas = (IHTMLCanvas)renderer.domElement;


            var old = new
            {



                CursorX = 0,
                CursorY = 0
            };


            var mouseX = 0;
            var mouseY = 0;
            var st = new Stopwatch();
            st.Start();


            canvas.css.active.style.cursor = IStyle.CursorEnum.move;

            #region onmousedown
            canvas.onmousedown +=
                e =>
                {

                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        canvas.requestFullscreen();
                    }
                    else
                    {
                        // movementX no longer works
                        old = new
                        {


                            e.CursorX,
                            e.CursorY
                        };


                        e.CaptureMouse();
                    }

                };
            #endregion



            // X:\jsc.svn\examples\javascript\Test\TestMouseMovement\TestMouseMovement\Application.cs
            #region onmousemove
            canvas.onmousemove +=
                e =>
                {
                    var pointerLock = canvas == Native.document.pointerLockElement;


                    //Console.WriteLine(new { e.MouseButton, pointerLock, e.movementX });

                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {

                        oo.WithEach(
                            x =>
                            {
                                x.rotation.y += 0.006 * (e.CursorX - old.CursorX);
                                x.rotation.x += 0.006 * (e.CursorY - old.CursorY);
                            }
                        );

                        old = new
                        {


                            e.CursorX,
                            e.CursorY
                        };



                    }

                };
            #endregion
            var z = camera.position.z;

            #region onmousewheel
            canvas.onmousewheel +=
                e =>
                {
                    //camera.position.z = 1.5;

                    // min max. shall adjust speed also!
                    // max 4.0
                    // min 0.6
                    z -= 10.0 * e.WheelDirection;

                    //camera.position.z = 400;
                    z = z.Max(200).Min(500);

                    //Native.document.title = new { z }.ToString();

                };
            #endregion


            Native.window.onframe +=
                e =>
            {
                renderer.clear();

                camera.aspect = canvas.aspect;
                camera.updateProjectionMatrix();

                camera.position.z += (z - camera.position.z) * e.delay.ElapsedMilliseconds / 200;


                camera.lookAt(scene.position);

                renderer.render(scene, camera);


            };

            Native.window.onresize +=
                delegate
            {
                if (canvas.parentNode == Native.document.body)
                {
                    renderer.setSize(window.Width, window.Height);
                }

            };
            #endregion

            new __AssetsLibrary__.ColladaAudiA4().Source.Task.ContinueWithResult(
                   dae =>
                    {
                        // 90deg
                        dae.rotation.x = -Math.Cos(Math.PI);

                        //dae.scale.x = 30;
                        //dae.scale.y = 30;
                        //dae.scale.z = 30;
                        dae.position.z = 65;


                        // jsc, do we have ILObserver available yet?
                        dae.scale.x = 1.0;
                        dae.scale.y = 1.0;
                        dae.scale.z = 1.0;

                        //dae.position.y = -80;

                        scene.add(dae);
                        oo.Add(dae);


                    }
               );


            // no! why?
            Console.WriteLine("do you see it?");
            // slow to load
            // then too small!
        }

    }
}
