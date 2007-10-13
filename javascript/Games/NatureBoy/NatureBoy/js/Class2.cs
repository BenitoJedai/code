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

            
@"Click on the background to spawn new dudes.
They will look at the mouse and do nothing :)
<h3>Mousewheel</h3>Use your mouse wheel to resize them. 
You can size them all at once or each invidually.
".AttachToDocument();


            /*
            var bg = new IHTMLImage("assets/NatureBoy/back/IMG_0572.jpg");

            bg.style.position = IStyle.PositionEnum.absolute;
            bg.style.SetLocation(0, 0);
            bg.style.width = "100%";
            bg.attachToDocument();*/

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



            Action<Dude2, IEvent> WheelAction =
                (d, ev) => 
                    d.Zoom.StaticZoom += ev.WheelDirection * 0.2;

            var list = new List<Dude2>();


            Func<FrameInfo[], int, int, Dude2> SpawnLookingDude =
            (f, x, y) =>
            {
                var r = new Dude2
                {
                    Frames = f,
                };

                r.Zoom.DynamicZoomFunc = a => (a + 300) / (600);
                r.Zoom.StaticZoom = 1;

                r.SetSize(48, 72);
                r.TeleportTo(x, y);

                r.Control.attachToDocument();
                r.Direction = 5;
                r.LookAtMouse(stage);

                r.Control.onmousewheel +=
                    (ev) =>
                        {
                            WheelAction(r, ev);
                        };

                list.Add(r);

                return r;
            };


            SpawnLookingDude(Frames.PigCop, 300, 300);
            SpawnLookingDude(Frames.Trooper, 500, 300);
            SpawnLookingDude(Frames.Duke, 400, 300);

            stage.onclick +=
                (ev) =>
                {
                    SpawnLookingDude(Frames.Duke, ev.CursorX, ev.CursorY);
                };

            stage.onmousewheel +=
                (ev) =>
                {
                    foreach (var v in list)
                    {
                        WheelAction(v, ev);
                    }
                };

        }

        static Class2()
        {
            ScriptCoreLib.JavaScript.Native.Spawn(
                Alias, e => new Class2(e)
                );
        }
    }
}
