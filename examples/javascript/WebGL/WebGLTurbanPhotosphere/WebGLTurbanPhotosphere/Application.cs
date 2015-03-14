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
using WebGLTurbanPhotosphere;
using WebGLTurbanPhotosphere.Design;
using WebGLTurbanPhotosphere.HTML.Pages;


using static THREE;
using static ScriptCoreLib.JavaScript.Native;

namespace WebGLTurbanPhotosphere
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
			// http://stackoverflow.com/questions/29048161/how-to-export-a-three-js-scene-into-a-360-texture-for-photosphere

			body.style.background = "black";

            body.style.margin = "0px";
            body.style.overflow = IStyle.OverflowEnum.hidden;
            body.Clear();



            // https://github.com/turban/photosphere/blob/gh-pages/stolanuten.html

            var scene = new THREE.Scene();


            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(Native.window.Width, Native.window.Height);
            // the thing you attach to dom
            renderer.domElement.AttachToDocument();


            var sphere = new Mesh(
                new SphereGeometry(100, 20, 20),
                new MeshBasicMaterial(
                    new
                    {
                        map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.stolanuten().src)
                    }
                )
            );
            sphere.scale.x = -1;
            sphere.AttachTo(scene);

            var camera = new PerspectiveCamera(75, window.aspect, 1, 1000);
            camera.position.x = 0.1;

            var controls = new THREE.OrbitControls(camera, renderer.domElement);






            window.onframe +=
                delegate
                {
                    controls.update();
                    camera.position = controls.center.clone();

                    renderer.render(scene, camera);


                };



            window.onresize +=
              delegate
              {
                  camera.aspect = window.aspect;
                  camera.updateProjectionMatrix();

                  renderer.setSize(window.Width, window.Height);

              };

            // http://www.visualstudio.com/en-us/news/vs2015-vs
        }

    }
}
