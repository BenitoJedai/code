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
                        new Document()
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
        public sealed class Document
        {
            public ImageRef[] Images;

            public Point ViewPortSize;
            public string ViewPortZoom;

            public ItemRef[] Items;

            public string BackgroundImageName;

            public Point EntryPoint;
        }


    }

    partial class Class5
    {
        const string ZakAssets = "assets/NatureBoy/zak";

        #region Wolf Soldier

        public static IEnumerable<IEnumerable<int>> WolfSoldierWalk
        {
            get { return 4.Range(j => 8.Range(i => (122 + j * 8) + (i + 6) % 8)); }
        }

        public static IEnumerable<int> WolfSoldierStand
        {
            get { return 8.Range(i => 114 + (i + 6) % 8); }
        }

        public static IEnumerable<Zak.ImageRef> LoadWolfSoldier()
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
                    new Zak.ImageRef { Name = "r1", Source = ZakAssets + "/sprites/r1.png" },
                    new Zak.ImageRef { Name = "r1_bed", Source = ZakAssets + "/sprites/r1_bed.png" }
                }
                .Concat(
                    LoadWolfSoldier()
                ).ToArray(),
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
                EntryPoint = new Zak.Point { X = "" + 100, Y = "" + 100 }
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

                            img.style.SetSize(iref.Size.X.ToInt32(), iref.Size.Y.ToInt32());

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





                    // create wolf soldier

                    var WolfSoldierInfo = new DudeAnimationInfo
                    {
                        Frames_Stand = WolfSoldierStand.Select
                        (
                            i => new FrameInfo
                            {
                                Source = Images["dude5/stand/" + i].Source,
                                Weight = 1d / 8,
                                OffsetY = 4
                            }
                        ).ToArray()
                    };

                    var dude = new Dude2
                    {
                        Frames = WolfSoldierInfo.Frames_Stand,

                    };

                    dude.HasShadow = false;
                    dude.TargetLocationDistanceMultiplier = 1;

                    dude.AnimationInfo.Frames_Stand = WolfSoldierInfo.Frames_Stand;
                    dude.AnimationInfo.Frames_Walk = new[] { WolfSoldierInfo.Frames_Stand };

                    dude.Zoom.DynamicZoomFunc = z => 1;
                    dude.Zoom.StaticZoom = 2;
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

                    InputLayer.onclick +=
                       ev =>
                       {
                           dude.WalkTo(ev.CursorPosition);
                           //Console.WriteLine("click: " + ev.CursorPosition);
                       };
                };
        }
    }


}
