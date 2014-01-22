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
using WebGLOBJToyota;
using WebGLOBJToyota.Design;
using WebGLOBJToyota.HTML.Images.FromAssets;
using WebGLOBJToyota.HTML.Pages;

namespace WebGLOBJToyota
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


            var ref0 = new TexturesImages();

            //var texture = new THREE.Texture(
            //    //new HTML.Images.FromAssets.ash_uvgrid01()
            //    //new HTML.Images.FromAssets.texture_palm()
            //           new TLC200_Side_89pc()
            //      );
            //texture.needsUpdate = true;

            // X:\jsc.svn\examples\javascript\WebGL\WebGLMTLExperiment\WebGLMTLExperiment\Application.cs

            var loader = new THREE.OBJMTLLoader();
            loader.load(
                "assets/WebGLOBJToyota/LX570_2008.obj",
                "assets/WebGLOBJToyota/LX570_2008.mtl",
                     new Action<THREE.Object3D>(
                    o =>
                    {
                        Console.WriteLine("onload " + new { o });

                        // need to use
                        // Toyota Land Cruiser 200 Series aka Lexus LX 570_2008.mtl
                        // http://pages.cs.wisc.edu/~lizy/mrdoob-three.js-ef5f05d/examples/webgl_loader_obj_mtl.html

                        
                        o.scale = new THREE.Vector3(40, 40, 40);

                        o.position.y = -80;
                        scene.add(o);
                        oo.Add(o);
                    }
                ),
                new Action<object>(
                    o =>
                    {
                        Console.WriteLine("progress " + new { o });
                    }
                ),

                 new Action<object>(
                    o =>
                    {
                        Console.WriteLine("error " + new { o });
                    }
                 )
            );


        }

    }
}
