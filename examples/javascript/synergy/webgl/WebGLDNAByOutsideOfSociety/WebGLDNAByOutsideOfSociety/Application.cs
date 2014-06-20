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
using ScriptCoreLib.JavaScript.Native;
using System.Math;
using System.Diagnostics;

namespace WebGLDNAByOutsideOfSociety
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
            // view-source:http://oos.moxiecode.com/js_webgl/dna/

            // var container;

            //var camera, scene, renderer, composer;

            //var has_gl = false;

            //var delta;
            //var time;
            //var oldTime;

            //var mesh, pmesh;

            //init();
            //animate();

            //function init() {

            //container = document.createElement('div');
            //document.body.appendChild(container);

            var scene = new THREE.Scene();
            scene.fog = new THREE.Fog(0x000000, 20, 70);

            var camera = new THREE.PerspectiveCamera(70, window.aspect, 1, 10000);
            camera.position.z = -50;
            camera.lookAt(scene.position);
            scene.add(camera);

            var pg = new THREE.PlaneGeometry(170, 10, 80, 1);

            var il = pg.vertices.Length;

            for (var i = 0; i < il; i++)
            {

                pg.vertices[i].z = (4 + (i / 15)) * Cos(i / 5);

                pg.vertices[i].y = (6 + (i / 30)) * Sin(i / 5);

            }

            var mat = new THREE.MeshBasicMaterial(new { wireframe = true, color = 0xb2ffd8});
            var mesh = new THREE.Mesh(pg, mat);
            mesh.rotation.x = -Math.PI / 2;
            scene.add(mesh);

            var vertices = pg.vertices;
            var vl = vertices.Length;

            var geometry = new THREE.Geometry();

            // c#, you can look ahead an realize how long our array needs to be!
            var vertices_tmp = new double[vl][];

            for (var i = 0; i<vl; i++)
            {
                var p = vertices[i];

                geometry.vertices[i] = p.clone();
                vertices_tmp[i] =new[] { p.x, p.y, p.z, 0, 0 };

            }


            var material = new THREE.ParticleBasicMaterial(
                new {
                    map = THREE.ImageUtils.loadTexture("bob.png"),
                    color = 0xb2ffd8, 
                    depthTest = false, 
                    size = 5, 
                    blending = THREE.NormalBlending
                } );

			var pmesh = new THREE.ParticleSystem( geometry, material );
			scene.add(pmesh);

			// renderer
			var renderer = new THREE.WebGLRenderer(new {antialias = false});
			renderer.setSize( );
			renderer.setClearColorHex( 0x000000, 1 );
			renderer.autoClear = false;
            //THREEx.WindowResize(renderer, camera);

            renderer.domElement.AttachToDocument();
            //container.appendChild( renderer.domElement );


            // post
            var renderModel = new THREE.RenderPass(scene, camera);
            //         var effectBloom = new THREE.BloomPass(3.0);
            //         var effectScreen = new THREE.ShaderPass(THREE.ShaderExtras["screen"]);

            //         effectHBlur = new THREE.ShaderPass( THREE.ShaderExtras["horizontalBlur"] );
            //effectHBlur.uniforms['h'].value = 2.0 / window.innerWidth;

            //effectVBlur = new THREE.ShaderPass( THREE.ShaderExtras["verticalBlur"] );
            //effectVBlur.uniforms['v'].value = 2.0 / window.innerHeight;

            //effectScreen.renderToScreen = true;

            var composer = new THREE.EffectComposer(renderer);

            composer.addPass(renderModel);

            //composer.addPass( effectHBlur );
            //composer.addPass( effectVBlur );				
            //composer.addPass( effectBloom );
            //composer.addPass( effectScreen );	


            var t = Stopwatch.StartNew();

            window.onframe +=
                delegate
                {
                    time = new Date().getTime();
                    delta = time - oldTime;
                    oldTime = time;

                    if (isNaN(delta) || delta > 1000 || delta == 0)
                    {
                        delta = 1000 / 60;
                    }

                    mesh.rotation.x += 0.02 + Abs(Sin(time / 3000)) / 40;
                    pmesh.rotation.x = mesh.rotation.x;

                    mesh.scale.y = Cos(time / 2500) * 2.0;
                    pmesh.scale.y = mesh.scale.y;
               
                    renderer.clear();
                    composer.render();
                };

        }

    }
}
