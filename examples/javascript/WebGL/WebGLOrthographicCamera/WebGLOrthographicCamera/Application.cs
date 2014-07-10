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
using WebGLOrthographicCamera;
using WebGLOrthographicCamera.Design;
using WebGLOrthographicCamera.HTML.Pages;
using System.Diagnostics;

namespace WebGLOrthographicCamera
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
            // "X:\opensource\github\three.js\examples\canvas_camera_orthographic.html"

            // if i type THREE, would jsc be able to add THREE package on rebuild?
            // Error	136	The type or namespace name 'THREE' could not be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\WebGL\WebGLOrthographicCamera\WebGLOrthographicCamera\Application.cs	35	26	WebGLOrthographicCamera


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

            // Cubes
            {
                var geometry = new THREE.BoxGeometry(50, 50, 50);
                var material = new THREE.MeshLambertMaterial(new { color = 0xffffff, shading = THREE.FlatShading, overdraw = 0.5 });

                for (var i = 0; i < 100; i++)
                {
                    var cube = new THREE.Mesh(geometry, material);

                    cube.scale.y = Math.Floor(random() * 2 + 1);

                    cube.position.x = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;
                    cube.position.y = (cube.scale.y * 50) / 2;
                    cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;

                    scene.add(cube);

                }
            }

            // Lights

            var ambientLight = new THREE.AmbientLight((int)(random() * 0x10));
            scene.add(ambientLight);

            {
                var directionalLight = new THREE.DirectionalLight((int)(random() * 0xffffff));

                directionalLight.position.x = random() - 0.5;
                directionalLight.position.y = random() - 0.5;
                directionalLight.position.z = random() - 0.5;
                directionalLight.position.normalize();
                scene.add(directionalLight);
            }

            {
                var directionalLight = new THREE.DirectionalLight((int)(random() * 0xffffff));
                directionalLight.position.x = random() - 0.5;
                directionalLight.position.y = random() - 0.5;
                directionalLight.position.z = random() - 0.5;
                directionalLight.position.normalize();
                scene.add(directionalLight);
            }

            //var renderer = new THREE.CanvasRenderer();
            var renderer = new THREE.WebGLRenderer();
            renderer.setClearColor(0xf0f0f0);
            //renderer.setSize(Native.window.Width, Native.window.Height);
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

        }

    }
}
