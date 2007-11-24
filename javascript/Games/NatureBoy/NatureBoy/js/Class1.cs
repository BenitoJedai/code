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


namespace NatureBoy.js
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Lambda;

    using ScriptCoreLib.Shared.Drawing;

    using StyleBuilder = Dictionary<string, System.Action<IStyleSheetRule>>;
    using System;

    [Script, ScriptApplicationEntryPoint]
    public class Class1
    {
        //public const string Alias = "Class1";
        //public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            IStyleSheet.Default.AddRule("*", "cursor: url('assets/NatureBoy/cursor.cur'), auto;", 0);

            IStyleSheet.Default.AddRule("body",
                r =>
                {
                    r.style.backgroundColor = Color.Gray;
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.padding = "0px";
                }
            );

            IStyleSheet.Default.AddRule(".stage",
                r =>
                {
                    r.style.backgroundColor = Color.White;

                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.left = "0px";
                    r.style.top = "0px";
                    r.style.right = "0px";
                    r.style.bottom = "0px";
                    r.style.Opacity = 0.0;
                }
            );



            var bg = new IHTMLImage("assets/NatureBoy/back/IMG_0572.jpg");

            bg.style.position = IStyle.PositionEnum.absolute;
            bg.style.SetLocation(0, 0);
            bg.style.width = "100%";
            bg.attachToDocument();



            var stage = new IHTMLDiv { className = "stage" };

            stage.attachToDocument();


            var dude4 = new Dude { ZoomFunc = y => (y + 300) / (600) };

            dude4.TeleportTo(200, 200);
            dude4.Control.attachToDocument();
            dude4.AutoRotate(1);

            var dude5 = new Dude { ZoomFunc = dude4.ZoomFunc };

            dude5.TeleportTo(600, 300);
            dude5.Control.attachToDocument();
            dude5.AutoRotateToCursor(stage);

            var dude6 = new Dude { ZoomFunc = dude4.ZoomFunc };

            dude6.TeleportTo(300, 400);
            dude6.Control.attachToDocument();

            var dude7 = new Dude { ZoomFunc = dude4.ZoomFunc };

            dude7.TeleportTo(200, 400);
            dude7.Control.attachToDocument();

            var dude8 = new Dude { ZoomFunc = dude4.ZoomFunc };

            dude8.TeleportTo(250, 300);
            dude8.Control.attachToDocument();

            var CurrentDude = default(Dude);

            Action<Dude> SelectDude = i =>
                                          {
                                              if (CurrentDude != null)
                                                  CurrentDude.IsSelected = false;

                                              CurrentDude = i;
                                              CurrentDude.IsSelected = true;
                                          };

            Action<Dude> BindSelectDude =
                i => i.Control.onclick += delegate { SelectDude(i); };

            BindSelectDude(dude6);
            BindSelectDude(dude7);
            BindSelectDude(dude8);


            stage.onclick +=
                delegate(IEvent ev)
                {
                    CurrentDude.WalkTo(ev.CursorPosition);

                    dude5.WalkTo(ev.CursorPosition);
                };

        }




        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1(i));

            

        }


    }

}
