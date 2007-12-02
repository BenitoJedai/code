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

namespace NatureBoy.js
{

    namespace Zak
    {
        [Script]
        public static class Settings
        {
            public static object[] Types
            {
                get
                {
                    return new object[] 
                    {
                        new ImageRef(), 
                        new ItemRef(),
                        new Point(),
                        new Document(),
                        new Rect()
                    };
                }
            }
        }


        [Script, Serializable]
        public sealed class ItemRef
        {
            public string Name;
            public string Description;
            public string ImageName;

            public Point Position;


        }

        [Script, Serializable]
        public sealed class ImageRef
        {
            public string Name;
            public string Source;

            public Point Size;
        }

        [Script, Serializable]
        public sealed class Point
        {
            public string X;
            public string Y;
        }

        [Script, Serializable]
        public sealed class Rect
        {
            public Point From;
            public Point To;

            public Rect()
            {

            }

            public Rect(int x, int y, int cx, int cy)
            {
                From = new Point { X = "" + x, Y = "" + y };
                To = new Point { X = "" + cx, Y = "" + cy };
            }
        }

        [Script, Serializable]
        public sealed class Document
        {
            public ImageRef[] Images;

            public Point ViewPortSize;
            public string ViewPortZoom;

            public ItemRef[] Items;

            public string BackgroundImageName;

            public Point EntryPoint;

            public Rect[] WalkArea;
        }


    }

    partial class Class5
    {
        const string ZakAssets = "assets/NatureBoy/zak";
        const string ZakSprites = "assets/NatureBoy/zak/sprites";

        #region Wolf Soldier

        public static IEnumerable<IEnumerable<Zak.ImageRef>> LoadWolfSoldierWalk()
        {
            return 4.Range().Select(
                j =>
                    8.Range(i => (122 + j * 8) + (i + 6) % 8)
                    .Select(i =>
                         new Zak.ImageRef
                            {
                                Source = "assets/NatureBoy/dude5/walk" + (j + 1) + "/" + i + ".png",
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
                        Source = "assets/NatureBoy/dude5/stand/" + i + ".png",
                        Name = "dude5/stand/" + i
                    }
            );
        }

        #endregion

        public static readonly Zak.Document DefaultData =
            new Zak.Document
            {
                Images = new[]
                {
                    new Zak.ImageRef { Name = "r1", Source = ZakSprites + "/r1.png" },
                    new Zak.ImageRef { Name = "r1_bed", Source = ZakSprites + "/r1_bed.png" },
                    new Zak.ImageRef { Name = "clock_left", Source = ZakSprites + "/clock_left.png" },
                    new Zak.ImageRef { Name = "clock_right", Source = ZakSprites + "/clock_right.png" }
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

    #region Spawn
    partial class Class5
    {
        // ClickOnce Support
        public readonly Zak.Document Data = DefaultData;
        public readonly IHTMLElement AnchorElement;

        public Class5()
            : this(null, null)
        {

        }

        public Class5(Zak.Document _Data)
            : this(_Data, null)
        {

        }

        public Class5(Zak.Document _Data, IHTMLElement _AnchorElement)
        {
            if (_Data != null)
                this.Data = _Data;

            this.AnchorElement = _AnchorElement;

            this.Initialize();
        }


        // Spawn Support
        static Class5()
        {
            typeof(Class5).SpawnTo<Zak.Document>(Zak.Settings.Types, (i, e) => new Class5(i, e));
        }
    }
    #endregion

    [Script]
    class ZoomedPoint
    {
        public Func<double> GetX;
        public Func<double> GetY;
        public Func<double> GetZ;

        public double Z { get { return GetZ(); } set { GetY = () => value; } }
        public double X { get { return GetX(); } set { GetX = () => value; } }
        public double Y { get { return GetY(); } set { GetY = () => value; } }

        public double ZoomedX { get { return GetX() * GetZ(); } }
        public double ZoomedY { get { return GetY() * GetZ(); } }

        public string ZoomedXpx { get { return ZoomedX.ToInt32() + "px"; } }
        public string ZoomedYpx { get { return ZoomedY.ToInt32() + "px"; } }


        public ZoomedPoint ApplyZoomedLocation(IHTMLElement e)
        {
            e.style.SetLocation(ZoomedX.ToInt32(), ZoomedY.ToInt32());

            return this;
        }

        public ZoomedPoint ApplyZoomedSize(IHTMLElement e)
        {
            e.style.width = ZoomedXpx;
            e.style.height = ZoomedYpx;

            return this;
        }


    }

    [Script, ScriptApplicationEntryPoint(IsClickOnce = true, Format = SerializedDataFormat.xml)]
    partial class Class5
    {
        // todo: overlay, inline spawn from space invaders

        void Initialize()
        {

            Console.WriteLine("new Class5");

            if (Data == null)
                Console.WriteLine("has no data");

            if (AnchorElement == null)
                Console.WriteLine("has no anchor");


            var c = new IHTMLDiv();

            var ViewPortSize = new ZoomedPoint
            {
                GetZ = () => Data.ViewPortZoom.ToDouble(),
                GetX = () => Data.ViewPortSize.X.ToInt32(),
                GetY = () => Data.ViewPortSize.Y.ToInt32(),
            }.ApplyZoomedSize(c);


            c.style.overflow = IStyle.OverflowEnum.hidden;
            c.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.Yellow;

            c.AttachAsNextOrToDocument(AnchorElement);
            c.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;

            var ImagesLoaded = 0;

            Action UpdateLoadingMessage =
                () =>
                {
                    c.innerHTML = "loading... " + ImagesLoaded + " of " + Data.Images.Length;
                };

            var ImagesLoadedEvent = default(Action);

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

            ImagesLoadedEvent =
                delegate
                {
                    c.innerHTML = "";

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

                    #region layers
                    var ContentLayer = new IHTMLDiv();
                    var InputLayer = new IHTMLDiv();

                    ViewPortSize.ApplyZoomedSize(ContentLayer);
                    ViewPortSize.ApplyZoomedSize(InputLayer);

                    ContentLayer.style.SetLocation(0, 0);
                    InputLayer.style.SetLocation(0, 0);

                    InputLayer.style.backgroundColor = Color.Green;
                    InputLayer.style.Opacity = 0.01;

                    ContentLayer.AttachTo(c);
                    InputLayer.AttachTo(c);

                    InputLayer.style.zIndex = 10000;
                    #endregion

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

                    // background
                    var bg = GetImage(Data.BackgroundImageName);
                    ViewPortSize.ApplyZoomedSize(bg);
                    bg.AttachTo(ContentLayer);


                    // item 1 - hardcoded
                    #region item - bed
                    var bed = GetImage("r1_bed");

                    new ZoomedPoint
                    {
                        GetZ = ViewPortSize.GetZ,
                        X = 132,
                        Y = 43
                    }.ApplyZoomedSize(bed);

                    bed.AttachTo(ContentLayer);
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
                    
                    clock_left.AttachTo(ContentLayer);
                    clock_right.AttachTo(ContentLayer);

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
                        }, 1000);
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

                    dude.Control.AttachTo(ContentLayer);



                    dude.CanTeleportTo =
                        pos =>
                        {
                            var ok = WalkArea.Any(r => r.Contains(pos));

                            if (!ok)
                                dude.IsWalking = false;

                            return !ok;
                        };

                    // click to  move the dude
                    InputLayer.onclick +=
                       ev =>
                       {
                           dude.WalkTo(ev.OffsetPosition);

                           Console.WriteLine("click: " + ev.OffsetPosition);
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
                                dude.WalkToArc(dude.CurrentWalkSpeed , (Math.PI / 2) * dict[ev.KeyCode]);

                        };
                };
        }
    }


}
