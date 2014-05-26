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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSSTransform3DFPSExperimentByKeith.Design;
using CSSTransform3DFPSExperimentByKeith.HTML.Pages;
using System.Drawing;
using ScriptCoreLib.GLSL;
using System.Windows.Forms;
using CSSTransform3DFPSExperimentByKeith.Controls;

namespace CSSTransform3DFPSExperimentByKeith
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
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            // http://www.keithclark.co.uk/labs/3dcss/demo/

            var prefetch = new HTML.Pages.TexturesImages();

            new Design.Library.threedee().Content.AttachToHead().onload +=
                delegate
            {
                InitializeContent();

            };
        }

        private
            // dynamic does not work in static yet?
            //static 
            void InitializeContent()
        {
            //        script: error JSC1000: Method: InitializeContent, Type: CSSTransform3DFPSExperimentByKeith.Application; emmiting failed : System.InvalidOperationException: unsupported flow detected, try to simplify.
            // Assembly V:\CSSTransform3DFPSExperimentByKeith.Application.exe
            // DeclaringType CSSTransform3DFPSExperimentByKeith.Application, CSSTransform3DFPSExperimentByKeith.Application, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
            //         OwnerMethod InitializeContent
            //         Offset 00a0
            //         .Try ommiting the return, break or continue instruction.
            //          at jsc.Script.CompilerBase.BreakToDebugger(String e) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerBase.cs:line 266
            //   at jsc.ILBlock.PrestatementBlock.AddPrestatement(Prestatement p) in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1654
            //   at jsc.ILBlock.PrestatementBlock.Populate(ILInstruction First, ILInstruction Last) in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1606
            //   at jsc.ILBlock.PrestatementBlock.Populate() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1433
            //   at jsc.ILBlock.get_Prestatements() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1759
            //   at jsc.Languages.JavaScript.MethodBodyOptimizer.TryOptimize(IdentWriter w, ILBlock xb) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\MethodBodyOptimizer.cs:line 89
            //   at jsc.IL2Script.EmitBody(IdentWriter w, MethodBase SourceMethod, Boolean define_self) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2Script.cs:line 576

            //Unhandled Exception: System.InvalidOperationException: Method: InitializeContent, Type: CSSTransform3DFPSExperimentByKeith.Application; emmiting failed : System.InvalidOperationException: unsupported flow detected, try to simplify.


            //dynamic window = Native.Window;

            //dynamic __osxPlane = window.__osxPlane;
            //IHTMLDiv __osxPlane_node = __osxPlane.node;

            var discover = new IHTMLIFrame
            {
                //border = "0",
                src = "http://discover.xavalon.net",
                allowFullScreen = true,
                frameBorder = "0"
            };


            //discover.style.transform = "scale(0.5)";
            //discover.style.transformOrigin = "0% 0%";

            //var scale = 1.25;
            var scale = 1;
            var zoom = 8;

            discover.style.transform = "scale(" + (1 / scale) + ")";
            discover.style.transformOrigin = "0% 0%";

            discover.style.SetSize(
                (int)(__wall_c.clientWidth * zoom * scale),
                 (int)(__wall_c.clientHeight * zoom * scale)
            );

            //dynamic ds = discover.style;

            //ds.zoom = (100.0 / zoom) + "%";

            discover.AttachTo(__wall_c);


            var c = new Controls.UserControl1();
            c.GetHTMLTarget().className = "nolock";


            #region button1
            c.button1.Click +=
                delegate
            {
                var cf = new Form1();

                cf.Show();

                cf.FormClosing +=
                    (ss, ee) =>
                        {
                            if (cf.WindowState == FormWindowState.Normal)
                            {
                                if (ee.CloseReason == CloseReason.UserClosing)
                                {
                                    ee.Cancel = true;
                                    cf.WindowState = FormWindowState.Minimized;
                                }
                            }
                        };

                cf.GetHTMLTarget().className = "nolock";

            };
            #endregion


            #region button2
            c.button2.Click +=
                delegate
            {
                var cf = new Form();

                var cw = new WebBrowser { Dock = DockStyle.Fill };

                cf.Controls.Add(cw);

                cw.Navigate(
            "http://discover.xavalon.net"

                        //"/"

                        );

                cf.FormClosing +=
                    (ss, ee) =>
                        {
                            if (cf.WindowState == FormWindowState.Normal)
                            {
                                if (ee.CloseReason == CloseReason.UserClosing)
                                {
                                    ee.Cancel = true;
                                    cf.WindowState = FormWindowState.Minimized;
                                }
                            }
                        };
                cf.Show();

                //Console.WriteLine("button2.Click");
                cf.GetHTMLTarget().className = "nolock";

                //cf.GetHTMLTarget().style.border = "2px solid red";

            };
            #endregion

            c.BackColor = Color.Transparent;

            var x = c.GetHTMLTargetContainer();

            x.style.transform = "scale(0.5)";
            x.style.transformOrigin = "0% 0%";

            x.style.SetSize(
                __osxPlane_node.clientWidth * 2,
                __osxPlane_node.clientHeight * 2
            );

            c.AttachControlTo(__osxPlane_node);




            #region onkeydown
            Native.Document.body.onkeydown += e =>
            {
                //Console.WriteLine(new { e.KeyCode });

                if (e.KeyCode == 87)
                    window.keyState.forward = true;
                if (e.KeyCode == 83)
                    window.keyState.backward = true;
                if (e.KeyCode == (int)Keys.A)
                    window.keyState.strafeleft = true;
                if (e.KeyCode == (int)Keys.D)
                    window.keyState.straferight = true;
            };

            Native.Document.body.onkeyup += e =>
            {
                if (e.KeyCode == 87)
                    window.keyState.forward = false;

                if (e.KeyCode == 83)
                    window.keyState.backward = false;

                if (e.KeyCode == (int)Keys.A)
                    window.keyState.strafeleft = false;
                if (e.KeyCode == (int)Keys.D)
                    window.keyState.straferight = false;

            };
            #endregion


            #region isnolock
            Func<INode, bool> isnolock =
                p =>
                {
                    var nolock = false;

                    while (p != Native.Document.body)
                    {
                        if (((IHTMLElement)p).className == "nolock")
                            nolock = true;

                        p = p.parentNode;
                    }

                    return nolock;
                };
            #endregion



            #region onmousemove
            Native.Document.body.tabIndex = 101;
            Native.Document.body.onmousedown +=
                e =>
                {
                    var nolock = isnolock(e.Element);
                    if (nolock)
                        return;

                    e.PreventDefault();
                    Native.Document.body.focus();
                    Native.Document.body.requestPointerLock();
                };

            Native.Document.body.onmousemove +=
                e =>
                {
                    if (Native.Document.pointerLockElement == Native.Document.body)
                    {
                        window.viewport.camera.rotation.x -= e.movementY / 2;
                        window.viewport.camera.rotation.z += e.movementX / 2;
                    }
                    else
                    {
                        var nolock = isnolock(e.Element);

                        if (nolock)
                            Native.Document.body.style.cursor = IStyle.CursorEnum.auto;
                        else
                            Native.Document.body.style.cursor = IStyle.CursorEnum.move;
                    }
                };


            Native.Document.body.onmouseup +=
                 e =>
                 {
                     if (Native.Document.pointerLockElement == Native.Document.body)
                     {
                         Native.Document.exitPointerLock();
                     }
                 };
            #endregion





            #region onframe
            Native.window.onframe += delegate
            {
                // is external target working bot ways?
                //window.speed = window.speed;

                //Console.WriteLine(new { window.keyState.forward });

                #region speed
                if (window.keyState.backward)
                {
                    if (window.speed > -window.maxSpeed)
                        window.speed -= window.accel;
                }
                else if (window.keyState.forward)
                {
                    if (window.speed < window.maxSpeed)
                        window.speed += window.accel;
                }
                else if (window.speed > 0)
                {
                    window.speed = Math.Max(window.speed - window.accel, 0);
                }
                else if (window.speed < 0)
                {
                    window.speed = Math.Max(window.speed + window.accel, 0);
                }
                else
                {
                    window.speed = 0;
                }
                #endregion


                #region strafespeed
                if (window.keyState.straferight)
                {
                    if (window.strafespeed > -window.maxSpeed)
                        window.strafespeed -= window.accel;
                }
                else if (window.keyState.strafeleft)
                {
                    if (window.strafespeed < window.maxSpeed)
                        window.strafespeed += window.accel;
                }
                else if (window.strafespeed > 0)
                {
                    window.strafespeed = Math.Max(window.strafespeed - window.accel, 0);
                }
                else if (window.strafespeed < 0)
                {
                    window.strafespeed = Math.Max(window.strafespeed + window.accel, 0);
                }
                else
                {
                    window.strafespeed = 0;
                }
                #endregion


                // sideway
                {

                    var xo = Math.Sin(window.viewport.camera.rotation.z * 0.0174532925);
                    var yo = Math.Cos(window.viewport.camera.rotation.z * 0.0174532925);

                    window.viewport.camera.position.x -= xo * window.speed;
                    window.viewport.camera.position.y -= yo * window.speed;
                }

                {
                    var xo = Math.Sin(window.viewport.camera.rotation.z * 0.0174532925 - 3.14 / 2);
                    var yo = Math.Cos(window.viewport.camera.rotation.z * 0.0174532925 - 3.14 / 2);

                    window.viewport.camera.position.x -= xo * window.strafespeed;
                    window.viewport.camera.position.y -= yo * window.strafespeed;
                }

                window.viewport.camera.update();


            };
            #endregion



        }




        [Script(ExternalTarget = "window.__wall_c.node")]
        static IHTMLDiv __wall_c;

        [Script(ExternalTarget = "window.__wall_b.node")]
        static IHTMLDiv __wall_b;

        [Script(ExternalTarget = "window.__wall_a.node")]
        static IHTMLDiv __wall_a;

        [Script(ExternalTarget = "window.__osxPlane.node")]
        static IHTMLDiv __osxPlane_node;

        [Script(ExternalTarget = "window")]
        static XWindow window;
    }

    [Script(IsNative = true)]
    class XWindow
    {
        public __vec3 pointer;

        public __keyState keyState;

        public double speed;
        public double strafespeed;

        public double accel;
        public double maxSpeed;
        public __Viewport viewport;
    }

    sealed class __keyState
    {
        public bool backward;


        public bool forward;

        public bool strafeleft;
        public bool straferight;
    }

    sealed class __Viewport
    {
        public __Camera camera;
    }

    sealed class __Camera
    {
        public __vec3 position;
        public __vec3 rotation;

        [Script(NoDecoration = true)]
        internal void update()
        {
        }
    }

    sealed class __vec3
    {
        public double x, y, z;
    }

    static class X
    {

    }
}
