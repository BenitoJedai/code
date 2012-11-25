using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using JellyworldExperiment.DualView.Design;
using JellyworldExperiment.DualView.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Windows.Forms;

namespace JellyworldExperiment.DualView
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        class delta
        {
            public double oldvalue = 0;
            public double dx = 0;
            public double newvalue = 0;

        }
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            #region IsRightScreen || IsLeftScreen
            var IsRightScreen = Native.Document.location.hash == "#/RightScreen";
            var IsLeftScreen = Native.Document.location.hash == "#/LeftScreen";

            if (IsRightScreen || IsLeftScreen)
            {
                if (IsRightScreen)
                {
                    "Right Screen".ToDocumentTitle();
                }

                if (IsLeftScreen)
                {
                    "Left Screen".ToDocumentTitle();
                }

                Native.Document.body.Clear();

                var a = new CSSTransform3DFPSBlueprint.HTML.Pages.App();

                a.Container.AttachToDocument();


                var hud = new IHTMLDiv().AttachToDocument();

                hud.style.position = IStyle.PositionEnum.absolute;
                hud.style.left = "0px";
                hud.style.top = "0px";
                hud.style.right = "0px";
                //hud.style.height = "2em";
                hud.style.zIndex = 1000;
                hud.style.backgroundColor = "rgba(0, 0, 0, 0.5)";
                hud.style.color = JSColor.White;

                var c = new IHTMLCenter { innerText = Native.Document.location.hash }.AttachTo(hud);

                new CSSTransform3DFPSBlueprint.Application().Initialize(a,
                   x =>
                   {
                       var w = CSSTransform3DFPSBlueprint.Application.window;

                       w.viewport.node.style.width = "200%";

                       if (IsRightScreen)
                           CSSTransform3DFPSBlueprint.Application.window.viewport.node.style.marginLeft = "-100%";

                       var du = true;
                       var qx = new delta();
                       var qy = new delta();
                       var qz = new delta();
                       var qp = new delta();

                       x.AfterKeystateChange +=
                        delegate
                        {
                            var data = new XElement("keyState",
                                new XAttribute("w", "" + w.keyState.forward),
                                new XAttribute("s", "" + w.keyState.backward),
                                new XAttribute("a", "" + w.keyState.strafeleft),
                                new XAttribute("d", "" + w.keyState.straferight)
                            );

                            Native.Window.opener.With(
                                parent =>
                                {
                                    //c.innerText = data.ToString();

                                    parent.postMessage(data.ToString());
                                }
                            );

                        };

                       x.AfterCameraRotationChange +=
                           delegate
                           {
                               var data = new XElement("viewport.camera.rotation",
                                   new XAttribute("x", "" + w.viewport.camera.rotation.x),
                                   new XAttribute("y", "" + w.viewport.camera.rotation.y),
                                   new XAttribute("z", "" + w.viewport.camera.rotation.z)
                               );

                               Native.Window.opener.With(
                                   parent =>
                                   {
                                       //c.innerText = data.ToString();

                                       parent.postMessage(data.ToString());
                                   }
                               );

                           };

                       Func<string, bool> bool_Parse =
                           xx =>
                           {
                               return xx.ToLower() == "true";
                           };

                       Native.Window.onmessage +=
                            e =>
                            {
                                var data = XElement.Parse("" + e.data);

                                if (data.Name.LocalName == "keyState")
                                {
                                    w.keyState.forward = bool_Parse(data.Attribute("w").Value);
                                    w.keyState.backward = bool_Parse(data.Attribute("s").Value);
                                    w.keyState.strafeleft = bool_Parse(data.Attribute("a").Value);
                                    w.keyState.straferight = bool_Parse(data.Attribute("d").Value);



                                }


                                if (data.Name.LocalName == "viewport.camera.rotation")
                                {
                                    new
                                    {
                                        x = int.Parse(data.Attribute("x").Value),
                                        y = int.Parse(data.Attribute("y").Value),
                                        z = int.Parse(data.Attribute("z").Value)
                                    }.With(
                                        r =>
                                        {
                                            w.viewport.camera.rotation.x = r.x;
                                            w.viewport.camera.rotation.y = r.y;
                                            w.viewport.camera.rotation.z = r.z;

                                        }
                                    );




                                }

                                if (data.Name.LocalName == "range")
                                {
                                    qx.newvalue = int.Parse(data.Attribute("x").Value);
                                    qy.newvalue = int.Parse(data.Attribute("y").Value);
                                    qz.newvalue = int.Parse(data.Attribute("z").Value);
                                    qp.newvalue = int.Parse(data.Attribute("p").Value);

                                    if (du)
                                    {
                                        qx.oldvalue = qx.newvalue;
                                        qy.oldvalue = qy.newvalue;
                                        qz.oldvalue = qz.newvalue;
                                        qp.oldvalue = qp.newvalue;
                                        du = false;
                                    }
                                    else
                                    {
                                        if (qx.newvalue != qx.oldvalue)
                                        {
                                            qx.dx = qx.newvalue - qx.oldvalue;
                                            qx.oldvalue = qx.newvalue;
                                            w.viewport.camera.rotation.x -= qx.dx * 0.5;
                                        }

                                        if (qy.newvalue != qy.oldvalue)
                                        {
                                            qy.dx = qy.newvalue - qy.oldvalue;
                                            qy.oldvalue = qy.newvalue;
                                            w.viewport.camera.rotation.y -= qy.dx * 0.1;
                                        }

                                        if (qz.newvalue != qz.oldvalue)
                                        {
                                            qz.dx = qz.newvalue - qz.oldvalue;
                                            qz.oldvalue = qz.newvalue;
                                            w.viewport.camera.rotation.z -= qz.dx * 0.5;
                                        }

                                        Console.WriteLine(
                                            new
                                            {
                                                w.viewport.camera.rotation.x,
                                                w.viewport.camera.rotation.y,
                                                w.viewport.camera.rotation.z
                                            }

                                            );

                                        if (qp.newvalue != qp.oldvalue)
                                        {
                                            //qz.dx = qz.newvalue - qz.oldvalue;
                                            qp.oldvalue = qp.newvalue;
                                            w.viewport.node.style.perspective = "" + (500 + qp.newvalue * 4);
                                        }
                                    }



                                }

                                //c.innerText = new { data, dx, newvalue, oldvalue }.ToString();
                                c.innerText = data.ToString();


                                //oldvalue = newvalue;
                                //w.viewport.camera.rotation.x -= e.movementY / 2;

                            };
                   }
               );




                return;
            }
            #endregion

            Action range_onchange = delegate
            {
            };

            #region bind
            Action<IHTMLButton, string, Action<IWindow, XElement>> bind =
                (btn, hash, yield) =>
                {
                    btn.onclick +=
                        delegate
                        {
                            btn.disabled = true;

                            var w = Native.Window.open(
                                hash,
                                "_blank",
                                400,
                                300,
                                false
                            );

                            w.focus();

                            w.onload +=
                                delegate
                                {
                                    Action onchange =
                                        delegate
                                        {


                                            //                       JellyworldExperiment.DualView.Application+<>c__DisplayClassc+<>c__DisplayClass14+<>c__DisplayClass16+<>c__DisplayClass18
                                            //script: error JSC1000: Method: <.ctor>b__7, Type: JellyworldExperiment.DualView.Application+<>c__DisplayClassc+<>c__DisplayClass14+<>c__DisplayClass16+<>c__DisplayClass18; emmiting failed : System.ArgumentNullException: Value cannot be null.
                                            //   at jsc.ILFlowStackItem.InlineLogic(   )
                                            //   at  .    .    ( ?   ,    , ILInstruction , ILFlowStackItem )
                                            //   at  .    .    ( ?   ,    , ILInstruction , ILFlowStackItem )
                                            //   at  . ?  .    (   , ILInstruction , ILFlowStackItem[] , Int32 , MethodBase )

                                            var xml = new XElement("range",
                                              new XAttribute("x", page.range_x.value),
                                              new XAttribute("y", page.range_y.value),
                                              new XAttribute("z", page.range_z.value),
                                              new XAttribute("p", page.range_p.value)
                                          );

                                            w.postMessage(xml.ToString());

                                        };

                                    onchange();

                                    page.range_x.onchange +=
                                        delegate
                                        {
                                            onchange();
                                        };
                                    page.range_y.onchange +=
                                        delegate
                                        {
                                            onchange();
                                        };
                                    page.range_z.onchange +=
                                        delegate
                                        {
                                            onchange();
                                        };

                                    page.range_p.onchange +=
                                        delegate
                                        {
                                            onchange();
                                        };

                                    range_onchange += onchange;

                                    Native.Window.onmessage +=
                                         e =>
                                         {
                                             if (e.source != w)
                                                 return;

                                             var data = XElement.Parse("" + e.data);

                                             yield(w, data);

                                         };

                                    yield(w, null);
                                };
                        };


                };
            #endregion

            #region do bind
            var wLeftScreen = default(IWindow);
            var wRightScreen = default(IWindow);

            bind(page._LeftScreen, "#/LeftScreen",
                (w, data) =>
                {
                    if (wLeftScreen == null)
                    {
                        wLeftScreen = w;

                        w.onbeforeunload +=
                            delegate
                            {
                                page._LeftScreen.innerText = "closed";
                            };

                        Native.Window.onbeforeunload +=
                            delegate
                            {
                                wLeftScreen.close();
                            };
                    }


                    if (data != null)
                    {
                        page._LeftScreen.innerText = data.ToString();

                        if (wRightScreen != null)
                        //if (data.Name.LocalName == "viewport.camera.rotation")
                        {
                            wRightScreen.postMessage(data.ToString());
                        }
                    }
                }
            );

            bind(page._RightScreen, "#/RightScreen",
                (w, data) =>
                {
                    if (wRightScreen == null)
                    {
                        wRightScreen = w;

                        w.onbeforeunload +=
                             delegate
                             {
                                 page._RightScreen.innerText = "closed";
                             };


                        Native.Window.onbeforeunload +=
                            delegate
                            {
                                wRightScreen.close();
                            };
                    }

                    if (data != null)
                    {
                        page._RightScreen.innerText = data.ToString();


                        if (wLeftScreen != null)
                        //if (data.Name.LocalName == "viewport.camera.rotation")
                        {
                            wLeftScreen.postMessage(data.ToString());
                        }
                    }
                }
            );
            #endregion

            var forward = false;
            var backward = false;
            var strafeleft = false;
            var straferight = false;

            Action AfterKeystateChange =
                delegate
                {
                    var data = new XElement("keyState",
                           new XAttribute("w", "" + forward),
                           new XAttribute("s", "" + backward),
                           new XAttribute("a", "" + strafeleft),
                           new XAttribute("d", "" + straferight)
                       );

                    if (wLeftScreen != null)
                        wLeftScreen.postMessage(data.ToString());

                    if (wRightScreen != null)
                        wRightScreen.postMessage(data.ToString());

                };

            #region onkeydown
            Native.Document.body.onkeydown += e =>
            {
                //Console.WriteLine(new { e.KeyCode });

                if (e.KeyCode == (int)Keys.W)
                    forward = true;
                if (e.KeyCode == (int)Keys.S)
                    backward = true;
                if (e.KeyCode == (int)Keys.A)
                    strafeleft = true;
                if (e.KeyCode == (int)Keys.D)
                    straferight = true;

                if (AfterKeystateChange != null)
                    AfterKeystateChange();
            };

            Native.Document.body.onkeyup += e =>
            {
                if (e.KeyCode == (int)Keys.W)
                    forward = false;
                if (e.KeyCode == (int)Keys.S)
                    backward = false;

                if (e.KeyCode == (int)Keys.A)
                    strafeleft = false;
                if (e.KeyCode == (int)Keys.D)
                    straferight = false;

                if (AfterKeystateChange != null)
                    AfterKeystateChange();
            };
            #endregion

            page.SimulateFace.onclick +=
                delegate
                {
                    page.SimulateFace.disabled = true;

                    new Form { Text = "Simulated Face Detection" }.With(
                        f =>
                        {
                            var attimer = false;

                            Action onchange =
                                delegate
                                {
                                    page.range_x.value = "" + Math.Max(0, (100 * f.Top / (Native.Window.Height - f.Height))).Min(100);

                                    var range_y_old = int.Parse(page.range_y.value);
                                    var range_z_old = int.Parse(page.range_z.value);
                                    var range_z_new =
                                        (int)(100.0 * f.Left / (Native.Window.Width - f.Left)).Max(0).Min(100);

                                    page.range_z.value = "" + range_z_new;

                                    //Console.WriteLine(new { range_z_old, range_z_new });
                                    if (range_z_old == range_z_new)
                                    {
                                        if (attimer)
                                        {
                                            attimer = false;

                                            if (range_y_old != 50)
                                            {
                                                if (range_y_old > 50)
                                                    page.range_y.value = "" + (int)(range_y_old - 1);
                                                else
                                                    page.range_y.value = "" + (int)(range_y_old + 1);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var range_y_new = (Math.Sign(range_z_old - range_z_new) * 4 + range_y_old).Min(100).Max(0);

                                        page.range_y.value = "" + range_y_new;
                                    }


                                    range_onchange();
                                };

                            f.LocationChanged +=
                                delegate
                                {
                                    onchange();
                                };

                            f.SizeChanged +=
                                delegate
                                {
                                    onchange();
                                };

                            new ScriptCoreLib.JavaScript.Runtime.Timer(
                                delegate
                                {
                                    attimer = true;
                                    onchange();
                                }
                            ).StartInterval(1000 / 60);
                        }
                    ).Show();


                };
        }

    }
}
