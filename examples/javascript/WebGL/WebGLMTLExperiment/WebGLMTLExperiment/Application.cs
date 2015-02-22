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
using WebGLMTLExperiment;
using WebGLMTLExperiment.Design;
using WebGLMTLExperiment.HTML.Pages;
using WebGLRah66Comanche.Library;

using THREE;

namespace WebGLMTLExperiment
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

            // this is of little help for now/
            // this one is loading dds?

            var mouseX = 0;
            var mouseY = 0;

            var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 2000);
            camera.position.z = 100;


            var scene = new THREE.Scene();

            var ambient = new THREE.AmbientLight(0x101030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1).normalize();
            scene.add(directionalLight);

            // model

            // THREE.ImageUtils.loadCompressedTexture has been removed.Use THREE.DDSLoader instead.

            var loader = new THREE.OBJMTLLoader();


            //            ImageUtils.parseDDS(): Invalid magic number in DDS header view-source:76325
            //256
            //WebGL: drawElements: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled. 192.168.1.88/:1
            //WebGL: too many errors, no more errors will be reported to the console for this context. 


            // Request URL:http://192.168.1.88:30122/assets/WebGLMTLExperiment/01_-_Default1noCulling.dds

            { var ref0 = "assets/WebGLMTLExperiment/01_-_Default1noCulling.dds"; }
            { var ref0 = "assets/WebGLMTLExperiment/male-02-1noCulling.dds"; }
            { var ref0 = "assets/WebGLMTLExperiment/orig_02_-_Defaul1noCulling.dds"; }



            loader.load(
                "assets/WebGLMTLExperiment/male02.obj",
                "assets/WebGLMTLExperiment/male02_dds.mtl",

                                new Action<THREE.Object3D>(
                    o =>
                    {

                        //o.scale = new THREE.Vector3(40, 40, 40);

                        o.position.y = -80;
                        scene.add(o);
                        //oo.Add(o);

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






            var renderer = new THREE.WebGLRenderer();
            //renderer.setSize();

            // why are we getting offset?
            renderer.setSize(Native.window.Width, Native.window.Height);

            //Native.document.body.Clear();


            renderer.domElement.AttachToDocument();
            //renderer.domElement.AttachTo(Native.document.documentElement);

            //window.addEventListener( 'resize', onWindowResize, false );

            //}

            //function onWindowResize() {

            //    windowHalfX = window.innerWidth / 2;
            //    windowHalfY = window.innerHeight / 2;

            //    camera.aspect = window.innerWidth / window.innerHeight;
            //    camera.updateProjectionMatrix();

            //    renderer.setSize( window.innerWidth, window.innerHeight );

            //}

            var controls = new THREE.OrbitControls(camera, renderer.domElement);


            ////

            //function animate() {

            //    requestAnimationFrame( animate );
            //    render();

            //}

            Native.window.onframe +=
                delegate
                {


                    controls.update();
                    camera.position = controls.center.clone();

                    renderer.render(scene, camera);

                };



            #region ZeProperties
            var ze = new ZeProperties();

            ze.Show();
            ze.treeView1.Nodes.Clear();

            ze.Add(() => renderer);
            ze.Add(() => controls);
            ze.Add(() => scene);
            ze.Left = 0;
            #endregion


        }

    }
}
