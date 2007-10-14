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
            IStyleSheet.Default.AddRule(".cursorred", "cursor: url('assets/NatureBoy/cursor-red.cur'), auto;", 0);
            IStyleSheet.Default.AddRule("body", "cursor: url('assets/NatureBoy/cursor.cur'), auto;", 0);

            IStyleSheet.Default.AddRule(".loading",
    r =>
    {

        r.style.backgroundColor = Color.Black;
        r.style.color = Color.Yellow;
        r.style.padding = "2em";
        r.style.margin = "2em";
    }
);

            IStyleSheet.Default.AddRule("body",
                r =>
                {

                    r.style.backgroundColor = Color.Gray;
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.padding = "0px";
                }
            );


            @"Ctrl+Click on the background to spawn new dudes.
You can select them and order them to move.
<h3>Mousewheel</h3>Use your mouse wheel to resize them. 
You can size them all at once or each invidually.
".AttachToDocument();


            /*
            var bg = new IHTMLImage("assets/NatureBoy/back/IMG_0572.jpg");

            bg.style.position = IStyle.PositionEnum.absolute;
            bg.style.SetLocation(0, 0);
            bg.style.width = "100%";
            bg.attachToDocument();*/

            var loading = "Loading...".AttachToDocument();

            loading.className = "loading";

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    var c = 0;

                    foreach (var i in Frames.AllFrames)
                    {
                        foreach (var j in i)
                        {
                            if (!j.Image.complete)
                                c++;
                        }
                    }

                    if (c == 0)
                    {
                        t.Stop();
                        loading.FadeOut();
                        BuildStage();

                        return;
                    }
                    loading.innerHTML = "Loading... " + c + " images";


                }, 0, 100);


        }

        private static void BuildStage()
        {
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

            var Selection = default(Dude2);

            Action<Dude2> SelectDude = i =>
            {
                if (Selection != null)
                    Selection.IsSelected = false;

                Selection = i;

                if (Selection != null)
                    Selection.IsSelected = true;
            };

            Action<Dude2> BindSelectDude =
                i => i.Control.onclick += delegate { SelectDude(i); };

            Func<FrameInfo[], int, int, Dude2> SpawnLookingDude =
            (f, x, y) =>
            {
                var r = new Dude2
                {
                    Frames = f,
                };

                //BindSelectDude(r);

                r.AnimationInfo.Frames_Stand = f;

                r.Zoom.DynamicZoomFunc = a => (a + 400) / (800);
                r.Zoom.StaticZoom = 1;

                r.SetSize(48, 72);
                r.TeleportTo(x, y);

                r.Control.className = "cursorred";

                r.Control.attachToDocument();

                r.Direction = Math.PI.Random() * 2;
                /*
                var look = r.LookAtMouse(stage);

                r.Control.onclick +=
                    (ev) =>
                    {
                        if (look == null)
                        {
                            look = r.LookAtMouse(stage);

                            return;
                        }

                        look();

                        look = null;
                    };*/

                r.Control.onmousewheel +=
                    (ev) =>
                    {
                        WheelAction(r, ev);
                    };

                list.Add(r);

                return r;
            };

            Func<int> r400 = () => 400.Random() + 100;


            SpawnLookingDude(Frames.PigCop, 300, 300).LookAtMouse(stage);
            SpawnLookingDude(Frames.Trooper, 500, 300).LookAtMouse(stage);


            var runner = SpawnLookingDude(Frames.Duke, r400(), r400());

            runner.AnimationInfo.Frames_Walk = Frames.Duke_Walk;
            runner.WalkTo(new Point(r400(), r400()));
            BindSelectDude(runner);

            stage.onmousemove +=
                ev =>
                {
                    if (Selection != null)
                        if (!Selection.IsWalking)
                            Selection.LookAt(ev.CursorPosition);
                    /*
                    foreach (var v in list.Where(i => i.AnimationInfo.Frames_Stand == Frames.Duke))
                        v.WalkTo(ev.CursorPosition);
                    */
                };



            stage.onclick +=
                (ev) =>
                {
                    if (ev.ctrlKey)
                    {
                        var n = SpawnLookingDude(Frames.Duke, ev.CursorX, ev.CursorY);

                        n.AnimationInfo.Frames_Walk = Frames.Duke_Walk;

                        BindSelectDude(n);
                        SelectDude(n);

                        return;
                    }

                    if (Selection != null)
                        Selection.WalkTo(ev.CursorPosition);

                    //SelectDude(null);

                    /*
                    var n = SpawnLookingDude(Frames.Duke, ev.CursorX, ev.CursorY);

                    n.AnimationInfo.Frames_Walk = Frames.Duke_Walk;*/
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
