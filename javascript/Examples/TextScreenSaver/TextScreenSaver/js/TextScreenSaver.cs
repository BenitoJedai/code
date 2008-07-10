//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using global::System.Linq;
using global::ScriptCoreLib.Shared.Lambda;




namespace TextScreenSaver.js
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.Shared.Drawing;

    using Qoutes.Extensions;
    using System;
    using ScriptCoreLib.JavaScript.Controls;


    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class TextScreenSaver
    {
        public static readonly Qoutes.DocumentList DefaultData =
            new Qoutes.DocumentList
            {
                Documents = new[]
                {
                    //new Qoutes.DocumentRef {
                    //    Document = new Qoutes.Document
                    //    {
                    //        Topic = "Debug",
                    //        Count = "10",
                    //        Style = new TextScreenSaver.js.Qoutes.Style
                    //        {
                    //            BackgroundColor = "black",
                    //            Color = "white",
                    //            HoverColor = "red"
                    //        },
                    //        Content = "Hello world1\nHello world2"
                    //    }
                    //}
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes.xml" },
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes2.xml" },
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes3.xml" },
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes4.xml" },
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes5.xml" },
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes6.xml" },
                }
            };

        public TextScreenSaver()
            : this(null)
        {
        }

        /// <summary>
        /// Creates a new control
        /// </summary>
        public TextScreenSaver(Qoutes.DocumentList list)
        {
            if (list == null)
                list = DefaultData;

            var type = typeof(TextScreenSaver);

            var name = type.Name;

            Console.WriteLine("type: " + name);

            try
            {
                IStyleSheet.Default.AddRule("*", "cursor: url('assets/TextScreenSaver/images/cursor.cur'), auto;", 0);

                IStyleSheet.Default.AddRule("html",
                    r =>
                    {
                        r.style.overflow = IStyle.OverflowEnum.hidden;
                    }
                );
            }
            catch (Exception exc)
            {
                new IHTMLElement(IHTMLElement.HTMLElementEnum.div, "error: " + exc.Message.Replace(",", ", ")).AttachToDocument().style.width = "80em";
            }

            Action<Qoutes.DocumentRef, Action<Qoutes.Document>> PrepareDocument =
                (doc, done) =>
                {
                    if (doc.Document == null)
                    {
                        Native.Document.title = "loading...";

                        //Console.WriteLine("loading: " + doc.Source);

                        doc.Source.DownloadToXML<Qoutes.Document>(Qoutes.Settings.KnownTypes,
                            newdoc =>
                            {

                                doc.Document = newdoc;
                                done(newdoc);
                            }
                        );
                    }
                    else
                        done(doc.Document);
                };


            var current = list.Documents.Random();

            var abort = default(Action);
            var kbd = new KeyboardEvents();

            var reset = default(Action);

            kbd.left +=
                ev =>
                {
                    kbd.Enabled = false;
                    ev.PreventDefault();

                    if (abort != null)
                        abort();
                    current = list.Documents.Previous(i => i == current);
                    reset();
                };

            kbd.right +=
                ev =>
                {
                    kbd.Enabled = false;
                    ev.PreventDefault();

                    if (abort != null)
                        abort();

                    current = list.Documents.Next(i => i == current);
                    reset();
                };

            Native.Document.onkeydown += kbd;


            reset =
                () =>
                PrepareDocument(current,
                doc =>
                {
                    Native.Document.title = doc.Topic.Trim();

                    var body = Native.Document.body;

                    body.style.overflow = IStyle.OverflowEnum.hidden;
                    body.style.width = "100%";
                    body.style.height = "100%";
                    body.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
                    //body.style.backgroundImage = "url(assets/TextScreenSaver/powered_by_jsc.png)";
                    body.style.backgroundRepeat = "no-repeat";


                    //("BackgroundColor: " + doc.Style.BackgroundColor).ToConsole();
                    //("Color: " + doc.Style.Color).ToConsole();

                    doc.Style.ApplyTo(body.style);



                    var lines = doc.Lines();

                    var timer_handler = new Action<Timer>(delegate { });
                    var timer_ref = 100.AsTimer(timer_handler);

                    var vectors = new List<IHTMLDiv>();

                    var abort_me = default(Action);

                    abort_me =
                        delegate
                        {
                            abort -= abort_me;

                            Console.WriteLine("aborting...");

                            timer_ref.Stop();

                            vectors.ForEach(v => v.Dispose());

                            abort_me = null;
                        };

                    abort += abort_me;

                    Action<Action> SpawnVector =
                        done =>
                        {
                            // we have been aborted
                            if (abort_me == null)
                                return;

                            var z = 0.5d.Random() + 0.5d;

                            var v = new IHTMLDiv { innerText = lines.Random() };

                            v.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

                            vectors.Add(v);

                            Action ApplyZ =
                                () =>
                                {
                                    v.style.fontSize = (z * 3) + "em";
                                    v.style.Opacity = z;
                                    v.style.zIndex = (z * 1000).Floor();
                                };


                            ApplyZ();

                            var x = 100d;
                            var y = 80.Random();

                            v.style.position = IStyle.PositionEnum.absolute;

                            Action UpdatePosition =
                                () =>
                                {
                                    v.style.left = x + "%";
                                    v.style.top = y + "%";
                                };

                            UpdatePosition();

                            v.AttachTo(body);


                            var handler = default(Action<Timer>);


                            Action DisposeThisVector =
                                delegate
                                {
                                    timer_handler -= handler;
                                    v.FadeOut();

                                    done();
                                };

                            v.ondblclick +=
                                ev =>
                                {

                                    DisposeThisVector();
                                };

                            var IsHover = false;

                            v.onmousedown +=
                                ev =>
                                {
                                    ev.PreventDefault();
                                };

                            v.onmouseover +=
                                delegate
                                {
                                    v.style.color = doc.Style.HoverColor;
                                    IsHover = true;
                                };

                            v.onmouseout +=
                                delegate
                                {
                                    v.style.color = Color.None;
                                    IsHover = false;
                                };

                            v.onmousewheel +=
                                ev =>
                                {
                                    z = (z + 0.02 * ev.WheelDirection).Max(0.5).Min(1.0);

                                    ApplyZ();
                                };

                            var drag = new DragHelper(v);

                            drag.Enabled = true;
                            drag.DragMove +=
                                delegate
                                {
                                    var w = Native.Window.Width;
                                    var h = Native.Window.Height;


                                    x = (drag.Position.X * 100 / w);
                                    y = (drag.Position.Y * 100 / h);

                                    UpdatePosition();

                                    // v.style.SetLocation(drag.Position.X, drag.Position.Y);

                                };

                            handler =
                                timer =>
                                {
                                    if (drag.IsDrag)
                                        return;

                                    if (IsHover)
                                        return;


                                    x -= 0.4 * z;

                                    UpdatePosition();

                                    drag.Position = new Point(v.offsetLeft, v.offsetTop);

                                    if (v.GetOffsetRight() < 0)
                                    {
                                        DisposeThisVector();
                                    }
                                };

                            timer_handler += handler;
                        };

                    var SpawnNextVector = SpawnVector.AsCyclic();


                    var SpawnRandom = default(Action<int, int, Action>);

                    SpawnRandom =
                        (counter, max, h) =>
                        {
                            max.Random().Delayed(
                                () =>
                                {
                                    h();

                                    counter--;

                                    if (counter > 0)
                                        SpawnRandom(counter, max, h);
                                }
                            );
                        };


                    SpawnRandom(doc.Count.ToInt32(), 3000, SpawnNextVector);

                    kbd.Enabled = true;
                }
            );

            reset();
        }


        static TextScreenSaver()
        {
            typeof(TextScreenSaver).SpawnTo(Qoutes.Settings.KnownTypes, (Qoutes.DocumentList i) => new TextScreenSaver(i));

        }


    }

    [Script]
    class KeyboardEvents
    {
        public bool Enabled { get; set; }

        void Dispatcher(IEvent evx)
        {
            if (!Enabled)
                return;

            if (Table == null)
                Table = new Dictionary<int, Action<IEvent>>
                            {
                                { 39, ev => right(ev) },
                                { 40, ev => down(ev) },
                                { 37, ev => left(ev) },
                                { 38, ev => up(ev) },
                            };

            if (Table.ContainsKey(evx.KeyCode))
                Table[evx.KeyCode](evx);

        }

        Dictionary<int, Action<IEvent>> Table;

        public event ScriptCoreLib.Shared.EventHandler<IEvent> left;
        public event ScriptCoreLib.Shared.EventHandler<IEvent> right;
        public event ScriptCoreLib.Shared.EventHandler<IEvent> up;
        public event ScriptCoreLib.Shared.EventHandler<IEvent> down;

        public static implicit operator ScriptCoreLib.Shared.EventHandler<IEvent>(KeyboardEvents e)
        {
            return e.Dispatcher;
        }
    }
}
