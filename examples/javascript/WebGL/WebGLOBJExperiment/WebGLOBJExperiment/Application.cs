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
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131109-gold
            // view-source:file:///X:/opensource/github/three.js/examples/webgl_loader_obj.html


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

  

            new HTML.Images.FromAssets.ash_uvgrid01().InvokeOnComplete(
                i =>
                {
                    var texture = new THREE.Texture(
                        i
                    );
                    texture.needsUpdate = true;

                    // model

                    var oo = default(THREE.Object3D);

                    var loader = new THREE.OBJLoader();
                    loader.load(
                        //'obj/male02/male02.obj', 
                        "assets/WebGLOBJExperiment/male02.obj",

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

                                            }

                                        }
                                    )
                                 );


                                o.position.y = -80;
                                scene.add(o);

                                oo = o;
                            }
                        ),
                        null, null
                    );

                    //

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

                            if (oo != null)
                                oo.rotation.y = st.ElapsedMilliseconds * 0.001;

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
                }
            );




        }

    }
}
