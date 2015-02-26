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
using VRTurbanPhotosphere;
using VRTurbanPhotosphere.Design;
using VRTurbanPhotosphere.HTML.Pages;

// did we have an analyzer to auto import the nuget yet?
using THREE;
using ScriptCoreLib.JavaScript.Native;


namespace VRTurbanPhotosphere
{
    using Math = System.Math;



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
            // would a device be able to stream photos from one device
            // into other over rtc?



            // should the first frame of the async app to be loaded
            // be a load progress screen to prepare the service worker?

            // this project is the first
            // template for hybrid vr apps.
            // it is based on webview/javascript/threejs for android

            // will future C# allow virtual code inheritance?
            // for now lets do copy n paste?

            body.style.margin = "0px";
            body.style.overflow = IStyle.OverflowEnum.hidden;
            body.Clear();



            // https://github.com/turban/photosphere/blob/gh-pages/stolanuten.html

            var scene = new THREE.Scene();


            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(1920, 1080);
            // the thing you attach to dom
            renderer.domElement.AttachToDocument();


            var sphere = new Mesh(
                new SphereGeometry(100, 20, 20),
                new MeshBasicMaterial(
                    new
                    {
                        map = THREE.ImageUtils.loadTexture(new WebGLTurbanPhotosphere.HTML.Images.FromAssets.stolanuten().src)
                    }
                )
            );
            sphere.scale.x = -1;
            sphere.AttachTo(scene);

            var camera = new PerspectiveCamera(75, window.aspect, 1, 1000);
            camera.position.x = 0.1;

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

            var camera_rotation_z = 0.0;


            //var controls = new THREE.OrbitControls(camera, renderer.domElement);



            var effect = new THREE.OculusRiftEffect(
 renderer, new
 {
     worldScale = 100,

     //HMD
 }
 );
            effect.setSize(1920, 1080);


            window.onframe +=
                delegate
                {
                    phi = THREE.Math.degToRad(90 - lat);
                    theta = THREE.Math.degToRad(lon);

                    var target = new THREE.Vector3(0, 0, 0);
                    target.x = 500 * Math.Sin(phi) * Math.Cos(theta);
                    target.y = 500 * Math.Cos(phi);
                    target.z = 500 * Math.Sin(phi) * Math.Sin(theta);

                    camera.lookAt(target);
                    camera.rotation.z += camera_rotation_z;

                    //renderer.render(scene, camera);
                    effect.render(scene, camera);
                };



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




            // we still have to add the layman acceleratomerer thingy
            // later we could connect the component to the chromevr api?
            // https://github.com/turban/photosphere


            // is the roslyn build ready for android/java?
            // no, we did fix the while break for jsc rewrrite mode
            // by not for java.
            // lets rebuild scriptcorelib with non roslyn to bypass the issue for now.


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






            #region nonroslyn
            //           System.InvalidOperationException: internal compiler error at method
            //assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
            //type: ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass0, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
            //method: <GetEnumerator>b__1
            //Java : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass0.<GetEnumerator>b__1
            //   at jsc.Script.CompilerBase.BreakToDebugger(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 267
            //  at jsc.Script.CompilerBase.Break(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 227

            //           BCL needs another method, please define it.
            //Cannot call type without script attribute :
            //System.Threading.Monitor for Void Enter(System.Object, Boolean ByRef)
            //               used at
            //VRTurbanPhotosphere.Activities.ApplicationWebServiceActivity +<> c__DisplayClass24.< CreateServer > b__29 at offset 0018.
            //     If the use of this method is intended, an implementation should be provided with the attribute[Script(Implements = typeof(...)] set.You may have mistyped it.

            // compiler/android server/ needs to be non roslyn too
            // as we seem to not correctly detect the new lock IL?

            // ding didly doo.
            // other libs need to be also non roslyn.

            //            -compile:
            //    [javac]
            //        Compiling 727 source files to V:\bin\classes
            //[javac] V:\src\ScriptCoreLib\Extensions\LinqExtensions.java:38: error: bad operand types for binary operator '>'
            //    [javac]         if (((e > null)))
            //    [javac]                 ^

            // moving code from "Y:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs"

            //            -compile:
            //    [javac]
            //        Compiling 780 source files to V:\bin\classes
            //[javac] V:\src\ScriptCoreLib\Ultra\WebService\InternalGlobalExtensions___c__DisplayClass0.java:94: error: incompatible types
            //[javac]         for (num3 = 0; num3; num3++)
            //    [javac]                        ^
            //    [javac]
            //        required: boolean
            //[javac] found:    int

            //            -compile:
            //    [javac]
            //        Compiling 773 source files to V:\bin\classes
            //[javac] V:\src\ScriptCoreLib\Ultra\WebService\InternalGlobalExtensions___c__DisplayClass0.java:94: error: incompatible types
            //[javac]         for (num3 = 0; num3; num3++)
            //    [javac]                        ^



            //            -compile:
            //    [javac]
            //        Compiling 775 source files to V:\bin\classes
            //[javac] V:\src\ScriptCoreLib\Ultra\WebService\InternalGlobalExtensions___c__DisplayClass0.java:94: error: incompatible types
            //[javac]         for (num3 = 0; num3; num3++)
            //    [javac]                        ^
            //    [javac]
            //        required: boolean
            //[javac] found:    int
            #endregion


        }

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

        //public sum(params ref double[] i)
        //{
        //}
    }
}
