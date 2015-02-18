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
using WebGLDashedLines;
using WebGLDashedLines.Design;
using WebGLDashedLines.HTML.Pages;

namespace WebGLDashedLines
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
            Native.body.Clear();
            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;


            // http://threejs.org/examples/webgl_lines_dashed.html

            var objects = new List<THREE.Line>();

            var camera = new THREE.PerspectiveCamera(60,
                Native.window.aspect, 1, 200);
            camera.position.z = 150;

            var scene = new THREE.Scene();

            scene.fog = new THREE.Fog(0x111111, 150, 200);

            var root = new THREE.Object3D();

            var subdivisions = 6;
            var recursion = 1;

            var points = hilbert3D(new THREE.Vector3(0, 0, 0), 25.0, recursion, 0, 1, 2, 3, 4, 5, 6, 7);

            Console.WriteLine(
                new { points.Length }
            );

            #region spline
            var spline = new THREE.Spline(points);
            var geometrySpline = new THREE.Geometry();

            for (var i = 0; i < points.Length * subdivisions; i++)
            {

                var index = (double)i / (points.Length * subdivisions);
                var position = spline.getPoint(index);

                geometrySpline.vertices[i] = new THREE.Vector3(position.x, position.y, position.z);

                Console.WriteLine(
           new { i, index, position.x, position.y, position.z }
       );

            }
            #endregion

            var geometryCube = cube(50);

            geometryCube.computeLineDistances();
            geometrySpline.computeLineDistances();

            {
                var o = new THREE.Line(geometrySpline, new THREE.LineDashedMaterial(
                    new { color = 0xffffff, dashSize = 1, gapSize = 0.5 }
                    ), THREE.LineStrip);

                objects.Add(o);
                scene.add(o);
            }
            {

                var o = new THREE.Line(geometryCube, new THREE.LineDashedMaterial(
                    new { color = 0xffaa00, dashSize = 3, gapSize = 1, linewidth = 2 }
                    ), THREE.LinePieces);

                objects.Add(o);
                scene.add(o);
            }

            {

                var o = new THREE.Line(geometryCube, new THREE.LineDashedMaterial(
                    new { color = 0xffaa00, dashSize = 3, gapSize = 1, linewidth = 2 }
                    ), THREE.LinePieces);

                objects.Add(o);
                scene.add(o);
            }

            var renderer = new THREE.WebGLRenderer(new { antialias = true });
            renderer.setClearColor(0x111111);
            //renderer.setPixelRatio( window.devicePixelRatio );
            renderer.setSize(
                Native.window.Width,
                Native.window.Height
                );


            renderer.domElement.AttachToDocument();



            var sw = Stopwatch.StartNew();

            Native.window.onframe +=
                delegate
                {
                    var time = sw.ElapsedMilliseconds * 0.001;

                    for (var i = 0; i < objects.Count; i++)
                    {

                        var o = objects[i];

                        //object.rotation.x = 0.25 * time * ( i%2 == 1 ? 1 : -1);
                        o.rotation.x = 0.25 * time;
                        o.rotation.y = 0.25 * time;

                    }

                    renderer.render(scene, camera);
                };
        }



        static THREE.Geometry cube(double size = 50)
        {

            var h = size * 0.5;

            var geometry = new THREE.Geometry();

            geometry.vertices = new[] {
                new THREE.Vector3(-h, -h, -h),
                new THREE.Vector3(-h, h, -h),

                new THREE.Vector3(-h, h, -h),
                new THREE.Vector3(h, h, -h),

                new THREE.Vector3(h, h, -h),
                new THREE.Vector3(h, -h, -h),

                new THREE.Vector3(h, -h, -h),
                new THREE.Vector3(-h, -h, -h),


                new THREE.Vector3(-h, -h, h),
                new THREE.Vector3(-h, h, h),

                new THREE.Vector3(-h, h, h),
                new THREE.Vector3(h, h, h),

                new THREE.Vector3(h, h, h),
                new THREE.Vector3(h, -h, h),

                new THREE.Vector3(h, -h, h),
                new THREE.Vector3(-h, -h, h),

                new THREE.Vector3(-h, -h, -h),
                new THREE.Vector3(-h, -h, h),

                new THREE.Vector3(-h, h, -h),
                new THREE.Vector3(-h, h, h),

                new THREE.Vector3(h, h, -h),
                new THREE.Vector3(h, h, h),

                new THREE.Vector3(h, -h, -h),
                new THREE.Vector3(h, -h, h)
             };

            return geometry;

        }


        // var points = hilbert3D( new THREE.Vector3( 0,0,0 ), 25.0, recursion, 0, 1, 2, 3, 4, 5, 6, 7 );
        static THREE.Vector3[] hilbert3D(
            THREE.Vector3 center,
            double size = 25.0,
            int iterations = 1,
            int v0 = 0,
            int v1 = 1,
            int v2 = 2,
            int v3 = 3,
            int v4 = 4,
            int v5 = 5,
            int v6 = 6,
            int v7 = 7)
        {
            // 0:71ms {{ i = 0, x = -18.75, y = 18.75, z = -18.75 }}

            // Default Vars
            var half = size / 2;


            var vec_s = new[] {
                new THREE.Vector3(center.x - half, center.y + half, center.z - half),
                new THREE.Vector3(center.x - half, center.y + half, center.z + half),
                new THREE.Vector3(center.x - half, center.y - half, center.z + half),
                new THREE.Vector3(center.x - half, center.y - half, center.z - half),
                new THREE.Vector3(center.x + half, center.y - half, center.z - half),
                new THREE.Vector3(center.x + half, center.y - half, center.z + half),
                new THREE.Vector3(center.x + half, center.y + half, center.z + half),
                new THREE.Vector3(center.x + half, center.y + half, center.z - half)
            };

            var vec = new[] {
                vec_s[v0],
                vec_s[v1],
                vec_s[v2],
                vec_s[v3],
                vec_s[v4],
                vec_s[v5],
                vec_s[v6],
                vec_s[v7]
            };

            // Recurse iterations

            //if (--iterations >= 0)
            if (iterations > 0)
            {
                iterations--;

                var tmp = new[] {
                    hilbert3D(vec[0], half, iterations, v0, v3, v4, v7, v6, v5, v2, v1),
                    hilbert3D(vec[1], half, iterations, v0, v7, v6, v1, v2, v5, v4, v3),
                    hilbert3D(vec[2], half, iterations, v0, v7, v6, v1, v2, v5, v4, v3),
                    hilbert3D(vec[3], half, iterations, v2, v3, v0, v1, v6, v7, v4, v5),
                    hilbert3D(vec[4], half, iterations, v2, v3, v0, v1, v6, v7, v4, v5),
                    hilbert3D(vec[5], half, iterations, v4, v3, v2, v5, v6, v1, v0, v7),
                    hilbert3D(vec[6], half, iterations, v4, v3, v2, v5, v6, v1, v0, v7),
                    hilbert3D(vec[7], half, iterations, v6, v5, v2, v1, v0, v3, v4, v7)
                };

                // Return recursive call
                return tmp.SelectMany(x => x).ToArray();
            }

            // Return complete Hilbert Curve.
            return vec;
        }

    }
}
