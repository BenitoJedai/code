//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace ImageZoomer.js
{

    [Script]
    public class XSize
    {
        public int w { get; set; }
        public int h { get; set; }
    }

    [Script]
    public class XMagnifier
    {
        public XSize size { get; set; }
        public double opacity { get; set; }
        public IHTMLDiv z { get; set; }
        public IHTMLImage x { get; set; }
    }

    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            var i = new IHTMLImage("assets/boat.jpg");

            i.style.margin = "1em";

            new IHTMLDiv("A simple image zoomer example").attachToDocument();

            i.attachToDocument();

            var ax = new List<XMagnifier>();
            var zoom = 2.5;

            for (int axi = 1; axi < 6; axi++)
            {
                ax.Add(
                   new XMagnifier
                   {
                       size = new XSize { w = 120 - axi * 4, h = 120 - axi * 4 },
                       opacity = axi / 10
                   }
                );
            }

            var mag1a = ax.ToArray();

            var p = new Point(0, 0);


            EventHandler<IEvent> update =
                delegate(IEvent e)
                {
                    p = new Point(e.CursorX - i.Bounds.Left, e.CursorY - i.Bounds.Top);

                    foreach (var mag1 in mag1a)
                    {
                        mag1.x.style.SetLocation(
                            (int)((p.X * -zoom) + mag1.size.w / 2),
                            (int)((p.Y * -zoom) + mag1.size.h / 2)
                            );

                        mag1.z.SetCenteredLocation(e.CursorPosition);
                    }
                };

            EventHandler<IEvent> onzoom =
                delegate(IEvent e)
                {
                    if (e.WheelDirection == 1)
                    {
                        zoom += 0.15;
                    }
                    else
                    {
                        zoom -= 0.15;

                    }
                    foreach (var mag1 in mag1a)
                    {
                        mag1.x.style.SetSize((int)(i.width * zoom), (int)(i.height * zoom));

                        mag1.x.style.SetLocation(
                            (int)((p.X * -zoom) + mag1.size.w / 2),
                            (int)((p.Y * -zoom) + mag1.size.h / 2)
                            );
                    }

                    Console.WriteLine(new { zoom = zoom }.ToString());
                };

            foreach (var mag1 in mag1a)
            {
                mag1.z = new IHTMLDiv();

                mag1.z.style.SetSize(mag1.size.w, mag1.size.h);
                mag1.z.style.overflow = IStyle.OverflowEnum.hidden;

                mag1.x = new IHTMLImage(i.src);

                mag1.x.style.SetSize((int)(i.width * zoom), (int)(i.height * zoom));
                mag1.z.style.Opacity = mag1.opacity;
                mag1.z.appendChild(mag1.x);
                mag1.z.attachToDocument();

                mag1.z.onmousemove += update;
                mag1.z.onmousewheel += onzoom;
            }







            i.onmousemove += update;

            i.onmousewheel += onzoom;



        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
