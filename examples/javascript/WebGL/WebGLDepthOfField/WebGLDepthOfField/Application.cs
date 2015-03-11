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
using WebGLDepthOfField;
using WebGLDepthOfField.Design;
using WebGLDepthOfField.HTML.Pages;
using WebGLSpeedBlendCharacter.HTML.Images.FromAssets;

namespace WebGLDepthOfField
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
            // X:\jsc.svn\examples\javascript\WebGL\WebGLDepthOfField\WebGLDepthOfField\Application.cs
            // http://threejs.org/examples/#webgl_postprocessing_dof
            // "X:\opensource\github\three.js\examples\webgl_postprocessing_dof.html"

            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.Clear();


            var camera = new THREE.PerspectiveCamera(70, Native.window.aspect, 1, 3000);
            camera.position.z = 200;

            var scene = new THREE.Scene();

            var renderer = new THREE.WebGLRenderer(new { antialias = false });
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.domElement.AttachToDocument();
            renderer.sortObjects = false;

            //var material_depth = new THREE.MeshDepthMaterial();


            var urls = new[] {
                            new px().src, new nx().src,
                            new py().src, new ny().src,
                            new pz().src, new nz().src,
                         };

            var textureCube = THREE.ImageUtils.loadTextureCube(urls);

            //var parameters = new { color = 0xff1100, envMap = textureCube, shading = THREE.FlatShading };
            // red vs yellow

            var parameters = new { color = 0xffff00, envMap = textureCube, shading = THREE.FlatShading };
            var cubeMaterial = new THREE.MeshBasicMaterial(parameters);

            var geo = new THREE.SphereGeometry(1, 20, 10);

            var xgrid = 14;
            var ygrid = 9;
            var zgrid = 14;

            var nobjects = xgrid * ygrid * zgrid;

            var c = 0;

            //var s = 0.25;
            var s = 60;

            for (var i = 0; i < xgrid; i++)
                for (var j = 0; j < ygrid; j++)
                    for (var k = 0; k < zgrid; k++)
                    {
                        var mesh = new THREE.Mesh(geo, cubeMaterial);

                        var x = 200 * (i - xgrid / 2);
                        var y = 200 * (j - ygrid / 2);
                        var z = 200 * (k - zgrid / 2);

                        mesh.position.set(x, y, z);
                        mesh.scale.set(s, s, s);

                        mesh.matrixAutoUpdate = false;
                        mesh.updateMatrix();

                        scene.add(mesh);
                        //objects.push(mesh);

                        c++;

                    }

            //scene.matrixAutoUpdate = false;

            var renderPass = new THREE.RenderPass(scene, camera);

            // THREE.BokehPass relies on THREE.BokehShader
            var bokehPass = new THREE.BokehPass(scene, camera, new
            {
                focus = 1.0,
                aperture = 0.025,
                maxblur = 1.0,

                width = Native.window.Width,
                height = Native.window.Height
            });

            bokehPass.renderToScreen = true;


            // maskpass
            // THREE.EffectComposer relies on THREE.CopyShader
            var composer = new THREE.EffectComposer(renderer);

            composer.addPass(renderPass);
            composer.addPass(bokehPass);

            renderer.autoClear = false;

            var effectController = new
            {
                focus = 1.0,
                aperture = 0.025,
                maxblur = 1.0
            };

            //var matChanger = function() {

            //        postprocessing.bokeh.uniforms["focus"].value = effectController.focus;
            //        postprocessing.bokeh.uniforms["aperture"].value = effectController.aperture;
            //        postprocessing.bokeh.uniforms["maxblur"].value = effectController.maxblur;

            //    };
            var controls = new THREE.OrbitControls(camera);

            #region onframe
            Native.window.onframe +=
                delegate
                {
                    controls.update();

                    camera.position = controls.center.clone();

                    composer.render(0.1);

                };

            #endregion


            new { }.With(
                  async delegate
                  {
                      //while (true)
                      do
                      {
                          camera.aspect = Native.window.aspect;
                          camera.updateProjectionMatrix();
                          renderer.setSize(Native.window.Width, Native.window.Height);

                          // convert to bool?
                      } while (await Native.window.async.onresize);
                      //} while (await Native.window.async.onresize != null);

                  }
              );


        }

    }
}
