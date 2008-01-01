using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using System;


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

            #region actor
            var actor = new Dude2();

            var frames = new DudeAnimationInfo
            {
                Frames_Stand = Frames.WolfSoldier,
                Frames_Walk = Frames.WolfSoldier_Walk
            };

            actor.Frames = frames.Frames_Stand;
            actor.AnimationInfo.Frames_Stand = frames.Frames_Stand;
            actor.AnimationInfo.Frames_Walk = frames.Frames_Walk;
            actor.Zoom.DynamicZoomFunc = a => 1;
            actor.Zoom.StaticZoom = 1;
            actor.SetSize(48, 72);
            actor.TeleportTo(Native.Window.Width / 2, Native.Window.Height / 2);
            actor.Direction = Math.PI * 0.5;
            actor.Control.AttachTo(arena.Layers.Canvas);
            #endregion

            arena.SelectionClick +=
                (coords, ev) =>
                {
                    actor.WalkTo(coords);
                };
            #endregion

        }


        static NatureBoyTestPad()
        {
            typeof(NatureBoyTestPad).SpawnTo(i => new NatureBoyTestPad());
        }

    }

}
