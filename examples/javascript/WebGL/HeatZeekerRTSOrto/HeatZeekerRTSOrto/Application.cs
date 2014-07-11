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
using HeatZeekerRTSOrto;
using HeatZeekerRTSOrto.Design;
using HeatZeekerRTSOrto.HTML.Pages;
using System.Diagnostics;

namespace HeatZeekerRTSOrto
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
            Native.body.style.margin = "0px";

            // jsc, add THREE
            // ... ok.

            // X:\jsc.svn\examples\javascript\WebGL\WebGLOrthographicCamera\WebGLOrthographicCamera\Application.cs

            var camera = new THREE.OrthographicCamera(Native.window.Width / -2, Native.window.Width / 2, Native.window.Height / 2, Native.window.Height / -2, -500, 1000);
            camera.position.x = 200;
            camera.position.y = 100;
            camera.position.z = 200;

            var scene = new THREE.Scene();

            // Grid

            var size = 500;
            var step = 50;


            Func<double> random = new Random().NextDouble;


            {
                var geometry = new THREE.Geometry();

                for (var i = -size; i <= size; i += step)
                {

                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(-size, 0, i));
                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(size, 0, i));

                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(i, 0, -size));
                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(i, 0, size));

                }

                var material = new THREE.LineBasicMaterial(new { color = 0x000000, opacity = 0.2 });

                var line = new THREE.Line(geometry, material);

                line.type = THREE.LinePieces;
                scene.add(line);
            }



            var interactiveObjects = new List<THREE.Object3D>();

            #region Cubes
            {
                var geometry = new THREE.BoxGeometry(50, 50, 50);
                var material = new THREE.MeshLambertMaterial(new { color = 0xffffff, shading = THREE.FlatShading, overdraw = 0.5 });

                for (var i = 0; i < 4; i++)
                {

                    new HZWaterTower().Source.Task.ContinueWithResult(
                        cube =>
                        {
                            // um can edit and continue insert code going back in time?
                            cube.scale.x = 2.0;
                            cube.scale.y = 2.0;
                            cube.scale.z = 2.0;

                            //dae.castShadow = true;
                            //dae.receiveShadow = true;

                            cube.position.x = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;
                            //cube.position.y = (cube.scale.y * 50) / 2;
                            cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;

                            scene.add(cube);
                            interactiveObjects.Add(cube);
                        }
                    );

                    new HZCannon().Source.Task.ContinueWithResult(
                        cube =>
                        {
                            // um can edit and continue insert code going back in time?
                            cube.scale.x = 2.0;
                            cube.scale.y = 2.0;
                            cube.scale.z = 2.0;

                            //dae.castShadow = true;
                            //dae.receiveShadow = true;

                            cube.position.x = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;
                            //cube.position.y = (cube.scale.y * 50) / 2;
                            cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;

                            scene.add(cube);
                            interactiveObjects.Add(cube);
                        }
                    );

                    //var cube = new THREE.Mesh(geometry, material);

                    ////cube.scale.y = Math.Floor(random() * 2 + 1);

                    //cube.position.x = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;
                    //cube.position.y = (cube.scale.y * 50) / 2;
                    //cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;

                    //scene.add(cube);

                }
            }
            #endregion


            #region  Lights

            var ambientLight = new THREE.AmbientLight((int)(random() * 0x10));
            scene.add(ambientLight);

            {
                var directionalLight = new THREE.DirectionalLight((int)(random() * 0xffffff));

                directionalLight.position.x = random() - 0.5;
                directionalLight.position.y = 400;
                directionalLight.position.z = random() - 0.5;
                directionalLight.position.normalize();
                scene.add(directionalLight);
            }

            {
                var directionalLight = new THREE.DirectionalLight((int)(random() * 0xffffff));
                directionalLight.position.x = random() - 0.5;
                directionalLight.position.y = 400;
                directionalLight.position.z = random() - 0.5;
                directionalLight.position.normalize();
                scene.add(directionalLight);
            }
            #endregion


            //var renderer = new THREE.CanvasRenderer();
            var renderer = new THREE.WebGLRenderer();

            // background-color: #B27D51;
            renderer.setClearColor(0xB27D51);
            //renderer.setSize(
            //    Native.window.Width , 
            //    Native.window.Height * 10
            //    );
            renderer.setSize();

            renderer.domElement.AttachToDocument();

            //window.addEventListener( 'resize', onWindowResize, false );


            var s = Stopwatch.StartNew();

            Native.window.onframe +=
                e =>
                {
                    // jsc, when can we have the edit and continue already?
                    //var timer = s.ElapsedMilliseconds * 0.1;
                    var timer = s.ElapsedMilliseconds * 0.0001;

                    camera.position.x = Math.Cos(timer) * 200;
                    camera.position.z = Math.Sin(timer) * 200;
                    camera.lookAt(scene.position);

                    renderer.render(scene, camera);
                };


            // view-source:http://mrdoob.github.io/three.js/examples/webgl_interactive_voxelpainter.html
            //var mouse2D = new THREE.Vector3(0, 10000, 0.5);

            //renderer.domElement.onclick +=
            //    e =>
            //    {
            //        e.preventDefault();

            //        mouse2D.x = (e.CursorX / (double)Native.window.Width) * 2 - 1;
            //        mouse2D.y = -(e.CursorY / (double)Native.window.Height) * 2 + 1;

            //        var vector = new THREE.Vector3(
            //              (e.CursorX / (double)Native.window.Width) * 2 - 1,
            //              -(e.CursorY / (double)Native.window.Height) * 2 + 1,
            //             0.5);


            //        // X:\jsc.svn\examples\javascript\WebGL\WebGLInteractiveCubes\WebGLInteractiveCubes\Application.cs
            //        var projector = new THREE.Projector();
            //        projector.unprojectVector(vector, camera);

            //        // http://stackoverflow.com/questions/18553209/orthographic-camera-and-selecting-objects-with-raycast
            //        // http://stackoverflow.com/questions/20361776/orthographic-camera-and-pickingray
            //        // view-source:http://stemkoski.github.io/Three.js/Mouse-Click.html
            //        // http://stackoverflow.com/questions/11921033/projector-and-ray-with-orthographiccamera

            //        // use picking ray since it's an orthographic camera
            //        // doesnt fkin work ???
            //        //var raycaster = projector.pickingRay(vector, camera);

            //        var raycaster = projector.pickingRay(mouse2D.clone(), camera);

            //        //var raycaster = new THREE.Raycaster(camera.position, vector.sub(camera.position).normalize());
            //        var intersects = raycaster.intersectObjects(interactiveObjects.ToArray());

            //        // https://github.com/mrdoob/three.js/issues/599
            //        Native.document.title = new { intersects.Length }.ToString();
            //    };
        }

    }

    [Obsolete("jsc should generate this")]
    class HZCannon : THREE_ColladaAsset
    {
        string ref0 = "assets/HeatZeekerRTSOrto/HZCannon_capture_009_04032013_192834.png";

        public HZCannon()
            : base(
                "assets/HeatZeekerRTSOrto/HZCannon.dae"
                )
        {

        }
    }

    [Obsolete("jsc should generate this")]
    class HZWaterTower : THREE_ColladaAsset
    {
        string ref0 = "assets/HeatZeekerRTSOrto/HZCannon_capture_009_04032013_192834.png";

        public HZWaterTower()
            : base(
                "assets/HeatZeekerRTSOrto/HZWaterTower.dae"
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
