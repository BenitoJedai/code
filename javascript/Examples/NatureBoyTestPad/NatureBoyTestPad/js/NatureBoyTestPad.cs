using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using ScriptCoreLib.JavaScript.DOM;
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

            var pending = default(Dude2);

            #region arsenal
            var arsenal = new Dictionary<string, DudeAnimationInfo>
            {
                {"Soldier", 
                    new DudeAnimationInfo 
                    { 
                        Frames_Stand = Frames.WolfSoldier,
                        Frames_Walk = Frames.WolfSoldier_Walk
                    }
                },
                {"Imp", 
                    new DudeAnimationInfo 
                    { 
                        Frames_Stand = Frames.DoomImp,
                        Frames_Walk = Frames.DoomImp_Walk
                    }
                }
            };
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

            var actors = new List<Dude2>
            {
                CreateActor(arsenal.Random().Value, new Point(40, Native.Window.Height /2 )),
                CreateActor(arsenal.Random().Value, new Point(200, Native.Window.Height /2))
            };

            var selection = from i in actors
                            where i.IsSelected
                            select i;

            arena.ApplySelection +=
                (rect, ev) =>
                {
                    if (pending != null)
                        return;

                    foreach (var v in actors)
                        v.IsSelected = rect.Contains(v.CurrentLocation);
                };

            arena.SelectionClick +=
                (p, ev) =>
                {
                    if (pending != null)
                        return;

                    foreach (var v in selection)
                        v.WalkTo(p);
                };


            #endregion

            #region tutorial step 4



            #region CreateDialogAt
            Func<Point, string, IHTMLDiv> CreateDialogAt =
                (pos, width) =>
                {
                    var dialog = new IHTMLDiv();

                    dialog.style.SetLocation(pos.X, pos.Y);

                    dialog.style.backgroundColor = Color.Gray;
                    dialog.style.padding = "1px";

                    var caption = new IHTMLDiv().AttachTo(dialog);

                    caption.style.backgroundColor = Color.Blue;
                    caption.style.width = width;
                    caption.style.height = "0.5em";
                    caption.style.cursor = IStyle.CursorEnum.move;

                    var drag = new DragHelper(caption);

                    drag.Enabled = true;
                    drag.DragMove +=
                        delegate
                        {
                            dialog.style.SetLocation(drag.Position.X, drag.Position.Y);
                        };

                    var _content = new IHTMLDiv().AttachTo(dialog);

                    _content.style.textAlign = IStyle.TextAlignEnum.center;
                    _content.style.backgroundColor = Color.White;
                    _content.style.padding = "1px";

                    dialog.AttachToDocument();

                    return _content;
                };
            #endregion

            #region dialog
            var content = CreateDialogAt(new Point(2, 2), "5em");

            var combo = new IHTMLSelect();
            var build = new IHTMLButton();

            build.style.SetSize(72, 72);
            build.style.padding = "0px";

            var avatar = new IHTMLImage().AttachTo(build);
            var remove = new IHTMLButton("Remove");

            combo.AttachTo(content);
            new IHTMLBreak().AttachTo(content);
            build.AttachTo(content);
            new IHTMLBreak().AttachTo(content);
            remove.AttachTo(content);
            #endregion


            #region GetSelectedArsenal
            Func<DudeAnimationInfo> GetSelectedArsenal =
                () =>
                {
                    if (arsenal.ContainsKey(combo.value))
                        return arsenal[combo.value];

                    return null;
                };
            #endregion

            Action Refresh =
                delegate
                {
                    var i = GetSelectedArsenal();

                    if (i != null)
                        avatar.src = i.Images.Random().src;
                };

            combo.Add(arsenal.Keys.ToArray());
            Refresh();

            combo.onchange +=
                delegate
                {
                    Refresh();
                };

            #region pending actor

            arena.MouseMove +=
                p =>
                {
                    if (pending == null)
                        return;

                    pending.TeleportTo(p.X, p.Y);
                };

            arena.SelectionClick +=
                (p, ev) =>
                {
                    if (pending == null)
                        return;

                    pending.TeleportTo(p.X, p.Y);
                    
                    actors.Add(pending);

                    pending.IsHot = false;
                    pending = null;
                };

            build.onclick +=
                delegate
                {
                    if (pending != null)
                    {
                        pending.Control.Dispose();
                        pending = null;

                        return;
                    }

                    var x = GetSelectedArsenal();

                    pending = CreateActor(x,
                        new Point(
                            Native.Window.Width / 2,
                            Native.Window.Height / 2
                            )
                    );

                    pending.IsHot = true;
                };
            #endregion

            remove.onclick +=
                delegate
                {
                    foreach (var v in selection.ToArray())
                    {
                        v.Control.Dispose();
                        actors.Remove(v);
                    }
                };
            
            #endregion
        }


        static NatureBoyTestPad()
        {
            typeof(NatureBoyTestPad).SpawnTo(i => new NatureBoyTestPad());
        }

    }

}
