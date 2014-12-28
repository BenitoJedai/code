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
using WebGLHeatZeekerColladaExperiment;
using WebGLHeatZeekerColladaExperiment.Design;
using WebGLHeatZeekerColladaExperiment.HTML.Pages;

namespace WebGLHeatZeekerColladaExperiment
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131110-dae

            var oo = new List<THREE.Object3D>();

            #region scene
            var window = Native.window;

            var camera = new THREE.PerspectiveCamera(
                45,
                window.aspect,
                1,
                10000
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


            Native.window.document.onmousemove +=
                e =>
                {
                    mouseX = e.CursorX - Native.window.Width / 2;
                    mouseY = e.CursorY - Native.window.Height / 2;
                };


            Native.window.onframe +=
                delegate
            {

                oo.WithEach(
                    x =>
                        x.rotation.y = (st.ElapsedMilliseconds + mouseX * 100) * 0.00001
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

            // why isnt it being found?
            new global::WebGLColladaExperiment.THREE_ColladaAsset(

                // we get purple small thingy
                "assets/WebGLHeatZeekerColladaExperiment/Promotion3D_daytime.dae"

            // maybe sketchup doesnt know how to export colors?
            //"assets/WebGLHeatZeekerColladaExperiment/sam_site.dae"
            ).Source.Task.ContinueWithResult(
                dae =>
                {

                    dae.position.y = -40;
                    //dae.position.z = 280;
                    scene.add(dae);
                    oo.Add(dae);

                    dae.scale = new THREE.Vector3(2, 2, 2);

                }
            );
        }

    }
}
