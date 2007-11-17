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


    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        public Class1()
        {
            IStyleSheet.Default.AddRule("body", "cursor: url('assets/TextScreenSaver/cursor.cur'), auto;", 0);

            new [] {
            "assets/TextScreenSaver/data/Qoutes.xml",
            "assets/TextScreenSaver/data/Qoutes2.xml",
            "assets/TextScreenSaver/data/Qoutes3.xml"
            }.Random().DownloadToXML<Qoutes.Document>(Qoutes.Settings.KnownTypes,
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
                            v.style.fontSize = (z * 3) + "em";
                            v.style.Opacity = z;
                            v.style.zIndex = (z * 1000).Floor();


                            var x = 100d;

                            v.style.position = IStyle.PositionEnum.absolute;
                            v.style.left = x + "%";
                            v.style.top = 80.Random() + "%";
                            v.AttachTo(body);

                            var handler = default(Action<Timer>);


                            Action DisposeThisVector =
                                delegate
                                {
                                    timer_handler -= handler;
                                    v.Dispose();

                                    done();
                                };

                            v.onclick +=
                                ev =>
                                {
                                    if (ev.ctrlKey)
                                    {
                                        DisposeThisVector();
                                    }
                                };

                            var IsHover = false;

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

                            handler =
                                timer =>
                                {
                                    if (IsHover)
                                        x -= 0.2 * z;
                                    else
                                        x -= 0.4 * z;

                                    v.style.left = x + "%";

                                    if (x < -200)
                                    {
                                        DisposeThisVector();
                                    }
                                };

                            timer_handler += handler;
                        };

                    var SpawnNextVector = SpawnVector.AsCyclic();

                    
                    var SpawnRandom = default(Action<int, int, Action> );

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

                    
                    SpawnRandom(4, 3000, SpawnNextVector);

                }
            );
        }

        static Class1()
        {
            Alias.SpawnTo(i => new Class1());

        }


    }

}
