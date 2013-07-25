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
using AccelerometerServerEvents.Design;
using AccelerometerServerEvents.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace AccelerometerServerEvents
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
            var y = page.ThePath_y;
            var x = page.ThePath_x;
            var z = page.ThePath_z;



            var history =
                new { x = 0.0, y = 0.0, z = 0.0 }.ToEmptyList();


            50.Times(
                delegate
                {
                    history.Add(
                        // vec2?
                       new { x = 0.0, y = 0.0, z = 0.0 }
                   );

                }
            );

            var movementX = 0.0;
            var movementY = 0.0;
            var movementZ = 0.0;

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    history.Add(
                        // vec2?
                        new { x = movementX, y = movementY, z = movementZ }
                            );

                    //movementX = 0;
                    //movementY = 0;
                    //movementZ = 0;

                    if (history.Count > 200)
                    {
                        history.RemoveAt(0);
                    }

                    // http://www.w3.org/TR/SVG/paths.html#PathDataMovetoCommands
                    var xw = new StringBuilder().Append("M10,200 ");
                    var yw = new StringBuilder().Append("M10,300 ");
                    var zw = new StringBuilder().Append("M10,400 ");


                    history.WithEachIndex(
                        (p, i) =>
                        {
                            xw.Append(" L" + (10 + 2 * i) + "," + (p.x + 200));
                            yw.Append(" L" + (10 + 2 * i) + "," + (p.y + 300));
                            zw.Append(" L" + (10 + 2 * i) + "," + (p.z + 400));

                        }
                    );

                    xw.Append(" L" + (10 + 2 * history.Count) + "," + (201));
                    yw.Append(" L" + (10 + 2 * history.Count) + "," + (301));
                    zw.Append(" L" + (10 + 2 * history.Count) + "," + (401));

                    //Console.WriteLine(new { xw, yw });

                    y.d = yw.ToString();
                    x.d = xw.ToString();
                    z.d = zw.ToString();
                }
            ).StartInterval(1000 / 10);


            try
            {
                // this is like a ComponentModel timer where handler can raise events

                // cache will block it..
                // https://code.google.com/p/chromium/issues/detail?id=264170
                new EventSource("/event-stream").onmessage +=
                     e =>
                     {
                         var xml = XElement.Parse((string)e.data);

                         movementX = double.Parse(xml.Attribute("x").Value);
                         movementY = -double.Parse(xml.Attribute("y").Value);
                         movementZ = -double.Parse(xml.Attribute("z").Value);


                         if (movementY < -2)
                         {
                             Native.Document.body.style.backgroundColor = JSColor.Green;
                         }
                         else if (movementY > 33)
                         {
                             Native.Document.body.style.backgroundColor = JSColor.Red;
                         }
                         else if (movementZ < -15)
                         {
                             Native.Document.body.style.backgroundColor = JSColor.Yellow;
                         }
                         else if (movementZ > 15)
                         {
                             Native.Document.body.style.backgroundColor = JSColor.Blue;
                         }
                         else
                             Native.Document.body.style.backgroundColor = JSColor.None;



                         page.Content.Clear();

                         new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.Content);
                     };
            }
            catch
            {
                // not available on built in web browser for android
            }

            Native.Window.ondeviceorientation +=
                eventData =>
                {
                    #region desktop chrome misreports?
                    // Uncaught ReferenceError: alpha is not defined 
                    if ("this.alpha == null".js<bool>(eventData))
                    {
                        Console.WriteLine("ondeviceorientation without alpha? " + eventData);
                        Console.WriteLine("ondeviceorientation without alpha? " + eventData.alpha);
                        Console.WriteLine("ondeviceorientation without alpha? ");
                        return;
                    }
                    #endregion




                    movementX = eventData.alpha;
                    movementY = eventData.beta;
                    movementZ = eventData.gamma;


                    if (movementY < -2)
                    {
                        Native.Document.body.style.backgroundColor = JSColor.Green;
                    }
                    else if (movementY > 33)
                    {
                        Native.Document.body.style.backgroundColor = JSColor.Red;
                    }
                    else if (movementZ < -15)
                    {
                        Native.Document.body.style.backgroundColor = JSColor.Yellow;
                    }
                    else if (movementZ > 15)
                    {
                        Native.Document.body.style.backgroundColor = JSColor.Blue;
                    }
                    else
                        Native.Document.body.style.backgroundColor = JSColor.None;



                    page.Content2.Clear();
                    new IHTMLPre { innerText = new { movementX, movementY, movementZ }.ToString().Replace(",", ",\n") }.AttachTo(page.Content2);
                };
        }

    }

    public static class X
    {
        public static T js<T>(this string body, object that = null)
        {
            return (T)new IFunction("return " + body + ";").apply(that);
        }
    }
}
