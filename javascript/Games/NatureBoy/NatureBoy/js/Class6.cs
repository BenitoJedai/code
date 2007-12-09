using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using NatureBoy.js.Zak;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;

namespace NatureBoy.js
{
    partial class Class6
    {
        static void AttachImageToDocument(IHTMLImage img)
        {
            img.AttachToDocument();
        }

        public static readonly Zak.WorldInfo DefaultData =
             new Zak.WorldInfo
             {
                 Sprites = new Zak.SpriteInfo[] {
                    "room 001, obj 005 (296,32)",
                    "room 001",
                    "room 002, obj 002 (0,24)",
                    "room 002"
                },
                 BackgroundColor = Color.FromRGB(0, 0, 0),
                 TextColor = Color.FromRGB(0, 0xff, 0),
                 ControlSize = new Zak.Point(320, 240),
                 Zoom = 1d.ToString(),
                 ClientRect = new Zak.Rect(0, 12, 320, 128),
                 ClientRectColor = Color.FromRGB(0, 0, 0xff)
             };


        private void Initialize()
        {
            var Images = new Dictionary<string, IHTMLImage>();
            var ImagesLoaded = default(Action);
            Func<string, IHTMLImage> CloneImage = name => (IHTMLImage)Images[name].cloneNode(false);

            var Zoom = Data.Zoom.ToDouble();

            var ControlSize = new ZoomedPoint
            {
                Z = Zoom,
                X = Data.ControlSize.Xint,
                Y = Data.ControlSize.Yint
            };

            Control.style.SetSize(ControlSize.ZoomedXint, ControlSize.ZoomedYint);
            Control.style.backgroundColor = Data.BackgroundColor;
            Control.style.color = Data.TextColor;
            Control.AttachAsNextOrToDocument(AnchorElement);
            Control.style.position = IStyle.PositionEnum.relative;

            Action LoadImages =
                delegate
                {
                    Data.Sprites.
                        ForEach(i =>
                            new IHTMLImage(
                                Data.AssetsLocation.DefaultToEmptyString() + Assets.Images + "/" + i.Value + ".png"
                            ).InvokeOnComplete(
                                img =>
                                {
                                    Images[i.Value] = img;

                                    if (Images.Count == Data.Sprites.Length)
                                        ImagesLoaded();
                                }
                            )
                        );
                };

            ImagesLoaded =
                delegate
                {
                    var ClientRectPos = new ZoomedPoint
                    {
                        Z = Zoom,
                        X = Data.ClientRect.From.Xint,
                        Y = Data.ClientRect.From.Yint
                    };

                    var ClientRectSize = new ZoomedPoint
                    {
                        Z = Zoom,
                        X = Data.ClientRect.Size.Xint,
                        Y = Data.ClientRect.Size.Yint
                    };

                    var ContentLayer = new IHTMLDiv();

                    ContentLayer.style.backgroundColor = Data.ClientRectColor;
                    ContentLayer.style.overflow = IStyle.OverflowEnum.hidden;

                    ContentLayer.style.SetLocation(
                        ClientRectPos.ZoomedXint,
                        ClientRectPos.ZoomedYint,
                        ClientRectSize.ZoomedXint,
                        ClientRectSize.ZoomedYint
                        );
                    ContentLayer.AttachTo(Control);


                    var r1 = CloneImage("room 001").AttachTo(ContentLayer);

                    r1.style.SetLocation(0, 0, ClientRectSize.ZoomedXint, ClientRectSize.ZoomedYint);

                    var r2 = CloneImage("room 002").AttachTo(ContentLayer);

                    var r2_Zoom = new ZoomedPoint
                    {
                        Z = Zoom,
                        X = Images["room 002"].width,
                        Y = Images["room 002"].height
                    };

                    r2.Hide();
                    r2.style.SetLocation(0, 0, r2_Zoom.ZoomedXint, r2_Zoom.ZoomedYint);


                    Action<double> SetClipTo =
                        percentage =>
                        {
                            var x = (ClientRectSize.ZoomedX * percentage / 2).ToInt32();
                            var y = (ClientRectSize.ZoomedY * percentage / 2).ToInt32();

                            var clip = new CSSClip
                            {
                                Left = x,
                                Top = y,
                                Right = (ClientRectSize.ZoomedX - x).ToInt32(),
                                Bottom = (ClientRectSize.ZoomedY - y).ToInt32()
                            };

                            Console.WriteLine(percentage + " clip: " + clip);

                            ContentLayer.style.clip = clip;

                        };

                    //ContentLayer.style.clip = "rect(15px auto auto 15px)";

                    //var timer = new ScriptCoreLib.JavaScript.Runtime.Timer(
                    //    delegate
                    //    {
                    //        var p = (Math.Sin(DateTime.Now.Ticks) + 1) / 2;

                    //        Console.WriteLine("p: " + p);
                    //        SetClipTo(p);
                    //    }
                    //    , 0, 200);

                    //var pc = 50;
                    //var px = 200;

                    //Action<double> pChange = i => { px += (i * pc).ToInt32(); timer.StartInterval(px); };

                    Action<Action, Action> FadeOut =
                        (Starting, Stopping) =>
                            new LinearTimeTween
                            {
                                Length = 1000,
                                Starting = Starting,
                                Stopping = Stopping,
                                Changed = t => SetClipTo(t),
                                Percision = 50
                            }.Start();

                    Action<Action, Action> FadeIn =
                                  (Starting, Stopping) =>
                                      new LinearTimeTween
                                      {
                                          Length = 1000,
                                          Starting = Starting,
                                          Stopping = Stopping,
                                          Changed = t => SetClipTo(1d - t),
                                          Percision = 50
                                      }.Start();

                    var kbd = new KeyboardEvents { Enabled = true };

                    kbd.left += ev =>
                        FadeOut(
                            () => kbd.Enabled = false,
                            () =>
                            {
                                r1.Hide();
                                r2.Show();

                                FadeIn(null, () => kbd.Enabled = true);
                            }
                        );

                    kbd.right += ev =>
                         FadeOut(
                            () => kbd.Enabled = false,
                            () =>
                            {
                                r2.Hide();
                                r1.Show();

                                FadeIn(null, () => kbd.Enabled = true);
                            }
                        );

                    Native.Document.onkeydown += kbd;

                    /*
                    var div1 = SpawnDiv(Images, false);

                    div1.style.backgroundColor = Color.Yellow;
                    div1.AttachTo(ClientRect);
                    div1.style.SetLocation(4, 4, 100, 100);

                    var div2 = SpawnDiv(Images, true);

                    div2.style.backgroundColor = Color.Gray;
                    div2.AttachTo(ClientRect);
                    div2.style.SetLocation(100, 4, 100, 100);

                    */

                };

            LoadImages();
        }

        private static IHTMLDiv SpawnDiv(Dictionary<string, IHTMLImage> Images, bool b)
        {
            var div1 = new IHTMLDiv();
            var div2 = new IHTMLDiv();


            var img1 = (IHTMLImage)Images["room 001, obj 005 (296,32)"].cloneNode(false);
            var img2 = (IHTMLImage)Images["room 002, obj 002 (0,24)"].cloneNode(false);
            var img3 = (IHTMLImage)Images["room 002, obj 002 (0,24)"].cloneNode(false);

            div2.AttachTo(div1);
            div2.style.position = IStyle.PositionEnum.relative;
            div2.style.SetSize(100, 100);
            div2.style.border = "1px solid red";

            if (b)
                div2.onfocus += ev => div2.blur();

            img1.style.SetLocation(4, 4);
            img1.AttachTo(div2);

            img2.style.SetLocation(33, 4);
            img2.AttachTo(div2);
            img2.onfocus += ev => img2.blur();

            img3.style.SetLocation(66, 4);
            img3.AttachTo(div2);
            img3.onfocus += ev => ev.PreventDefault();

            return div1;
        }
    }

    [Script]
    class LinearTimeTween
    {
        public Action Starting;
        public Action Stopping;


        public long Length;
        public long Elapsed;

        public long TimeStarted;
        public long TimeNow;

        public double ElapsedPercentage
        {
            get
            {
                return Elapsed / Length;
            }
        }

        public Action<LinearTimeTween> Changed;

        public int Percision = 100;

        ScriptCoreLib.JavaScript.Runtime.Timer _Timer;

        private const long TicksPerMillisecond = 0x10000;

        public LinearTimeTween Start()
        {
            if (_Timer != null)
                _Timer.Stop();

            TimeStarted = DateTime.Now.Ticks / TicksPerMillisecond;

            if (Starting != null)
                Starting();

            _Timer = new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    TimeNow = DateTime.Now.Ticks / TicksPerMillisecond;

                    var x = TimeNow - TimeStarted;

                    Console.WriteLine("x: " + x);
                    Console.WriteLine("length: " + Length);

                    if (x > Length)
                    {
                        t.Stop();
                        _Timer = null;

                        Elapsed = Length;
                        Changed(this);
                        Stopping();

                    }
                    else
                    {
                        Elapsed = x;
                        Changed(this);
                    }

                }, 0, Percision
            );

            return this;
        }

        public static implicit operator double(LinearTimeTween e)
        {
            return e.ElapsedPercentage;
        }
    }

    [Script]
    class CSSClip
    {


        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public override string ToString()
        {
            // rect (<top> <right> <bottom> <left>) 
            return "rect(" + Top + "px " + Right + "px " + Bottom + "px " + Left + "px)";
        }

        public static implicit operator string(CSSClip e)
        {
            return e.ToString();
        }
    }
}
