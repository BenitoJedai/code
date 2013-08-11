using NatureBoyTestPadExperiment.HTML.Audio.FromAssets;
using NatureBoyTestPadExperiment.HTML.Images.FromAssets;
using NatureBoyTestPadExperiment.HTML.Pages;
using ScriptCoreLib;
//using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.LayeredControl;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Abstractatech.JavaScript.FormAsPopup;


namespace NatureBoyTestPad.js
{

    public class NatureBoyTestPad
    {
        public static string Title =
            "NatureBoyTestPad. Middle click to pan around. Drag to select actors and then click to order them move. Use toolbar to build new actors.";

        public static double DefaultActiorZoom = 1.0;

        public NatureBoyTestPad()
        {

        }

        public static bool FilterToImpAndSoldier;

        public static void InitializeContent()
        {
            #region tutorial step 2
            #region arena
            var map = new Point(2000, 2000);

            var arena = new ArenaControl();

            arena.Layers.Canvas.style.backgroundColor =
                Color.FromGray(0xc0);
            arena.SetLocation(
                Rectangle.Of(0, 0, Native.Window.Width, Native.Window.Height));

            arena.SetCanvasSize(map);

            arena.Control.AttachToDocument();


            arena.DrawTextToInfo(Title, new Point(8, 8), Color.Blue);

            Native.Window.onresize +=
                delegate
                {
                    arena.SetLocation(
                        Rectangle.Of(0, 0, Native.Window.Width, Native.Window.Height));

                    arena.SetCanvasPosition(
                        arena.CurrentCanvasPosition
                        );
                };
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
                    actor.SetSize(48, 72);
                    actor.TeleportTo(_coords.X, _coords.Y);
                    actor.Zoom.StaticZoom = DefaultActiorZoom;
                    actor.Direction = Math.PI * 0.5;
                    actor.Control.AttachTo(arena.Layers.Canvas);
                    //actor.HasShadow = _frames.Frames_Stand.Length > 1;
                    if (_frames.Frames_Stand.Length == 1)
                        actor.Shadow.style.Opacity = 0.4;
                    actor.AnimationInfo.WalkAnimationInterval = 1000 / 30;
                    return actor;
                };

            var actors = new List<Dude2>
            {

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
                        v.IsSelected = rect.Contains(v.CurrentLocation.ToInt32());
                };

            var Argh = new Argh();
            var Affirmative = new Affirmative();
            var ghoullaugh = new ghoullaugh();
            var sheep = new sheep();
            var pig = new pig();
            var click = new click().AttachToDocument();

            arena.SelectionClick +=
                (p, ev) =>
                {
                    if (pending != null)
                        return;

                    foreach (var v in selection)
                    {
                        if (v.AnimationInfo.Frames_Stand[0].Source == MyFrames.ManWithHorns.Frames_Stand[0].Source)
                        {
                            ghoullaugh.play();
                            ghoullaugh = new ghoullaugh();
                        }
                        else if (v.AnimationInfo.Frames_Stand[0].Source == MyFrames.ThePig.Frames_Stand[0].Source)
                        {
                            pig.play();
                            pig = new pig();
                        }
                        else if (v.AnimationInfo.Frames_Stand[0].Source == MyFrames.TheSheep.Frames_Stand[0].Source)
                        {
                            sheep.play();
                            sheep = new sheep();
                        }
                        else if (v.AnimationInfo.Frames_Stand[0].Source == Frames.WolfSoldier[0].Source)
                        {
                            Affirmative.play();
                            Affirmative = new Affirmative();
                        }
                        else
                        {
                            Argh.play();
                            Argh = new Argh();
                        }



                        v.WalkTo(p.ToDouble());

                        // move in group formation
                        p.X += 16;
                        p.Y += 16;
                    }
                };


            #endregion

            #region tutorial step 4



            #region CreateDialogAt
            var CreateDialogAt =
                new
                {
                    //Dialog = default(IHTMLDiv),
                    Content = default(IHTMLDiv),
                    Width = default(string)
                }
                .ToFunc(
                (Point pos, string width) =>
                {
                    var f = new Form();

                    f.Show();

                    f.SizeTo(200, 200);

                    f.PopupInsteadOfClosing();

                    f.MoveTo(pos.X, pos.Y);
                    //f.SizeTo(


                    //var dialog = new IHTMLDiv();

                    //dialog.style.SetLocation(pos.X, pos.Y);

                    //dialog.style.backgroundColor = Color.Gray;
                    //dialog.style.padding = "1px";

                    //var caption = new IHTMLDiv().AttachTo(dialog);

                    //caption.style.backgroundColor = Color.Blue;
                    //caption.style.width = width;
                    //caption.style.height = "0.5em";
                    //caption.style.cursor = IStyle.CursorEnum.move;

                    //var drag = new DragHelper(caption);

                    //drag.Position = pos;
                    //drag.Enabled = true;
                    //drag.DragMove +=
                    //    delegate
                    //    {
                    //        dialog.style.SetLocation(drag.Position.X, drag.Position.Y);
                    //    };

                    var _content = new IHTMLDiv().AttachTo(f.GetHTMLTargetContainer());

                    _content.style.textAlign = IStyle.TextAlignEnum.center;
                    _content.style.backgroundColor = Color.White;
                    _content.style.padding = "1px";

                    //dialog.AttachToDocument();

                    return new
                    { //Dialog = dialog,
                        Content = _content,
                        Width = width
                    };
                }
            );
            #endregion

            #region dialog
            var toolbar = CreateDialogAt(new Point(2, 2), "8em");

            var combo = new IHTMLSelect();
            var build = new IHTMLButton();

            build.style.SetSize(72, 72);
            build.style.padding = "0px";

            var avatar = new IHTMLImage().AttachTo(build);
            var remove = new IHTMLButton("Remove");


            combo.AttachTo(toolbar.Content);
            new IHTMLBreak().AttachTo(toolbar.Content);
            build.AttachTo(toolbar.Content);
            new IHTMLBreak().AttachTo(toolbar.Content);
            remove.AttachTo(toolbar.Content);
            new IHTMLBreak().AttachTo(toolbar.Content);
            #endregion


            #region GetSelectedArsenal
            Func<DudeAnimationInfo> GetSelectedArsenal =
                () =>
                {
                    if (arsenal.ContainsKey(combo[combo.selectedIndex].value))
                        return arsenal[combo[combo.selectedIndex].value];

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



                    click.play();
                    click = new click().AttachToDocument();
                };

            #region pending actor

            arena.MouseMove +=
                p =>
                {
                    if (pending == null)
                        return;

                    pending.TeleportTo(p.X, p.Y);
                };

            arena.Layers.User.oncontextmenu +=
                e =>
                {
                    e.preventDefault();

                    if (pending != null)
                    {
                        pending.Control.Orphanize();
                        pending = null;
                        arena.ShowSelectionRectangle = true;

                        return;
                    }

                    actors.ForEach(
                        k => k.IsSelected = false
                            );
                };

            arena.SelectionClick +=
                (p, ev) =>
                {
                    if (pending == null)
                    {

                        return;
                    }

                    pending.TeleportTo(p.X, p.Y);

                    actors.Add(pending);

                    pending.IsHot = false;


                    var x = GetSelectedArsenal();
                    pending = CreateActor(x,
                       new Point(
                           Native.window.Width / 2,
                           Native.window.Height / 2
                           )
                   );

                    pending.IsHot = true;


                    click.play();
                    click = new click().AttachToDocument();
                };

            build.onclick +=
                delegate
                {
                    if (pending != null)
                    {
                        pending.Control.Orphanize();
                        pending = null;

                        return;
                    }

                    var x = GetSelectedArsenal();

                    pending = CreateActor(x,
                        new Point(
                            Native.window.Width / 2,
                            Native.window.Height / 2
                            )
                    );

                    pending.IsHot = true;
                    arena.ShowSelectionRectangle = false;

                    click.play();
                    click = new click().AttachToDocument();
                };
            #endregion

            remove.onclick +=
                delegate
                {
                    foreach (var v in selection.ToArray())
                    {
                        v.Control.Orphanize();
                        actors.Remove(v);
                    }


                    click.play();
                    click = new click().AttachToDocument();
                };

            #endregion

            if (FilterToImpAndSoldier)
            { }
            else
            {
                #region step 6

                {
                    var n = "NPC";

                    arsenal.Add(n, MyFrames.NPC3);
                    combo.Add(n);
                }

                #endregion


                {
                    var n = "ManWithHorns";

                    arsenal.Add(n, MyFrames.ManWithHorns);
                    combo.Add(n);
                }
                {
                    var n = "TheSheep";

                    arsenal.Add(n, MyFrames.TheSheep);
                    combo.Add(n);
                }
                {
                    var n = "ThePig";

                    arsenal.Add(n, MyFrames.ThePig);
                    combo.Add(n);
                }

                {
                    var n = "TheCactus";

                    arsenal.Add(n, MyFrames.TheCactus);
                    combo.Add(n);
                }
            }


            if (BeforeAddingDebris != null)
                BeforeAddingDebris(arena.Layers.Canvas);

            3.Times(
              delegate()
              {
                  new DebrisImages().Images.ForEach(
                      img => img.AttachTo(arena.Layers.Canvas).style.SetLocation(map.X.Random(), map.Y.Random())
                  );
              }
      );

            16.Times(
                delegate()
                {
                    actors.Add(
                        CreateActor(arsenal.Random().Value, new Point(map.X.Random(), map.Y.Random()))
                        );
                }
            );
        }



        public static Action<IHTMLDiv> BeforeAddingDebris;
    }

}
