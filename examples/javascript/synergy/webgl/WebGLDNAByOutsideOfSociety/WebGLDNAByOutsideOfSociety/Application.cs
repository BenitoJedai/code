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
using WebGLDNAByOutsideOfSociety;
using WebGLDNAByOutsideOfSociety.Design;
using WebGLDNAByOutsideOfSociety.HTML.Pages;

//Application.cs(19,7): error CS0138: A using namespace directive can only be applied to namespaces; 'ScriptCoreLib.JavaScript.Native' is a type not a namespace [X:\jsc.svn\examples\javascript\synergy\webgl\WebGLDNAByOutsideOfSociety\WebGLDNAByOutsideOfSociety\WebGLDNAByOutsideOfSociety.csproj]
//Application.cs(20,7): error CS0138: A using namespace directive can only be applied to namespaces; 'System.Math' is a type not a namespace [X:\jsc.svn\examples\javascript\synergy\webgl\WebGLDNAByOutsideOfSociety\WebGLDNAByOutsideOfSociety\WebGLDNAByOutsideOfSociety.csproj]
// jsc.bc cannot use roslyn via msbuild yet?
//using ScriptCoreLib.JavaScript.Native;
//using System.Math;
using System.Diagnostics;
using WebGLDNAByOutsideOfSociety.HTML.Images.FromAssets;

namespace WebGLDNAByOutsideOfSociety
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public Application(IApp page)
        {
            // view-source:http://oos.moxiecode.com/js_webgl/dna/

            var scene = new THREE.Scene();
            scene.fog = new THREE.Fog(0x000000, 20, 70);

            var camera = new THREE.PerspectiveCamera(70, Native.window.aspect, 1, 10000);
            camera.position.z = -50;
            camera.lookAt(scene.position);
            scene.add(camera);

            var pg = new THREE.PlaneGeometry(170, 10, 80, 1);
            var il = pg.vertices.Length;

            for (var i = 0; i < il; i++)
            {
                pg.vertices[i].z = (4 + (i / 15.0)) * Math.Cos(i / 5.0);
                pg.vertices[i].y = (6 + (i / 30.0)) * Math.Sin(i / 5.0);
            }

            var mat = new THREE.MeshBasicMaterial(new { wireframe = true, color = 0xb2ffd8 });
            var mesh = new THREE.Mesh(pg, mat);
            mesh.rotation.x = -Math.PI / 2;
            scene.add(mesh);

            var vertices = pg.vertices;
            var vl = vertices.Length;
            var geometry = new THREE.Geometry();
            // c#, you can look ahead an realize how long our array needs to be!
            var vertices_tmp = new double[vl][];

            for (var i = 0; i < vl; i++)
            {
                var p = vertices[i];

                geometry.vertices[i] = p.clone();
                vertices_tmp[i] = new[] { p.x, p.y, p.z, 0, 0 };

            }


            var material = new THREE.ParticleSystemMaterial(
                new
            {
                map = THREE.ImageUtils.loadTexture(new bob().src),
                color = 0xb2ffd8,
                depthTest = false,
                size = 5,
                blending = THREE.NormalBlending
            }
            );

            var pmesh = new THREE.ParticleSystem(geometry, material);
            scene.add(pmesh);

            var renderer = new THREE.WebGLRenderer(new { antialias = false });
            renderer.setSize();
            renderer.autoClear = false;

            renderer.domElement.AttachToDocument();
            //renderer.domElement.style.position = IStyle.PositionEnum.@fixed;
            renderer.domElement.style.SetLocation(0, 0);
            renderer.setSize();

            Native.window.onresize +=
                delegate
                {
            renderer.setSize();
            };

            var w = Stopwatch.StartNew();

            Native.window.onframe +=
                delegate
            {
                var time = w.ElapsedMilliseconds;

                mesh.rotation.x += 0.02 + Math.Abs(Math.Sin(time / 3000.0)) / 40;
                pmesh.rotation.x = mesh.rotation.x;

                mesh.scale.y = Math.Cos(time / 2500.0) * 2.0;
                pmesh.scale.y = mesh.scale.y;

                renderer.render(scene, camera);
            };

        }

    }
}
