﻿//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
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
            IStyleSheet.Default.AddRule("body",
                r =>
                {
                    r.style.backgroundColor = Color.Gray;
                    r.style.backgroundImage = "assets/NatureBoy/back/IMG_0506.jpg".ToCSSImage();
                    r.style.overflow = IStyle.OverflowEnum.hidden;
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

            var stage = new IHTMLDiv { className = "stage" };

            stage.attachToDocument();


            var dude4 = new Dude { ZoomFunc = y => (y + 300) / (600) };




            dude4.TeleportTo(200, 200);

            dude4.Control.attachToDocument();

            //dude4.AutoRotate(0.25);
            dude4.IsWalking = true;

            stage.onclick +=
                delegate(IEvent ev)
                {
                    var a =  ev.CursorPosition.GetAngle(dude4.X, dude4.Y);

                    System.Console.WriteLine(new { a = a}.ToString());


                    //dude4.TeleportTo(ev.CursorX, ev.CursorY);
                };
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
