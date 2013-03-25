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
using CSSSpadeWarrior.Design;
using CSSSpadeWarrior.HTML.Pages;
using System.Drawing;
using ScriptCoreLib.GLSL;
using System.Windows.Forms;
using CSSSpadeWarrior.Controls;

namespace CSSSpadeWarrior
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
        public Application(IApp page = null)
        {
            if (page == null)
                return;

            Initialize(page);
        }

        public void Initialize(IApp page, Action<Application> yield = null)
        {
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            // http://www.keithclark.co.uk/labs/3dcss/demo/

            var prefetch = new HTML.Pages.TexturesImages();

            new Design.Library.threedee().Content.AttachToHead().onload +=
                delegate
                {
                    InitializeContent();

                    if (yield != null)
                        yield(this);
                };
        }

        class cube
        {
            // as appears on the control designer

            public Plane
                roof,
                north,
                east,
                west,
                south,
                bottom
                ;

            public cube(string colour, double w, double h, double d, double x, double y, double z, double rx, double ry, double rz)
            {
                roof = new Plane(colour, h, w, x, y, z, 0, 180, 90);
                world.addPlane(roof);

                north = new Plane(colour, w, d, x, y, z, 90, 0, 0);
                world.addPlane(north);

                east = new Plane(colour, d, h, x, y, z, 0, 270, 0);
                world.addPlane(east);

                west = new Plane(colour, d, h, x + w, y, z + d, 0, 90, 0);
                world.addPlane(west);

                south = new Plane(colour, w, d, x + w, y + h, z, 90, 180, 0);
                world.addPlane(south);

                bottom = new Plane(colour, w, h, x, y, z + d, 0, 0, 0);
                world.addPlane(bottom);
            }
        }

        public Floorplan floorplan;

        private
            // dynamic does not work in static yet?
            //static 
            void InitializeContent()
        {
            floorplan = new Floorplan();


            var c = new Controls.UserControl1();




            Action<string, double, double, double, double, double, double, double, double, double> buildCube =
                (colour, w, h, d, x, y, z, rx, ry, rz) =>
                {
                    new cube(colour, w, h, d, x, y, z, rx, ry, rz);

                    //world.addPlane(new Plane(colour, h, w, x, y, z, 0, 180, 90));
                    //world.addPlane(new Plane(colour, w, d, x, y, z, 90, 0, 0));
                    //world.addPlane(new Plane(colour, d, h, x, y, z, 0, 270, 0));
                    //world.addPlane(new Plane(colour, d, h, x + w, y, z + d, 0, 90, 0));
                    //world.addPlane(new Plane(colour, w, d, x + w, y + h, z, 90, 180, 0));
                    //world.addPlane(new Plane(colour, w, h, x, y, z + d, 0, 0, 0));
                };



            var zoom = 4;
            var zz = 60;

            #region CreateFromFloorplan
            Action CreateFromFloorplan = delegate
            {
                foreach (var item in floorplan.Controls)
                {
                    (item as DeskCube).With(
                        f =>
                        {


                            var cubeheight = Math.Max(300, f.CubeHeight);

                            new cube(
                                "url(assets/CSSSpadeWarrior/desk.jpg)",

                                 //w, h, d, x, y, z, rx, ry, rz
                                f.Width * zoom,

                                f.Height * zoom,

                                cubeheight,
                                //0,

                                -f.Right * zoom,

                                f.Top * zoom,

                                -cubeheight + zz,
                                //-250, 

                                0, 0, 0
                            ).With(
                                cube =>
                                {
                                    if (!string.IsNullOrEmpty(f.LeftWallSource))
                                        new IHTMLDiv { }.AttachTo(cube.west.node).With(
                                            westContainer =>
                                            {
                                                //innerText = f.LeftWallSource
                                                //btn.className = "nolock";


                                                westContainer.style.transform = "rotateZ(90deg)";
                                                westContainer.style.transformOrigin = "0% 0%";

                                                westContainer.style.position = IStyle.PositionEnum.absolute;
                                                westContainer.style.width = cube.west.node.clientHeight + "px";
                                                westContainer.style.left = cube.west.node.clientWidth + "px";
                                                westContainer.style.height = cube.west.node.clientWidth + "px";

                                                Action LoadContent = delegate
                                                {

                                                    new IHTMLIFrame { allowFullScreen = true, src = f.LeftWallSource, frameBorder = "0" }.AttachTo(westContainer).With(
                                                        iframe =>
                                                        {
                                                            iframe.style.position = IStyle.PositionEnum.absolute;
                                                            iframe.style.left = "0px";
                                                            iframe.style.top = "0px";
                                                            iframe.style.width = "100%";
                                                            iframe.style.height = "100%";
                                                        }
                                                    );
                                                };

                                                if (f.LeftWallSourceAutoLoad)
                                                {
                                                    LoadContent();
                                                }
                                                else
                                                {
                                                    new IHTMLButton { innerText = "Click to see " + f.LeftWallSource, className = "nolock" }.AttachTo(westContainer).With(
                                                       btn =>
                                                       {
                                                           btn.style.position = IStyle.PositionEnum.absolute;
                                                           btn.style.left = "2em";
                                                           btn.style.top = "2em";
                                                           btn.style.right = "2em";
                                                           btn.style.bottom = "2em";

                                                           btn.onclick +=
                                                               delegate
                                                               {
                                                                   btn.Orphanize();

                                                                   LoadContent();
                                                               };

                                                           f.LeftWallSourceAutoLoadChanged +=
                                                               delegate
                                                               {
                                                                   if (btn == null)
                                                                       return;

                                                                   if (f.LeftWallSourceAutoLoad)
                                                                   {
                                                                       btn.Orphanize();
                                                                       btn = null;

                                                                       LoadContent();
                                                                   }
                                                               };
                                                       }
                                                        );
                                                }
                                            }
                                        );
                                }
                            );

                        }
                    );
                    (item as Floor).With(
                        f =>
                        {

                            buildCube("url(assets/CSSSpadeWarrior/wood.jpg)",

                                 //w, h, d, x, y, z, rx, ry, rz
                                f.Width * zoom,

                                f.Height * zoom,

                                10,
                                //0,

                                -f.Right * zoom, f.Top * zoom,

                                0 - f.Z * zoom + zz,
                                //-250, 

                                0, 0, 0);

                        }
                    );
                }
            };

            CreateFromFloorplan();

            //zz += 300;

            //CreateFromFloorplan();
            #endregion

            //   1.0f, 0.6f, 0.0f, 1.0f,

            new cube(
                "rgba(255, 160, 0, 1.0)",
                100,
                100,
                200,
                0, 0, -300,
                0, 0, 33
            );



            //avoid out of memory - elements will go missing
            //zz += 300;

            //CreateFromFloorplan();


            c.GetHTMLTarget().className = "nolock";




            c.BackColor = Color.Transparent;

            //var xx = c.GetHTMLTargetContainer();

            //xx.style.transform = "scale(0.5)";
            //xx.style.transformOrigin = "0% 0%";

            //xx.style.SetSize(
            //    __osxPlane_node.clientWidth * 2,
            //    __osxPlane_node.clientHeight * 2
            //);

            //c.AttachControlTo(__osxPlane_node);


            #region onkeydown
            Native.Document.body.onkeydown += e =>
            {
                //Console.WriteLine(new { e.KeyCode });

                if (e.KeyCode == (int)Keys.W)
                    window.keyState.forward = true;
                if (e.KeyCode == (int)Keys.S)
                    window.keyState.backward = true;
                if (e.KeyCode == (int)Keys.A)
                    window.keyState.strafeleft = true;
                if (e.KeyCode == (int)Keys.D)
                    window.keyState.straferight = true;

                if (AfterKeystateChange != null)
                    AfterKeystateChange();
            };

            Native.Document.body.onkeyup += e =>
            {
                if (e.KeyCode == (int)Keys.W)
                    window.keyState.forward = false;
                if (e.KeyCode == (int)Keys.S)
                    window.keyState.backward = false;

                if (e.KeyCode == (int)Keys.A)
                    window.keyState.strafeleft = false;
                if (e.KeyCode == (int)Keys.D)
                    window.keyState.straferight = false;

                if (AfterKeystateChange != null)
                    AfterKeystateChange();
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

                        if (AfterCameraRotationChange != null)
                            AfterCameraRotationChange();
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

            var touchx = 0;
            var touchy = 0;

            Native.Document.body.ontouchstart +=
             e =>
             {
                 touchx = e.touches[0].pageX;
                 touchy = e.touches[0].pageY;

                 // to be lowercased
                 e.PreventDefault();
             };

            Native.Document.body.ontouchmove +=
              e =>
              {
                  e.PreventDefault();

                  var ztouchx = e.touches[0].pageX;
                  var ztouchy = e.touches[0].pageY;

                  window.viewport.camera.rotation.x -= (ztouchy - touchy) / 2;
                  window.viewport.camera.rotation.z += (ztouchx - touchx) / 2;

                  touchx = ztouchx;
                  touchy = ztouchy;
              };
            //document.addEventListener("touchstart", function (ev) {
            //    pointer.x = ev.targetTouches[0].pageX;
            //    pointer.y = ev.targetTouches[0].pageY;
            //    ev.preventDefault();
            //}, false);

            //document.addEventListener("touchmove", function (ev) {
            //    viewport.camera.rotation.x -= (ev.targetTouches[0].pageY - pointer.y) / 2;
            //    viewport.camera.rotation.z += (ev.targetTouches[0].pageX - pointer.x) / 2;
            //    pointer.x = ev.targetTouches[0].pageX;
            //    pointer.y = ev.targetTouches[0].pageY;
            //    ev.preventDefault();
            //}, false);




            #region loop
            Action loop = delegate
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


            loop.AtAnimationFrame();
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
        public static XWindow window;

        [Script(ExternalTarget = "window.world")]
        static World world;


        public event Action AfterCameraRotationChange;
        public event Action AfterKeystateChange;

    }

    [Script(HasNoPrototype = true, ExternalTarget = "Plane")]
    public class Plane
    {
        public IHTMLDiv node;

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
    public class World
    {
        public void addPlane(Plane p)
        {
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "World")]
    public sealed class Triplet
    {
        public double x, y, z;
        public Triplet(double x, double y, double z)
        {

        }
    }
    [Script(IsNative = true)]
    public class XWindow
    {
        public __vec3 pointer;

        public __keyState keyState;

        public double speed;
        public double strafespeed;

        public double accel;
        public double maxSpeed;
        public __Viewport viewport;
    }

    public sealed class __keyState
    {
        public bool backward;


        public bool forward;

        public bool strafeleft;
        public bool straferight;
    }

    public sealed class __Viewport
    {
        public IHTMLDiv node;
        public __Camera camera;
    }

    public sealed class __Camera
    {
        public __vec3 position;
        public __vec3 rotation;

        [Script(NoDecoration = true)]
        internal void update()
        {
        }
    }

    public sealed class __vec3
    {
        public double x, y, z;
    }

    static class X
    {
        public static void AtAnimationFrame(this Action e)
        {
            Action x = null;

            x = delegate
            {
                e();
                Native.Window.requestAnimationFrame += x;

            };

            Native.Window.requestAnimationFrame += x;
        }
    }
}
