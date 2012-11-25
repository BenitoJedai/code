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

namespace JellyworldExperiment.DualView
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
            var IsRightScreen = Native.Document.location.hash == "#/RightScreen";
            var IsLeftScreen = Native.Document.location.hash == "#/LeftScreen";

            if (IsRightScreen || IsLeftScreen)
            {
                Native.Document.body.Clear();

                var a = new CSSTransform3DFPSBlueprint.HTML.Pages.App();

                a.Container.AttachToDocument();


                var hud = new IHTMLDiv().AttachToDocument();

                hud.style.position = IStyle.PositionEnum.absolute;
                hud.style.left = "0px";
                hud.style.top = "0px";
                hud.style.right = "0px";
                hud.style.height = "2em";
                hud.style.zIndex = 1000;
                hud.style.backgroundColor = "rgba(0, 0, 0, 0.5)";
                hud.style.color = JSColor.White;

                var c = new IHTMLCenter { innerText = "RightScreen" }.AttachTo(hud);

                new CSSTransform3DFPSBlueprint.Application().Initialize(a,
                   x =>
                   {
                       var w = CSSTransform3DFPSBlueprint.Application.window;

                       w.viewport.node.style.width = "200%";

                       if (IsRightScreen)
                           CSSTransform3DFPSBlueprint.Application.window.viewport.node.style.marginLeft = "-100%";

                       var du = true;
                       var oldvalue = 0;
                       var dx = 0;
                       var newvalue = 0;

                       Native.Window.onmessage +=
                            e =>
                            {
                                var data = "" + e.data;

                                if (data.StartsWith("value: "))
                                {
                                    newvalue = int.Parse(data.SkipUntilOrEmpty("value: "));

                                    if (du)
                                    {
                                        Console.WriteLine("du false");
                                        oldvalue = newvalue;
                                        du = false;
                                    }
                                    else
                                    {
                                        if (newvalue == oldvalue)
                                        {
                                            // skip it?
                                        }
                                        else
                                        {
                                            dx = newvalue - oldvalue;
                                            oldvalue = newvalue;

                                            // tilt?
                                            //w.viewport.camera.rotation.y -= dx;

                                            // scale down delta
                                            w.viewport.camera.rotation.z -= dx * 0.5;

                                        }
                                    }



                                }

                                c.innerText = new { data, dx, newvalue, oldvalue }.ToString();


                                //oldvalue = newvalue;
                                //w.viewport.camera.rotation.x -= e.movementY / 2;

                            };
                   }
               );




                return;
            }

            page._LeftScreen.onclick +=
                delegate
                {
                    page._LeftScreen.disabled = true;

                    var w = Native.Window.open(
                        "#/LeftScreen",
                        "_blank",
                        400,
                        300,
                        false
                    );

                    w.focus();

                    w.onload +=
                        delegate
                        {
                            w.postMessage("hi from " + Native.Document.location.hash);

                            page.range.onchange +=
                                delegate
                                {
                                    w.postMessage("value: " + page.range.value);

                                };
                        };
                };

            page._RightScreen.onclick +=
               delegate
               {
                   page._RightScreen.disabled = true;


                   var w = Native.Window.open(
                       "#/RightScreen",
                       "_blank",
                       400,
                       300,
                       false
                   );

                   w.focus();

                   w.onload +=
                       delegate
                       {
                           w.postMessage("hi from " + Native.Document.location.hash);

                           page.range.onchange +=
                               delegate
                               {
                                   w.postMessage("value: " + page.range.value);

                               };
                       };
               };
        }

    }
}
