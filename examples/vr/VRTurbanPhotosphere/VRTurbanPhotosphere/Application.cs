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
            renderer.setSize(Native.window.Width, Native.window.Height);
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

            // we still have to add the layman acceleratomerer thingy
            // later we could connect the component to the chromevr api?
            // https://github.com/turban/photosphere


            // is the roslyn build ready for android/java?
            // no, we did fix the while break for jsc rewrrite mode
            // by not for java.
            // lets rebuild scriptcorelib with non roslyn to bypass the issue for now.

            //           System.InvalidOperationException: internal compiler error at method
            //assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
            //type: ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass0, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
            //method: <GetEnumerator>b__1
            //Java : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass0.<GetEnumerator>b__1
            //   at jsc.Script.CompilerBase.BreakToDebugger(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 267
            //  at jsc.Script.CompilerBase.Break(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 227



        }

    }
}
