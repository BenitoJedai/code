using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLEquirectangularPanorama;
using WebGLEquirectangularPanorama.Design;
using WebGLEquirectangularPanorama.HTML.Pages;

namespace WebGLEquirectangularPanorama
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        //---------------------------
        //Asset Compiler
        //---------------------------
        //The Asset Compiler has found a few issues while preparing the assets! 

        //'.', hexadecimal value 0x00, is an invalid character.Line 1, position 1.

        //Please fix the issues and try again!
        //You may need to reconnect your external drive.

        //X:\jsc.svn\examples\javascript\synergy\webgl\WebGLEquirectangularPanorama\WebGLEquirectangularPanorama\WebGLEquirectangularPanorama.csproj
        //---------------------------
        //OK
        //---------------------------

        public Application(IApp page)
        {
			// http://mrdoob.github.io/three.js/examples/webgl_panorama_equirectangular.html
			// http://stackoverflow.com/users/94411/zproxy
			// https://www.shadertoy.com/view/XsBSDR#
			// https://www.shadertoy.com/view/4dsGD2
			// https://www.shadertoy.com/view/ldjGRw
			// https://www.youtube.com/watch?v=GnFGYN-npqM

			var window = Native.window;

            var fov = 70.0;

            var camera = new THREE.PerspectiveCamera(fov,
                window.aspect, 1, 1100);
            var target = new THREE.Vector3(0, 0, 0);

            //(camera as dynamic).target = target;

            var scene = new THREE.Scene();

            var mesh = new THREE.Mesh(new THREE.SphereGeometry(500, 60, 40),
                new THREE.MeshBasicMaterial(new
                {
                    map = THREE.ImageUtils.loadTexture(
                        new WebGLEquirectangularPanorama.HTML.Images.FromAssets._2294472375_24a3b8ef46_o().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20130616_222058().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20121225_210448().src

                        )
                }));
            mesh.scale.x = -1;
            scene.add(mesh);

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();

            //renderer.domElement.style.position = IStyle.PositionEnum.absolute;
            renderer.domElement.style.SetLocation(0, 0);

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;


            #region onresize
            Native.window.onresize +=
                delegate
                {
                    camera.aspect = Native.window.aspect;
                    camera.updateProjectionMatrix();

                    renderer.setSize(Native.window.Width, Native.window.Height);
                };
            #endregion


            Native.document.body.onmousewheel +=
                e =>
                {
                    fov -= e.WheelDirection * 5.0;
                    camera.projectionMatrix.makePerspective(fov,
                        (double)window.Width / window.Height, 1, 1100);
                };

            var lon = 90.0;
            var lat = 0.0;
            var phi = 0.0;
            var theta = 0.0;

            Native.window.onframe +=
                delegate
                {
                    if (Native.document.pointerLockElement == Native.document.body)
                        lon += 0.00;
                    else
                        lon += 0.01;

                    lat = Math.Max(-85, Math.Min(85, lat));

                    //Native.document.title = new { lon, lat }.ToString();


                    phi = THREE.Math.degToRad(90 - lat);
                    theta = THREE.Math.degToRad(lon);

                    target.x = 500 * Math.Sin(phi) * Math.Cos(theta);
                    target.y = 500 * Math.Cos(phi);
                    target.z = 500 * Math.Sin(phi) * Math.Sin(theta);

                    camera.lookAt(target);

                    renderer.render(scene, camera);

                };



            // http://blog.thematicmapping.org/2013/10/terrain-visualization-with-threejs-and.html

            #region camera rotation
            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        lon += e.movementX * 0.1;
                        lat -= e.movementY * 0.1;

                        //Console.WriteLine(new { lon, lat, e.movementX, e.movementY });
                    }
                };


            Native.document.body.onmouseup +=
              e =>
              {
                  //drag = false;
                  e.preventDefault();
              };

            Native.document.body.onmousedown +=
                e =>
                {
                    //e.CaptureMouse();

                    //drag = true;
                    e.preventDefault();
                    Native.document.body.requestPointerLock();

                };


            Native.document.body.ondblclick +=
                e =>
                {
                    e.preventDefault();

                    Console.WriteLine("requestPointerLock");
                };

            #endregion

        }

    }
}
