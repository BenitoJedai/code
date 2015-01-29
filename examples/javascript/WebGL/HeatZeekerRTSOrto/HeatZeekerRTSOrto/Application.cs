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
using System.Windows.Forms;

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

            // https://chrome.google.com/webstore/detail/dglmddjmdpdbijfkoaiadbpmjampfdjh/publish-delayed


            //#region ChromeTCPServer
            //dynamic self = Native.self;
            //dynamic self_chrome = self.chrome;
            //object self_chrome_socket = self_chrome.socket;

            //if (self_chrome_socket != null)
            //{
            //    #region AtFormCreated
            //    FormStyler.AtFormCreated =
            //         ss =>
            //     {
            //         ss.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //         // this is working?
            //         var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(ss.Context.GetHTMLTarget());
            //     };
            //    #endregion

            //    chrome.Notification.DefaultTitle = "Heat Zeeker";
            //    chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Promotion3D_iso1_tiltshift_128().src;

            //    ChromeTCPServer.TheServerWithStyledForm.Invoke(
            //        AppSource.Text,
            //        AtFormCreated: FormStyler.AtFormCreated
            //    );

            //    return;
            //}
            //#endregion


            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;

            // jsc, add THREE
            // ... ok.

            // X:\jsc.svn\examples\javascript\WebGL\WebGLOrthographicCamera\WebGLOrthographicCamera\Application.cs




            var camera = new THREE.OrthographicCamera(
                Native.window.Width / -2, Native.window.Width / 2, 
                Native.window.Height / 2, Native.window.Height / -2
                ,
                // if we change these values what will change?
                -1000, 1000
                );
            camera.position.x = 200;
            camera.position.y = 100;
            camera.position.z = 200;

            var scene = new THREE.Scene();

            // Grid

            var size = 600;
            var step = 50;


            Func<double> random = new Random().NextDouble;


            // how do I add a new ground box?
            {
                var geometry = new THREE.BoxGeometry(size * 2, 2, size * 2);
                var material = new THREE.MeshLambertMaterial(new
                {
                    color = 0xB27D51
                    //                                                     , shading = THREE.FlatShading, overdraw = 0.5
                });

                {
                    var cube = new THREE.Mesh(geometry, material);


                    // why cant we get the shadows??
                    cube.receiveShadow = true;


                    cube.scale.y = Math.Floor(random() * 2 + 1);

                    cube.position.x = 0;
                    //cube.position.y = (cube.scale.y * 50) / 2;
                    cube.position.y = -2;
                    cube.position.z = 0;

                    scene.add(cube);

                }
            }


            {
                var geometry = new THREE.Geometry();

                for (var i = -size; i <= size; i += step)
                {

                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(-size, 0, i));
                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(size, 0, i));

                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(i, 0, -size));
                    ((IArray<THREE.Vector3>)(object)geometry.vertices).push(new THREE.Vector3(i, 0, size));

                }

                var material = new THREE.LineBasicMaterial(new { color = 0, opacity = 0.2 });

                var line = new THREE.Line(geometry, material) { type = THREE.LinePieces };
                scene.add(line);
            }



            var interactiveObjects = new List<THREE.Object3D>();

            #region Cubes
            {

                for (var i = 0; i < 8; i++)
                {
                    new HZBunker().Source.Task.ContinueWithResult(
                        cube =>
                                {
                                    // https://github.com/mrdoob/three.js/issues/1285
                                    //cube.children.WithEach(c => c.castShadow = true);

                                    cube.traverse(
                                        new Action<THREE.Object3D>(
                                            child =>
                                                    {
                                                        // does it work? do we need it?
                                                        //if (child is THREE.Mesh)
                                                        child.castShadow = true;
                                                        child.receiveShadow = true;

                                                    }
                                        )
                                    );

                                    // um can edit and continue insert code going back in time?
                                    cube.scale.x = 2.0;
                                    cube.scale.y = 2.0;
                                    cube.scale.z = 2.0;

                                    //cube.castShadow = true;
                                    //dae.receiveShadow = true;

                                    cube.position.x = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;
                                    //cube.position.y = (cube.scale.y * 50) / 2;
                                    cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;

                                    scene.add(cube);
                                    interactiveObjects.Add(cube);
                                }
                    );


                    new HZWaterTower().Source.Task.ContinueWithResult(
                        cube =>
                        {
                            // https://github.com/mrdoob/three.js/issues/1285
                            // https://github.com/mrdoob/three.js/issues/1285
                            //cube.children.WithEach(c => c.castShadow = true);
                            // http://stackoverflow.com/questions/15906248/three-js-objloader-obj-model-not-casting-shadows


                            // http://stackoverflow.com/questions/22895120/imported-3d-objects-are-not-casting-shadows-with-three-js
                            cube.traverse(
                                new Action<THREE.Object3D>(
                                    child =>
                                    {
                                        // does it work? do we need it?
                                        //if (child is THREE.Mesh)
                                        child.castShadow = true;
                                        child.receiveShadow = true;

                                    }
                                )
                            );

                            // um can edit and continue insert code going back in time?
                            cube.scale.x = 2.0;
                            cube.scale.y = 2.0;
                            cube.scale.z = 2.0;

                            //cube.castShadow = true;
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
                            // https://github.com/mrdoob/three.js/issues/1285
                            //cube.children.WithEach(c => c.castShadow = true);

                            cube.traverse(
                                new Action<THREE.Object3D>(
                                    child =>
                                            {
                                                // does it work? do we need it?
                                                //if (child is THREE.Mesh)
                                                child.castShadow = true;
                                                child.receiveShadow = true;

                                            }
                                )
                            );

                            // um can edit and continue insert code going back in time?
                            cube.scale.x = 2.0;
                            cube.scale.y = 2.0;
                            cube.scale.z = 2.0;



                            //cube.castShadow = true;
                            //dae.receiveShadow = true;

                            cube.position.x = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;
                            //cube.position.y = (cube.scale.y * 50) / 2;
                            cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;



                            // if i want to rotate, how do I do it?
                            //cube.rotation.z = random() + Math.PI;
                            //cube.rotation.x = random() + Math.PI;
                            cube.rotation.y = random() * Math.PI * 2;


                            scene.add(cube);
                            interactiveObjects.Add(cube);
                        }
                    );



                }
            }
            #endregion

            // we need expression evaluator with intellisense for live debugging sessions
            #region  Lights

            var ambientLight = new THREE.AmbientLight((int)(random() * 0x10));
            scene.add(ambientLight);



            // can we get our shadows?
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
            renderer.shadowMapEnabled = true;

            // background-color: #B27D51;
            renderer.setClearColor(0xB27D51);
            //renderer.setSize(
            //    Native.window.Width , 
            //    Native.window.Height * 10
            //    );
            renderer.setSize();

            renderer.domElement.AttachToDocument();

            Native.window.onresize +=
                delegate
            {
                camera.left = Native.window.Width / -2;
                camera.right = Native.window.Width / 2;
                camera.top = Native.window.Height / 2;
                camera.bottom = Native.window.Height / -2;

                camera.updateProjectionMatrix();

                renderer.setSize();
            };

            //window.addEventListener( 'resize', onWindowResize, false );



            #region ee
            // X:\jsc.svn\examples\javascript\forms\NumericTextBox\NumericTextBox\ApplicationControl.cs
            // can we restile the window as the pin window in IDE?
            var ee = new Form { Left = 0, StartPosition = FormStartPosition.Manual };
            var ee_camera_y = new TextBox { Dock = DockStyle.Fill, Text = camera.position.y + "" }.AttachTo(ee);
            //ee.AutoSize = AutoSizeMode.

            //ee.ClientSize = new System.Drawing.Size(ee_camera_y.Width, ee_camera_y.Height);
            ee.ClientSize = new System.Drawing.Size(200, 24);

            ee.Show();

            //ee_camera_y.
            ee_camera_y.TextChanged += delegate
            {
                camera.position.y = double.Parse(ee_camera_y.Text);
            };
            #endregion

            var s = Stopwatch.StartNew();

            Native.window.onframe +=
                e =>
                {
                    // jsc, when can we have the edit and continue already?
                    //var timer = s.ElapsedMilliseconds * 0.1;
                    var timer = s.ElapsedMilliseconds * 0.0001;

                    camera.position.x = Math.Cos(timer) * 200;
                    camera.position.z = Math.Sin(timer) * 200;


                    // camera.position.z = 200;
                    //camera.position.y = 100;
                    //camera.position.y = Math.Sin(timer * 0.1) * 200;

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
    public class HZBunker : THREE_ColladaAsset
    {
        string ref0 = "assets/HeatZeekerRTSOrto/HZCannon_capture_009_04032013_192834.png";

        public HZBunker()
            : base(
                "assets/HeatZeekerRTSOrto/HZBunker.dae"
                )
        {

        }
    }



    // x:\jsc.svn\examples\javascript\webgl\WebGLHZBlendCharacter\WebGLHZBlendCharacter\Application.cs
    [Obsolete("jsc should generate this")]
    public class HZCannon : THREE_ColladaAsset
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
