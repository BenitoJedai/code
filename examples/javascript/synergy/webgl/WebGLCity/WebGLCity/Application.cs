using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLCity;
using WebGLCity.Design;
using WebGLCity.HTML.Pages;

namespace WebGLCity
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed partial class Application
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page = null)
        {
            InitializeComponent();

            // http://johnstejskal.com/wp/super-hero-city-my-3d-webgl-game-using-three-js/
            // http://www.johnstejskal.com/dev/super-hero-city/

            //DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            // decent simcity comes to mind
            // http://www.mrdoob.com/lab/javascript/webgl/city/01/

            //var renderer = this.renderer.renderer;

            var renderer = new THREE.WebGLRenderer(
                new { antialias = false, alpha = false }
            );

            renderer.setClearColor(new THREE.Color(0xd8e7ff));

            renderer.setSize(Native.window);

            // INodeConvertible?
            renderer.domElement.AttachToDocument();
            renderer.domElement.style.SetLocation(0, 0);

            var camera = new THREE.PerspectiveCamera(40, Native.window.aspect, 1, 3000);
            camera.position.y = 80;

            var scene = new THREE.Scene();
            scene.fog = new THREE.FogExp2(0xd0e0f0, 0.0025);


            {
                var light = new THREE.HemisphereLight(0xfffff0, 0x101020, 1.25);
                light.position.set(0.75, 1, 0.25);
                scene.add(light);
            }



            var plane = new THREE.Mesh(
                new THREE.PlaneGeometry(2000, 2000),
                new THREE.MeshBasicMaterial(new { color = 0x101018 })
            );

            plane.rotation.x = -90 * Math.PI / 180;
            scene.add(plane);







            var building_geometry = new THREE.CubeGeometry(1, 1, 1);
            building_geometry.applyMatrix(new THREE.Matrix4().makeTranslation(0, 0.5, 0));

            ((IArray<THREE.Face4>)(object)building_geometry.faces).splice(3, 1);

            ((IArray<object>)(object)building_geometry.faceVertexUvs[0]).splice(3, 1);

            building_geometry.faceVertexUvs[0][2][0].set(0, 0);
            building_geometry.faceVertexUvs[0][2][1].set(0, 0);
            building_geometry.faceVertexUvs[0][2][2].set(0, 0);

            // Uncaught TypeError: Cannot call method 'set' of undefined view-source:84609
            //building_geometry.faceVertexUvs[0][2][3].set(0, 0);


            Func<double> random = new Random().NextDouble;


            var building = new THREE.Mesh(building_geometry);
            var city = new THREE.Geometry();

            {
                var light = new THREE.Color(0xffffff);
                var shadow = new THREE.Color(0x303050);

                #region city
                for (var i = 0; i < 20000; i++)
                {

                    var value = 1 - random() * random();
                    var color = new THREE.Color(0).setRGB(value + random() * 0.1, value, value + random() * 0.1);

                    var top = color.clone().multiply(light);
                    var bottom = color.clone().multiply(shadow);

                    building.position.x = Math.Floor(random() * 200 - 100) * 10;
                    building.position.z = Math.Floor(random() * 200 - 100) * 10;
                    building.rotation.y = random();

                    building.scale.z = random() * random() * random() * random() * 50 + 10;
                    building.scale.x = building.scale.z;

                    building.scale.y = (random() * random() * random() * building.scale.x) * 8 + 8;

                    var geometry = building.geometry;

                    var jl = geometry.faces.Length;

                    for (var j = 0; j < jl; j++)
                    {
                        if (j == 2)
                        {
                            geometry.faces[j].vertexColors = new[] { color, color, color, color };
                        }
                        else
                        {
                            geometry.faces[j].vertexColors = new[] { top, bottom, bottom, top };
                        }
                    }

                    // THREE.GeometryUtils: .merge() has been moved to Geometry. Use geometry.merge( geometry2, matrix, materialIndexOffset ) instead.
                    // stop moving around code!


                    //city.merge
                    //city.merge(building.geometry);
                    // how??
                    THREE.GeometryUtils.merge(city, building);

                }
                #endregion

                #region generateTexture
                Func<IHTMLCanvas> generateTexture = delegate
                {


                    var context1 = new CanvasRenderingContext2D(32, 64);

                    context1.fillStyle = "#ffffff";
                    context1.fillRect(0, 0, 32, 64);

                    for (var y = 2; y < 64; y += 2)
                        for (var x = 0; x < 32; x += 2)
                        {

                            var value = Math.Floor(random() * 64);
                            context1.fillStyle = "rgb(" + value + "," + value + "," + value + ")";
                            context1.fillRect(x, y, 2, 1);

                        }


                    var context = new CanvasRenderingContext2D(512, 1024)
                    {
                        ImageSmoothingEnabled = false
                    };


                    context.drawImage(context1.canvas, 0, 0, context.canvas.width, context.canvas.height);

                    return context.canvas;

                };
                #endregion

                var texture = new THREE.Texture(generateTexture())
                {
                    anisotropy = renderer.getMaxAnisotropy(),
                    needsUpdate = true
                };

                var mesh = new THREE.Mesh(city, new THREE.MeshLambertMaterial(new { map = texture, vertexColors = THREE.VertexColors }));
                scene.add(mesh);
            }


            var controls = new THREE.FirstPersonControls(camera)
            {
                movementSpeed = 20,
                lookSpeed = 0.05,
                lookVertical = true
            };


            //var lastTime = Native.window.performance.now() / 1000;
            var delta = new Stopwatch();

            Native.window.onframe +=
                delegate
                {
                    //var time = Native.window.performance.now() / 1000;

                    controls.update(delta.ElapsedMilliseconds);
                    renderer.render(scene, camera);

                    //lastTime = time;
                };
        }



    }


}
