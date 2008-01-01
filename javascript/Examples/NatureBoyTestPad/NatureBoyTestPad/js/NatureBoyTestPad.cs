using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using System;
using System.Linq;
using System.Collections.Generic;


namespace NatureBoyTestPad.js
{
    [Script, ScriptApplicationEntryPoint]
    public class NatureBoyTestPad
    {
        public NatureBoyTestPad()
        {
            #region tutorial step 2
            #region arena
            var map = new Point(1000, 1000);

            var arena = new ArenaControl();

            arena.Layers.Canvas.style.backgroundColor =
                Color.FromGray(0xc0);
            arena.SetLocation(
                Rectangle.Of(0, 0, Native.Window.Width, Native.Window.Height));

            arena.SetCanvasSize(map);

            arena.Control.AttachToDocument();
            #endregion
            #endregion

            #region tutorial step 3

            Func<DudeAnimationInfo, Point, Dude2> CreateActor =
                (_frames, _coords) =>
                {
                    var actor = new Dude2();

                    actor.Frames = _frames.Frames_Stand;
                    actor.AnimationInfo.Frames_Stand = _frames.Frames_Stand;
                    actor.AnimationInfo.Frames_Walk = _frames.Frames_Walk;
                    actor.Zoom.DynamicZoomFunc = a => 1;
                    actor.Zoom.StaticZoom = 1;
                    actor.SetSize(48, 72);
                    actor.TeleportTo(_coords.X, _coords.Y);
                    actor.Direction = Math.PI * 0.5;
                    actor.Control.AttachTo(arena.Layers.Canvas);

                    return actor;
                };

            var frames = new DudeAnimationInfo
            {
                Frames_Stand = Frames.WolfSoldier,
                Frames_Walk = Frames.WolfSoldier_Walk
            };

            var actors = new List<Dude2>
            {
                CreateActor(frames, new Point(40, Native.Window.Height /2 )),
                CreateActor(frames, new Point(200, Native.Window.Height /2))
            };

            var selection = from i in actors
                            where i.IsSelected
                            select i;

            arena.ApplySelection +=
                (rect, ev) =>
                {
                    foreach (var v in actors)
                        v.IsSelected = rect.Contains(v.CurrentLocation);
                };

            arena.SelectionClick +=
                (p, ev) =>
                {
                    foreach (var v in selection)
                        v.WalkTo(p);
                };


            #endregion

        }


        static NatureBoyTestPad()
        {
            typeof(NatureBoyTestPad).SpawnTo(i => new NatureBoyTestPad());
        }

    }

}
