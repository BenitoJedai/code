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
using com.abstractatech.cloud.gallery.Design;
using com.abstractatech.cloud.gallery.HTML.Pages;
using System.Drawing;
using ScriptCoreLib.GLSL;
using System.Windows.Forms;
using com.abstractatech.cloud.gallery.Controls;

namespace com.abstractatech.cloud.gallery
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
            // https://code.google.com/p/chromium/issues/detail?id=171666

            Console.WriteLine(new { Native.Document.location.hash });

            if (Native.Document.location.hash == "#window")
            {
                // ask opener where in 3D am i?
                new IHTMLButton { innerText = "window" }.AttachToDocument();

                return;
            }

            if (Native.Document.location.hash == "#cloud")
            {
                WebGLClouds.Application.DefaultMouseY = 0.8;
                WebGLClouds.Application.DisableBackground = true;

                new WebGLClouds.Application();
                return;
            }


            //FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            //FormStyler.AtFormCreated = FormStylerLikeAero.LikeAero;

            // http://www.keithclark.co.uk/labs/3dcss/demo/

            //var prefetch = new HTML.Pages.TexturesImages();

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






            Action<string, double, double, double, double, double, double, double, double, double> buildCube =
                (colour, w, h, d, x, y, z, rx, ry, rz) =>
                {
                    new cube(colour, w, h, d, x, y, z, rx, ry, rz);

                };



            var zoom = 4;
            var zz = 60;
            var low = -300;

            #region CreateFromFloorplan
            Action CreateFromFloorplan = delegate
            {
                foreach (var item in floorplan.Controls)
                {
                    (item as DeskCube).With(
                        f =>
                        {


                            var cubeheight = Math.Max(300, f.CubeHeight);
                            var z = 0;
                            var cubetex = "rgba(0,0,0,0.5)";

                            if (f.LeftWallSource != null)
                                if (f.LeftWallSource.StartsWith("#cloud"))
                                {
                                    z = low;
                                    cubetex = "";
                                }

                            if (f.WallSourceRight != null)
                                if (f.WallSourceRight.StartsWith("#cloud"))
                                {
                                    z = low;
                                    cubetex = "";
                                }

                            if (f.WallSourceTop != null)
                                if (f.WallSourceTop.StartsWith("#cloud"))
                                {
                                    z = low;
                                    cubetex = "";
                                }

                            if (f.WallSourceBottom != null)
                                if (f.WallSourceBottom.StartsWith("#cloud"))
                                {
                                    z = low;
                                    cubetex = "";
                                }

                            new cube(
                                cubetex,

                                 //w, h, d, x, y, z, rx, ry, rz
                                f.Width * zoom,

                                f.Height * zoom,

                                cubeheight / 4 * zoom,
                                //0,

                                -f.Right * zoom,

                                f.Top * zoom,

                                -(cubeheight / 4 * zoom) + zz - z * zoom,

                                //-250, 

                                0, 0, 0
                            ).With(
                                cube =>
                                {

                                    #region LoadContent
                                    Action<IHTMLDiv, string> LoadContent =
                                        (westContainer, src) =>
                                        {
                                            if (src == "#window")
                                            {
                                                var morespace = new IHTMLDiv().AttachTo(westContainer);

                                                morespace.className = "nolock";

                                                // 3by3 grid
                                                morespace.style.position = IStyle.PositionEnum.absolute;
                                                morespace.style.top = -32 + "px";
                                                morespace.style.left = 0 + "px";
                                                morespace.style.width = westContainer.clientWidth + "px";
                                                morespace.style.height = westContainer.clientHeight + "px";

                                                // 3D DOCK
                                                var ff = new Form();

                                                ff.StartPosition = FormStartPosition.Manual;
                                                ff.Show();
                                                ff.Left = 0;

                                                ff.Top = -32;

                                                ff.Width = westContainer.clientWidth;
                                                ff.Height = westContainer.clientHeight + 32;

                                                ff.GetHTMLTarget().AttachTo(morespace);

                                                var ffw = new WebBrowser { Dock = DockStyle.Fill };

                                                ffw.AttachTo(ff);

                                                ffw.Navigate(src);



                                                return;
                                            }

                                            new IHTMLIFrame { allowFullScreen = true, src = src, frameBorder = "0" }.AttachTo(westContainer).With(
                                                iframe =>
                                                {
                                                    iframe.style.position = IStyle.PositionEnum.absolute;
                                                    iframe.style.left = "0px";
                                                    iframe.style.top = "0px";
                                                    iframe.style.width = "10%";
                                                    iframe.style.height = "10%";
                                                    iframe.style.transform = "scale(10.0)";
                                                    iframe.style.transformOrigin = "0% 0%";
                                                }
                                            );

                                            // disable mouse
                                            new IHTMLDiv { }.AttachTo(westContainer).With(
                                                 overlay =>
                                                 {
                                                     overlay.style.position = IStyle.PositionEnum.absolute;
                                                     overlay.style.left = "0px";
                                                     overlay.style.top = "0px";
                                                     overlay.style.width = "100%";
                                                     overlay.style.height = "100%";

                                                     overlay.style.backgroundColor = "red";
                                                     overlay.style.Opacity = 0;
                                                 }
                                             );
                                        };
                                    #endregion



                                    #region WallSourceRight
                                    if (!string.IsNullOrEmpty(f.WallSourceBottom))
                                        new IHTMLDiv { }.AttachTo(cube.south.node).With(
                                           westContainer =>
                                           {

                                               //innerText = f.LeftWallSource
                                               //btn.className = "nolock";


                                               //westContainer.style.transform = "rotateZ(-90deg)";
                                               westContainer.style.transformOrigin = "0% 0%";

                                               westContainer.style.position = IStyle.PositionEnum.absolute;
                                               westContainer.style.width = cube.south.node.clientWidth + "px";
                                               westContainer.style.left = "0px";
                                               westContainer.style.top = "0px";
                                               westContainer.style.height = cube.south.node.clientHeight + "px";


                                               if (f.LeftWallSourceAutoLoad)
                                               {
                                                   LoadContent(westContainer, f.WallSourceBottom);
                                               }

                                           }
                                       );
                                    #endregion
                                    #region WallSourceRight
                                    if (!string.IsNullOrEmpty(f.WallSourceTop))
                                        new IHTMLDiv { }.AttachTo(cube.north.node).With(
                                           westContainer =>
                                           {

                                               //innerText = f.LeftWallSource
                                               //btn.className = "nolock";


                                               //westContainer.style.transform = "rotateZ(-90deg)";
                                               westContainer.style.transformOrigin = "0% 0%";

                                               westContainer.style.position = IStyle.PositionEnum.absolute;
                                               westContainer.style.width = cube.north.node.clientWidth + "px";
                                               westContainer.style.left = "0px";
                                               westContainer.style.top = "0px";
                                               westContainer.style.height = cube.north.node.clientHeight + "px";



                                               if (f.LeftWallSourceAutoLoad)
                                               {
                                                   LoadContent(westContainer, f.WallSourceTop);

                                               }

                                           }
                                       );
                                    #endregion

                                    #region WallSourceRight
                                    if (!string.IsNullOrEmpty(f.WallSourceRight))
                                        new IHTMLDiv { }.AttachTo(cube.east.node).With(
                                           westContainer =>
                                           {

                                               //innerText = f.LeftWallSource
                                               //btn.className = "nolock";


                                               westContainer.style.transform = "rotateZ(-90deg)";
                                               westContainer.style.transformOrigin = "0% 0%";

                                               westContainer.style.position = IStyle.PositionEnum.absolute;
                                               westContainer.style.width = cube.east.node.clientHeight + "px";
                                               westContainer.style.top = cube.east.node.clientHeight + "px";
                                               westContainer.style.height = cube.east.node.clientWidth + "px";



                                               if (f.LeftWallSourceAutoLoad)
                                               {
                                                   LoadContent(westContainer, f.WallSourceRight);

                                               }

                                           }
                                       );
                                    #endregion

                                    #region LeftWallSource
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

                                                if (f.LeftWallSourceAutoLoad)
                                                {
                                                    LoadContent(westContainer, f.LeftWallSource);

                                                }

                                            }
                                        );
                                    #endregion


                                }
                            );

                        }
                    );
                    (item as Floor).With(
                        f =>
                        {

                            window.viewport.camera.position.x = (f.Left + f.Width / 2) * zoom;
                            window.viewport.camera.position.y = -(f.Top + f.Height / 2) * zoom;

                            buildCube("white",

                                 //w, h, d, x, y, z, rx, ry, rz
                                f.Width * zoom,

                                f.Height * zoom,

                                10,
                                //0,

                                -f.Right * zoom, f.Top * zoom,

                                0 - low * zoom + zz,
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


            //avoid out of memory - elements will go missing
            //zz += 300;

            //CreateFromFloorplan();







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
                        var x = window.viewport.camera.rotation.x;
                        x -= e.movementY / 2;

                        x = Math.Min(x, -60).Max(-120);

                        //Console.WriteLine(new { x });
                        window.viewport.camera.rotation.x = x;


                        var z = window.viewport.camera.rotation.z;
                        z += e.movementX / 2;


                        window.viewport.camera.rotation.z = z;

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
