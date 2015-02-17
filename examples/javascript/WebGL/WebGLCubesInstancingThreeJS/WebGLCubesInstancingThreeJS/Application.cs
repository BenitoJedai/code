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
using WebGLCubesInstancingThreeJS;
using WebGLCubesInstancingThreeJS.Design;
using WebGLCubesInstancingThreeJS.HTML.Pages;
using ScriptCoreLib.Lambda;


namespace WebGLCubesInstancingThreeJS
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
            // what are we looking at?

            var scene = new THREE.Scene();

            var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 1000);
            camera.position.z = 300;

            var renderer = new THREE.WebGLRenderer(
                   new
                   {
                       preserveDrawingBuffer = true
                   }
                );
            renderer.setSize();

            scene.add(new THREE.AmbientLight(0x333333));

            var light = new THREE.DirectionalLight(0xffffff, 1);
            light.position.set(5, 3, 5);
            scene.add(light);
   
            var cube = new THREE.CubeGeometry(0.5,0.5,0.5);
            var material = new THREE.MeshBasicMaterial(new
                        {
                            color = new THREE.Color(0xADD8E6),
                        });

            var obj = new THREE.Object3D();

            for (var x = 0; x < 100; x++)
            {
                for (var y = 0; y < 100; y++)
                {
                    for (var zz = 0; zz < 15; zz++)
                    {
                        var mesh1 = new THREE.Mesh(cube, material);
                        mesh1.position.x = x;
                        mesh1.position.y = y;
                        mesh1.position.z = zz;
                        obj.add(mesh1);
                    }
                }
            }

            //var mesh1 = new THREE.Mesh(cube, material);
            //mesh1.position.x = 1;
            //mesh1.position.y = 1;
            //mesh1.position.z = 1;
            //obj.add(mesh1);
            scene.add(obj);

            var z = camera.position.z;

            this.canvas = (IHTMLCanvas)renderer.domElement;

            //renderer.domElement.AttachToDocument();
            this.canvas.AttachToDocument();
            this.canvas.style.SetLocation(0, 0);

            Native.window.onframe +=
                e =>
                {
                    //if (this.canvas.parentNode == null)
                    //    return;

                    camera.aspect = canvas.clientWidth / (double)canvas.clientHeight;
                    camera.updateProjectionMatrix();

                    camera.position.z += (z - camera.position.z) * e.delay.ElapsedMilliseconds / 200;


                    // the larger the vew the slower the rotation shall be
                    var speed = 0.0001 * e.delay.ElapsedMilliseconds +
                        0.007
                        * 96.0 / canvas.clientHeight
                        * 1.0 / camera.position.z;

                    //Native.document.title = new { s = 96.0 / canvas.clientHeight }.ToString();
                    //Native.document.title = new { speed }.ToString();


                    //controls.update();
                    //sphere.rotation.y += speed;
                    obj.rotation.y += speed;


                    renderer.render(scene, camera);
                };

            Native.window.onresize +=
                delegate
                {


                    //if (canvas.parentNode == Native.document.body)

                    // are we embedded?
                    if (page != null)
                        renderer.setSize();
                };
        }
    }
}
