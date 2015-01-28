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
using WebGLOBJExperiment;
using WebGLOBJExperiment.Design;
using WebGLOBJExperiment.HTML.Pages;

namespace WebGLOBJExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        THREE.Texture ref0;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // .obj nolonger works?


            // http://forums.newtek.com/showthread.php?139633-how-to-convert-sketchup-files-into-OBJ-without-the-PRO-version

            // http://www.gameartmarket.com/details?id=ag9zfmRhZGEtM2RtYXJrZXRyMgsSBFVzZXIiE3BpZXJ6YWtAcGllcnphay5jb20MCxIKVXNlclVwbG9hZBjc_5ik8ycM
            // http://www.gameartmarket.com/details?id=ag9zfmRhZGEtM2RtYXJrZXRyMgsSBFVzZXIiE3BpZXJ6YWtAcGllcnphay5jb20MCxIKVXNlclVwbG9hZBjXwOHe-icM
            // http://scfreak.0catch.com/dlindex.html
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131109-gold
            // view-source:file:///X:/opensource/github/three.js/examples/webgl_loader_obj.html

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

            var ambient = new THREE.AmbientLight(0x101030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();
            renderer.domElement.style.SetLocation(0, 0);


            var mouseX = 0;
            var mouseY = 0;
            var st = new Stopwatch();
            st.Start();

            Native.window.onframe +=
                delegate
                {

                    oo.WithEach(
                        x =>
                            x.rotation.y = st.ElapsedMilliseconds * 0.001
                    );


                    camera.position.x += (mouseX - camera.position.x) * .05;
                    camera.position.y += (-mouseY - camera.position.y) * .05;

                    camera.lookAt(scene.position);

                    renderer.render(scene, camera);


                };

            Native.window.onresize +=
                delegate
                {
                    camera.aspect = window.aspect;
                    camera.updateProjectionMatrix();

                    renderer.setSize(window.Width, window.Height);

                };
            #endregion








            new THREE_OBJAsset(
                 new HTML.Images.FromAssets.texture_palm(),
                   "assets/WebGLOBJExperiment/palm.obj"
            ).Source.Task.ContinueWithResult(
                o =>
                {
                    o.position.y = -80;
                    scene.add(o);
                    oo.Add(o);

                    o.position.x = -200;
                    o.scale = new THREE.Vector3(5, 5, 5);
                }
            );

            new THREE_OBJAsset(
                  new HTML.Images.FromAssets.texture_palm(),
                    "assets/WebGLOBJExperiment/palm.obj"
             ).Source.Task.ContinueWithResult(
                 o =>
                 {
                     o.position.y = -80;
                     scene.add(o);
                     oo.Add(o);

                     o.position.x = 200;
                     o.scale = new THREE.Vector3(5, 5, 5);
                 }
             );

            new THREE_OBJAsset(
                 new HTML.Images.FromAssets.Fence_texture3(),
                   "assets/WebGLOBJExperiment/fence.obj"
            ).Source.Task.ContinueWithResult(
                o =>
                {
                    o.position.y = -80;
                    scene.add(o);
                    oo.Add(o);

                    o.position.x = -100;
                    o.scale = new THREE.Vector3(0.2, 0.2, 0.2);
                }
            );

            new sack_of_gold2().Source.Task.ContinueWithResult(
               o =>
               {
                   o.position.y = -80;
                   scene.add(o);
                   oo.Add(o);

                   o.position.x = 70;
                   o.position.z = 100;
                   o.scale = new THREE.Vector3(0.5, 0.5, 0.5);
               }
           );

            new sack_of_gold2().Source.Task.ContinueWithResult(
          o =>
          {
              o.position.y = -80;
              scene.add(o);
              oo.Add(o);

              o.position.x = -70;
              o.position.z = 100;
              o.scale = new THREE.Vector3(0.5, 0.5, 0.5);
          }
      );

            new THREE_OBJAsset(
              new HTML.Images.FromAssets.ash_uvgrid01(),
                   "assets/WebGLOBJExperiment/male02.obj"
            ).Source.Task.ContinueWithResult(
                o =>
                {
                    o.position.y = -80;
                    scene.add(o);
                    oo.Add(o);

                    o.position.x = 50;
                    //o.scale = new THREE.Vector3(5, 5, 5);
                }
            );
        }

    }


    public class sack_of_gold2 : THREE_OBJAsset
    {
        public sack_of_gold2()
            : base(
                new HTML.Images.FromAssets.sack_of_coins___diffuse_512(),
                "assets/WebGLOBJExperiment/sack_of_gold2.obj"
                )
        {

        }
    }

    public class THREE_OBJAsset
    {
        public readonly TaskCompletionSource<THREE.Object3D> Source = new TaskCompletionSource<THREE.Object3D>();

        public THREE_OBJAsset(IHTMLImage i, string uri)
        {
            new Action(
                async delegate
                {

                    await i;

                    var texture = new THREE.Texture(
                        //new HTML.Images.FromAssets.ash_uvgrid01()
                        //new HTML.Images.FromAssets.texture_palm()
                         i
                    );
                    texture.needsUpdate = true;

                    // model


                    var loader = new THREE.OBJLoader();
                    loader.load(
                        //'obj/male02/male02.obj', 
                        //"assets/WebGLOBJExperiment/male02.obj",
                        //"assets/WebGLOBJExperiment/palm.obj",
                        uri,

                        new Action<THREE.Object3D>(
                            o =>
                            {

                                o.traverse(
                                    new Action<object>(
                                        child =>
                                        {
                                            var m = child as THREE.Mesh;
                                            if (m != null)
                                            {

                                                m.material.map = texture;

                                                // http://stackoverflow.com/questions/10857233/in-three-js-alpha-channel-works-inconsistently
                                                (m.material as dynamic).transparent = true;

                                            }

                                        }
                                    )
                                 );




                                this.Source.SetResult(o);
                            }
                        )
                        //, null, null
                    );

                    //
                    //await s.Task;
                }
            )();

        }
    }
}
