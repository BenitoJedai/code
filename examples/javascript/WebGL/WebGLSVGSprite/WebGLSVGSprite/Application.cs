using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLEquirectangularPanorama.HTML.Images.FromAssets;
using WebGLRah66Comanche.Library;
using WebGLSVGSprite;
using WebGLSVGSprite.Design;
using WebGLSVGSprite.HTML.Pages;

namespace WebGLSVGSprite
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
            // http://stackoverflow.com/questions/14103986/canvas-and-spritematerial

            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGAnonymous\WebGLSVGAnonymous\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLVRCreativeLeadership\WebGLVRCreativeLeadership\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGSprite\WebGLSVGSprite\Application.cs

            //var l = new NotificationLayout().layout;

            //l.AttachToDocument();

            // : INodeConvertible<IHTMLElement>
            //var c = (IHTMLCanvas)l.layout;
            //var c = (IHTMLCanvas)l;

            // look we have a databound 2D image!

            // make it implicit operator for assetslibrary?

            //l.style



            //c.AttachToDocument();



            // https://play.google.com/store/apps/details?id=com.abstractatech.vr
            // could we display LAN UDP notifications too. like
            // which youtube video is playing?

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
                1920.0 / 1080.0,
                //window.aspect,
                1, 1100);
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

            var labove = new NotificationLayout();

            #region sprite2 above
            {
                labove.Message.innerText = "VR CREATIVE LEADERSHIP";
                labove.layout.style.background = "";

                new { }.With(
                    async delegate
                    {
                        retry:

                        // make it blink. gaze cursor is on it?


                        labove.box.style.background = "rgba(255,255,0,0.7)";
                        labove.box.setAttribute("invoke", "onmutation1");
                        await Task.Delay(1500);

                        // is mutation observer noticing it?
                        labove.box.style.background = "rgba(255,255,255,0.7)";
                        labove.box.setAttribute("invoke", "onmutation2");
                        await Task.Delay(1500);


                        goto retry;
                    }
                );



                var c = labove.AsCanvas();

                var xcrateTexture = new THREE.Texture(c);

                // like video texture
                Native.window.onframe += delegate { xcrateTexture.needsUpdate = true; };

                var xcrateMaterial = new THREE.SpriteMaterial(
                    new
                    {
                        map = xcrateTexture,
                        useScreenCoordinates = false,
                        //color = 0xff0000
                        color = 0xffffff
                    }
            );



                var xsprite2 = new THREE.Sprite(xcrateMaterial);

                //floor
                //sprite2.position.set(0, -200, 0);

                // left
                xsprite2.position.set(200, 0, 0);

                //sprite2.position.set(0, 0, 200);

                //sprite2.position.set(-100, 0, 0);
                xsprite2.scale.set(
                   c.width * 0.5,
                   c.height * 0.5,
                    //64, 64,
                    1.0); // imageWidth, imageHeight
                scene.add(xsprite2);
            }
            #endregion




            //var lineTo = new List<THREE.Vector3>();
            var others = new
            {
                ui = default(NotificationLayout),
                canvas = default(IHTMLCanvas),

                map = default(THREE.Texture),

                sprite = default(THREE.Sprite)

            }.ToEmptyList();

            #region add
            Action<NotificationLayout> add = ui =>
            {
                ui.layout.style.background = "";

                var canvas = ui.AsCanvas();
                var index = others.Count;

                //ui.Message  += new { index };
                //ui.Message.innerText += new { index };

                //lbelow0.Message = new { THREE.REVISION }.ToString();

                // THREE.WebGLRenderer: Texture is not power of two. Texture.minFilter is set to THREE.LinearFilter or THREE.NearestFilter. ( undefined )

                var map = new THREE.Texture(canvas);

                map.minFilter = THREE.LinearFilter;

                // like video texture

                var xcrateMaterial = new THREE.SpriteMaterial(
                    new
                    {
                        map,
                        useScreenCoordinates = false,
                        //color = 0xff0000
                        color = 0xffffff
                    }
                );



                var sprite = new THREE.Sprite(xcrateMaterial);

                //floor
                //sprite2.position.set(0, -200, 0);

                // left middle
                //sprite2.position.set(200, 0, 0);
                //sprite.position.set(250, -50, 50);
                //lineTo.Add(sprite.position);




                //sprite2.position.set(0, 0, 200);

                //sprite2.position.set(-100, 0, 0);
                sprite.scale.set(
                   canvas.width * 0.5,
                   canvas.height * 0.5,
                    //64, 64,
                    1.0); // imageWidth, imageHeight
                scene.add(sprite);

                others.Add(
                    new
                    {
                        ui,
                        canvas,
                        map,
                        sprite
                    }
                );

                var sw = Stopwatch.StartNew();

                Native.window.onframe += delegate
                {
                    // can we get some of the crazy c++ template bitmapbuffer code from the past?
                    map.needsUpdate = true;

                    var offset = Math.PI * 2 * ((double)(index + 1) / others.Count);

                    sprite.position.x = 300;

                    sprite.position.z = Math.Sin(sw.ElapsedMilliseconds * 0.00001 + offset) * 120;
                    sprite.position.y = Math.Cos(sw.ElapsedMilliseconds * 0.00001 + offset) * 120;
                };

            };
            #endregion


            add(
                new NotificationLayout
                {
                    // keep attributes around?
                    // like we do with XElement sync?
                    Icon = new HTML.Images.FromAssets._0_10122014_ECAF4B1F638429B44F4B142C2A4F38EE().With(
                        x =>
                        {
                            x.style.width = "96px";
                            x.style.height = "96px";
                        }
                    ),
                    Message = "Advanced Mechanics by Portugal Design Lab"
                }
              );

            add(
                   new NotificationLayout
                   {
                       // keep attributes around?
                       // like we do with XElement sync?
                       Icon = new HTML.Images.FromAssets._42_08122014_D2639E0AAA3E54E5F4568760AEE605EE().With(
                           x =>
                           {
                               x.style.width = "96px";
                               x.style.height = "96px";
                           }
                       ),
                       Message = "Face Value by mshariful"
                   }
                 );



            add(
                   new NotificationLayout
                   {
                       // keep attributes around?
                       // like we do with XElement sync?
                       Icon = new HTML.Images.FromAssets._371_28122014_2045510F2F7974319A9F7E9F4B39DF07().With(
                           x =>
                           {
                               x.style.width = "96px";
                               x.style.height = "96px";
                           }
                       ),
                       Message = "Mind Wall. by Sumit Goski"
                   }
                 );


            #region lines
            {
                var geometry = new THREE.Geometry
                {
                    // how can we keep streaming verticies data points to gpu?
                    vertices =

                        others.SelectMany(
                            lineTo =>
                                new[]
                                {
                                    new THREE.Vector3(200, 0, 0),
                                    lineTo.sprite.position
                                }
                        ).ToArray()

                    // https://github.com/mrdoob/three.js/wiki/Updates
                    // http://stackoverflow.com/questions/17842521/adding-geometry-to-a-three-js-mesh-after-render
                    //verticesNeedUpdate = true
                };

                Native.window.onframe += delegate { geometry.verticesNeedUpdate = true; };

                var o = new THREE.Line(geometry, new THREE.LineDashedMaterial(
                    new { color = 0x00aaff, dashSize = 1, gapSize = 0.5, linewidth = 1 }
                    ), THREE.LineStrip);

                //objects.Add(o);
                scene.add(o);
            }
            #endregion


            //          // DK2
            //          hResolution: 1920,
            //vResolution: 1080,

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(1920, 1080);

            #region HMD
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
            #endregion


            //var effect = new THREE.OculusRiftEffect(
            //    renderer, new
            //    {
            //        worldScale = 100,

            //        //HMD
            //    }
            //    );

            //effect.setSize(1920, 1080);


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

            var lon0 = -45.0;
            var lon1 = 0.0;

            var lon = new sum(
                 () => lon0,
                 () => lon1
             );

            var lat0 = 0.0;
            var lat1 = 0.0;

            // or could we do it with byref or pointers?
            var lat = new sum(
                () => lat0,
                () => lat1
            );

            var phi = 0.0;
            var theta = 0.0;

            //var controls = new THREE.OrbitControls(camera);

            var camera_rotation_z = 0.0;

            //Native.document.onmousemove +=
            //  e =>
            //  {

            //      l.Message = new { e.CursorX, e.CursorY }.ToString();
            //  };


            Native.window.onframe +=
                        ee =>
                        {
                            //labove.Message = new
                            //{
                            //    lon,
                            //    lat,
                            //    ee.counter
                            //}.ToString();

                            //if (Native.document.pointerLockElement == Native.document.body)
                            //    lon += 0.00;
                            //else
                            //    lon += 0.01;

                            //lat = Math.Max(-85, Math.Min(85, lat));

                            //Native.document.title = new { lon, lat }.ToString();


                            phi = THREE.Math.degToRad(90 - lat);
                            theta = THREE.Math.degToRad(lon);

                            target.x = 500 * Math.Sin(phi) * Math.Cos(theta);
                            target.y = 500 * Math.Cos(phi);
                            target.z = 500 * Math.Sin(phi) * Math.Sin(theta);


                            //controls.update();
                            //camera.position = controls.center.clone();


                            camera.lookAt(target);
                            camera.rotation.z += camera_rotation_z;

                            renderer.render(scene, camera);

                            //effect.render(scene, camera);
                        };



            // http://blog.thematicmapping.org/2013/10/terrain-visualization-with-threejs-and.html

            // http://stackoverflow.com/questions/13278087/determine-vertical-direction-of-a-touchmove



            var compassHeadingOffset = 0.0;
            var compassHeadingInitialized = 0;

            #region compassHeading
            // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeading\TestCompassHeading\Application.cs
            Native.window.ondeviceorientation +=
                          dataValues =>
                          {
                              // Convert degrees to radians
                              var alphaRad = dataValues.alpha * (Math.PI / 180);
                              var betaRad = dataValues.beta * (Math.PI / 180);
                              var gammaRad = dataValues.gamma * (Math.PI / 180);

                              // Calculate equation components
                              var cA = Math.Cos(alphaRad);
                              var sA = Math.Sin(alphaRad);
                              var cB = Math.Cos(betaRad);
                              var sB = Math.Sin(betaRad);
                              var cG = Math.Cos(gammaRad);
                              var sG = Math.Sin(gammaRad);

                              // Calculate A, B, C rotation components
                              var rA = -cA * sG - sA * sB * cG;
                              var rB = -sA * sG + cA * sB * cG;
                              var rC = -cB * cG;

                              // Calculate compass heading
                              var compassHeading = Math.Atan(rA / rB);

                              // Convert from half unit circle to whole unit circle
                              if (rB < 0)
                              {
                                  compassHeading += Math.PI;
                              }
                              else if (rA < 0)
                              {
                                  compassHeading += 2 * Math.PI;
                              }

                              /*
                              Alternative calculation (replacing lines 99-107 above):

                                var compassHeading = Math.atan2(rA, rB);

                                if(rA < 0) {
                                  compassHeading += 2 * Math.PI;
                                }
                              */

                              // Convert radians to degrees
                              compassHeading *= 180 / Math.PI;

                              // Compass heading can only be derived if returned values are 'absolute'

                              // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeadingWithReset\TestCompassHeadingWithReset\Application.cs

                              //Native.document.body.innerText = new { compassHeading }.ToString();
                              if (compassHeadingInitialized > 0)
                              {
                                  lon1 = compassHeading - compassHeadingOffset;
                              }
                              else
                              {
                                  compassHeadingOffset = compassHeading;
                                  compassHeadingInitialized++;
                              }

                          };
            #endregion

            #region gamma
            Native.window.ondeviceorientation +=
                //e => Native.body.innerText = new { e.alpha, e.beta, e.gamma }.ToString();
                //e => lon = e.gamma;
                e =>
                {
                    lat1 = e.gamma;

                    // after servicing a running instance would be nice
                    // either by patching or just re running the whole iteration in the backgrou
                    camera_rotation_z = e.beta * 0.02;
                };
            #endregion



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

                        lon0 += (n.clientX - old.clientX) * 0.2;
                        lat0 -= (n.clientY - old.clientY) * 0.2;

                        old = n;
                    };


            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        lon0 += e.movementX * 0.1;
                        lat0 -= e.movementY * 0.1;

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

            Native.body.onmousewheel +=
                e =>
                {

                    camera_rotation_z += 0.1 * e.WheelDirection; ;

                };
            // https://developer.android.com/training/system-ui/immersive.html




            var ze = new ZeProperties();

            ze.Show();
            ze.treeView1.Nodes.Clear();

            ze.Add(() => renderer);
            //ze.Add(() => controls);
            ze.Add(() => scene);
            ze.Left = 0;
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

    // http://stackoverflow.com/questions/32664/is-there-a-constraint-that-restricts-my-generic-method-to-numeric-types
    class sum //<T>
    {
        public static implicit operator double (sum that)
        {
            return that.i.Sum(x => x());
        }


        Func<double>[] i;
        public sum(params Func<double>[] i)
        {
            this.i = i;
        }

        public override string ToString()
        {
            return "" + (double)this;

        }

        //public sum(params ref double[] i)
        //{
        //}
    }
}