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
using WebGLVRCreativeLeadership;
using WebGLVRCreativeLeadership.Design;
using WebGLVRCreativeLeadership.HTML.Pages;

namespace WebGLVRCreativeLeadership
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //0001 02000178 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProvider
        //script: error JSC1000: Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
        //internal compiler error at method

        public Application(IApp page)
        {
            //          hResolution: 1920,
            //vResolution: 1080,

            // "X:\jsc.svn\examples\javascript\synergy\webgl\WebGLEquirectangularPanorama\WebGLEquirectangularPanorama.sln"

            // http://oculusstreetview.eu.pn/?lat=44.301987&lng=9.211561999999958&q=3&s=false&heading=0
            // https://github.com/troffmo5/OculusStreetView

            // http://stackoverflow.com/questions/23817633/threejs-using-a-sprite-with-the-oculusrifteffect
            // http://laht.info/dk2-parameters-for-three-oculusrifteffect-js/

            // http://stemkoski.github.io/Three.js/Sprites.html
            // http://stemkoski.github.io/Three.js/Texture-Animation.html
            // http://blog.thematicmapping.org/2013/10/terrain-visualization-with-threejs-and.html

            // http://mrdoob.github.io/three.js/examples/webgl_panorama_equirectangular.html

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
                        new WebGLVRCreativeLeadership.HTML.Images.FromAssets._2294472375_24a3b8ef46_o().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20130616_222058().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20121225_210448().src

                        )
                }));
            mesh.scale.x = -1;
            scene.add(mesh);

            var crateTexture = THREE.ImageUtils.loadTexture(
                new ChromeCreativeLeadership.HTML.Images.FromAssets.Mockup().src

            );

            var crateMaterial = new THREE.SpriteMaterial(
                new
                {
                    map = crateTexture,
                    useScreenCoordinates = false,
                    //color = 0xff0000
                    color = 0xffffff
                }
        );

            var sprite2 = new THREE.Sprite(crateMaterial);

            //floor
            //sprite2.position.set(0, -200, 0);

            // left
            //sprite2.position.set(200, 50, 0);

            sprite2.position.set(0, 0, 200);

            //sprite2.position.set(-100, 0, 0);
            sprite2.scale.set(
                WebGLVRCreativeLeadership.HTML.Images.FromAssets._2294472375_24a3b8ef46_o.ImageDefaultWidth * 0.08,
                WebGLVRCreativeLeadership.HTML.Images.FromAssets._2294472375_24a3b8ef46_o.ImageDefaultHeight * 0.08,
                //64, 64,
                1.0); // imageWidth, imageHeight
            scene.add(sprite2);


            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.Width, window.Height);


            var effect = new THREE.OculusRiftEffect(renderer, new { worldScale = 100 });
            effect.setSize(window.Width, window.Height);


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

                    renderer.setSize(window.Width, window.Height);
                    effect.setSize(window.Width, window.Height);
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

                    //renderer.render(scene, camera);
                    effect.render(scene, camera);
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
