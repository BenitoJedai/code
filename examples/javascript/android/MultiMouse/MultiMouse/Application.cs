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
using MultiMouse.Design;
using MultiMouse.HTML.Pages;
using System.Windows.Forms;
using System.Drawing;
using SQLiteWithDataGridView.Library;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;
using MultiMouse.SVGCursor.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace MultiMouse
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        public static void LikeDesktop(FormStyler s)
        {
            s.TargetOuterBorder.style.boxShadow = "rgba(0, 148, 155, 0.3) 0px 0px 6px 3px";
            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(0, 148, 155);

            s.TargetInnerBorder.style.borderWidth = "0px";

            s.CloseButton.style.color = JSColor.White;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";

            s.TargetResizerPadding.style.left = "0px";
            s.TargetResizerPadding.style.top = "0px";
            s.TargetResizerPadding.style.right = "0px";
            s.TargetResizerPadding.style.bottom = "0px";

            s.ContentContainerPadding.style.top = "26px";


            s.Caption.style.backgroundColor = JSColor.FromRGB(0, 148, 155);
        }

        public static void LikeVirtualScreen(FormStyler s)
        {
            s.TargetOuterBorder.style.boxShadow = "rgba(255, 0, 0, 0.3) 0px 0px 6px 3px";
            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(255, 0, 0);

            s.TargetInnerBorder.style.borderWidth = "0px";
            s.TargetInnerBorder.style.backgroundColor = "";

            s.CloseButton.style.color = JSColor.White;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";

            s.TargetResizerPadding.style.left = "0px";
            s.TargetResizerPadding.style.top = "0px";
            s.TargetResizerPadding.style.right = "0px";
            s.TargetResizerPadding.style.bottom = "0px";

            s.ContentContainerPadding.style.top = "26px";


            s.Caption.style.backgroundColor = "";
        }

        public static void LikeVirtualWindow(FormStyler s)
        {
            s.TargetOuterBorder.style.boxShadow = "";
            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(127, 0, 0);
            s.TargetOuterBorder.style.borderStyle = "dotted";

            s.TargetInnerBorder.style.borderWidth = "0px";
            s.TargetInnerBorder.style.backgroundColor = "";

            s.CloseButton.style.color = JSColor.White;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";

            s.TargetResizerPadding.style.left = "0px";
            s.TargetResizerPadding.style.top = "0px";
            s.TargetResizerPadding.style.right = "0px";
            s.TargetResizerPadding.style.bottom = "0px";

            s.ContentContainerPadding.style.top = "26px";


            s.Caption.style.backgroundColor = "";
        }

        public readonly int device_id;

        public Action<Action<XElement>> device_bind;
        public Action<int, XElement> device_onmessage;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var random = new Random();
            device_id = random.Next();

            #region con
            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();
            con.StartPosition = FormStartPosition.Manual;
            con.Show();

            // make it slim
            con.Height = 100;

            // TopMost
            con.GetHTMLTarget().style.zIndex = 20002;
            con.Opacity = 0.9;




            Action Toggle =
                delegate
                {
                    if (con.WindowState == FormWindowState.Minimized)
                    {
                        con.WindowState = FormWindowState.Normal;

                    }
                    else
                    {
                        con.WindowState = FormWindowState.Minimized;


                    }

                    // put the console far right bottom
                    con.MoveTo(
                        Native.window.Width - con.Width,
                        Native.window.Height - con.Height
                    );

                };

            Action<int> AtKeyCode =
               KeyCode =>
               {
                   Console.WriteLine(
                       new { KeyCode }

                   );



                   // US
                   if (KeyCode == 222)
                   {
                       Toggle();
                   }
                   // EE
                   if (KeyCode == 192)
                   {
                       Toggle();
                   }
               };


#if onorientationchange
            Native.window.onorientationchange +=
                e =>
                {
                    Toggle();
                };
#endif

            Native.document.onkeyup +=
                e =>
                {
                    AtKeyCode(e.KeyCode);

                };

            Toggle();
            #endregion

            Console.WriteLine("console ready for " + new { id = device_id });

            var x = 0;
            var y = 0;

            #region Virtual Screen
            FormStyler.AtFormCreated = LikeDesktop;
            var fs = new Form { };

            //fs.FormBorderStyle = FormBorderStyle.None;

            fs.BackColor = Color.FromArgb(0, 148, 155);
            fs.Width = Native.screen.width / 4;
            fs.Height = Native.screen.height / 4;
            fs.Show();
            fs.Opacity = 0.5;

            FormStyler.AtFormCreated = LikeVirtualScreen;
            var fvs = new Form { Text = "Virtual Screen" };
            fvs.BackColor = Color.Transparent;
            FormStyler.AtFormCreated = FormStyler.LikeWindowsClassic;
            fvs.Width = Native.screen.width / 4;
            fvs.Height = Native.screen.height / 4;
            fvs.Show();
            fvs.Owner = fs;

            var fw = new Form { };

            fw.Width = Native.window.Width / 4;
            fw.Height = Native.window.Height / 4;
            fw.Show();
            fw.Owner = fvs;
            fw.Opacity = 0.8;

            KeepOwnedFormsLinkedToOwnerLocation(fs);
            KeepOwnedFormsLinkedToOwnerLocation(fvs);
            KeepOwnedFormsLinkedToOwnerLocation(fw);
            #endregion


            // doesnt work yet?
            //fw.SizeGripStyle = SizeGripStyle.Hide;

            var svg = new ISVGSVGElement().AttachTo(page.content);
            svg.style.SetLocation(0, 0);

            var vsvg = new ISVGSVGElement().AttachTo(fvs.GetHTMLTarget());
            vsvg.style.SetLocation(0, 0);

            #region VirtualScreenUpdate
            Action VirtualScreenUpdate = delegate
            {
                if (fs.Capture)
                    return;

                // dragging it?
                if (fvs.Capture)
                    return;

                var max_right = fvs.OwnedForms.Max(k => k.Right);
                var min_left = fvs.OwnedForms.Min(k => k.Left);

                var max_bottom = fvs.OwnedForms.Max(k => k.Bottom);
                var min_top = fvs.OwnedForms.Min(k => k.Top);

                DisableKeepOwnedFormsLinkedToOwnerLocation = true;

                fvs.Left = min_left;
                fvs.Top = min_top;
                fvs.Width = max_right - min_left;
                fvs.Height = max_bottom - min_top;

                page.content.style.SetLocation(
                    (min_left - fw.Left) * 4,
                    (min_top - fw.Top) * 4,
                    (max_right - min_left) * 4,
                    (max_bottom - min_top) * 4
                );



                DisableKeepOwnedFormsLinkedToOwnerLocation = false;

            };
            #endregion


            fw.LocationChanged +=
                delegate
                {
                    VirtualScreenUpdate();
                };

            #region AtResize
            Action AtResize = delegate
            {
                // screen can change, but only once, when window is moved to the other monitor?
                fs.Text = "Screen " + new { Native.screen.width, Native.screen.height };
                fs.Width = Native.screen.width / 4;
                fs.Height = Native.screen.height / 4;

                fw.Text = " " + new { Native.window.Width, Native.window.Height
#if onorientationchange                    
                    , Native.window.orientation 
#endif
                };
                fw.Width = Native.window.Width / 4;
                fw.Height = Native.window.Height / 4;

                VirtualScreenUpdate();
            };



            Native.window.onresize +=
                delegate
                {
                    AtResize();
                };

            Native.window.onfocus +=
                delegate
                {
                    AtResize();
                };

            Native.window.onblur +=
                delegate
                {
                    AtResize();
                };

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    AtResize();
                }
            ).StartInterval(1000 / 2);
            #endregion


            // what is attaching what?
            // what about await
            var svgcursor1ghost = new cursor1().AttachTo(page.content).ToSVG();

            var svgcursor1 = new cursor1().AttachTo(page.content).ToSVG();

            svgcursor1.fill += svgcursor1ghost.fill;

            var vcursor = new cursor1().AttachTo(fvs.GetHTMLTarget()).ToSVG();

            var Shadows = new List<Form>();

            Func<Form, Form> CreateShadow =
                _fw =>
                {
                    FormStyler.AtFormCreated = LikeVirtualWindow;
                    var fwshadow = new Form();
                    Shadows.Add(fwshadow);

                    fwshadow.BackColor = Color.Transparent;
                    FormStyler.AtFormCreated = FormStyler.LikeWindowsClassic;
                    fwshadow.Width = Native.screen.width / 4;
                    fwshadow.Height = Native.screen.height / 4;
                    fwshadow.Show();


                    Action fwshadow_update = delegate
                    {

                        fwshadow.MoveTo(_fw.Left, _fw.Top);
                        fwshadow.SizeTo(_fw.Width, _fw.Height);
                    };

                    _fw.LocationChanged +=
                        delegate
                        {
                            fwshadow_update();
                        };

                    _fw.SizeChanged +=
                        delegate
                        {
                            fwshadow_update();

                        };

                    fwshadow_update();

                    _fw.BringToFront();

                    return fwshadow;
                };

            Shadows.Add(CreateShadow(fw));

            bool canexit = false;
            bool canenter = true;

            Action at_exit_MultiMouseMode = delegate
            {
                //Console.WriteLine("at_exit_MultiMouseMode");
            };

            Action at_enter_MultiMouseMode = delegate
            {

                //Console.WriteLine("at_enter_MultiMouseMode");
            };

            Action<IEvent.MouseButtonEnum> at_mousedown = button =>
            {

                //Console.WriteLine("at_enter_mousedown: " + button);
            };


            Action at_mouseup = delegate
            {

                //Console.WriteLine("at_enter_mouseup");
            };




            var path = new ISVGPathElement().AttachTo(svg);
            path.setAttribute("style", "stroke: black; stroke-width: 4; fill: none;");

            var path_d = "";



            var vpath = new ISVGPathElement().AttachTo(vsvg);
            vpath.setAttribute("style", "stroke: black; stroke-width: 1; fill: none;");

            var vpath_d = "";

            bool internal_ismousedown = false;

            Action<IEvent.MouseButtonEnum> internal_mousedown = button =>
            {
                internal_ismousedown = true;

                path = new ISVGPathElement().AttachTo(svg);

                if (button == IEvent.MouseButtonEnum.Left)
                    path.setAttribute("style", "stroke: black; stroke-width: 4; fill: none;");
                else
                    path.setAttribute("style", "stroke: rgb(0, 108, 115); stroke-width: 32; fill: none;");

                path_d = "";


                vpath = new ISVGPathElement().AttachTo(vsvg);

                if (button == IEvent.MouseButtonEnum.Left)
                    vpath.setAttribute("style", "stroke: black; stroke-width: 1; fill: none;");
                else
                    vpath.setAttribute("style", "stroke: rgb(0, 108, 115); stroke-width: 8; fill: none;");

                vpath_d = "";

                svgcursor1.fill(0, 0, 255);
                vcursor.fill(0, 0, 255);

                //path.d = "    M100,50  L10,10   L200,200   ";
                path_d += " M" + x + "," + y;
                path.d = path_d;

                vpath_d += " M" + (x / 4) + "," + (y / 4);
                vpath.d = vpath_d;
            };

            Action<IEvent.MouseButtonEnum> mousedown = button =>
            {
                at_mousedown(button);
                internal_mousedown(button);
            };

            Action internal_mouseup = delegate
            {
                internal_ismousedown = false;





                svgcursor1.fill(0, 255, 0);
                vcursor.fill(0, 255, 0);
            };

            Action mouseup = delegate
            {
                at_mouseup();
                internal_mouseup();
            };



            #region exit_MultiMouseMode
            Action internal_exit_MultiMouseMode = delegate
            {
                svgcursor1.fill(0, 0, 0);
                vcursor.fill(0, 0, 0);

                con.Opacity = 0.9;
                page.content.style.Opacity = 0.3;
                page.content.style.zIndex = 0;

                page.content.style.backgroundColor = "rgba(0, 148, 155, 1)";
                Native.Document.body.style.backgroundColor = "black";

                page.content.style.With(
                    (dynamic s) =>
                    {
                        s.webkitFilter = "blur(3px)";
                    }
                );

                page.info.style.With(
                    (dynamic s) =>
                    {
                        s.webkitFilter = "";
                    }
                );

                Shadows.WithEach(
                   f =>
                       f.GetHTMLTarget().style.With(
                           (dynamic s) =>
                           {
                               s.webkitFilter = "";
                           }
                       )
                 );


                fs.FormsByOwnership().WithEach(
                 f =>
                     f.GetHTMLTarget().style.With(
                         (dynamic s) =>
                         {
                             s.webkitFilter = "";

                         }
                     )
            );


                fvs.OwnedForms.WithEach(
                    k =>
                    {
                        k.GetHTMLTarget().style.display = IStyle.DisplayEnum.block;
                    }
                );




            };

            Action exit_MultiMouseMode = delegate
            {
                if (!canexit)
                    return;

                canexit = false;
                canenter = true;

                at_exit_MultiMouseMode();
                internal_exit_MultiMouseMode();
            };
            #endregion

            #region enter_MultiMouseMode
            Action internal_enter_MultiMouseMode = delegate
            {
                svgcursor1.fill(255, 0, 0);
                vcursor.fill(255, 0, 0);
                con.Opacity = 0.5;

                page.content.style.Opacity = 1.0;
                page.content.style.backgroundColor = "";
                Native.Document.body.style.backgroundColor = "rgba(0, 148, 155, 1)";

                page.content.style.zIndex = 20000;
                con.GetHTMLTarget().style.zIndex = 20002;
                page.content.style.With(
                    (dynamic s) =>
                    {
                        s.webkitFilter = "";
                    }
                );

                page.info.style.With(
                    (dynamic s) =>
                    {
                        s.webkitFilter = "blur(3px)";
                    }
                );


                fvs.OwnedForms.WithEach(
                     k =>
                     {
                         k.GetHTMLTarget().style.display = IStyle.DisplayEnum.none;
                     }
                 );

                Shadows.WithEach(
                  f =>
                      f.GetHTMLTarget().style.With(
                          (dynamic s) =>
                          {
                              s.webkitFilter = "blur(3px)";
                          }
                      )
                );


                fs.FormsByOwnership().WithEach(
                    f =>
                        f.GetHTMLTarget().style.With(
                            (dynamic s) =>
                            {
                                s.webkitFilter = "blur(3px)";
                            }
                        )
                );
            };

            Action enter_MultiMouseMode = delegate
            {
                if (!canenter)
                    return;

                canexit = true;
                canenter = false;

                at_enter_MultiMouseMode();
                internal_enter_MultiMouseMode();
            };
            #endregion





            #region onmousemove



            Native.Document.body.onmouseup +=
              e =>
              {
                  if (Native.Document.pointerLockElement == Native.Document.body)
                  {
                      mouseup();
                      return;
                  }
              };

            Native.Document.body.onmousedown +=
                e =>
                {
                    if (Native.Document.pointerLockElement == Native.Document.body)
                    {
                        mousedown(e.MouseButton);
                        return;
                    }

                    Console.WriteLine("requesting MultiMouse mode!");

                    x = e.CursorX;
                    y = e.CursorY;
                    e.preventDefault();
                    Native.Document.body.requestPointerLock();

                };

            Action<int, int> at_set_cursor_position = delegate { };


            var pxx = 0;
            var pyy = 0;

            var ghost_busy = false;

            Action<int, int> internal_set_cursor_position =
                 (xx, yy) =>
                 {
                     // already set to that exact location!
                     if (pxx == xx)
                         if (pyy == yy)
                             return;


                     pxx = xx;
                     pyy = yy;

                     vcursor.Element.style.SetSize(
                           cursor1.ImageDefaultWidth / 4,
                           cursor1.ImageDefaultHeight / 4
                       );

                     vcursor.Element.style.SetLocation(
                           (xx - 14) / 4,
                           (yy - (64 - 56)) / 4
                       );


                     svgcursor1.Element.style.SetLocation(

                         xx - 14,

                         // inscaope/svg Y is upside down!
                         yy - (64 - 56)

                     );

                     if (!ghost_busy)
                     {
                         ghost_busy = true;

                         svgcursor1ghost.Element.style.Opacity = 0.2;
                         svgcursor1ghost.Element.style.With(
                                (dynamic s) => s.webkitTransition = "all 0.5s linear"
                         );

                         svgcursor1ghost.Element.style.SetLocation(

                            pxx - 14,

                            // inscaope/svg Y is upside down!
                            pyy - (64 - 56)

                        );

                         new ScriptCoreLib.JavaScript.Runtime.Timer(
                             delegate
                             {
                                 svgcursor1ghost.Element.style.SetLocation(

                                    pxx - 14,

                                    // inscaope/svg Y is upside down!
                                    pyy - (64 - 56)

                                );

                                 ghost_busy = false;
                             }
                         ).StartTimeout(500);
                     }


                     // if this window will be activated next time we can continue where we were
                     // told to..
                     x = xx;
                     y = yy;

                     if (internal_ismousedown)
                     {
                         path_d += " L" + x + "," + y;
                         path.d = path_d;

                         vpath_d += " L" + (x / 4) + "," + (y / 4);
                         vpath.d = vpath_d;
                     }

                 };

            Action<int, int> set_cursor_position =
                (xx, yy) =>
                {
                    at_set_cursor_position(xx, yy);
                    internal_set_cursor_position(xx, yy);
                };

            Native.Document.body.onmousemove +=
                e =>
                {
                    if (Native.Document.pointerLockElement == Native.Document.body)
                    {
                        enter_MultiMouseMode();

                        x += e.movementX;
                        y += e.movementY;

                        // clip it
                        // fullscreen behaves differently?
                        x = x.Min(fvs.Width * 4).Max(0);
                        y = y.Min(fvs.Height * 4).Max(0);

                        set_cursor_position(x, y);
                    }
                    else
                    {
                        exit_MultiMouseMode();
                    }

                };
            #endregion

            internal_exit_MultiMouseMode();
            internal_set_cursor_position(0, 0);

            Native.document.body.ontouchstart +=
                 e =>
                 {
                     e.preventDefault();
                     e.stopPropagation();

                     e.touches[0].With(
                         touch =>
                         {
                             // how do we enter?
                             enter_MultiMouseMode();
                             // exit by broswer history move?

                             set_cursor_position(touch.clientX, touch.clientY);

                             // ipad has 11 touchpoints. multiply that with the number of devices/
                             // for now we support 1 pointer per session :)

                             if (e.touches.length == 1)
                                 mousedown(IEvent.MouseButtonEnum.Left);
                             else
                                 mousedown(IEvent.MouseButtonEnum.Right);

                         }
                     );
                 };

            Native.document.body.ontouchend +=
               e =>
               {

                   e.preventDefault();
                   e.stopPropagation();



                   // ipad has 11 touchpoints. multiply that with the number of devices/
                   // for now we support 1 pointer per session :)

                   if (e.touches.length == 0)
                       mouseup();
                   else
                       mousedown(IEvent.MouseButtonEnum.Left);

               };

            Native.document.body.ontouchmove +=
                 e =>
                 {
                     e.preventDefault();
                     e.stopPropagation();


                     e.touches[0].With(
                         touch =>
                         {
                             set_cursor_position(touch.clientX, touch.clientY);
                         }
                     );
                 };



            #region onmessage

            bool disable_bind_reconfigure = false;


            Action<int, int> internal_reconfigure =
                delegate { };

            Action<MessageEvent, XElement> internal_onmessage =
                (e, xml) =>
                {
                    device_onmessage(0, xml);
                };

            Native.window.onmessage +=
                e =>
                {
                    // who sent this? :P
                    var source = (string)e.data;
                    //var now = DateTime.Now;
                    //Console.WriteLine(now + " " + source);


                    var xml = XElement.Parse(source);




                    internal_onmessage(e, xml);
                };

            var friendly_devices = new
            {
                source_device_id = 0,
                f = default(Form),
                children = new
                {
                    child_id = 0,
                    fc = default(Form)
                }.ToEmptyList()
            }.ToEmptyList();

            #region device_onmessage
            this.device_onmessage =
                (source_device_id, xml) =>
                {
                    // mothership to local network?
                    // source_device_id = 0 means it came from one of our virtual screens?

                    if (xml.Name.LocalName == "at_mousedown")
                    {
                        int button = int.Parse(xml.Attribute("button").Value);
                        internal_mousedown((IEvent.MouseButtonEnum)button);
                    }

                    if (xml.Name.LocalName == "at_mouseup")
                    {
                        internal_mouseup();
                    }

                    if (xml.Name.LocalName == "at_enter_MultiMouseMode")
                    {
                        internal_enter_MultiMouseMode();
                    }

                    if (xml.Name.LocalName == "at_exit_MultiMouseMode")
                    {
                        internal_exit_MultiMouseMode();
                    }

                    if (xml.Name.LocalName == "at_set_cursor_position")
                    {
                        int xx = int.Parse(xml.Attribute("x").Value);
                        int yy = int.Parse(xml.Attribute("y").Value);

                        internal_set_cursor_position(xx, yy);
                    }


                };
            #endregion

            var ListOfChildren = new { child_id = 0, fc = default(Form) }.ToEmptyList();

            // when is this called?
            this.device_bind =
                (mothership_postXElement) =>
                {
                    // we might now be able to invoke the server, and via that any other device
                    Console.WriteLine("device_bind");

                    #region at_enter_MultiMouseMode
                    at_enter_MultiMouseMode +=
                        delegate
                        {
                            var xml = new XElement("at_enter_MultiMouseMode");

                            mothership_postXElement(xml);
                        };
                    #endregion

                    #region at_exit_MultiMouseMode
                    at_exit_MultiMouseMode +=
                        delegate
                        {
                            mothership_postXElement(new XElement("at_exit_MultiMouseMode"));
                        };
                    #endregion

                    #region at_mousedown
                    at_mousedown +=
                      button =>
                      {
                          mothership_postXElement(new XElement("at_mousedown", new XAttribute("button", "" + (int)button)));
                      };
                    #endregion

                    #region at_mouseup
                    at_mouseup +=
                     delegate
                     {
                         mothership_postXElement(new XElement("at_mouseup"));
                     };
                    #endregion

                    #region at_set_cursor_position
                    at_set_cursor_position +=
                       (xx, yy) =>
                       {

                           var xml = new XElement("at_set_cursor_position",
                               // int not yet supported?
                                   new XAttribute("x", "" + xx),
                                   new XAttribute("y", "" + yy)
                               );

                           mothership_postXElement(
                               xml
                           );



                       };
                    #endregion

                    // now we can reply..
                    this.device_onmessage +=
                       (source_device_id, xml) =>
                       {
                           #region at_virtualwindowsync_reconfigure
                           if (source_device_id != 0)
                               if (xml.Name.LocalName == "at_virtualwindowsync_reconfigure")
                               {
                                   int __device_id = int.Parse(xml.Attribute("device_id").Value);

                                   if (__device_id == device_id)
                                   {
                                       // are we being reconfigured?

                                       friendly_devices.Where(k => k.source_device_id == source_device_id).WithEach(
                                           q =>
                                           {
                                               int dx = int.Parse(xml.Attribute("dx").Value);
                                               int dy = int.Parse(xml.Attribute("dy").Value);
                                               disable_bind_reconfigure = true;

                                               q.f.MoveTo(
                                                   fw.Left - dx,
                                                   fw.Top - dy
                                               );
                                               disable_bind_reconfigure = false;

                                           }
                                       );
                                   }
                               }
                           #endregion

                           #region at_virtualwindowsync
                           if (source_device_id != 0)
                               if (xml.Name.LocalName == "at_virtualwindowsync")
                               {
                                   Console.WriteLine("got at_virtualwindowsync");

                                   // do we know this device?
                                   var q = friendly_devices.FirstOrDefault(k => k.source_device_id == source_device_id);

                                   int w = int.Parse(xml.Attribute("w").Value);
                                   int h = int.Parse(xml.Attribute("h").Value);

                                   Action reposition = delegate { };

                                   if (q == null)
                                   {
                                       var fc = new Form { Text = new { source_device_id }.ToString() };

                                       q = new { source_device_id, f = fc, children = new { child_id = 0, fc = default(Form) }.ToEmptyList() };

                                       friendly_devices.Add(q);

                                       q.f.StartPosition = FormStartPosition.Manual;
                                       q.f.Show();
                                       // show should respect opacity?
                                       q.f.Opacity = 0.3;


                                       // where to put it?
                                       // left or right?

                                       var max_right = fvs.OwnedForms.Max(k => k.Right);
                                       var min_left = fvs.OwnedForms.Min(k => k.Left);

                                       if (source_device_id < device_id)
                                           q.f.Left = min_left - w;
                                       else
                                           q.f.Left = max_right;

                                       q.f.Top = fw.Top;
                                       q.f.Owner = fvs;

                                       var fcShadow = CreateShadow(q.f);
                                       Shadows.Add(fcShadow);

                                       #region from now on if we move any of our screens
                                       // in relation to this source_device_id we have to notify it

                                       Action SendDelta = delegate
                                       {
                                           var pdx = fc.Left - fw.Left;
                                           var pdy = fc.Top - fw.Top;

                                           mothership_postXElement(
                                             new XElement("at_virtualwindowsync_reconfigure",
                                                 new XAttribute("device_id", "" + source_device_id),
                                                 new XAttribute("dx", "" + pdx),
                                                 new XAttribute("dy", "" + pdy)
                                             )
                                           );
                                       };

                                       fw.LocationChanged +=
                                           delegate
                                           {
                                               if (disable_bind_reconfigure)
                                                   return;

                                               SendDelta();
                                           };

                                       fc.LocationChanged +=
                                           delegate
                                           {
                                               if (disable_bind_reconfigure)
                                                   return;


                                               SendDelta();
                                           };


                                       #endregion


                                   }

                                   // thanks for letting us know that you changed your size...
                                   q.f.Width = w;
                                   q.f.Height = h;


                                   xml.Elements("child").WithEach(
                                       cxml =>
                                       {
                                           // any new children?
                                           int child_id = int.Parse(cxml.Attribute("child_id").Value);

                                           int pdx = int.Parse(cxml.Attribute("pdx").Value);
                                           int pdy = int.Parse(cxml.Attribute("pdy").Value);

                                           int cw = int.Parse(cxml.Attribute("w").Value);
                                           int ch = int.Parse(cxml.Attribute("h").Value);

                                           var cq = q.children.FirstOrDefault(k => k.child_id == child_id);

                                           if (cq == null)
                                           {
                                               var fc = new Form { Text = new { source_device_id, child_id }.ToString() };

                                               cq = new { child_id, fc };

                                               q.children.Add(cq);

                                               cq.fc.StartPosition = FormStartPosition.Manual;
                                               cq.fc.Show();
                                               // show should respect opacity?
                                               cq.fc.Opacity = 0.2;

                                               // if this child needs to be between then add it
                                               // before reposition

                                               cq.fc.Owner = fvs;

                                               var fcShadow = CreateShadow(cq.fc);
                                               Shadows.Add(fcShadow);

                                           }


                                           cq.fc.Left = q.f.Left + pdx;
                                           cq.fc.Top = q.f.Top + pdy;

                                           // thanks for letting us know that you changed your size...
                                           cq.fc.Width = cw;
                                           cq.fc.Height = ch;
                                       }
                                   );

                               }
                           #endregion

                       };

                    // lets tell the world about virtual screens owned by us.
                    // lets start by advertising our size.

                    #region at_virtualwindowsync
                    var t = new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            // do we know whats the dx to other windows?
                            var xml = new XElement("at_virtualwindowsync",
                                // int not yet supported?
                                new XAttribute("w", "" + fw.Width),
                                new XAttribute("h", "" + fw.Height)


                            );

                            #region what about children?
                            ListOfChildren.WithEach(
                                c =>
                                {
                                    var pdx = c.fc.Left - fw.Left;
                                    var pdy = c.fc.Top - fw.Top;

                                    xml.Add(

                                        new XElement("child",
                                            new XAttribute("child_id", "" + c.child_id),
                                        // int not yet supported?
                                            new XAttribute("pdx", "" + pdx),
                                            new XAttribute("pdy", "" + pdy),
                                            new XAttribute("w", "" + c.fc.Width),
                                            new XAttribute("h", "" + c.fc.Height)
                                        )

                                    );
                                }
                            );
                            #endregion


                            mothership_postXElement(
                              xml
                            );

                            Console.WriteLine("sent at_virtualwindowsync");

                        }
                    );

                    t.StartInterval(5000);
                    #endregion

                };

            Action<IWindow, Form> bind =
                (w, fc) =>
                {
                    this.device_bind(w.postXElement);

                    internal_onmessage +=
                        (e, xml) =>
                        {
                            if (xml.Name.LocalName == "reconfigure")
                            {
                                // how do we know this reconfigrue event is for us?

                                if (e.source == w)
                                {
                                    disable_bind_reconfigure = true;

                                    int dx = int.Parse(xml.Attribute("dx").Value);
                                    int dy = int.Parse(xml.Attribute("dy").Value);

                                    //Console.WriteLine("reconfigure " + new { dx, dy, fw.Left });

                                    //fw.Left += dx;
                                    //fw.Top += dy;


                                    fc.MoveTo(
                                        fw.Left - dx,
                                        fw.Top - dy
                                    );

                                    disable_bind_reconfigure = false;
                                }
                            }
                        };

                    Action SendDelta = delegate
                    {
                        var pdx = fc.Left - fw.Left;
                        var pdy = fc.Top - fw.Top;

                        w.postXElement(
                          new XElement("reconfigure",
                              new XAttribute("dx", "" + pdx),
                              new XAttribute("dy", "" + pdy)
                          )
                        );
                    };

                    fw.LocationChanged +=
                        delegate
                        {
                            if (disable_bind_reconfigure)
                                return;

                            SendDelta();
                        };

                    fc.LocationChanged +=
                        delegate
                        {
                            if (disable_bind_reconfigure)
                                return;


                            SendDelta();
                        };
                };
            #endregion


            #region opener
            Native.window.opener.With(
                w =>
                {
                    // disable features
                    page.info.Hide();

                    Console.WriteLine("we have opener: " + w.document.location.href);

                    var fc = new Form { Text = "opener" };

                    fc.Owner = fvs;

                    Action cAtResize = delegate
                    {
                        fc.Text = "Opener " + new { w.Width, w.Height };
                        fc.Width = w.Width / 4;
                        fc.Height = w.Height / 4;
                    };

                    w.onresize += delegate
                    {
                        cAtResize();
                    };

                    var ct = new ScriptCoreLib.JavaScript.Runtime.Timer(
                       delegate
                       {
                           cAtResize();
                       }
                    );

                    ct.StartInterval(1000 / 15);

                    cAtResize();

                    fc.StartPosition = FormStartPosition.Manual;
                    fc.Show();
                    fc.Opacity = 0.7;
                    fc.BackColor = Color.Transparent;

                    var fcShadow = CreateShadow(fc);
                    Shadows.Add(fcShadow);



                    Native.window.requestAnimationFrame +=
                        delegate
                        {
                            // ScriptCoreLib Windows Forms has a few bugs:P
                            fc.MoveTo(fw.Left - fc.Width, fw.Top);

                            bind(w, fc);
                        };

                }
            );
            #endregion

            #region make info clickable

            page.info.onmousedown +=
             e =>
             {
                 if (internal_ismousedown)
                     return;

                 e.stopPropagation();
             };

            page.info.ontouchstart +=
              e =>
              {
                  if (internal_ismousedown)
                      return;


                  e.stopPropagation();
              };

            page.info.ontouchmove +=
          e =>
          {
              if (internal_ismousedown)
                  return;


              e.stopPropagation();
          };

            page.info.ontouchend +=
     e =>
     {
         if (internal_ismousedown)
             return;

         e.stopPropagation();
     };
            #endregion

            #region OpenChildSession



            page.OpenChildSession.onclick +=
                e =>
                {
                    e.preventDefault();

                    Console.WriteLine("open child session...");

                    Native.window.open(
                        Native.Document.location.href,
                        "_blank", 400, 400, true).With(
                        w =>
                        {
                            w.onload +=
                                delegate
                                {
                                    if (w.document.location.href == "about:blank")
                                        return;

                                    Console.WriteLine("child onload " + w.document.location.href);

                                    var fc = new Form { Text = "child" };



                                    Action cAtResize = delegate
                                    {
                                        fc.Text = "Child " + new { w.Width, w.Height };
                                        fc.Width = w.Width / 4;
                                        fc.Height = w.Height / 4;

                                        VirtualScreenUpdate();
                                    };

                                    w.onresize += delegate
                                    {
                                        cAtResize();

                                    };

                                    var ct = new ScriptCoreLib.JavaScript.Runtime.Timer(
                                       delegate
                                       {
                                           cAtResize();
                                       }
                                    );

                                    ct.StartInterval(1000 / 2);

                                    //cAtResize();

                                    fc.StartPosition = FormStartPosition.Manual;
                                    fc.Show();
                                    fc.Opacity = 0.5;
                                    // first child could be a monitor to our right
                                    fc.MoveTo(fw.Right, fw.Top);
                                    fc.Owner = fvs;
                                    fc.BackColor = Color.Transparent;

                                    fc.Width = 400 / 4;
                                    fc.Height = 400 / 4;

                                    VirtualScreenUpdate();



                                    fc.LocationChanged +=
                                        delegate
                                        {
                                            VirtualScreenUpdate();

                                        };

                                    var fcShadow = CreateShadow(fc);
                                    Shadows.Add(fcShadow);

                                    var token = new { child_id = random.Next(), fc };

                                    ListOfChildren.Add(token);

                                    #region FormClosing
                                    w.onbeforeunload +=
                                        delegate
                                        {
                                            if (fc == null)
                                                return;

                                            w = null;

                                            ct.Stop();
                                            fc.Close();
                                            fc = null;
                                        };

                                    Native.window.onbeforeunload +=
                                        delegate
                                        {
                                            if (w == null)
                                                return;

                                            w.close();
                                            w = null;
                                        };

                                    fc.FormClosing +=
                                        delegate
                                        {

                                            if (w == null)
                                                return;

                                            ListOfChildren.Remove(token);
                                            Shadows.Remove(fcShadow);

                                            fc = null;

                                            w.close();
                                            w = null;
                                        };
                                    #endregion



                                    bind(w, fc);


                                };
                        }
                    );
                };
            #endregion





            Native.document.documentElement.style.overflow = IStyle.OverflowEnum.hidden;
        }

        public static bool DisableKeepOwnedFormsLinkedToOwnerLocation;

        private static void KeepOwnedFormsLinkedToOwnerLocation(Form fs)
        {
            int __counter = 0;
            int __x = 0;
            int __y = 0;

            Action InternalAtLocationChanged = () =>
            {
                if (__counter > 0)
                {
                    var dx = fs.Location.X - __x;
                    var dy = fs.Location.Y - __y;

                    foreach (var item in fs.OwnedForms)
                    {
                        if (!DisableKeepOwnedFormsLinkedToOwnerLocation)
                            item.Location += new Size(dx, dy);
                    }
                }
                __counter++;
                __x = fs.Location.X;
                __y = fs.Location.Y;
            };

            fs.LocationChanged +=
                delegate
                {
                    InternalAtLocationChanged();
                };
        }

    }


    public static class X
    {
        public static IEnumerable<Form> FormsByOwnership(this Form e)
        {
            return new[] { e }.Concat(e.OwnedForms.SelectMany(c => c.FormsByOwnership()));
        }

        public static void postXElement(this IWindow w, XElement e)
        {
            var now = DateTime.Now;

            // ToString on empty xelement fails?
            e.Add(new XAttribute("sentat", "" + now));

            var source = e.ToString();

            //Console.WriteLine("postXElement " + source);

            w.postMessage(source);
        }
    }
}
