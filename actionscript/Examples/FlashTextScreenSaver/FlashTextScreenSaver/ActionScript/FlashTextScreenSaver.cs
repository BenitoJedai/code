using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.utils;
using System.Collections.Generic;

using FlashTextScreenSaver.ActionScript.Qoutes.Extensions;

namespace FlashTextScreenSaver.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashTextScreenSaver : Sprite
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

        public FlashTextScreenSaver()
            : this(null)
        {
        }

      /// <summary>
        /// Creates a new control
        /// </summary>
        public FlashTextScreenSaver(Qoutes.DocumentList list)
        {
            if (list == null)
                list = DefaultData;

            var type = typeof(FlashTextScreenSaver);

            var name = type.Name;

            Console.WriteLine("type: " + name);

            //try
            //{
            //    IStyleSheet.Default.AddRule("*", "cursor: url('assets/TextScreenSaver/images/cursor.cur'), auto;", 0);

            //    IStyleSheet.Default.AddRule("html",
            //        r =>
            //        {
            //            r.style.overflow = IStyle.OverflowEnum.hidden;
            //        }
            //    );
            //}
            //catch (Exception exc)
            //{
            //    new IHTMLElement(IHTMLElement.HTMLElementEnum.div, "error: " + exc.Message.Replace(",", ", ")).AttachToDocument().style.width = "80em";
            //}

            Action<Qoutes.DocumentRef, Action<Qoutes.Document>> PrepareDocument =
                (doc, done) =>
                {
                    if (doc.Document == null)
                    {
                        //Native.Document.title = "loading...";

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
            //var kbd = new KeyboardEvents();

            var reset = default(Action);

            //kbd.left +=
            //    ev =>
            //    {
            //        kbd.Enabled = false;
            //        ev.PreventDefault();

            //        if (abort != null)
            //            abort();
            //        current = list.Documents.Previous(i => i == current);
            //        reset();
            //    };

            //kbd.right +=
            //    ev =>
            //    {
            //        kbd.Enabled = false;
            //        ev.PreventDefault();

            //        if (abort != null)
            //            abort();

            //        current = list.Documents.Next(i => i == current);
            //        reset();
            //    };

            //Native.Document.onkeydown += kbd;


            reset =
                () =>
                PrepareDocument(current,
                doc =>
                {
                    //Native.Document.title = doc.Topic.Trim();

                    //var body = Native.Document.body;

                    //body.style.overflow = IStyle.OverflowEnum.hidden;
                    //body.style.width = "100%";
                    //body.style.height = "100%";
                    //body.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
                    ////body.style.backgroundImage = "url(assets/TextScreenSaver/powered_by_jsc.png)";
                    //body.style.backgroundRepeat = "no-repeat";


                    ////("BackgroundColor: " + doc.Style.BackgroundColor).ToConsole();
                    ////("Color: " + doc.Style.Color).ToConsole();

                    //doc.Style.ApplyTo(body.style);



                    var lines = doc.Lines();

                    var timer_handler = new Action<Timer>(delegate { });
                    var timer_ref = 100.AtInterval(timer_handler);

                    var vectors = new List<TextField>();

                    var abort_me = default(Action);

                    abort_me =
                        delegate
                        {
                            abort -= abort_me;

                            Console.WriteLine("aborting...");

                            timer_ref.stop();

                            vectors.ForEach(v => v.Orphanize());

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

                            var v = new TextField { text = lines.Random() };

                            // v.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

                            vectors.Add(v);

                            Action ApplyZ =
                                () =>
                                {
                                    //v.style.fontSize = (z * 3) + "em";
                                    //v.style.Opacity = z;
                                    //v.style.zIndex = (z * 1000).Floor();
                                };


                            ApplyZ();

                            var x = 100d;
                            var y = 80.Random();

                            // v.style.position = IStyle.PositionEnum.absolute;

                            Action UpdatePosition =
                                () =>
                                {
                                    v.x = stage.stageWidth * x;
                                    v.y = stage.stageWidth * x;

                                    //v.style.left = x + "%";
                                    //v.style.top = y + "%";
                                };

                            UpdatePosition();

                            v.AttachTo(this);


                            var handler = default(Action<Timer>);


                            Action DisposeThisVector =
                                delegate
                                {
                                    timer_handler -= handler;
                                    v.FadeOutAndOrphanize();

                                    done();
                                };

                            v.doubleClick +=
                                ev =>
                                {

                                    DisposeThisVector();
                                };

                            var IsHover = false;

                            v.mouseDown +=
                                ev =>
                                {
                                    //ev.PreventDefault();
                                };

                            v.mouseOver +=
                                delegate
                                {
                                    
                                    //v.style.color = doc.Style.HoverColor;
                                    IsHover = true;
                                };

                            v.mouseOut +=
                                delegate
                                {
                                    //v.style.color = Color.None;
                                    IsHover = false;
                                };

                            v.mouseWheelEnabled = true;
                            v.mouseWheel +=
                                ev =>
                                {
                                    z = (z + 0.02 * ev.WheelDirection()).Max(0.5).Min(1.0);

                                    ApplyZ();
                                };

                            //var drag = new DragHelper(v);

                            //drag.Enabled = true;
                            //drag.DragMove +=
                            //    delegate
                            //    {
                            //        var w = Native.Window.Width;
                            //        var h = Native.Window.Height;


                            //        x = (drag.Position.X * 100 / w);
                            //        y = (drag.Position.Y * 100 / h);

                            //        UpdatePosition();

                            //        // v.style.SetLocation(drag.Position.X, drag.Position.Y);

                            //    };

                            handler =
                                timer =>
                                {
                                    //if (drag.IsDrag)
                                    //    return;

                                    if (IsHover)
                                        return;


                                    x -= 0.4 * z;

                                    UpdatePosition();

                                    //drag.Position = new Point(v.offsetLeft, v.offsetTop);

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
                            max.Random().ToInt32().AtDelay(
                                delegate
                                {
                                    h();

                                    counter--;

                                    if (counter > 0)
                                        SpawnRandom(counter, max, h);
                                }
                            );
                        };


                    SpawnRandom(doc.Count.ToInt32(), 3000, SpawnNextVector);

                    //kbd.Enabled = true;
                }
            );

            reset();
        }


        


    
    }
}