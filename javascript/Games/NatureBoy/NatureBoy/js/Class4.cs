﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Lambda;

namespace NatureBoy.js
{



    [Script, ScriptApplicationEntryPoint]
    class Class4
    {

        public Class4()
        {
            IStyleSheet.Default.AddRule(".cursorred", "cursor: url('assets/NatureBoy/cursor-red.cur'), auto;", 0);

            IStyleSheet.Default.AddRule("body", "cursor: url('assets/NatureBoy/cursor.cur'), auto;", 0);

            IStyleSheet.Default.AddRule("html",
                   r =>
                   {

                       r.style.backgroundColor = Color.Black;
                       r.style.color = Color.White;
                       r.style.overflow = IStyle.OverflowEnum.hidden;
                       r.style.padding = "0px";
                       r.style.margin = "0px";
                       r.style.height = "100%";
                       r.style.width = "100%";
                   }
               );

            IStyleSheet.Default.AddRule("body",
                r =>
                {

                    r.style.backgroundColor = Color.Black;
                    r.style.color = Color.White;
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.padding = "0px";
                    r.style.margin = "0px";
                    r.style.height = "100%";
                    r.style.width = "100%";

                }
            );

            Native.Document.title = "Adventures Of The Great Imp";

            Preload();
        }

        private void Preload()
        {
            var loading = "loading".AttachToDocument();

            loading.style.fontFamily = IStyle.FontFamilyEnum.Consolas;

            var ToBeLoaded = 0;
            var AlreadyLoaded = 0;

            Action refresh = delegate { };

            Action<IHTMLImage> LoadImage =
                img =>
                {
                    ToBeLoaded++;
                    refresh();

                    img.InvokeOnComplete(
                        delegate
                        {
                            AlreadyLoaded++;
                            refresh();
                        }
                    );
                };



            // load me an imp

            LoadImage("assets/NatureBoy/alpha/2.png");
            LoadImage("assets/NatureBoy/alpha/power.png");
            LoadImage("assets/NatureBoy/alpha/green-ring.png");
            LoadImage("assets/NatureBoy/alpha/yellow-ring-50.png");

            var dude = new DudeAnimationInfo
            {
                Frames_Stand = Frames.DoomImp,
                Frames_Walk = Frames.DoomImp_Walk
            };


            dude.Images.ForEach(LoadImage);

            var scene = "assets/NatureBoy/data/LostInTime.xml";

            Action<Scene.Document> SceneDownloaded = delegate { };

            Action ResetImageLoader =
                delegate
                {
                    refresh = delegate { };
                    ToBeLoaded = 0;
                    AlreadyLoaded = 0;
                };

            Func<Action, Action> CreateImageLoader =
                done => delegate
                {
                    if (ToBeLoaded == AlreadyLoaded)
                    {
                        if (ToBeLoaded > 0)
                        {
                            ResetImageLoader();
                            done();
                            return;
                        }
                    }

                    loading.innerText = "loading: " + AlreadyLoaded + " images of " + ToBeLoaded + " loaded";
                };

            refresh += CreateImageLoader(
                delegate
                {
                    loading.innerText = "loading: " + scene;
                    scene.DownloadToXML(Scene.Settings.KnownTypes, SceneDownloaded);
                }
            );

            SceneDownloaded =
                doc =>
                {
                    //loading.innerText = "loading: done";

                    doc.Frames.ForEach(
                        f =>
                        {

                            LoadImage(f.Image.Source);
                        }
                    );

                    refresh += CreateImageLoader(
                        delegate
                        {
                            loading.Hide();

                            // now we have an imp, the xml file, and all the frames we need

                            Spawn(dude, doc);
                        }
                    );

                    refresh();
                };

            refresh();
        }

        [Script]
        class TryToChangeRoomsArgs
        {
            public Func<bool> Condition;
            public Func<Scene.Frame, bool> NextRoomSelector;
            public Action ReadyToTeleport;
        }

        void Spawn(DudeAnimationInfo LoadedCharacter, Scene.Document LoadedScene)
        {
            var ViewSize = new Size { Width = 640, Height = 480 };

            var Wallpaper = new IHTMLDiv();

            Wallpaper.style.SetSize(ViewSize.Width, ViewSize.Height + 60);
            Wallpaper.style.background = "url(assets/NatureBoy/alpha/power.png)";
            Wallpaper.style.backgroundRepeat = "no-repeat";
            Wallpaper.style.backgroundPosition = "center center";
            Wallpaper.AttachToDocument();
            Wallpaper.KeepInCenter();




            var Margin = 48;
            var MarginSafe = 72;



            var CurrentFrame = LoadedScene.Frames.Randomize().First();
            //var CurrentFrame = LoadedScene.Frames.Single(f => f.Name == "C");

            var Room = new IHTMLDiv();



            Room.style.border = "1px solid #00ff00";
            Room.style.SetSize(ViewSize.Width, ViewSize.Height);
            Room.style.position = IStyle.PositionEnum.absolute;
            Room.style.overflow = IStyle.OverflowEnum.hidden;


            Room.AttachToDocument();
            Room.KeepInCenter();




            var tween = Room.ToOpacityTween();

            Action HideRoom = () => tween.Value = 1;
            Action ShowRoom = () => tween.Value = 0;

            HideRoom();

            //var GroundOverlay2 = new IHTMLDiv();

            //GroundOverlay2.style.backgroundColor = Color.Blue;
            ////GroundOverlay.style.Opacity = 0;

            //GroundOverlay2.style.position = IStyle.PositionEnum.absolute;
            //GroundOverlay2.style.SetLocation(0, 0, ViewSize.Width, ViewSize.Height);
            //GroundOverlay2.AttachTo(Room);

            var BackgroundImage = new IHTMLImage(CurrentFrame.Image.Source);

            BackgroundImage.style.SetLocation(0, 0, ViewSize.Width, ViewSize.Height);
            BackgroundImage.alt = "BackgroundImage";
            BackgroundImage.AttachTo(Room);

            //GroundOverlay2.style.backgroundImage = "url(" + CurrentFrame.Image.Source + ")";
            //BackgroundImage.InvokeOnComplete(
            //    delegate
            //    {
            //        //BackgroundImage.style.backgroundColor = Color.Red;
            //        //BackgroundImage.style.SetLocation(0,0, ViewSize.Width, ViewSize.Height);
            //        BackgroundImage.AttachTo(GroundOverlay2);
            //    }
            //);


            var GroundOverlay = new IHTMLDiv();

            GroundOverlay.style.backgroundColor = Color.Blue;
            GroundOverlay.style.Opacity = 0;
            GroundOverlay.style.SetLocation(0, 0, ViewSize.Width, ViewSize.Height);
            GroundOverlay.AttachTo(Room);

            var Ground = new IHTMLDiv();

            Ground.style.SetLocation(0, 0, ViewSize.Width, ViewSize.Height);
            Ground.AttachTo(Room);


            var AnimateRoomChange = default(Action<Action>);

            Func<TryToChangeRoomsArgs, bool> TryToChangeRooms =
                e =>
                {
                    if (e == null)
                        return false;

                    if (e.NextRoomSelector == null) throw new ArgumentNullException("NextRoomSelector");

                    var next = LoadedScene.Frames.SingleOrDefault(e.NextRoomSelector);

                    var r = next != null;

                    if (r)
                    {
                        AnimateRoomChange(
                            delegate
                            {
                                CurrentFrame = next;
                                //GroundOverlay2.style.backgroundImage = "url(" + CurrentFrame.Image.Source + ")";
                                BackgroundImage.src = CurrentFrame.Image.Source;

                                e.ReadyToTeleport();
                            }
                        );
                    }


                    return r;
                };

            var dude = CreateDude(LoadedCharacter);

            dude.Control.AttachTo(Ground);


            var Doors = new[]
                {
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.X > ViewSize.Width - Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Right,
                                ReadyToTeleport = delegate
                                {

                                    dude.TeleportTo(-MarginSafe, dude.CurrentLocation.Y);
                                    dude.LookAt(new Point(MarginSafe, dude.CurrentLocation.Y));
                                }
                            },
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.X < Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Left,
                                ReadyToTeleport = delegate
                                {

                                    dude.TeleportTo(ViewSize.Width + MarginSafe, dude.CurrentLocation.Y);
                                    dude.LookAt(new Point(ViewSize.Width - MarginSafe, dude.CurrentLocation.Y));
                                }
                            },
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.Y < Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Top,
                                ReadyToTeleport = delegate
                                {

                                     dude.TeleportTo(dude.CurrentLocation.X, ViewSize.Height + MarginSafe);
                                    dude.LookAt(new Point(dude.CurrentLocation.X, ViewSize.Height - MarginSafe));

                                }
                            },
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.Y > ViewSize.Height - Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Bottom,
                                ReadyToTeleport = delegate
                                {

                                  dude.TeleportTo(dude.CurrentLocation.X, -Margin);
                                dude.LookAt(new Point(dude.CurrentLocation.X, MarginSafe));

                                }
                            }
                };

            Action<string, Action> PrintText =
                (text, done) =>
                {
                    text.Length.Range().AsyncForEach(
                        i =>
                        {
                            Wallpaper.innerText = text.Left(i + 1);

                            var c = text[i];

                            if (LoadedScene.SlowText.Contains("" + c))
                                return 100.Random();

                            return 50.Random();
                        }, done
                    );
                };

            Action<string, Action> PrintRandomText =
                (text, done) => PrintText(text.Split(LoadedScene.TextDelimiter).Randomize().First(), done);



            dude.DoneWalking +=
                delegate
                {
                    // compiler bug: cannot invoke Action<func, action> delegate ?

                    System.Console.WriteLine("done walking in " + CurrentFrame.Name + " at " + dude.CurrentLocation);

                    if (TryToChangeRooms(Doors.FirstOrDefault(d => d.Condition())))
                        return;


                    if (CurrentFrame.Items != null)
                    {
                        var item = CurrentFrame.Items.Where(
                            i => new Point(i.X.ToInt32(), i.Y.ToInt32()).GetRange(dude.CurrentLocation) < i.R.ToInt32()
                            ).FirstOrDefault();

                        if (item != null)
                        {

                            dude.IsSelected = false;
                            dude.LookDown();

                            PrintRandomText(item.Text,
                                delegate
                                {

                                    dude.WalkingOnce +=
                                        delegate
                                        {
                                            Wallpaper.innerText = "";
                                        };

                                    dude.IsSelected = true;
                                }
                            );
                        }
                    }

                };

            AnimateRoomChange =
                ReadyToTeleport =>
                {
                    var Step1 = default(System.Action);
                    var Step2 = default(System.Action);
                    var Step3 = default(Action);

                    Step1 =
                        delegate
                        {
                            tween.Done -= Step1;

                            ReadyToTeleport();

                            tween.Done += Step2;

                            ShowRoom();
                        };

                    Step2 =
                        delegate
                        {
                            tween.Done -= Step2;

                            dude.DoneWalking += Step3;

                            dude.IsWalking = true;

                        };

                    Step3 =
                        delegate
                        {
                            dude.DoneWalking -= Step3;

                            dude.IsSelected = true;
                        };

                    dude.IsSelected = false;

                    tween.Done += Step1;
                    // go left
                    HideRoom();
                };

            Ground.onclick +=
                ev =>
                {
                    if (ev.Element != Ground)
                        return;

                    System.Console.WriteLine(ev.CursorPosition);

                    if (dude.IsSelected)
                        dude.WalkTo(ev.OffsetPosition);
                };

            GroundOverlay.onclick +=
                ev =>
                {
                    if (ev.Element != GroundOverlay)
                        return;

                    System.Console.WriteLine(ev.CursorPosition);

                    if (dude.IsSelected)
                        dude.WalkTo(ev.OffsetPosition);
                };


            dude.TeleportTo(ViewSize.Width / 2, (ViewSize.Height - MarginSafe) / 2);
            dude.LookDown();

            ShowRoom();

            dude.DoneWalkingOnce +=
                delegate
                {
                    PrintRandomText(
                        LoadedScene.IntroText,
                        delegate
                        {

                            dude.WalkingOnce +=
                              delegate
                              {
                                  Wallpaper.innerText = "";
                              };

                            dude.IsSelected = true;
                        }
                    );
                };

            dude.WalkToArc(MarginSafe, dude.Direction);

        }

        private static Dude2 CreateDude(DudeAnimationInfo LoadedCharacter)
        {
            var r = new Dude2();

            r.Frames = LoadedCharacter.Frames_Stand;

            r.AnimationInfo.Frames_Stand = LoadedCharacter.Frames_Stand;
            r.AnimationInfo.Frames_Walk = LoadedCharacter.Frames_Walk;

            r.Zoom.DynamicZoomFunc = a => (a + 400) / (800);
            r.Zoom.StaticZoom = 1.75;

            r.SetSize(48, 72);
            r.TeleportTo(100, 100);

            r.Control.className = "cursorred";
            r.TargetLocationDistanceMultiplier = 1;


            r.Direction = Math.PI.Random() * 2;
            return r;
        }

        static Class4()
        {
            typeof(Class4).SpawnTo(e => new Class4());
        }
    }
}
