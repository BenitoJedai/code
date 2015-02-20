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
using WebGLRah66Comanche;
using WebGLRah66Comanche.Design;
using WebGLRah66Comanche.HTML.Pages;
using WebGLRah66Comanche.Library;

namespace WebGLRah66Comanche
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
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;

            // https://3dwarehouse.sketchup.com/model.html?id=e78dca4863e8572d86ea4fa6bd93bc43
            // https://3dwarehouse.sketchup.com/model.html?id=38d1045b8de1cf12b08e958a32ef3184

            var oo = new List<THREE.Object3D>();

            #region scene
            var window = Native.window;



            // scene

            var scene = new THREE.Scene();

            //var ambient = new THREE.AmbientLight(0x101030);
            //// addTrace?
            //scene.add(ambient);

            // should jsc package c# source code along here for code lense like peeking?
            new THREE.AmbientLight(0x101030).AttachTo(scene);

            var lightOffset = new THREE.Vector3(0, 1000, 1000.0);

            var light = new THREE.DirectionalLight(0xffffff, 1.0);
            //var light = new THREE.DirectionalLight(0xffffff, 1.0);
            //var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 1.5);
            light.position.copy(lightOffset);

            light.castShadow = true;

            var xlight = light as dynamic;
            xlight.shadowMapWidth = 4096;
            xlight.shadowMapHeight = 2048;

            xlight.shadowDarkness = 0.3;
            //xlight.shadowDarkness = 0.5;

            xlight.shadowCameraNear = 10;
            xlight.shadowCameraFar = 10000;
            xlight.shadowBias = 0.00001;
            xlight.shadowCameraRight = 4000;
            xlight.shadowCameraLeft = -4000;
            xlight.shadowCameraTop = 4000;
            xlight.shadowCameraBottom = -4000;

            xlight.shadowCameraVisible = true;

            light.AttachTo(scene);






            var planeGeometry = new THREE.CubeGeometry(512, 512, 1);

            var plane = new THREE.Mesh(
                planeGeometry,
                material: new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
            );

            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                plane.AttachTo(parent);


                parent.rotation.x = -Math.PI / 2;
                parent.scale.set(10, 10, 10);

                parent.AttachTo(scene);
            }




            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();
            renderer.domElement.style.SetLocation(0, 0);

            renderer.shadowMapEnabled = true;
            renderer.shadowMapType = THREE.PCFSoftShadowMap;

            //var mouseX = 0;
            //var mouseY = 0;
            //var st = new Stopwatch();
            //st.Start();


            //Native.window.document.onmousemove +=
            //    e =>
            //    {
            //        mouseX = e.CursorX - Native.window.Width / 2;
            //        mouseY = e.CursorY - Native.window.Height / 2;
            //    };

            var camera = new THREE.PerspectiveCamera(
                //40,
                20,
                //10,

                Native.window.aspect, 2,

                // how far out do we want to zoom?
                200000
                //9000
                );
            camera.position.set(-1200, 800, -3200);

            camera.AttachTo(scene);

            var controls = new THREE.OrbitControls(camera, renderer.domElement);

            Native.window.onframe +=
                delegate
                {

                    //oo.WithEach(
                    //    x =>
                    //        x.rotation.y = (st.ElapsedMilliseconds + mouseX * 100) * 0.00001
                    //);


                    //camera.position.x += (mouseX - camera.position.x) * .05;
                    //camera.position.y += (-mouseY - camera.position.y) * .05;

                    //camera.lookAt(scene.position);

                    controls.update();
                    camera.position = controls.center.clone();

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

            #region THREE_ColladaAsset
            // why isnt it being found?
            new Comanche().Source.Task.ContinueWithResult(
                dae =>
                {

                    dae.position.y = 200;

                    //dae.position.z = 280;

                    dae.AttachTo(scene);

                    //scene.add(dae);
                    oo.Add(dae);

                    // wont do it
                    //dae.castShadow = true;

                    // http://stackoverflow.com/questions/15492857/any-way-to-get-a-bounding-box-from-a-three-js-object3d
                    //var helper = new THREE.BoundingBoxHelper(dae, 0xff0000);
                    //helper.update();
                    //// If you want a visible bounding box
                    //scene.add(helper);

                    dae.children[0].children[0].children.WithEach(x => x.castShadow = true);


                    // the rotors?
                    dae.children[0].children[0].children.Last().children.WithEach(x => x.castShadow = true);


                    dae.scale.set(0.5, 0.5, 0.5);
                    //helper.scale.set(0.5, 0.5, 0.5);

                    var sw = Stopwatch.StartNew();

                    Native.window.onframe += delegate
                    {
                        //dae.children[0].children[0].children.Last().al
                        //dae.children[0].children[0].children.Last().rotation.z = sw.ElapsedMilliseconds * 0.01;
                        //dae.children[0].children[0].children.Last().rotation.x = sw.ElapsedMilliseconds * 0.01;
                        //rotation.y = sw.ElapsedMilliseconds * 0.01;

                        dae.children[0].children[0].children.Last().rotation.y = sw.ElapsedMilliseconds * 0.001;

                        //dae.children[0].children[0].children.Last().app
                    };
                }
            );
            #endregion


            var f = new ZeProperties();

            f.Show();


            f.Add(nameof(renderer), () => renderer);
            f.Add(nameof(controls), () => controls);
            f.Add(nameof(scene), () => scene);

            //f.treeView1.Nodes.Add("controls : " + typeof(THREE.OrbitControls)).Tag = controls;


        }

    }

    public class Comanche : global::WebGLColladaExperiment.THREE_ColladaAsset

    // we get purple small thingy

    // maybe sketchup doesnt know how to export colors?
    //"assets/WebGLHeatZeekerColladaExperiment/sam_site.dae"

    {
        public Comanche() : base(
                "assets/WebGLRah66Comanche/RAH-66-Comanche-by-decten.dae"

            )
        {

        }
    }
}
