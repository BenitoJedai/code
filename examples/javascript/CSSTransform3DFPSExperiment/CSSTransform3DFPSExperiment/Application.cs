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
using CSSTransform3DFPSExperiment.Design;
using CSSTransform3DFPSExperiment.HTML.Pages;
using System.Drawing;
using ScriptCoreLib.GLSL;
using System.Windows.Forms;
using CSSTransform3DFPSExperiment.Controls;

namespace CSSTransform3DFPSExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

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
            var c = new Controls.UserControl1();

            // artwork
            var __artworkPlane = new Plane(
                "url(assets/CSSTransform3DFPSExperiment/osx.jpg)", 424, 174, -398, -240, -300, 90, 90, 0);

            world.addPlane(__artworkPlane);

            world.addPlane(
                new Plane(
                    "url(assets/CSSTransform3DFPSExperiment/wood.jpg)", 800, 800, -400, 400, 53, 180, 0, 0
                )
            );



            new Plane("url(assets/CSSTransform3DFPSExperiment/wall.jpg?3)", 800, 500, -400, -400, -447, 270, 90, 180).With(
                __wall_a =>
                {
                    world.addPlane(__wall_a);
                }
            );

            var ix = 0;

            Action NextWallToTHeRight = delegate
                    {

                        ix++;

                        world.addPlane(
                          new Plane(
                              "url(assets/CSSTransform3DFPSExperiment/wood.jpg)", 800, 800, -400, 400 + 800 * ix, 53, 180, 0, 0
                          )
                      );

                        new Plane("url(assets/CSSTransform3DFPSExperiment/wall.jpg?3)", 800, 500, -400, -400 + 800 * ix, -447, 270, 90, 180).With(
                            __wall_a =>
                            {
                                world.addPlane(__wall_a);
                            }
                        );
                    }
                ;

            NextWallToTHeRight();
            c.button3.Click += delegate { NextWallToTHeRight(); };

            Action<string, double, double, double, double, double, double, double, double, double> buildCube0 =
            (colour, w, h, d, x, y, z, rx, ry, rz) =>
            {
                //world.addPlane(new Plane(colour, h, w, x, y, z, 0, 180, 90));
                //world.addPlane(new Plane(colour, w, d, x, y, z, 90, 0, 0));
                //world.addPlane(new Plane(colour, d, h, x, y, z, 0, 270, 0));

                new Plane(colour, d, h, x + w, y, z + d, 0, 90, 0).With(
                    p =>
                    {
                        world.addPlane(p);

                        //new ScriptCoreLib.JavaScript.Runtime.Timer(
                        //    delegate
                        //    {
                        //        p.rotation.z += 15;
                        //        p.update();
                        //    }
                        //).StartInterval(150);
                    }
                );

                //world.addPlane(new Plane(colour, w, d, x + w, y + h, z, 90, 180, 0));
                //world.addPlane(new Plane(colour, w, h, x, y, z + d, 0, 0, 0));
            };

            Action<string, double, double, double, double, double, double, double, double, double> buildCube =
                (colour, w, h, d, x, y, z, rx, ry, rz) =>
                {
                    world.addPlane(new Plane(colour, h, w, x, y, z, 0, 180, 90));
                    world.addPlane(new Plane(colour, w, d, x, y, z, 90, 0, 0));
                    world.addPlane(new Plane(colour, d, h, x, y, z, 0, 270, 0));
                    world.addPlane(new Plane(colour, d, h, x + w, y, z + d, 0, 90, 0));
                    world.addPlane(new Plane(colour, w, d, x + w, y + h, z, 90, 180, 0));
                    world.addPlane(new Plane(colour, w, h, x, y, z + d, 0, 0, 0));
                };

            buildCube0("url(assets/CSSTransform3DFPSExperiment/desk.jpg)", 10, 50, 300, -150 + 400, 345, -250, 0, 0, 0);

            for (int xi = 0; xi < 20; xi++)
            {
                buildCube("url(assets/CSSTransform3DFPSExperiment/desk.jpg)", 10, 50, 300, -150 + 400, 345 + 60 * xi, -250, 0, 0, 0);

            }


            new Plane(
                "url(assets/CSSTransform3DFPSExperiment/wood.jpg)", 800, 800, -400 + 800, 400, 53, 180, 0, 0
            ).With(
               pp =>
               {
                   world.addPlane(pp);


                   pp.position.x += 20;
                   //pp.rotation.z += 15;

                   pp.update();

               }
           );


            c.GetHTMLTarget().className = "nolock";


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

            c.BackColor = Color.Transparent;

            var xx = c.GetHTMLTargetContainer();

            xx.style.transform = "scale(0.5)";
            xx.style.transformOrigin = "0% 0%";

            xx.style.SetSize(
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


            #region loop
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

        [Script(ExternalTarget = "window.world")]
        static World world;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Plane")]
    class Plane
    {
        public Triplet position;
        public Triplet rotation;

        public Plane(string colour, double w, double h, double x, double y, double z, double rx, double ry, double rz)
        {

        }


        public void update()
        {

        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "World")]
    class World
    {
        public void addPlane(Plane p)
        {
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "World")]
    sealed class Triplet
    {
        public double x, y, z;
        public Triplet(double x, double y, double z)
        {

        }
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
