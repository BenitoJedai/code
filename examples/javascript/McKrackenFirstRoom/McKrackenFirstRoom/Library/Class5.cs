using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Extensions;
using McKrackenSceneSlideshow.Library;
using McKrackenFirstRoom.HTML.Audio.FromAssets;

namespace NatureBoy.js
{


    partial class Class5
    {
        const string ZakAssets = "assets/NatureBoy/zak";
        const string ZakSprites = "assets/NatureBoy/zak/sprites";

        static IHTMLImage[] __Images
        {
            get
            {
                return
                    new IHTMLImage[]
                    {
                        new McKrackenFirstRoom.HTML.Images.FromAssets._114(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._115(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._116(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._117(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._118(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._119(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._120(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._121(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._122(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._123(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._124(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._125(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._126(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._127(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._128(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._129(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._130(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._131(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._132(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._133(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._134(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._135(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._136(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._137(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._138(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._139(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._140(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._141(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._142(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._143(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._144(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._145(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._146(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._147(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._148(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._149(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._150(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._151(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._152(),
                        new McKrackenFirstRoom.HTML.Images.FromAssets._153(),
                    };
            }
        }


        #region Wolf Soldier

        public static IEnumerable<IEnumerable<Zak.ImageRef>> LoadWolfSoldierWalk()
        {
            return 4.Range().Select(
                j =>
                    8.Range(i => (122 + j * 8) + (i + 6) % 8)
                    .Select(i =>
                         new Zak.ImageRef
                            {
                                Source = __Images.First(kk => kk.src.EndsWith("/_" + i + ".png")).src,
                                Name = "dude5/walk" + (j + 1) + "/" + i
                            }
                    )
            );
        }




        public static IEnumerable<int> WolfSoldierStand
        {
            get { return 8.Range(i => 114 + (i + 6) % 8); }
        }



        public static IEnumerable<Zak.ImageRef> LoadWolfSoldierStand()
        {
            return WolfSoldierStand.Select(
                i =>
                    new Zak.ImageRef
                    {
                        Source = __Images.First(kk => kk.src.EndsWith("/_" + i + ".png")).src,
                        Name = "dude5/stand/" + i
                    }
            );
        }

        #endregion

        public static Zak.Document DefaultData
        {
            get
            {
                return
                new Zak.Document
                {
                    Images = new[]
                {
                    new Zak.ImageRef { Name = "empty", Source = new McKrackenFirstRoom.HTML.Images.FromAssets.empty().src },
                    new Zak.ImageRef { Name = "r1", Source = new McKrackenFirstRoom.HTML.Images.FromAssets.r1().src  },
                    new Zak.ImageRef { Name = "r1_bed", Source =new McKrackenFirstRoom.HTML.Images.FromAssets.r1_bed().src },
                    new Zak.ImageRef { Name = "clock_left", Source =new McKrackenFirstRoom.HTML.Images.FromAssets.clock_left().src  },
                    new Zak.ImageRef { Name = "clock_right", Source =new McKrackenFirstRoom.HTML.Images.FromAssets.clock_right().src  }
                }
                    .Concat(LoadWolfSoldierStand())
                    .Concat(LoadWolfSoldierWalk().SelectMany())
                    .ToArray(),
                    ViewPortSize = new Zak.Point
                    {
                        X = "" + 320,
                        Y = "" + 128
                    },
                    ViewPortZoom = "" + 2d,
                    BackgroundImageName = "r1",
                    Items = new[]
                {
                    new Zak.ItemRef 
                    {
                        Name = "r1_bed",
                        Description = "Zak's bed",
                        ImageName = "r1_bed",
                        Position = new Zak.Point { X = "" + 128, Y = "" + 43 }
                    }
                },
                    EntryPoint = new Zak.Point { X = "" + 222, Y = "" + 117 },
                    WalkArea =
                        new[] {
                        new Zak.Rect(141, 106, 296, 130)
                        ,
                        new Zak.Rect { 
                            From = new Zak.Point { X = "" + 74, Y = "" + 98 },
                            To = new Zak.Point { X = "" + 150, Y = "" + 117 }
                        },
                        new Zak.Rect { 
                            From = new Zak.Point { X = "" + 142, Y = "" + 93 },
                            To = new Zak.Point { X = "" + 175, Y = "" + 104 }
                        },
                    }
                };
            }
        }
    }



    partial class Class5
    {
        // todo: overlay, inline spawn from space invaders
        public readonly IHTMLDiv Control = new IHTMLDiv();
        public readonly Zak.Document Data = DefaultData;

        public Class5()
        {

            Console.WriteLine("new Class5");

            if (Data == null)
                Console.WriteLine("has no data");

            //if (AnchorElement == null)
            //    Console.WriteLine("has no anchor");


            var c = Control;

            var ViewPortSize = new ZoomedPoint
            {
                GetZ = () => Data.ViewPortZoom.ToDouble(),
                GetX = () => Data.ViewPortSize.X.ToInt32(),
                GetY = () => Data.ViewPortSize.Y.ToInt32(),
            }.ApplyZoomedSize(c);


            c.style.overflow = IStyle.OverflowEnum.hidden;
            c.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.Yellow;

            c.AttachToDocument();
            //c.style.SetLocation(0, 0);

            var ImagesLoaded = 0;

            Action UpdateLoadingMessage =
                () =>
                {
                    c.innerHTML = "loading... " + ImagesLoaded + " of " + Data.Images.Length;
                };

            var ImagesLoadedEvent = default(Action);


            ImagesLoadedEvent =
                delegate
                {
                    c.innerHTML = "building...";

                    var Images = Data.Images.ToDictionary(iref => iref.Name);

                    var WalkArea = Data.WalkArea.Select(
                          r =>
                              Rectangle.Of(
                                      (r.From.X.ToInt32() * ViewPortSize.Z).ToInt32(),
                                      (r.From.Y.ToInt32() * ViewPortSize.Z).ToInt32(),
                                      ((r.To.X.ToInt32() - r.From.X.ToInt32()) * ViewPortSize.Z).ToInt32(),
                                      ((r.To.Y.ToInt32() - r.From.Y.ToInt32()) * ViewPortSize.Z).ToInt32()
                                 )
                      ).ToArray();

                    Func<string, IHTMLImage> GetImage = Name =>
                        {
                            var iref = Images[Name];

                            var img = new IHTMLImage(iref.Source);

                            new ZoomedPoint
                            {
                                GetZ = ViewPortSize.GetZ,
                                X = iref.Size.X.ToInt32(),
                                Y = iref.Size.Y.ToInt32()
                            }.ApplyZoomedSize(img);

                            return img;
                        };

                    #region layers
                    var ContentLayer = new IHTMLDiv();
                    var InputLayer = new IHTMLDiv();

                    ViewPortSize.ApplyZoomedSize(ContentLayer);
                    ViewPortSize.ApplyZoomedSize(InputLayer);

                    ContentLayer.style.SetLocation(0, 0);
                    InputLayer.style.SetLocation(0, 0);

                    new IHTMLImage(Images["empty"].Source).ToBackground(InputLayer);



                    InputLayer.style.zIndex = 10000;
                    #endregion



                    // background
                    var bg = GetImage(Data.BackgroundImageName);
                    ViewPortSize.ApplyZoomedSize(bg);




                    // item 1 - hardcoded
                    #region item - bed
                    var bed = GetImage("r1_bed");

                    new ZoomedPoint
                    {
                        GetZ = ViewPortSize.GetZ,
                        X = 132,
                        Y = 43
                    }.ApplyZoomedSize(bed);

                    bed.style.zIndex = 1000;

                    new ZoomedPoint
                    {
                        GetZ = ViewPortSize.GetZ,
                        X = 0,
                        Y = 128 - 43
                    }.ApplyZoomedLocation(bed);
                    #endregion



                    // item 2 - clock
                    #region item
                    var clock_left = GetImage("clock_left");
                    var clock_right = GetImage("clock_right");

                    var clock = new[] {
                        clock_left,
                        clock_right
                    };

                    clock_right.Hide();


                    new ZoomedPoint
                    {
                        GetZ = ViewPortSize.GetZ,
                        X = 150,
                        Y = 25
                    }
                    .ApplyZoomedLocation(clock_left)
                    .ApplyZoomedLocation(clock_right);



                    ScriptCoreLib.JavaScript.Runtime.Timer.Interval(
                        timer =>
                        {
                            clock.ForEach(img => img.Hide());
                            clock[timer.Counter % clock.Length].Show();
                        }, 900);
                    #endregion

                    // create wolf soldier

                    var WolfSoldierInfo = new DudeAnimationInfo
                    {
                        Frames_Stand = WolfSoldierStand.Select
                        (
                            i => new FrameInfo
                            {
                                Source = Images["dude5/stand/" + i].Source,
                                Weight = 1d / 8,
                                //OffsetY = 4
                            }
                        ).ToArray(),
                        Frames_Walk =
                            4.Range().Select(
                                j =>
                                    8.Range(i => (122 + j * 8) + (i + 6) % 8)
                                    .Select(i =>
                                        new FrameInfo
                                        {
                                            Source = Images["dude5/walk" + (j + 1) + "/" + i].Source,
                                            Weight = 1d / 8,
                                            //OffsetY = 4
                                        }
                                    ).ToArray()
                             ).ToArray()
                    };

                    var dude = new Dude2
                    {
                        Frames = WolfSoldierInfo.Frames_Stand,

                    };

                    dude.HasShadow = false;
                    dude.TargetLocationDistanceMultiplier = 1;

                    dude.AnimationInfo.Frames_Stand = WolfSoldierInfo.Frames_Stand;
                    dude.AnimationInfo.Frames_Walk = WolfSoldierInfo.Frames_Walk;

                    dude.Zoom.DynamicZoomFunc = z => 1;
                    dude.Zoom.StaticZoom = ViewPortSize.Z;
                    dude.SetSize(48, 72);

                    var DudeLocation = new ZoomedPoint
                        {
                            GetX = () => Data.EntryPoint.X.ToInt32(),
                            GetY = () => Data.EntryPoint.Y.ToInt32(),
                            GetZ = ViewPortSize.GetZ
                        };




                    dude.TeleportTo(DudeLocation.ZoomedX, DudeLocation.ZoomedY);
                    dude.Direction = Math.PI.Random() * 2;


                    var Affirmative = new Affirmative();



                    dude.CanTeleportTo =
                        pos =>
                        {
                            var ok = WalkArea.Any(r => r.Contains(pos));

                            if (!ok)
                                dude.IsWalking = false;

                            return !ok;
                        };


                    Action<IEvent> onclick =
                         ev =>
                         {
                             ev.PreventDefault();
                             ev.StopPropagation();

                             Affirmative.play();
                             Affirmative = new Affirmative();

                             dude.WalkTo(ev.OffsetPosition);

                             Console.WriteLine("click: " + ev.OffsetPosition);
                         };

                    InputLayer.ontouchstart +=
                        ev =>
                        {
                            ev.PreventDefault();
                            ev.StopPropagation();
                            InputLayer.onclick -= onclick;

                            Affirmative.play();
                            Affirmative = new Affirmative();


                            dude.WalkTo(
                                new Point(ev.touches[0].clientX, ev.touches[0].clientY
                                    )
                            );

                        };

                    // click to  move the dude
                    InputLayer.onclick += onclick;


                    Native.Document.body.onselectstart +=
                        ev =>
                        {
                            ev.PreventDefault();
                            ev.StopPropagation();
                        };

                    Native.Document.onkeydown +=
                        ev =>
                        {


                            var dict = new Dictionary<int, int>
                            {
                                { 39, 0 },
                                { 40, 1 },
                                { 37, 2 },
                                { 38, 3 },
                            };


                            if (dict.ContainsKey(ev.KeyCode))
                            {
                                Affirmative.play();
                                Affirmative = new Affirmative();


                                dude.WalkToArc(dude.CurrentWalkSpeed, (Math.PI / 2) * dict[ev.KeyCode]);
                            }
                        };




                    c.innerHTML = "done!";

                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            c.innerHTML = "";

                            ContentLayer.AttachTo(c);
                            InputLayer.AttachTo(c);

                            bg.AttachTo(ContentLayer);
                            bed.AttachTo(ContentLayer);

                            clock_left.AttachTo(ContentLayer);
                            clock_right.AttachTo(ContentLayer);
                            dude.Control.AttachTo(ContentLayer);
                        }, 500, 0);
                };


            #region loading
            Data.Images.ForEach(
                iref =>
                    new IHTMLImage(iref.Source).InvokeOnComplete(
                        img =>
                        {
                            Console.WriteLine("loaded: " + img.src);

                            iref.Size = new Zak.Point { X = "" + img.width, Y = "" + img.height };

                            ImagesLoaded++;

                            UpdateLoadingMessage();

                            if (ImagesLoaded == Data.Images.Length)
                                ImagesLoadedEvent();
                        }
                    )
            );
            #endregion

        }
    }


}
