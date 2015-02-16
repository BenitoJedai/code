using com.abstractatech.apps.vr.HTML.Images.FromAssets;
using com.abstractatech.apps.vr.HTML.Pages;
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
using WebGLAudi.__AssetsLibrary__;
using WebGLEarthByBjorn.HTML.Images.FromAssets;
using WebGLNexus7;
using WebGLVRCreativeLeadership;

//namespace WebGLVRCreativeLeadership
namespace com.abstractatech.vr
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : WebGLVRCreativeLeadershipApplicationWebService
    {
        //0001 02000178 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProvider
        //script: error JSC1000: Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
        //internal compiler error at method

        //       script: error JSC1000: Java :
        //BCL needs another method, please define it.
        //Cannot call type without script attribute :
        //System.Threading.Monitor for Void Enter(System.Object, Boolean ByRef) used at
        //WebGLVRCreativeLeadership.Activities.ApplicationWebServiceActivity+<>c__DisplayClass24.<CreateServer>b__29 at offset 0018.
        //If the use of this method is intended, an implementation should be provided with the attribute[Script(Implements = typeof(...)] set.You may have mistyped it.

        public Application(IApp page)
        {
            // https://play.google.com/store/apps/details?id=com.abstractatech.vr

            Native.body.Clear();
            Native.body.style.margin = "0px";
            Native.body.style.backgroundColor = "black";

            // https://vronecontest.zeiss.com/index.php?controller=ideas&view=show&id=652

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
            //scene.add(new THREE.AmbientLight(0xffffff));
            //scene.add(new THREE.AmbientLight(0xafafaf));

            // http://www.html5canvastutorials.com/three/html5-canvas-webgl-ambient-lighting-with-three-js/

            // http://stackoverflow.com/questions/14717135/three-js-ambient-light-unexpected-effect

            scene.add(new THREE.AmbientLight(0x2f2f2f));


            //var light = new THREE.DirectionalLight(0xffffff, 1.0);
            #region light
            var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 1.5);
            //var lightOffset = new THREE.Vector3(0, 1000, 2500.0);
            var lightOffset = new THREE.Vector3(
                2000,
                700,

                // lower makes longer shadows 
                700.0
                );
            light.position.copy(lightOffset);
            light.castShadow = true;

            var xlight = light as dynamic;
            xlight.shadowMapWidth = 4096;
            xlight.shadowMapHeight = 2048;

            xlight.shadowDarkness = 0.3;
            //xlight.shadowDarkness = 0.5;

            xlight.shadowCameraNear = 10;
            xlight.shadowCameraFar = 10000;
            xlight.shadowBias = 0.00001;
            xlight.shadowCameraRight = 4000;
            xlight.shadowCameraLeft = -4000;
            xlight.shadowCameraTop = 4000;
            xlight.shadowCameraBottom = -4000;

            xlight.shadowCameraVisible = true;

            scene.add(light);
            #endregion

            var mesh = new THREE.Mesh(new THREE.SphereGeometry(500, 60, 40),
                new THREE.MeshBasicMaterial(new
                {
                    map = THREE.ImageUtils.loadTexture(
                        new _2294472375_24a3b8ef46_o().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20130616_222058().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20121225_210448().src

                        )
                }));
            mesh.scale.x = -1;
            scene.add(mesh);

            #region sprite2
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
               _2294472375_24a3b8ef46_o.ImageDefaultWidth * 0.08,
               _2294472375_24a3b8ef46_o.ImageDefaultHeight * 0.08,
                //64, 64,
                1.0); // imageWidth, imageHeight
            scene.add(sprite2);
            #endregion


            #region ColladaAudiA4
            new ColladaAudiA4().Source.Task.ContinueWithResult(
                   dae =>
                   {
                       // 90deg
                       //dae.rotation.x = -Math.Cos(Math.PI);

                       //dae.scale.x = 30;
                       //dae.scale.y = 30;
                       //dae.scale.z = 30;
                       //dae.position.z = 65;

                       // right
                       dae.position.z = -20;
                       dae.position.x = -100;
                       dae.position.y = -90;


                       // jsc, do we have ILObserver available yet?
                       dae.scale.x = 1.0;
                       dae.scale.y = 1.0;
                       dae.scale.z = 1.0;

                       //dae.position.y = -80;

                       scene.add(dae);
                       //oo.Add(dae);


                   }
               );
            #endregion




            #region nexus7
            new nexus7().Source.Task.ContinueWithResult(
                   dae =>
                   {
                       // 90deg
                       dae.rotation.x = -Math.Cos(Math.PI);

                       //dae.scale.x = 30;
                       //dae.scale.y = 30;
                       //dae.scale.z = 30;
                       //dae.position.z = 65;

                       // left
                       //dae.position.x = 200;

                       dae.position.z = -100;
                       dae.position.y = -50;

                       //dae.position.z = 100;



                       // jsc, do we have ILObserver available yet?
                       dae.scale.x = 13.5;
                       dae.scale.y = 13.5;
                       dae.scale.z = 13.5;

                       //dae.position.y = -80;

                       scene.add(dae);
                       //oo.Add(dae);


                   }
               );
            #endregion


     


            //          // DK2
            //          hResolution: 1920,
            //vResolution: 1080,

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(1920, 1080);


            // broken?
            var distortionK = new double[] { 1.0, 0.22, 0.24, 0.0 };
            var chromaAbParameter = new double[] { 0.996, -0.004, 1.014, 0.0 };

            var HMD = new OculusRiftEffectOptions
            {
                hResolution = window.Width,
                vResolution = window.Height,

                hScreenSize = 0.12576,
                vScreenSize = 0.07074,
                interpupillaryDistance = 0.0635,
                lensSeparationDistance = 0.0635,
                eyeToScreenDistance = 0.041,

                //  j.distortionK = [0, 1.875, -71.68, 1.595, -3.218644E+26, 1.615, 0, 0];
                //distortionK = new double[] { 1.0, 0.22, 0.24, 0.0 },
                distortionK = distortionK,

                // j.chromaAbParameter = [1.609382E+22, 1.874, -5.189695E+11, -0.939, 4.463059E-29, 1.87675, 0, 0];
                //chromaAbParameter = new double[] { 0.996, -0.004, 1.014, 0.0 }
                chromaAbParameter = chromaAbParameter

            };

            var effect = new THREE.OculusRiftEffect(
                renderer, new
                {
                    worldScale = 100,

                    //HMD
                }
                );

            effect.setSize(1920, 1080);


            renderer.domElement.AttachToDocument();

            //renderer.domElement.style.position = IStyle.PositionEnum.absolute;
            renderer.domElement.style.SetLocation(0, 0);

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            // x:\jsc.svn\examples\javascript\synergy\comanchebysiorki\comanchebysiorki\application.cs

            new { }.With(
                     async delegate
                     {
                         retry:

                         var s = (double)Native.window.Width / 1920.0;


                         Native.document.body.style.transform = "scale(" + s + ")";
                         Native.document.body.style.transformOrigin = "0% 0%";

                         await Native.window.async.onresize;
                         goto retry;
                     }
                   );

            //#region onresize
            //Native.window.onresize +=
            //    delegate
            //    {
            //        camera.aspect = Native.window.aspect;
            //        camera.updateProjectionMatrix();

            //        renderer.setSize(window.Width, window.Height);
            //        effect.setSize(window.Width, window.Height);
            //    };
            //#endregion


            //Native.document.body.onmousewheel +=
            //    e =>
            //    {
            //        fov -= e.WheelDirection * 5.0;
            //        camera.projectionMatrix.makePerspective(fov,
            //            (double)window.Width / window.Height, 1, 1100);
            //    };

            var lon = 90.0;
            var lat = 0.0;
            var phi = 0.0;
            var theta = 0.0;

            //var controls = new THREE.OrbitControls(camera);


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


                    //controls.update();
                    //camera.position = controls.center.clone();


                    camera.lookAt(target);

                    //renderer.render(scene, camera);
                    effect.render(scene, camera);
                };



            // http://blog.thematicmapping.org/2013/10/terrain-visualization-with-threejs-and.html

            // http://stackoverflow.com/questions/13278087/determine-vertical-direction-of-a-touchmove

            #region camera rotation
            var old = new { clientX = 0, clientY = 0 };

            Native.document.body.ontouchstart +=
                e =>
                {
                    var n = new { e.touches[0].clientX, e.touches[0].clientY };
                    old = n;
                };

            Native.document.body.ontouchmove +=
                    e =>
                    {
                        var n = new { e.touches[0].clientX, e.touches[0].clientY };

                        e.preventDefault();

                        lon += (n.clientX - old.clientX) * 0.2;
                        lat -= (n.clientY - old.clientY) * 0.2;

                        old = n;
                    };


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

            // https://developer.android.com/training/system-ui/immersive.html

        }

    }

    internal class OculusRiftEffectOptions
    {
        internal double[] chromaAbParameter;
        internal double[] distortionK;
        internal double eyeToScreenDistance;
        internal int hResolution;
        internal double hScreenSize;
        internal double interpupillaryDistance;
        internal double lensSeparationDistance;
        internal int vResolution;
        internal double vScreenSize;
    }
}
