using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoy.js
{
    [Script]
    class Class2 //: Class1
    {
        public const string Alias = "Class2";

        public Class2(IHTMLElement DataElement)
        //    : base(DataElement)
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

                        var bg = new IHTMLImage("assets/NatureBoy/back/IMG_0572.jpg");

            bg.style.position = IStyle.PositionEnum.absolute;
            bg.style.SetLocation(0, 0);
            bg.style.width = "100%";
            bg.attachToDocument();

            var stage = new IHTMLDiv { className = "stage" };

            stage.attachToDocument();

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



            var stand = "assets/NatureBoy/dude2/stand/";

            var frames = new[] {
                new FrameInfo { Image = stand + "TILE1406h.png",  Weight = 1d / 6 } ,
                new FrameInfo { Image = stand + "TILE1405.png",  Weight = 1d / 6 } ,
                new FrameInfo { Image = stand + "TILE1406.png",  Weight = 1d / 6 } ,
                new FrameInfo { Image = stand + "TILE1407.png",  Weight = 1d / 10 } ,
                new FrameInfo { Image = stand + "TILE1408.png",  Weight = 1d / 10 } ,
                new FrameInfo { Image = stand + "TILE1409.png",  Weight = 1d / 10 } ,
                new FrameInfo { Image = stand + "TILE1408h.png",  Weight = 1d / 10 } ,
                new FrameInfo { Image = stand + "TILE1407h.png",  Weight = 1d / 10 } 
            };



            var dude1 = new Dude2
            {
                Frames = frames,
                
            };

            dude1.Zoom.DynamicZoomFunc = y => (y + 300) / (600);
            dude1.Zoom.StaticZoom = 1;

            dude1.SetSize(48, 72);
            dude1.TeleportTo(300, 300);
            
            dude1.Control.attachToDocument();

            dude1.Direction = 5;

            stage.onmousemove +=
                delegate(IEvent ev)
                {
                    dude1.LookAt(ev.CursorPosition);
                };

            /* 
            new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    dude1.SetCurrentFrame(frames[t.Counter % frames.Length]);
                }
                ,0, 300
            );
            */
        }

        static Class2()
        {
            ScriptCoreLib.JavaScript.Native.Spawn(
                Alias, e => new Class2(e)
                );
        }
    }
}
