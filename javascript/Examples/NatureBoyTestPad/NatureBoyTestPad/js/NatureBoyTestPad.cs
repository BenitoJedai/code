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
using ScriptCoreLib.JavaScript.Runtime;


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
            var CreateDialogAt =
                new
                {
                    Dialog = default(IHTMLDiv),
                    Content = default(IHTMLDiv),
                    Width = default(string)
                }
                .ToFunc(
                (Point pos, string width) =>
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

                    drag.Position = pos;
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

                    return new { Dialog = dialog, Content = _content, Width = width };
                }
            );
            #endregion

            #region dialog
            var toolbar = CreateDialogAt(new Point(2, 2), "5em");

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

            #region step 5

            var define = new IHTMLButton("Define").AttachTo(toolbar.Content);

            new IHTMLBreak().AttachTo(toolbar.Content);

            var definition_dialog = CreateDialogAt(new Point(100, 2), "306px");

            definition_dialog.Dialog.Hide();

            define.onclick +=
                delegate
                {
                    definition_dialog.Dialog.ToggleVisible();
                };

            var definition_text = new IHTMLTextArea();

            definition_text.style.SetSize(300, 128);
            definition_text.setAttribute("wrap", "off");
            definition_text.AttachTo(definition_dialog.Content);

            new IHTMLBreak().AttachTo(definition_dialog.Content);

            var definition_done = new IHTMLButton("Load").AttachTo(definition_dialog.Content);

            #region Parse
            Func<string[], DudeAnimationInfo> Parse =
                lines =>
                {
                    // format:

                    // first line is the path
                    // empty line
                    // stand array
                    // empty line
                    // walk array n

                    if (lines.Length < 2)
                        "not enough lines".Throw();

                    var i = 0;

                    var path = lines[i];

                    i++;
                    if (lines[i] != "")
                        "empty line missing after path".Throw();

                    #region ReadGroup
                    Func<List<string>> ReadGroup =
                        delegate
                        {

                            var stand = new List<string>();

                            var looping = true;
                            while (looping)
                            {
                                i++;
                                if (!(i < lines.Length))
                                    looping = false;
                                else if (lines[i] != "")
                                    stand.Add(lines[i]);
                                else
                                    looping = false;
                            }

                            return stand;
                        };
                    #endregion

                    var groups = new List<string[]>();

                    while (i < lines.Length)
                        groups.Add(ReadGroup().ToArray());

                    #region Prepare
                    Func<string[], FrameInfo[]> Prepare =
                        data =>
                            data.Select(
                            k =>
                                new FrameInfo
                                {
                                    Source = path + "/" + k,
                                    Weight = 1 / data.Length
                                }
                            ).ToArray();
                    #endregion



                    var x = new DudeAnimationInfo();

                    x.Frames_Stand = Prepare(groups[0]);

                    if (groups.Count > 1)
                    {
                        groups.RemoveAt(0);

                        x.Frames_Walk = groups.Select(k => Prepare(k)).ToArray();
                    }
                    else
                        x.Frames_Walk = new[] { x.Frames_Stand };

                    Console.WriteLine(new { groups.Count, path }.ToString());

                    return x;
                };
            #endregion


            definition_done.onclick +=
                delegate
                {

                    var lines = definition_text.value.Trim().Split('\n').Select(i => i.Trim());

                    try
                    {
                        var x = Parse(lines.ToArray());

                        Action DoneLoading =
                            delegate
                            {
                                var n = "# " + arsenal.Count;

                                arsenal.Add(n, x);
                                combo.Add(n);
                                combo.value = n;
                                Refresh();
                            };

                        #region try loading
                        foreach (var v in x.Images)
                            Console.WriteLine("preloading: " + v.src);

                        new Timer(
                            t =>
                            {
                                if (x.Images.Any(i => !i.complete))
                                    return;

                                if (x.Images.Any(i => i.complete && i.width == 0))
                                {
                                    t.Stop();

                                    Native.Window.alert("Cannot load from given path!");
                                    return;
                                }

                                t.Stop();

                                DoneLoading();
                            }
                        , 1, 100).TimeToLive = 100;
                        #endregion

                    }
                    catch (Exception exc)
                    {
                        Native.Window.alert("Parse error: " + exc.Message);
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
