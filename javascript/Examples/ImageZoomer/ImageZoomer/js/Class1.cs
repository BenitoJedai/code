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
using IDisposable = global::System.IDisposable;



namespace ImageZoomer.js
{

    [Script]
    public class XSize
    {
        public int w { get; set; }
        public int h { get; set; }
    }

    [Script]
    public class XMagnifierInfo
    {
        public XSize size { get; set; }
        public double opacity { get; set; }
        public IHTMLDiv z { get; set; }
        public IHTMLImage x { get; set; }
    }

    [Script]
    public class MyMagnifier : IDisposable
    {
        /// <summary>
        /// user clicks on the magnifier
        /// </summary>
        public event Action<IEvent> Click;

        /// <summary>
        /// magnifier should be moved to the location given by the event
        /// </summary>
        public readonly EventHandler<IEvent> UpdateTo;

        public MyMagnifier(IHTMLImage i, int size, int frames, int framestep)
        {
            var ax = new List<XMagnifierInfo>();
            var zoom = 2.5;

            for (int axi = 1; axi < frames; axi++)
            {
                ax.Add(
                   new XMagnifierInfo
                   {
                       size = new XSize { w = size - axi * framestep, h = size - axi * framestep },
                       opacity = axi / (frames - 1)
                   }
                );
            }

            var mag1a = ax.ToArray();

            var p = new Point(0, 0);


            UpdateTo =
                delegate(IEvent e)
                {
                    if (mag1a == null)
                        return;

                    p = new Point(e.CursorX - i.Bounds.Left, e.CursorY - i.Bounds.Top);

                    var vis = new[] { p.X, p.Y, i.width - p.X, i.height - p.Y }.Min();


                    var vis_margin = (size / 2) / zoom;
                    int vis_length = size;

                    foreach (var mag1 in mag1a)
                    {
                        if (vis < vis_margin)
                        {
                            mag1.z.style.display = IStyle.DisplayEnum.none;
                        }
                        else
                        {
                            mag1.z.style.display = IStyle.DisplayEnum.block;
                            if (vis > vis_length + vis_margin)
                                mag1.z.style.Opacity = mag1.opacity;
                            else
                                mag1.z.style.Opacity = mag1.opacity * (vis - vis_margin) / vis_length;
                        }

                        
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
                    e.PreventDefault();

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
                mag1.z.style.display = IStyle.DisplayEnum.none;

                mag1.z.style.SetSize(mag1.size.w, mag1.size.h);
                mag1.z.style.overflow = IStyle.OverflowEnum.hidden;

                mag1.x = new IHTMLImage(i.src);

                mag1.x.style.SetSize((int)(i.width * zoom), (int)(i.height * zoom));
                mag1.z.style.Opacity = mag1.opacity;
                mag1.z.appendChild(mag1.x);
                mag1.z.attachToDocument();

                mag1.z.onmousemove += UpdateTo;
                mag1.z.onmousewheel += onzoom;
                mag1.z.onclick += (ev) => this.Click(ev);



            }




            this.OnDispose +=
                delegate
                {
                    this.OnDispose = null;

                    foreach (var mag1 in mag1a)
                    {
                        mag1.x.Dispose();
                        mag1.x = null;

                        mag1.z.Dispose();
                        mag1.z = null;
                    }

                    mag1a = null;
                };


            i.onmousemove += UpdateTo;

            //i.onmousewheel += onzoom;
        }

        #region IDisposable Members

        Action OnDispose;

        public void Dispose()
        {
            if (OnDispose != null)
                OnDispose();

        }

        #endregion

        [Script(NoDecoration=true)]
        public static Func<MyMagnifier> CreateClickableMagnifier(IHTMLImage i)
        {
            var x = default(MyMagnifier);

            Action create =
                () =>
                {
                    x = new MyMagnifier(i, 180, 8, 5);

                    x.Click +=
                         delegate
                         {
                             if (x != null)
                             {
                                 x.Dispose();
                                 x = null;
                             }
                         };
                };


            create();

            i.onclick +=
                (ev) =>
                {
                    if (x == null)
                    {
                        create();
                        x.UpdateTo(ev);
                    }
                };

            return () => x;
        }
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
            var Control = new IHTMLElement(IHTMLElement.HTMLElementEnum.center);

            DataElement.insertNextSibling(Control);

            Control.appendChild( new IHTMLDiv("A simple image zoomer example") );
            Control.appendChild(new IHTMLAnchor("http://valid.tjp.hu/tjpzoom/", "based on tjpZoom"));
            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.hr));

            new[] {
                "assets/boat.jpg",
                "assets/boat2.jpg",
                "assets/tea.jpg",
                "assets/town.jpg",
            }.ForEach(
            src =>
            {
                var i = new IHTMLImage(src);

                i.style.margin = "2px";

                Control.appendChild(i);

                MyMagnifier.CreateClickableMagnifier(i);
            }
            );
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
