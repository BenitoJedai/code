using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using ImpAdventures.HTML.Images.FromAssets;
using ImpAdventures.HTML.Audio.FromAssets;

namespace NatureBoy.js
{



    public class Class4
    {

        public Class4()
        {

            IStyleSheet.Default.AddRule(".cursorred", "cursor: url('" + new cursor_red().src + "'), auto;", 0);

            IStyleSheet.Default.AddRule("body", "cursor: url('" + new cursor().src + "'), auto;", 0);

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

            //LoadImage("assets/NatureBoy/alpha/2.png");
            //LoadImage("assets/NatureBoy/alpha/power.png");
            //LoadImage("assets/NatureBoy/alpha/green-ring.png");
            //LoadImage("assets/NatureBoy/alpha/yellow-ring-50.png");

            var dude = new DudeAnimationInfo
            {
                Frames_Stand = Frames.DoomImp,
                Frames_Walk = Frames.DoomImp_Walk
            };

            dude.Images.ForEach(LoadImage);

            var scene = "LostInTime.xml";

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

                    ImpAdventures.Data.LostInTime.Create(
                        e =>
                        {
                            Console.WriteLine(e.Document.ToString());

                            var s = new IXMLSerializer<Scene.Document>(Scene.Settings.KnownTypes);

                            var doc = s.Deserialize(e.Document.AsIXMLDocument());

                            SceneDownloaded(doc);
                        }
                    );

                }
            );

            SceneDownloaded =
                doc =>
                {
                    //loading.innerText = "loading: done";

                    //doc.Frames.ForEach(
                    //    f =>
                    //    {

                    //        LoadImage(f.Image.Source);
                    //    }
                    //);
                    new ImpAdventures.HTML.Pages.LostInTimeImages().Images.ForEach(LoadImage);

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

        class TryToChangeRoomsArgs
        {
            public Func<bool> Condition;
            public Func<Scene.Frame, bool> NextRoomSelector;
            public Action ReadyToTeleport;
        }

        void Spawn(DudeAnimationInfo LoadedCharacter, Scene.Document LoadedScene)
        {
            var ViewSize = new Size { Width = 640, Height = 480 };

            var Container = new IHTMLDiv();
            Container.AttachToDocument();
            Container.style.SetSize(ViewSize.Width, ViewSize.Height + 22);
            Container.KeepInCenter();

            //Container.MakeCSSShaderCrumple();

            var Wallpaper = new IHTMLDiv().AttachTo(Container);
            Wallpaper.style.SetSize(ViewSize.Width, ViewSize.Height + 22);


            new power().ToBackground(Wallpaper.style);
            Wallpaper.style.position = IStyle.PositionEnum.absolute;
            Wallpaper.style.backgroundRepeat = "no-repeat";
            Wallpaper.style.backgroundPosition = "center center";





            var Margin = 48;
            var MarginSafe = 72;



            var CurrentFrame = LoadedScene.Frames.Randomize().First();
            //var CurrentFrame = LoadedScene.Frames.Single(f => f.Name == "C");

            var Room = new IHTMLDiv();



            Room.style.border = "1px solid #00ff00";
            Room.style.SetSize(ViewSize.Width, ViewSize.Height);
            Room.style.position = IStyle.PositionEnum.absolute;
            Room.style.overflow = IStyle.OverflowEnum.hidden;

            Room.AttachTo(Container);
            Room.style.SetLocation(0, 22);

            //Room.AttachToDocument();
            //Room.KeepInCenter();




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

            var LostInTime_Images = new ImpAdventures.HTML.Pages.LostInTimeImages().Images;

            var BackgroundImage = new IHTMLImage();

            LostInTime_Images.FirstOrDefault(
                                   k => k.src.SkipUntilLastIfAny("/") == CurrentFrame.Image.Source.SkipUntilLastIfAny("/")
                               ).With(
                                   ImageSource =>
                                   {
                                       Console.WriteLine(ImageSource.src);
                                       BackgroundImage.src = ImageSource.src;
                                   }
                               );

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

            #region TryToChangeRooms
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

                                Console.WriteLine("AnimateRoomChange");

                                LostInTime_Images.FirstOrDefault(
                                    k => k.src.SkipUntilLastIfAny("/") == CurrentFrame.Image.Source.SkipUntilLastIfAny("/")
                                ).With(
                                    ImageSource =>
                                    {
                                        Console.WriteLine(ImageSource.src);
                                        BackgroundImage.src = ImageSource.src;
                                    }
                                );

                                //GroundOverlay2.style.backgroundImage = "url(" + CurrentFrame.Image.Source + ")";
                                //BackgroundImage.src = CurrentFrame.Image.Source;

                                e.ReadyToTeleport();
                            }
                        );
                    }


                    return r;
                };
            #endregion


            var dude = CreateDude(LoadedCharacter);

            dude.Control.AttachTo(Ground);

            #region Doors
            var Doors = new[]
                {
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.X > ViewSize.Width - Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Right,
                                ReadyToTeleport = delegate
                                {

                                    dude.TeleportTo(-MarginSafe, dude.CurrentLocation.Y);
                                    dude.LookAt(new Point(MarginSafe, (int)dude.CurrentLocation.Y));
                                }
                            },
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.X < Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Left,
                                ReadyToTeleport = delegate
                                {

                                    dude.TeleportTo(ViewSize.Width + MarginSafe, dude.CurrentLocation.Y);
                                    dude.LookAt(new Point(ViewSize.Width - MarginSafe, (int)dude.CurrentLocation.Y));
                                }
                            },
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.Y < Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Top,
                                ReadyToTeleport = delegate
                                {

                                     dude.TeleportTo(dude.CurrentLocation.X, ViewSize.Height + MarginSafe);
                                    dude.LookAt(new Point((int)dude.CurrentLocation.X, ViewSize.Height - MarginSafe));

                                }
                            },
                    new TryToChangeRoomsArgs
                            {
                                Condition = () => dude.CurrentLocation.Y > ViewSize.Height - Margin,
                                NextRoomSelector = f => f.Name == CurrentFrame.Bottom,
                                ReadyToTeleport = delegate
                                {

                                  dude.TeleportTo(dude.CurrentLocation.X, -Margin);
                                dude.LookAt(new Point((int)dude.CurrentLocation.X, MarginSafe));

                                }
                            }
                };
            #endregion

            Console.WriteLine(new { Doors = Doors.Length });

            Doors.WithEachIndex(
                (x, index) =>
                {
                    Console.WriteLine(new { index, x });
                    Console.WriteLine(new { index, x.Condition });
                }
            );


            var ChangeRoom = new ChangeRoom { autobuffer = true };
            var Talk = new Talk { autobuffer = true };
            var Argh_RChannel = new Argh_RChannel { autobuffer = true };
            var Argh_LChannel = new Argh_LChannel { autobuffer = true };
            var Argh_Disabled = false;
            var Argh_VolumeMultiplier = 1.0;

            #region Argh_Stereo
            Action<double, double> Argh_Stereo =
                (volume, balance) =>
                {
                    if (Argh_Disabled)
                        return;

                    Argh_RChannel.AttachToDocument();
                    Argh_LChannel.AttachToDocument();

                    Argh_RChannel.volume = Argh_VolumeMultiplier * volume * balance;
                    Argh_LChannel.volume = Argh_VolumeMultiplier * volume * (1 - balance);

                    Argh_RChannel.play();
                    Argh_LChannel.play();

                    Argh_RChannel = new Argh_RChannel { autobuffer = true };
                    Argh_LChannel = new Argh_LChannel { autobuffer = true };

                    Argh_Disabled = true;
                    Argh_VolumeMultiplier /= 2;

                    new Timer(t => Argh_Disabled = false).StartTimeout(800);
                    new Timer(t => Argh_VolumeMultiplier = 1).StartTimeout(5000);
                };
            #endregion

            #region PrintText
            Action<string, Action> PrintText =
                (text, done) =>
                {
                    Talk.AttachToDocument();
                    Talk.load();
                    Talk.volume = Math.Min(1, dude.Zoom.DynamicZoom / 4);
                    Talk.play();
                    Talk = new Talk { autobuffer = true };

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
            #endregion

            Action<string, Action> PrintRandomText =
                (text, done) => PrintText(text.Split(LoadedScene.TextDelimiter).Randomize().First(), done);




            dude.DoneWalking +=
                delegate
                {
                    // compiler bug: cannot invoke Action<func, action> delegate ?

                    System.Console.WriteLine("done walking in " + CurrentFrame.Name + " at " + dude.CurrentLocation);

                    var xFirstOrDefault = Doors.FirstOrDefault(d => d.Condition());

                    System.Console.WriteLine("done walking in " + new { xFirstOrDefault });

                    // Doors null?
                    if (TryToChangeRooms(xFirstOrDefault))
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

            #region AnimateRoomChange
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

                    // http://stackoverflow.com/questions/3009888/autoplay-audio-files-on-an-ipad-with-html5
                    ChangeRoom.AttachToDocument();
                    ChangeRoom.load();
                    ChangeRoom.volume = 0.2;
                    ChangeRoom.play();
                    ChangeRoom = new ChangeRoom();
                };
            #endregion

            var pointer_x = 0;
            var pointer_y = 0;

            #region onmousemove
            Container.onmousemove +=
                ev =>
                {
                    if (Native.Document.pointerLockElement == Container)
                    {
                        if (dude.IsSelected)
                        {
                            var volume = Math.Min(1, dude.Zoom.DynamicZoom / 4);
                            var balance = dude.CurrentLocation.X / ViewSize.Width;

                            pointer_x += ev.movementX;
                            pointer_y += ev.movementY;

                            pointer_x = Math.Min(ViewSize.Width - 0, Math.Max(0, pointer_x));
                            pointer_y = Math.Min(ViewSize.Height - 0, Math.Max(0, pointer_y));

                            var OffsetPosition = new Point(pointer_x, pointer_y

                            );

                            Console.WriteLine(OffsetPosition);

                            Argh_Stereo(volume, balance);
                            dude.WalkTo(OffsetPosition);
                        }
                    }
                };
            #endregion

            #region ontouchstart
            Container.ontouchstart +=
                ev =>
                {
                    ev.PreventDefault();

                    System.Console.WriteLine(ev.CursorPosition);

                    if (dude.IsSelected)
                    {
                        var volume = Math.Min(1, dude.Zoom.DynamicZoom / 4);
                        var balance = dude.CurrentLocation.X / ViewSize.Width;

                        var ev_OffsetPosition = new Point(
                            ev.touches[0].clientX - Container.Bounds.Left,
                            ev.touches[0].clientY - Container.Bounds.Top
                            );

                        Argh_Stereo(volume, balance);
                        dude.WalkTo(ev_OffsetPosition);
                    }
                };
            #endregion

            #region onclick
            Container.onclick +=
                ev =>
                {
                    ev.PreventDefault();

                    if (ev.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        if (Native.Document.pointerLockElement == Container)
                        {
                            Native.Document.exitPointerLock();
                            return;
                        }

                        pointer_x = (int)dude.CurrentLocation.X;
                        pointer_y = (int)dude.CurrentLocation.Y;

                        //Container.requestFullscreen();
                        Container.requestPointerLock();
                        return;
                    }

                    if (ev.Element != Ground)
                        return;

                    System.Console.WriteLine(ev.CursorPosition);

                    if (dude.IsSelected)
                    {
                        var volume = Math.Min(1, dude.Zoom.DynamicZoom / 4);
                        var balance = dude.CurrentLocation.X / ViewSize.Width;

                        Argh_Stereo(volume, balance);
                        dude.WalkTo(ev.OffsetPosition);
                    }
                };
            #endregion



            //GroundOverlay.onclick +=
            //    ev =>
            //    {
            //        if (ev.Element != GroundOverlay)
            //            return;

            //        System.Console.WriteLine(ev.CursorPosition);

            //        if (dude.IsSelected)
            //        {
            //            new Argh().play();

            //            dude.WalkTo(ev.OffsetPosition);
            //        }
            //    };


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


    }
}
