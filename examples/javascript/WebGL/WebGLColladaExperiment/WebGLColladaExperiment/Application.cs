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
using WebGLColladaExperiment;
using WebGLColladaExperiment.Design;
using WebGLColladaExperiment.HTML.Pages;

namespace WebGLColladaExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public IHTMLCanvas canvas;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
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

            var ambient = new THREE.AmbientLight(0x101030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);

            // WebGLRenderer preserveDrawingBuffer 
            var renderer = new THREE.WebGLRenderer(

                new
                {
                    preserveDrawingBuffer = true
                }
            );

            // https://github.com/mrdoob/three.js/issues/3836
            renderer.setClearColor(0xfffff, 1);

            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();
            renderer.domElement.style.SetLocation(0, 0);

            this.canvas = (IHTMLCanvas)renderer.domElement;


            var mouseX = 0;
            var mouseY = 0;
            var st = new Stopwatch();
            st.Start();

            Native.window.onframe +=
                delegate
                {
                    renderer.clear();


                    //camera.aspect = window.aspect;
                    //camera.aspect = canvas.clientWidth / (double)canvas.clientHeight;
                    camera.aspect = canvas.aspect;
                    camera.updateProjectionMatrix();


                    oo.WithEach(
                        x =>
                            x.rotation.y = st.ElapsedMilliseconds * 0.0001
                    );


                    camera.position.x += (mouseX - camera.position.x) * .05;
                    camera.position.y += (-mouseY - camera.position.y) * .05;

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

            new truck().Source.Task.ContinueWithResult(
                   dae =>
                    {
                        //dae.scale.x = 30;
                        //dae.scale.y = 30;
                        //dae.scale.z = 30;
                        dae.position.z = 65;

                        dae.scale.x = 10;
                        dae.scale.y = 10;
                        dae.scale.z = 10;

                        dae.position.y = -80;

                        scene.add(dae);
                        oo.Add(dae);


                    }
               );


            new skpBOX().Source.Task.ContinueWithResult(
                dae =>
                {
                    //dae.scale.x = 30;
                    //dae.scale.y = 30;
                    //dae.scale.z = 30;


                    dae.scale.x = 1;
                    dae.scale.y = 1;
                    dae.scale.z = 1;

                    dae.position.y = -80;
          
                    scene.add(dae);
                    oo.Add(dae);


                }
            );
        }


    }

    [Obsolete("jsc should generate this")]
    class truck : THREE_ColladaAsset
    {
        public truck()
            : base(
                "assets/WebGLColladaExperiment/truck.dae"
                )
        {

        }
    }

    [Obsolete("jsc should generate this")]
    class skpBOX : THREE_ColladaAsset
    {
        public skpBOX()
            : base(
                "assets/WebGLColladaExperiment/skpBOX.dae"
                )
        {

        }
    }


    public class THREE_ColladaAsset
    {
        public readonly TaskCompletionSource<THREE.Object3D> Source = new TaskCompletionSource<THREE.Object3D>();

        public THREE_ColladaAsset(string uri)
        {
            var loader = new THREE.ColladaLoader();

            loader.options.convertUpAxis = true;

            // this does NOT work correctly?
            //loader.options.centerGeometry = true;

            loader.load(
                //"assets/WebGLColladaExperiment/truck.dae",

                uri,

                new Action<THREE.ColladaLoaderResult>(
                    collada =>
                    {
                        var dae = collada.scene;


                        ////o.position.y = -80;
                        //scene.add(dae);
                        //oo.Add(dae);

                        //dae.scale = new THREE.Vector3(5, 5, 5);

                        this.Source.SetResult(dae);

                    }
                )
            );
        }
    }
}
