//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
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


    [Script, ScriptApplicationEntryPoint(IsClickOnce=true)]
    public class Class1
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
                    new Qoutes.DocumentRef { Source = "assets/TextScreenSaver/data/Qoutes3.xml" }
                }
            };

        /// <summary>
        /// Creates a new control
        /// </summary>
        public Class1(Qoutes.DocumentList list)
        {
            var type = typeof(Class1);

            var name = type.Name;

            Console.WriteLine("type: " + name);

            IStyleSheet.Default.AddRule("*", "cursor: url('assets/TextScreenSaver/cursor.cur'), auto;", 0);

            IStyleSheet.Default.AddRule("html", 
                r =>
                {
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );

            Action<Qoutes.DocumentRef,Action<Qoutes.Document>> PrepareDocument =
                (doc, done) =>
                {
                    if (doc.Document == null)
                        doc.Source.DownloadToXML<Qoutes.Document>(Qoutes.Settings.KnownTypes, done);
                    else
                        done(doc.Document);
                };

            PrepareDocument(list.Documents.Random(),
                doc =>
                {
                    Native.Document.title = doc.Topic.Trim();

                    var body = Native.Document.body;

                    body.style.overflow = IStyle.OverflowEnum.hidden;
                    body.style.width = "100%";
                    body.style.height = "100%";
                    body.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
                    body.style.backgroundImage = "url(assets/TextScreenSaver/powered_by_jsc.png)";
                    body.style.backgroundRepeat = "no-repeat";


                    //("BackgroundColor: " + doc.Style.BackgroundColor).ToConsole();
                    //("Color: " + doc.Style.Color).ToConsole();

                    doc.Style.ApplyTo(body.style);



                    var lines = doc.Lines();

                    var timer_handler = new Action<Timer>(delegate { });
                    var timer_ref = 100.AsTimer(timer_handler);

                    Action<Action> SpawnVector =
                        done =>
                        {
                            var z = 0.5d.Random() + 0.5d;

                            var v = new IHTMLDiv { innerText = lines.Random() };

                            v.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

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

                }
            );
        }

        static Class1()
        {
            typeof(Class1).SpawnTo<Qoutes.DocumentList>(Qoutes.Settings.KnownTypes, i => new Class1(i));

        }


    }

}
